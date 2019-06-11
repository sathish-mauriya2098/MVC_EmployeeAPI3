using EmployeeDataAccess;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace EmployeeMVC.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        //View All Employee Details
        public ActionResult Index()
        {
            IEnumerable<Employee> list;
            HttpResponseMessage response = GlobalVariables.client.GetAsync("Employee").Result;
            list = response.Content.ReadAsAsync<IEnumerable<Employee>>().Result;
            return View(list);
        }
        public ActionResult AddDetails()
        {
            return View(new Employee());
        }

        //Add Employee Details
        [HttpPost]
        public ActionResult AddDetails(Employee emp)
        {
            HttpResponseMessage response = GlobalVariables.client.PostAsJsonAsync("Employee", emp).Result;
            TempData["SuccessMessage"] = "Created Successfully";
            return RedirectToAction("Index");
        }

        //Update Employee Details
        public ActionResult Update(int id)
        {
            HttpResponseMessage response = GlobalVariables.client.GetAsync("Employee/" + id.ToString()).Result;
            return View(response.Content.ReadAsAsync<Employee>().Result);
        }

        [HttpPost]
        public ActionResult Update(Employee emp)
        {

            HttpResponseMessage response = GlobalVariables.client.PutAsJsonAsync("Employee/" + emp.EmpId, emp).Result;
            if(response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Updated Successfully";
                String message = HttpUtility.UrlEncode("Name:"+emp.EmpName+"\nEMAIL:"+emp.EmailId + "\nDOB:" + emp.EmpDob + "\nMOBILE:" + emp.Mobile + "\nDOJ:" + emp.EmpDoj + "\nDESIGNATION:" + emp.Designation + "\nSALARY:" + emp.EmpSalary);

                using (var wb = new WebClient())
                {
                    byte[] data = wb.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
                {
                {"apikey" , "apiKey"},
                {"numbers" , "91"+emp.Mobile},
                {"message" ,message},
                {"sender" , "TXTLCL"}
                });
                    string result = System.Text.Encoding.UTF8.GetString(data);    
                }
            }
            //TempData["SuccessMessage"] = "Updated Successfully";
            return RedirectToAction("Index");
        }

        //For Delete Employee
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.client.DeleteAsync("Employee/" + id).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
