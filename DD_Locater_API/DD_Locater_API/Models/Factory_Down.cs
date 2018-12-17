using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DD_Locater_API.Models
{
    public class Factory_Down: Factory
    {
        public Factory_Down(MySqlDataReader reader)
        {
            ftr_idx = Convert.ToInt64(reader["ftr_idx"].ToString());
            company = reader["company"].ToString();
            institute = reader["institute"].ToString();
            danji = reader["danji"].ToString();
            found_code_nm = reader["found_code_nm"].ToString();
            yongji_area = reader["yongji_area"].ToString();
            geonchuk_area = reader["geonchuk_area"].ToString();
            employees = reader["employees"].ToString();
            scale_code_nm = reader["scale_code_nm"].ToString();
            regist_date = reader["regist_date"].ToString();
            yongdo_region = reader["yongdo_region"].ToString();
            jimok = reader["jimok"].ToString();
            upjong = reader["upjong"].ToString();
            product = reader["product"].ToString();
            water_pollution = reader["water_pollution"].ToString();
            air_pollution = reader["air_pollution"].ToString();
            noise = reader["noise"].ToString();
            life_water_usage = reader["life_water_usage"].ToString();
            ind_water_usage = reader["ind_water_usage"].ToString();
            self_capital = reader["self_capital"].ToString();
            other_capital = reader["other_capital"].ToString();
            phone = reader["phone"].ToString();
            website = reader["website"].ToString();
            zip_code = reader["zip_code"].ToString();
            addr_jibun = reader["addr_jibun"].ToString();
            addr_doro = reader["addr_doro"].ToString();
            latitude = reader["latitude"].ToString();
            longitude = reader["longitude"].ToString();
            created = reader["created"].ToString();
        }
    }
}

