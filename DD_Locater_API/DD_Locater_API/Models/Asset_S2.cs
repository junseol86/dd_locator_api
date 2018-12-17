using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DD_Locater_API.Models
{
    public class Asset_S2
    {
        public Int64 bld_idx {get; set;}
        public string bld_type {get; set;}
        public string plat_plc { get; set; }
        public string new_plat_plc { get; set; }
        public string main_purps { get; set; }
        public string etc_purps { get; set; }
        public string grnd_flr_cnt {get; set;}
        public string useapr_day {get; set;}
        public string fmly_cnt {get; set;}
        public string bld_name {get; set;}
        public string bld_fmly_cnt {get; set;}
        public string bld_memo {get; set;}
        public string bld_ipkey {get; set;}
        public string bld_roomkey {get; set;}
        public string bld_gwan {get; set;}
        public string bld_tel_owner {get; set;}
        public string bld_tel_gwan {get; set;}
        public string bld_map_x {get; set;}
        public string bld_map_y {get; set;}
        public string bld_on_wall {get; set;}
        public string bld_on_parked {get; set;}
        public string work_requested {get; set;}
        public string photo {get; set;}
        public Int16 visited {get; set;}
        public Int16 factory_count {get; set;}
    }
}