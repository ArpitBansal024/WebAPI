using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Entity;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class LoginController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage CheckUser(string user, string pwd)
        {
            using (DBEntities dBEntities = new DBEntities())
            {
                var data = dBEntities.Logins.Where(s => s.Name == user && s.Password == pwd).
                    Select(w => new VMLogin
                    {
                        Name = w.Name,
                        Password = w.Password,
                        IsActive = w.IsActive,
                        Message = w.IsActive == true ? "Success" : "User Account is De-Active"
                    }).SingleOrDefault();//.FirstOrDefault();
                return new HttpResponseMessage()
                {
                    Content = new StringContent(JObject.FromObject(data == null ? new object() : data).ToString(), Encoding.UTF8, "application/json")
                };
                // return Ok(data);
            }
        }


        // Return Single Record Without Using HttpResponseMessage As Return Type
        [HttpGet]
        [ResponseType(typeof(VMLogin))]
        public VMLogin CheckUserNew(string user, string pwd)
        {
            using (DBEntities dBEntities = new DBEntities())
            {
                var data = dBEntities.Logins.Where(s => s.Name == user && s.Password == pwd).
                    Select(w => new VMLogin
                    {
                        Name = w.Name,
                        Password = w.Password,
                        IsActive = w.IsActive,
                        Message = w.IsActive == true ? "Success" : "User Account is De-Active"
                    }).SingleOrDefault();//.FirstOrDefault();
                 return data;
                // return Ok(data);
            }
        }

        [HttpGet]
        public void TestAPI()
        {

        }
    }
}
