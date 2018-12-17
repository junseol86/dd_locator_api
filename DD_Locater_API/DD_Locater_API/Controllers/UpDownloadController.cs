using DD_Locater_API.Models;
using DD_Locater_API.Services;
using DD_Locater_API.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace DD_Locater_API.Controllers
{
    public class UpDownloadController : CustomController
    {
        UpDownloadRepository repo;

        public UpDownloadController()
        {
            repo = new UpDownloadRepository();
        }

        [Route("api/asset/uploadPhoto")]
        [HttpPost]
        public string UploadPhoto([FromBody] ImageUpload imageUpload)
        {
            string result = "";

            try
            {
                string fileName = (DateTime.Now.ToString() + ".jpg").Replace(" ", "_").Replace(":", "-").Replace("오전", "am").Replace("오후", "pm");

                byte[] bytes = Convert.FromBase64String(imageUpload.image);
                File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/App_Data/uploaded/" + fileName), bytes);

                result = repo.InsertPhotoData(imageUpload.bld_idx, fileName);
            } catch
            {

            }

            return result;
        }

        [Route("api/asset/deletePhoto")]
        [HttpDelete]
        public string DeletePhoto()
        {
            return repo.deletePhotoData(getHdStr("bld_idx"));
        }

        [Route("api/asset/downloadPhoto/{fileName}")]
        [HttpGet]
        public HttpResponseMessage DownloadPhoto(string fileName)
        {
            HttpResponseMessage result = null;
            string path = HttpContext.Current.Server.MapPath("~/App_Data/uploaded/" + fileName);
            if (!File.Exists(path))
            {
                result = Request.CreateResponse(HttpStatusCode.Gone);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new StreamContent(new FileStream(path, FileMode.Open, FileAccess.Read));
                result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = fileName;
            }
            return result;
        }
    }
}
