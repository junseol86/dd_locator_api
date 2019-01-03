using System;
using MySql.Data.MySqlClient;

namespace DD_Locater_API.Models
{
	public class AstStatHsDown: AstStatHs
    {
		public AstStatHsDown(MySqlDataReader reader)
        {
            hosu_idx = Convert.ToInt64(reader["hosu_idx"].ToString());
            ad_yn = Convert.ToInt64(reader["ad_yn"].ToString());
            meta_bd_pic_idx = Convert.ToInt64(reader["meta_bd_pic_idx"].ToString());
            hosu = reader["hosu"].ToString();
            hosu_type = reader["hosu_type"].ToString();
            floor = Convert.ToInt64(reader["floor"].ToString());
            price_w = Convert.ToInt64(reader["price_w"].ToString());
            price_wbo = Convert.ToInt64(reader["price_wbo"].ToString());
            price_j = Convert.ToInt64(reader["price_j"].ToString());
            pwd = reader["pwd"].ToString();
            memo = reader["memo"].ToString();
            memo_manager = reader["memo_manager"].ToString();
            cond_blackout = Convert.ToInt64(reader["cond_blackout"].ToString());
            cond_dirty = Convert.ToInt64(reader["cond_dirty"].ToString());
            cond_wallpaper = Convert.ToInt64(reader["cond_wallpaper"].ToString());
            person_tel_open = Convert.ToInt64(reader["person_tel_open"].ToString());
        }
    }
}
