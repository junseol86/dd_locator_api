using DD_Locater_API.Utils;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DD_Locater_API.Services
{
    public class OneroomInfoRepository: DBFuncs
    {
        public void LoadAndInsertOneroomInfo(JObject ppt)
        {
            string bldIdx = "";
            string bldTelOwner = "";
            string bldIpkey = "";

            using (MySqlConnection conn = openCon())
            {
                string getBldQuery = $@"
                    SELECT * FROM dd_locator_bld
                    WHERE 
                        PLAT_PLC LIKE '% {((string)ppt["읍면동"]).Replace(" ", "")} {((string)ppt["리"]).Replace(" ", "")}%'
                        AND PLAT_PLC LIKE '% {((string)ppt["지번"]).Replace(" ", "")}번지';
                ";
                using (MySqlDataReader reader = exReader(getBldQuery, conn))
                {
                    if (reader.Read())
                    {
                        bldIdx = reader["bld_idx"].ToString();
                        bldTelOwner = reader["bld_tel_owner"].ToString();
                        bldIpkey = reader["bld_ipkey"].ToString();
                    }
                }
                        
            }

            if (bldIdx.Trim() != "")
            {
                using (MySqlConnection conn = openCon())
                {
                    string additional = "";
                    if (bldTelOwner == "" && (string)ppt["주인전화번호"] != "")
                    {
                        additional += $" , bld_tel_owner = '{(string)ppt["주인전화번호"]}' ";
                    }
                    if (bldIpkey == "" && (string)ppt["현관비번"] != "")
                    {
                        additional += $" , bld_ipkey = '{(string)ppt["현관비번"]}' ";
                    }

                    string updateQuery = $@"
                        
                        UPDATE dd_locator_bld
                        SET
                            bld_name = '{(string)ppt["건물명"]}',
                            bld_gwan = '{(string)ppt["관리업체"]}',
                            bld_tel_gwan = '{(string)ppt["관리자번호1"]}'
                            {additional}
                        WHERE
                            bld_idx = {bldIdx};
                    ";
                    //System.Diagnostics.Debug.WriteLine(updateQuery);
                    exNonQuery(updateQuery, conn);
                }
            } else
            {
                System.Diagnostics.Debug.WriteLine(ppt);
            }
            
        }

        public void RemoveGwan(string dong, string ri, string bunji)
        {
            string bldIdx = "";
            using (MySqlConnection conn = openCon())
            {
                string getBldQuery = $@"
                    SELECT * FROM dd_locator_bld
                    WHERE 
                        PLAT_PLC LIKE '% {dong} {ri}%'
                        AND PLAT_PLC LIKE '% {bunji.Split('\n')[0]}번지';
                ";

                System.Diagnostics.Debug.WriteLine(getBldQuery);
                using (MySqlDataReader reader = exReader(getBldQuery, conn))
                {
                    if (reader.Read())
                    {
                        bldIdx = reader["bld_idx"].ToString();
                    }
                }
                        
            }

            if (bldIdx.Trim() != "")
            {
                using (MySqlConnection conn = openCon())
                {
                    string updateQuery = $@"
                        
                        UPDATE dd_locator_bld
                        SET
                            bld_gwan = '',
                            bld_tel_gwan = ''
                        WHERE
                            bld_idx = {bldIdx};
                    ";
                    exNonQuery(updateQuery, conn);
                }
            }
            
        }
    }
}