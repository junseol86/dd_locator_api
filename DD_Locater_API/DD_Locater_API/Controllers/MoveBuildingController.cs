using DD_Locater_API.Services;
using DD_Locater_API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DD_Locater_API.Controllers
{
    public class MoveBuildingController : CustomController
    {

        [Route("api/moveBuilding")]
        [HttpGet]
        public void MoveBuilding()
        {
            new MoveBuildingRepository().MoveBuilding();
        }
    }
}
