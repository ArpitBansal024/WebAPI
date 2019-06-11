using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebAPI.Entity;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [AllowAnonymous]
    public class UserController : BaseController
    {
        [HttpGet]
        [ResponseType(typeof(List<VMLogin>))]
        public HttpResponseMessage GetUsersList()
        {
            using (DBEntities dBEntities = new DBEntities())
            {
                var data = dBEntities.Logins.Select(s =>
                 new VMLogin { UserID = s.id, Name = s.Name, Password = s.Password, IsActive = s.IsActive }).ToList();//.FirstOrDefault();
                return new HttpResponseMessage()
                {
                    Content = new StringContent(JArray.FromObject(data).ToString(), Encoding.UTF8, "application/json")
                };
            }
        }

        [HttpPost]
        [ResponseType(typeof(int))]
        public IHttpActionResult SaveUser(dynamic user)
        {
            try
            {
                using (DBEntities dBEntities = new DBEntities())
                {

                    dBEntities.Logins.Add(
                        new Login
                        {
                            id = Guid.NewGuid(),
                            Name = user.Name,
                            Password = user.Password,
                            IsActive = user.IsActive
                        });
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
        [ResponseType(typeof(int))]
        public IHttpActionResult ModifyUser(dynamic user, Guid id)
        {
            using (DBEntities dBEntities = new DBEntities())
            {
                var data = dBEntities.Logins.Where(s => s.id == id).First();
                data.Name = user.Name;
                data.Password = user.Password;
                data.IsActive = user.IsActive;
               return Ok(dBEntities.SaveChanges());
            }
        }

        [HttpDelete]
        [ResponseType(typeof(int))]
        public IHttpActionResult DeleteUser(Guid id)
        {
            using (DBEntities dBEntities = new DBEntities())
            {
                dynamic data = dBEntities.Logins.Find(id);
                dBEntities.Logins.Remove(data);
                return Ok(dBEntities.SaveChanges());
            }
        }
    }
}
