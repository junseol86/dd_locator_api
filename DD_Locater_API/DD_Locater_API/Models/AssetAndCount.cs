using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DD_Locater_API.Models
{
    public class AssetAndCount
    {
        public List<AssetMarkV2> asset_list { get; set; }
        public Int64 total_count { get; set; }

        public AssetAndCount(List<AssetMarkV2> _assetList, Int64 _totalCount)
        {
            asset_list = _assetList;
            total_count = _totalCount;
        }
    }

    public class AssetAndCount_S2
    {
        public List<AssetMark_S2> asset_list { get; set; }
        public Int64 total_count { get; set; }

        public AssetAndCount_S2(List<AssetMark_S2> _assetList, Int64 _totalCount)
        {
            asset_list = _assetList;
            total_count = _totalCount;
        }
    }

}