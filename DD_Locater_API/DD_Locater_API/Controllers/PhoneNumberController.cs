using DD_Locater_API.Models;
using DD_Locater_API.Services;
using DD_Locater_API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace DD_Locater_API.Controllers
{
    public class PhoneNumberController : CustomController
    {
        PhoneNumberRepository phoneNumberRepository;

        public PhoneNumberController()
        {
            this.phoneNumberRepository = new PhoneNumberRepository();
        }

        [Route("api/phoneNumberList")]
        [HttpGet]
        public List<PhoneNumberDown> GetPhoneNumbers()
        {
            return phoneNumberRepository.getNumbers(getHdStr("keyword"));
        }

        [Route("api/phoneNumberInsert")]
        [HttpPost]
        public Int64 InsertPhonenum([FromBody] PhoneNumber pn)
        {
            return phoneNumberRepository.InsertPhonenum(pn);
        }

        [Route("api/phoneNumberDelete")]
        [HttpDelete]
        public Int64 DeletePhonenum()
        {
            return phoneNumberRepository.DeletePhonenum(getHdInt("pn_idx"));
        }

        [Route("api/phoneNumberFromFile")]
        [HttpGet]
        public void UploadPhoneNumberFromFile()
        {
            string srcDir = "E:\\GoogleCloud\\Programming\\ASP\\DD_Locater_API\\agent_numbers.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(srcDir, Encoding.GetEncoding("utf-8"), true);
            string srcStr = file.ReadToEnd();
            string[] lines = srcStr.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                string[] words = lines[i].Split('\t');
                if (words.Length > 1 && words[0].Trim().Length > 0)
                {
                    for (int j = 1; j < words.Length; j++)
                    {
                        if (words[j].Trim().Length > 0)
                        {
                            phoneNumberRepository.UploadPhoneNumberFromFile(words[0], words[j]);
                        }
                    }

                }
            }
        }

    }
}
