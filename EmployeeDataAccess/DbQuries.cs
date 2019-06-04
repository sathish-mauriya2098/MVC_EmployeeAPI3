using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDataAccess
{
    public class DbQuries
    {
        private aspireEntities1 db = new aspireEntities1();
        //To view data
        public IList<Employee> View()
        {
            return db.Employees.ToList();
        }

        //To view particular employee details
        public dynamic View(int id)
        {
            Employee employee = db.Employees.Find(id);
            return employee;
        }

        //To insert employee details
        public int Insert(Employee employee)
        {
            db.Employees.Add(employee);
            int returnValue = db.SaveChanges();
            return returnValue;
        }

        //To Update employee details
        public int Update(Employee employee)
        {
            db.Entry(employee).State = EntityState.Modified;
            int returnValue = db.SaveChanges();
            return returnValue;
        }

        //To delete employee
        public int Delete(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            int returnValue = db.SaveChanges();
            return returnValue;
        }
    }
}
