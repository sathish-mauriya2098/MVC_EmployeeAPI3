using EmployeeDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace EmployeeAPINew3.Controllers
{
    public class EmployeeController : ApiController
    {
        private aspireEntities1 entities = new aspireEntities1();
        DbQuries crud = new DbQuries();

        public IList<Employee> GetEmployees()
        {
            return crud.View();
        }

        // GET api/Employee/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetEmployee(int id)
        {
            Employee employee = crud.View(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // POST api/Employee
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PostEmployee(Employee employee)
        {
            int value = crud.Insert(employee);
            if (value > 0)
            {
                return CreatedAtRoute("DefaultApi", new { id = employee.EmpId }, employee);
            }
            else
            {
                return NotFound();
            }
        }
        // PUT api/Employee/5
        public IHttpActionResult PutEmployee(int id, Employee employee)
        {
            int value = crud.Update(employee);
            if (id != employee.EmpId)
            {
                return BadRequest();
            }
            if (value > 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return NotFound();
            }

        }

        // DELETE api/Employee/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            int value = crud.Delete(id);
            if (value > 0)
            {
                return Ok(new Employee());
            }
            else
            {
                return NotFound();
            }

        }
    }
}
