using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DD_Locater_API.Models
{
    public class AssetMarkV2
    {
        public int markOrCluster;
        public Int64 asset_count;

        public Int64 bld_idx;
        public string bld_type;
        public string bld_map_x;
        public string bld_map_y;
        public string bld_name;
        public string bld_ipkey;
        public string bld_roomkey;
        public string bld_tel_owner;
        public string work_requested;

        public AssetMarkV2(
            Int64 _bldIdx, 
            string _bldType, 
            string _bldMapX, string _bldMapY, 
            string _bldName, 
            string _bldIpkey, string _bldRoomkey, 
            string _bldTelOwner,
            string _workRequested
            )
        {
            markOrCluster = 0;
            bld_idx = _bldIdx;
            bld_type = _bldType;
            bld_map_x = _bldMapX;
            bld_map_y = _bldMapY;
            bld_name = _bldName;
            bld_ipkey = _bldIpkey;
            bld_roomkey = _bldRoomkey;
            bld_tel_owner = _bldTelOwner;
            work_requested = _workRequested;
        }
    }

}