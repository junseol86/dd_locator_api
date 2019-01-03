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
    public class Asset_S2_Controller : CustomController
    {
        private AssetRepository_S2 assetRepository;
        private AssetStatusRepository assetStatusRepository;

        public Asset_S2_Controller()
        {
            this.assetRepository = new AssetRepository_S2();
            this.assetStatusRepository = new AssetStatusRepository();
        }

        [Route("api/asset_S2/{baseIdx}")]
        [HttpGet]
        public Asset_S2_Down GetAnAsset(Int64 baseIdx)
        {
            return assetRepository.GetAnAsset(baseIdx);
        }

        [Route("api/asset_S2/status/hs/{baseIdx}")]
        [HttpGet]
        public List<AstStatHsDown> GetAssetStatusHs(Int64 baseIdx)
        {
            return assetStatusRepository.GetAssetStatusHs(baseIdx);
        }

        [Route("api/asset_S2/status/ob/{baseIdx}")]
        [HttpGet]
        public AstStatObDown GetAssetStatusOb(Int64 baseIdx)
        {
            return assetStatusRepository.GetAssetStatusOb(baseIdx);
        }

        [Route("api/asset/modify_S2")]
        [HttpPut]
        public Int64 ModifyAsset([FromBody] Asset_S2 asset)
        {
            return assetRepository.ModifyAsset(asset, getHdStr("bld_idx"));
        }

        [Route("api/asset/status/update_date")]
        [HttpPut]
        public Int64 UpdateStatusDateResearch()
        {
            return assetStatusRepository.updateResearchDate(getHdStr("orm_bd_idx"));
        }

        [Route("api/asset/status/modify_ob")]
        [HttpPut]
        public Int64 ModifyAstStatusOb([FromBody] AstStatOb ob)
        {
            return assetStatusRepository.modifyOb(ob);
        }

        [Route("api/asset/status/modify_hs")]
        [HttpPut]
        public Int64 ModifyAstStatusOb([FromBody] AstStatHs hs)
        {
            return assetStatusRepository.modifyHs(hs);
        }
    }
}
