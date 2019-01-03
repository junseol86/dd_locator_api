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
    public class AssetList_S2_Controller : CustomController
    {

        private AssetRepository_S2 assetRepository;

        public AssetList_S2_Controller()
        {
            this.assetRepository = new AssetRepository_S2();
        }

        [Route("api/assetList_S2")]
        [HttpGet]
        public AssetAndCount_S2 GetAssetList()
        {
            return assetRepository.AssetsInBound(getHdStr("bldType").Replace("all", ""), 
                getHdDbl("left"), getHdDbl("right"), getHdDbl("top"), getHdDbl("bottom"),
                getHdInt("hasName"), getHdInt("hasNumber"), getHdInt("hasGwan"), 
                getHdInt("fmlyMin"), getHdInt("fmlyMax"), 
                getHdStr("mainPurps"), getHdStr("useaprDay"),
                getHdInt("visited"), getHdInt("factory_count"), getHdStr("doCode", ""));
        }
        [Route("api/assetList_mobile_S2")]
        [HttpGet]
        public List<AssetMark_S2> GetAssetListMobile()
        {
            return assetRepository.AssetsInBoundMobile(
                getHdStr("bldCtgr"),
                getHdStr("bldType").Replace("all", ""),
                getHdDbl("left"), getHdDbl("right"), getHdDbl("top"), getHdDbl("bottom"),
                getHdInt("hasName"), getHdInt("hasNumber"), getHdInt("hasGwan"),
                getHdInt("fmlyMin"), getHdInt("fmlyMax"),
                getHdStr("mainPurps"), getHdStr("useaprDay"),
                getHdInt("visited"), getHdInt("factory_count"), getHdInt("floor_min"),
                getHdInt("hasGongsil"), getHdInt("hasHosuPic"), 
                getHdInt("approvedPic"), getHdInt("hasAgency"), getHdInt("smsNotSent")
            );
        }

        [Route("api/assetRequested_S2")]
        [HttpGet]
        public List<Asset_S2_Down> GetAssetRequested()
        {
            return assetRepository.AssetsRequested(getHdStr("bldType").Replace("all", ""), 
                getHdInt("hasName"), getHdInt("hasNumber"), getHdInt("hasGwan"), 
                getHdInt("fmlyMin"), getHdInt("fmlyMax"), 
                getHdStr("mainPurps"), getHdStr("useaprDay"),
                System.Web.HttpUtility.UrlDecode(getHdStr("bld_name")), System.Web.HttpUtility.UrlDecode(getHdStr("bld_memo")),
                getHdInt("visited"), getHdInt("factory_count"), getHdStr("doCode", "")
                );
        }

        [Route("api/assetSearched_S2")]
        [HttpGet]
        public List<Asset_S2_Down> GetAssetSearched()
        {
            return assetRepository.AssetsSearched(getHdStr("bldType").Replace("all", ""), 
                getHdInt("hasName"), getHdInt("hasNumber"), getHdInt("hasGwan"), 
                getHdInt("fmlyMin"), getHdInt("fmlyMax"), 
                getHdStr("mainPurps"), getHdStr("useaprDay"),
                System.Web.HttpUtility.UrlDecode(getHdStr("bld_name")), System.Web.HttpUtility.UrlDecode(getHdStr("bld_memo")),
                getHdInt("visited"), getHdInt("factory_count"), getHdStr("doCode", "")
                );
        }
    }

}
