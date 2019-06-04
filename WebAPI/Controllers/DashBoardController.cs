using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Entity;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class DashBoardController : BaseController
    {
        [HttpGet]
        [ResponseType(typeof(List<string>))]
        public HttpResponseMessage GetAllCounts()
        {
            using (DBEntities dBEntities = new DBEntities())
            {
              var data =  dBEntities.DashBoardCounts();
                return new HttpResponseMessage()
                {
                    Content = new StringContent(JArray.FromObject(data).ToString(), Encoding.UTF8, "application/json")
                };
            }
        }
    }
}
