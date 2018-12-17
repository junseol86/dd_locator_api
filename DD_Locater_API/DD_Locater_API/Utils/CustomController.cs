using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DD_Locater_API.Utils
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CustomController : ApiController
    {
        protected string getHdStr(string value)
        {
            return Request.Headers.GetValues(value).FirstOrDefault();
        }
        protected string getHdStr(string value, string alt)
        {
            try
            {
                return Request.Headers.GetValues(value).FirstOrDefault();
            } catch
            {
                return alt;
            }
        }

        protected double getHdDbl(string value)
        {
            return Convert.ToDouble(Request.Headers.GetValues(value).FirstOrDefault());
        }
        protected double getHdDbl(string value, double alt)
        {
            try
            {
                return Convert.ToDouble(Request.Headers.GetValues(value).FirstOrDefault());
            } catch
            {
                return alt;
            }
        }

        protected Int64 getHdInt(string value)
        {
            return Convert.ToInt64(Request.Headers.GetValues(value).FirstOrDefault());
        }
        protected Int64 getHdInt(string value, Int64 alt)
        {
            try
            {
                return Convert.ToInt64(Request.Headers.GetValues(value).FirstOrDefault());
            } catch
            {
                return alt;
            }
        }

        protected bool getHdBool(string value, bool alt)
        {
            try
            {
                return Convert.ToBoolean(Request.Headers.GetValues(value).FirstOrDefault());
            } catch
            {
                return alt;
            }
        }
    }
}
