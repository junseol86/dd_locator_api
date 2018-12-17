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
    public class AssetController : CustomController
    {
        private AssetRepositoryV2 assetRepositoryV2;

        public AssetController()
        {
            this.assetRepositoryV2 = new AssetRepositoryV2();
        }

        [Route("api/assetV2/{baseIdx}")]
        [HttpGet]
        public AssetV2Down GetAnAsset(Int64 baseIdx)
        {
            return assetRepositoryV2.GetAnAsset(baseIdx);
        }

        [Route("api/asset/insertV2")]
        [HttpPost]
        public Int64 InsertSimple([FromBody] AssetV2 asset)
        {
            return assetRepositoryV2.InsertAssetV2(asset);
        }

        [Route("api/asset/modifyV2")]
        [HttpPut]
        public Int64 ModifyAsset2([FromBody] AssetV2 asset)
        {
            return assetRepositoryV2.ModifyAsset(asset, getHdStr("bld_idx"));
        }

        [Route("api/asset/modifyV3")]
        [HttpPut]
        public Int64 ModifyAsset3([FromBody] AssetV2 asset)
        {
            return assetRepositoryV2.ModifyAsset2(asset, getHdStr("bld_idx"));
        }

        [Route("api/asset/deleteV2")]
        [HttpDelete]
        public Int64 deleteAssetV2()
        {
            return assetRepositoryV2.DeleteAssetV2(getHdStr("bld_idx"));
        }
    }
}
