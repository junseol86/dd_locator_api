using DD_Locater_API.Models;
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
    public class AssetDongV2Controller : CustomController
    {
        AssetDongRepositoryV2 assetDongRepository;
        public AssetDongV2Controller()
        {
            assetDongRepository = new AssetDongRepositoryV2();
        }

        [Route("api/assetDongsV4")]
        [HttpGet]
        public DongAndCount GetAssetList4()
        {
            return assetDongRepository.DongsInBound4(getHdStr("bldType").Replace("all", ""), 
                getHdDbl("left"), getHdDbl("right"), getHdDbl("top"), getHdDbl("bottom"),
                getHdInt("hasName"), getHdInt("hasNumber"), getHdInt("hasGwan"), 
                getHdInt("fmlyMin"), getHdInt("fmlyMax"), 
                getHdStr("mainPurps"), getHdStr("useaprDay"));
        }
    }
}
