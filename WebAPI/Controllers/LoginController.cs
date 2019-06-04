using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Entity;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class LoginController : ApiController
    {
        [HttpGet]
        [ResponseType(typeof(VMLogin))]
        public IHttpActionResult CheckUser(string user, string pwd)
        {
            using (DBEntities dBEntities = new DBEntities())
            {
                var data = dBEntities.Logins.Where(s => s.Name == user && s.Password == pwd).
                    Select(w => new VMLogin
                    {
                        id = w.id,
                        Name = w.Name,
                        Password = w.Password,
                        IsActive = w.IsActive,
                        Message = w.IsActive == true ? "Success" : "User Account is DeActive"
                    }).Take(1);//.FirstOrDefault();

                return Ok(data);
            }
        }
    }
}
