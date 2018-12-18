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
    public class AssetDong_S2_Controller : CustomController
    {
        AssetDongRepository_S2 assetDongRepository;
        public AssetDong_S2_Controller()
        {
            assetDongRepository = new AssetDongRepository_S2();
        }

        [Route("api/assetDongs_S2")]
        [HttpGet]
        public DongAndCount GetAssetList()
        {
            return assetDongRepository.DongsInBound(getHdStr("bldType").Replace("all", ""), 
                getHdDbl("left"), getHdDbl("right"), getHdDbl("top"), getHdDbl("bottom"),
                getHdInt("hasName"), getHdInt("hasNumber"), getHdInt("hasGwan"), 
                getHdInt("fmlyMin"), getHdInt("fmlyMax"), 
                getHdStr("mainPurps"), getHdStr("useaprDay"),
                getHdInt("visited"), getHdInt("factory_count"), getHdStr("doCode", ""));
        }

        [Route("api/assetDongs_mobile_S2")]
        [HttpGet]
        public List<AssetDongV2> GetAssetListMobile()
        {
            return assetDongRepository.DongsInBoundMobile(
                getHdStr("bldCtgr"),
                getHdStr("bldType").Replace("all", ""),
                getHdDbl("left"), getHdDbl("right"), getHdDbl("top"), getHdDbl("bottom"),
                getHdInt("hasName"), getHdInt("hasNumber"), getHdInt("hasGwan"),
                getHdInt("fmlyMin"), getHdInt("fmlyMax"),
                getHdStr("mainPurps"), getHdStr("useaprDay"),
                getHdInt("visited"), getHdInt("factory_count"));
        }
    }
}
