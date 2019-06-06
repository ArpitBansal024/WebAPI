using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Entity;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        [HttpGet]
        [ResponseType(typeof(List<Employees>))]
        public HttpResponseMessage GetEmployeesList()
        {
            using (DBEntities dBEntities = new DBEntities())
            {
                var data = dBEntities.employees.Select(s =>
                 new Employees { EmployeeID = s.employee_id, Name = s.Name, Age = s.Age, Gender = s.Gender, Email = s.Email, Salary = s.Salary }).ToList();//.FirstOrDefault();
                return new HttpResponseMessage()
                {
                    Content = new StringContent(JArray.FromObject(data).ToString(), Encoding.UTF8, "application/json")
                };
            }
        }

        [HttpPost]
        [ResponseType(typeof(int))]
        public IHttpActionResult SaveEmployee(dynamic employee)
        {
            try
            {

                using (DBEntities dBEntities = new DBEntities())
                {

                    dBEntities.employees.Add(
                        new employee { First_name = employee.Name, last_name = employee.Name, Name = employee.Name, Age = employee.Age, Email = employee.Email, Salary = employee.Salary, Gender = employee.Gender });
                    return Ok(dBEntities.SaveChanges());
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        [HttpPut]
        public void ModifyEmployee(dynamic employee, int id)
        {
            using (DBEntities dBEntities = new DBEntities())
            {
                var data = dBEntities.employees.Where(s => s.employee_id == id).First();
                data.First_name = employee.Name;
                data.last_name = employee.Name;
                data.Name = employee.Name;
                data.Age = employee.Age;
                data.Email = employee.Email;
                data.Salary = employee.Salary;
                data.Gender = employee.Gender;
                dBEntities.SaveChanges();
            }
        }

        [HttpDelete]
        [ResponseType(typeof(int))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            using (DBEntities dBEntities = new DBEntities())
            {
                dynamic data = dBEntities.employees.Find(id);
                dBEntities.employees.Remove(data);
                return Ok(dBEntities.SaveChanges());
            }
        }
    }
}
