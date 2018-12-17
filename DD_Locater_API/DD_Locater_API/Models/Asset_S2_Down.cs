using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DD_Locater_API.Models
{
    public class Asset_S2_Down: Asset_S2
    {
        public Asset_S2_Down(MySqlDataReader reader)
        {
            bld_idx = Convert.ToInt64(reader["bld_idx"].ToString());
            bld_type = reader["bld_type"].ToString();

            plat_plc = reader["plat_plc"].ToString();
            new_plat_plc = reader["new_plat_plc"].ToString();
            main_purps = reader["main_purps_cd_nm"].ToString();
            etc_purps = reader["etc_purps"].ToString();
            grnd_flr_cnt = reader["grnd_flr_cnt"].ToString();
            useapr_day = reader["useapr_day"].ToString();
            fmly_cnt = reader["fmly_cnt"].ToString();

            bld_name = reader["bld_name"].ToString();
            bld_fmly_cnt = reader["bld_fmly_cnt"].ToString();
            bld_memo = reader["bld_memo"].ToString();
            bld_ipkey = reader["bld_ipkey"].ToString();
            bld_roomkey = reader["bld_roomkey"].ToString();
            bld_gwan = reader["bld_gwan"].ToString();
            bld_tel_owner = reader["bld_tel_owner"].ToString();
            bld_tel_gwan = reader["bld_tel_gwan"].ToString();
            bld_map_x = reader["geo_lng"].ToString();
            bld_map_y = reader["geo_lat"].ToString();
            bld_on_wall = reader["bld_on_wall"].ToString();
            bld_on_parked = reader["bld_on_parked"].ToString();
            work_requested = reader["work_requested"].ToString();
            photo = reader["photo"].ToString();
            visited = Convert.ToInt16(reader["visited"].ToString());
            factory_count = Convert.ToInt16(reader["factory_count"].ToString());
        }

        public Asset_S2_Down()
        {
            bld_idx = 0;
        }
    }
}