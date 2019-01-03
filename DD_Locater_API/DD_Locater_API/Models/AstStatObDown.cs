using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DD_Locater_API.Models
{
    public class AstStatObDown: AstStatOb
    {
        public AstStatObDown (MySqlDataReader reader)
        {
            bld_idx = Convert.ToInt64(reader["bld_idx"].ToString());
            bld_type = reader["bld_type"].ToString();
            bld_name = reader["bld_name"].ToString();
            plat_plc = reader["plat_plc"].ToString();
            new_plat_plc = reader["new_plat_plc"].ToString();
            orm_bd_idx = reader["orm_bd_idx"].ToString();
            host_mobile = reader["host_mobile"].ToString();
            host_mobile2 = reader["host_mobile2"].ToString();
            memo = reader["memo"].ToString();
            pwd_building = reader["pwd_building"].ToString();
            picture_agree = reader["picture_agree"].ToString();
            date_research = reader["date_research"].ToString();
        }
    }
}
