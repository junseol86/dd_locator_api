using DD_Locater_API.Models;
using DD_Locater_API.Services;
using DD_Locater_API.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace DD_Locater_API.Controllers
{
    public class UploadBuilding2Controller : CustomController
    {
        JArray data;
        int dataCount;
        int validCount = 0;
        WebRequest request;
        BuildingRepository2 buildingRepository;

        public UploadBuilding2Controller()
        {
            buildingRepository = new BuildingRepository2();
        }

        [Route("api/uploadBuildings2")]
        [HttpGet]
        public void UploadBuildings()
        {
            string[] regions = {"anyang-dongan", "anyang-manan", "gunpo", "hwaseong", "osan", "pyeongtaek",
                "suwon-gwonseon", "suwon-jangan", "suwon-paldal", "suwon-yeongtong", "yongin-cheoin", "yongin-kiheung", "yongin-suji"};
            foreach (string region in regions)
            {

                string srcDir = $"E:\\GoogleCloud\\Programming\\ASP\\DD_Locater_API\\regionData\\{region}.json";
                System.IO.StreamReader file = new System.IO.StreamReader(srcDir, Encoding.GetEncoding("ks_c_5601-1987"), true);
                string srcStr = file.ReadToEnd();
                JObject obj = JObject.Parse(srcStr);
                data = (JArray)obj["Data"];
                dataCount = data.Count;

                foreach (JObject datum in data)
                {

                    try
                    {
                        buildingRepository.InsertOnlyBuilding(datum);
                    }
                    catch
                    {
                        System.Diagnostics.Debug.WriteLine(datum);
                    }
                }

            }

        }

        [Route("api/fillMissingCoords")]
        [HttpGet]
        public void fillMissingCoords()
        {
            bool moreToFill = true;
            while (moreToFill)
            {
                List<string[]> founds = buildingRepository.selectBldsMissingCoords();
                
                foreach(string[] found in founds)
                {
                    //System.Diagnostics.Debug.WriteLine(found[0] + " " + found[1]);

                    string address = "https://openapi.naver.com/v1/map/geocode?query=" + found[1];
                    request = WebRequest.Create(address);
                    request.Method = "GET";
                    //request.Headers["X-Naver-Client-Id"] = "VWXs42NMTWXj7ziOn_4t";
                    //request.Headers["X-Naver-Client-Secret"] = "bNv7p9ebJn";
                    request.Headers["X-Naver-Client-Id"] = "zgIERIui2VzCjd43kVjX";
                    request.Headers["X-Naver-Client-Secret"] = "XuhSDkfyRU";

                    try
                    {
                        string responseStr = new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
                        JObject rspObj = JObject.Parse(responseStr);
                        JObject addr = (JObject)rspObj["result"]["items"][0]["addrdetail"];
                        JObject point = (JObject)rspObj["result"]["items"][0]["point"];

                        buildingRepository.fillCoords(found[0], (string)point["x"], (string)point["y"]);
                        //System.Diagnostics.Debug.WriteLine(found[1] + " " + (string)point["x"] + " " + (string)point["y"]);
                    }
                    catch
                    {
                        //System.Diagnostics.Debug.WriteLine($"ERROR {found[0]} {found[1]}");
                        //System.Diagnostics.Debug.WriteLine(datum);
                    }

                }

                moreToFill = founds.Count > 0;
            }

        }

        [Route("api/inputFactories")]
        [HttpGet]
        public void inputFactories()
        {
            string srcDir = $"E:\\GoogleCloud\\Programming\\ASP\\DD_Locater_API\\regionData\\kkd_factory.tsv";
            System.IO.StreamReader file = new System.IO.StreamReader(srcDir, Encoding.GetEncoding("utf-8"), true);
            string[] title = new string[27];
            int[] colLength = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            string srcStr = file.ReadToEnd();
            string[] lines = srcStr.Split('\n');
            string colCounts = "";
            for (int i = 0; i < lines.Count(); i++)
            {
                string[] cols = lines[i].Split('\t');

                if (i == 0)
                {
                    for (int j = 0; j < cols.Count(); j++)
                    {
                        title[j] = cols[j];
                    }
                } else
                {
                    try
                    {
                        buildingRepository.InsertFactory(cols);
                    } catch
                    {
                        System.Diagnostics.Debug.WriteLine($"ERROR: ${lines[i]}");
                    }
                }
            }
        }

        [Route("api/matchFactories")]
        [HttpGet]
        public void matchFactories()
        {
            bool moreToLoad = true;
            Int64 offset = 0;
            while (moreToLoad)
            {
                List<Asset_S2> aList = buildingRepository.SelectFactories(offset++);

                if (aList.Count == 0)
                {
                    moreToLoad = false;
                } else
                {
                    foreach (Asset_S2 asset in aList)
                    {
                        List<Factory> fList = buildingRepository.SelectMatchedFactories(asset);
                        buildingRepository.ApplyFactoryMatch(asset.bld_idx, fList);
                    }
                }
            }
        }

        [Route("api/notFactories")]
        [HttpGet]
        public void notFactories()
        {
            for (int i = 1; i < 3; i++)
            {
                string srcDir = $"E:\\GoogleCloud\\Programming\\ASP\\DD_Locater_API\\regionData\\{i}jong_gr";
                System.IO.StreamReader file = new System.IO.StreamReader(srcDir, Encoding.GetEncoding("utf-8"), true);
                string srcStr = file.ReadToEnd();
                string[] lines = srcStr.Split('\n');
                foreach(string line in lines)
                {
                    buildingRepository.NotFactories(i, line.Substring(0, line.Length - 2));
                }
            }
        }

        [Route("api/notNotFactories")]
        [HttpGet]
        public void notNotFactories()
        {
            for (int i = 1; i < 3; i++)
            {
                string srcDir = $"E:\\GoogleCloud\\Programming\\ASP\\DD_Locater_API\\regionData\\{i}jong_gr_pos";
                System.IO.StreamReader file = new System.IO.StreamReader(srcDir, Encoding.GetEncoding("utf-8"), true);
                string srcStr = file.ReadToEnd();
                string[] lines = srcStr.Split('\n');
                foreach(string line in lines)
                {
                    buildingRepository.NotNotFactories(i, line.Substring(0, line.Length - 2));
                }
            }
        }

        [Route("api/setDongRiName")]
        [HttpGet]
        public void setDongRiName()
        {
            bool moreToLoad = true;
            while (moreToLoad)
            {
                string[] sector = buildingRepository.selectSggBjdWithoutDongriName();

                if (sector[2].Length == 0)
                {
                    moreToLoad = false;
                } else
                {
                    buildingRepository.saveDongName(sector[0], sector[1], sector[2]);
                }
            }
        }

    }
}
