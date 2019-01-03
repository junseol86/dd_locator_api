using System;
using MySql.Data.MySqlClient;

namespace DD_Locater_API.Models
{
    public class AstStatHsDown: AstStatHs
    {
        public AstStatHsDown(MySqlDataReader reader)
        {
            hosu_idx = reader["hosu_idx"].ToString();
            ad_yn = reader["ad_yn"].ToString();
            meta_bd_pic_idx = reader["meta_bd_pic_idx"].ToString();
            hosu = reader["hosu"].ToString();
            hosu_type = reader["hosu_type"].ToString();
            floor = reader["floor"].ToString();
            kind_ad = reader["kind_ad"].ToString();
            price_ad_w = reader["price_ad_w"].ToString();
            price_ad_wbo = reader["price_ad_wbo"].ToString();
            price_ad_j = reader["price_ad_j"].ToString();
            pwd = reader["pwd"].ToString();
            pwd_open = reader["pwd_open"].ToString();
            memo = reader["memo"].ToString();
            memo_manager = reader["memo_manager"].ToString();
            cond_blackout = reader["cond_blackout"].ToString();
            cond_dirty = reader["cond_dirty"].ToString();
            cond_wallpaper = reader["cond_wallpaper"].ToString();
            person_tel_open = reader["person_tel_open"].ToString();
        }
    }
}
