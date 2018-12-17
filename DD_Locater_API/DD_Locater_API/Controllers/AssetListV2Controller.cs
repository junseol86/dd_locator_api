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
    public class AssetListV2Controller : CustomController
    {

        private AssetRepositoryV2 assetRepository;

        public AssetListV2Controller()
        {
            this.assetRepository = new AssetRepositoryV2();
        }

        [Route("api/assetListV4")]
        [HttpGet]
        public AssetAndCount GetAssetListV4()
        {
            return assetRepository.AssetsInBound4(getHdStr("bldType").Replace("all", ""), 
                getHdDbl("left"), getHdDbl("right"), getHdDbl("top"), getHdDbl("bottom"),
                getHdInt("hasName"), getHdInt("hasNumber"), getHdInt("hasGwan"), 
                getHdInt("fmlyMin"), getHdInt("fmlyMax"), 
                getHdStr("mainPurps"), getHdStr("useaprDay"));
        }

        [Route("api/assetRequestedV3")]
        [HttpGet]
        public List<AssetV2Down> GetAssetRequested2()
        {
            return assetRepository.AssetsRequested2(getHdStr("bldType").Replace("all", ""), 
                getHdInt("hasName"), getHdInt("hasNumber"), getHdInt("hasGwan"), 
                getHdInt("fmlyMin"), getHdInt("fmlyMax"), 
                getHdStr("mainPurps"), getHdStr("useaprDay"),
                System.Web.HttpUtility.UrlDecode(getHdStr("bld_name")), System.Web.HttpUtility.UrlDecode(getHdStr("bld_memo")));
        }

        [Route("api/assetSearchedV2")]
        [HttpGet]
        public List<AssetV2Down> GetAssetSearched2()
        {
            return assetRepository.AssetsSearched2(getHdStr("bldType").Replace("all", ""), 
                getHdInt("hasName"), getHdInt("hasNumber"), getHdInt("hasGwan"), 
                getHdInt("fmlyMin"), getHdInt("fmlyMax"), 
                getHdStr("mainPurps"), getHdStr("useaprDay"),
                System.Web.HttpUtility.UrlDecode(getHdStr("bld_name")), System.Web.HttpUtility.UrlDecode(getHdStr("bld_memo")));
        }

                // System.Web.HttpUtility.UrlDecode(getHdStr("searchName")), 
    }

}
