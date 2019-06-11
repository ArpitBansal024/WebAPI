using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPI.Controllers
{
    // [EnableCors(origins: "http://127.0.0.1:*", headers: "*", methods: "*")]
    public class BaseController : ApiController
    {
        public BaseController()
        {

        }
    }
}
