using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Entity;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class RoleController : BaseController
    {
        [HttpGet]
        [ResponseType(typeof(List<Roles>))]
        public IHttpActionResult GetRolesList()
        {
            using (DBEntities dBEntities = new DBEntities())
            {
                var data = dBEntities.Roles.Select(s =>
                 new Roles { Role_ID = s.Role_ID,Name= s.Name, Description = s.Description }).ToList();
                return Ok(data);
            }
        }

        [HttpPost]
        [ResponseType(typeof(int))]
        public IHttpActionResult SaveRole(dynamic role)
        {
            try
            {

                using (DBEntities dBEntities = new DBEntities())
                {

                    dBEntities.Roles.Add(
                        new Role { Name=role.Name, Description = role.Description });
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
        public void ModifyRole(dynamic role, int id)
        {
            using (DBEntities dBEntities = new DBEntities())
            {
                var data = dBEntities.Roles.Where(s => s.Role_ID == id).First();
                data.Name = role.Name;
                data.Description = role.Description;
                dBEntities.SaveChanges();
            }
        }

        [HttpDelete]
        [ResponseType(typeof(int))]
        public IHttpActionResult DeleteRole(int id)
        {
            using (DBEntities dBEntities = new DBEntities())
            {
                dynamic data = dBEntities.Roles.Find(id);
                dBEntities.Roles.Remove(data);
                return Ok(dBEntities.SaveChanges());
            }
        }
    }
}
