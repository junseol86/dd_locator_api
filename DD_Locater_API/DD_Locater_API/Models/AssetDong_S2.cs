using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DD_Locater_API.Models
{
    public class AssetDong_S2
    {
        public string sigungu_cd;
        public string bjdong_cd;
        public string dong_name;
        public double bld_map_x;
        public double bld_map_y;
        public int asset_count;


        public AssetDong_S2(
            string _sigunguCd, string _bjdongCd, string _dongName, double bld_map_x, double bld_map_y, int assetCount
            )
        {
            this.sigungu_cd = _sigunguCd;
            this.bjdong_cd = _bjdongCd;
            this.dong_name = _dongName;
            this.bld_map_x = bld_map_x;
            this.bld_map_y = bld_map_y;
            this.asset_count = assetCount;
        }
    }
}