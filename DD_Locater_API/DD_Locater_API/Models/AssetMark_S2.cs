using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DD_Locater_API.Models
{
    public class AssetMark_S2
    {
        public int markOrCluster;
        public Int64 bld_idx;
        public string bld_type;
        public string bld_map_x;
        public string bld_map_y;
        public string bld_name;
        public string bld_ipkey;
        public string bld_roomkey;
        public string bld_tel_owner;
        public string bld_on_wall;
        public string bld_on_parked;
        public string work_requested;
        public string work_request;
        public Int16 visited;
        public Int16 has_factory_tel;
        public Int16 factory_count;
        public string date_research;
        public Int64 asset_count;

        public AssetMark_S2(MySqlDataReader reader)
        {
            markOrCluster = 0;
            bld_idx = Convert.ToInt64(reader["bld_idx"].ToString());
            bld_type = reader["bld_type"].ToString();
            bld_map_x = reader["bld_map_x"].ToString();
            bld_map_y = reader["bld_map_y"].ToString();
            bld_name = reader["bld_name"].ToString();
            bld_ipkey = reader["bld_ipkey"].ToString();
            bld_roomkey = reader["bld_roomkey"].ToString();
            bld_tel_owner = reader["bld_tel_owner"].ToString();
            bld_on_wall = reader["bld_on_wall"].ToString();
            bld_on_parked = reader["bld_on_parked"].ToString();
            work_requested = reader["work_requested"].ToString();
            work_request = reader["work_request"].ToString();
            visited = Convert.ToInt16(reader["visited"].ToString());
            factory_count = Convert.ToInt16(reader["factory_count"].ToString());
            date_research = reader["date_research"].ToString();
        }
    }

}