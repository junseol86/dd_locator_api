using DD_Locater_API.Services;
using DD_Locater_API.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace DD_Locater_API.Controllers
{
    public class OneroomInfoController : CustomController
    {
        JArray data;
        int dataCount;
        int validCount = 0;
        WebRequest request;

        OneroomInfoRepository oneroomInfoRepository;

        public OneroomInfoController()
        {
            oneroomInfoRepository = new OneroomInfoRepository();
        }

        [Route("api/oneroomInfo")]
        [HttpGet]
        public void LoadAndInsertOneroomInfo()
        {

            JObject point = new JObject();

            string srcDir = "E:\\GoogleCloud\\Programming\\ASP\\DD_Locater_API\\oneroom_info2.json";
            System.IO.StreamReader file = new System.IO.StreamReader(srcDir, Encoding.GetEncoding("utf-8"), true);
            string srcStr = file.ReadToEnd();
            JArray data = JArray.Parse(srcStr);

            foreach (JObject datum in data)
            {
                //System.Diagnostics.Debug.WriteLine(datum);
                oneroomInfoRepository.LoadAndInsertOneroomInfo((JObject)datum);
            }

        }

        //[Route("api/removeGwan")]
        //[HttpGet]
        //public void removeGwan()
        //{
        //    string srcDir = "E:\\GoogleCloud\\Programming\\ASP\\DD_Locater_API\\remove_gwan.txt";
        //    System.IO.StreamReader file = new System.IO.StreamReader(srcDir, Encoding.GetEncoding("utf-8"), true);
        //    string srcStr = file.ReadToEnd();
        //    string[] addrs = srcStr.Split('\n');
        //    foreach (string addr in addrs)
        //    {
        //        string[] pcs = addr.Split(',');
        //        oneroomInfoRepository.RemoveGwan(pcs[0], pcs[1], Regex.Replace(pcs[2], @"\t|\n|\r", ""));
        //    }
        //}
    }
}
