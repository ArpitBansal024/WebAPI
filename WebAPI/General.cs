using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebAPI
{
    public class General
    {
        public static bool UseHttp
        {
            get
            {
                if (ConfigurationManager.AppSettings["UseHttp"] != null)
                {
                    return Convert.ToBoolean(ConfigurationManager.AppSettings["UseHttp"]);
                }
                else return false;
            }
        }

        public static double AccessTokenExpiryDays { get; internal set; }
    }
}