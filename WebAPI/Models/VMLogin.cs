using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class VMLogin
    {
            public dynamic id { get; set; }
            public string Name { get; set; }
            public string Password { get; set; }
            public Nullable<bool> IsActive { get; set; }
            public string Message { get; set; }
    }
}