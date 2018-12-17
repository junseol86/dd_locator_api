using DD_Locater_API.Models;
using DD_Locater_API.Utils;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace DD_Locater_API.Services
{
    public class AssetDongRepositoryV2: DBFuncs
    {
        WebRequest request;
        
        public List<AssetDongV2> DongsInBound(string bld_type, double left, double right, double top, double bottom, 
            bool noName, bool noNumber, bool noGwan, Int64 fmlyMin, Int64 fmlyMax, string mainPurps, string useAprDay)
        {
            List<AssetDongV2> result = new List<AssetDongV2>();

            using (MySqlConnection conn = openCon())
            {
                string condition = "";
                condition += noName ? $"AND (bld_name = '' OR bld_name = '(자동입력)') " : "";
                condition += noNumber ? $"AND bld_tel_owner = '' " : "";
                condition += noGwan ? $"AND (bld_gwan = '' OR bld_gwan IS NULL) " : "";
                condition += fmlyMin >= 0 ? $"AND bld_fmly_cnt >= {fmlyMin} " : "";
                condition += fmlyMax >= 0 ? $"AND bld_fmly_cnt <= {fmlyMax} " : "";
                condition += $" AND main_purps_cd_nm LIKE '%{System.Web.HttpUtility.UrlDecode(mainPurps)}%'";
                condition += useAprDay.Trim() != "" ? $" AND useapr_day >= '{useAprDay}'" : "";

                string getAssetDongsQuery =
                    $@"
                        SELECT 
                            SIGUNGU_CD, BJDONG_CD,
                            AVG(CAST(bld_map_x AS DECIMAL(11,7))) AS bld_map_x, AVG(CAST(bld_map_y AS DECIMAL(11,7))) as bld_map_y, 
                            COUNT(BJDONG_CD) as asset_count FROM dd_locator_bld
                        WHERE
                            bld_type LIKE '%{bld_type}%'
                            AND bld_map_x > '{left}'
                            AND bld_map_x < '{right}'
                            AND bld_map_y < '{top}'
                            AND bld_map_y > '{bottom}'
                            {condition}
                        GROUP BY SIGUNGU_CD, BJDONG_CD
                        ORDER BY bld_map_y
                    ";

                using (MySqlDataReader reader = exReader(getAssetDongsQuery, conn))
                {
                    while (reader.Read())
                    {
                        string sigungu =  reader["SIGUNGU_CD"].ToString();
                        string bjdong = reader["BJDONG_CD"].ToString();
                        string dongName = "(동 이름 받기 실패)";

                        string address = "https://openapi.nsdi.go.kr/nsdi/eios/service/rest/AdmService/admDongList.json?authkey=fd14b8208e06b2f0e7ea4c&admCode="
                            + sigungu;
                        request = WebRequest.Create(address);
                        request.Method = "GET";

                        try
                        {
                            string responseStr = new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
                            JObject rspObj = JObject.Parse(responseStr);
                            JArray dongs = (JArray)((JObject)rspObj["admVOList"])["admVOList"];
                            for (int i = 0; i < dongs.Count; i++)
                            {
                                if ((string)dongs[i]["admCode"] == sigungu + bjdong.Substring(0, 3))
                                    dongName = (string)dongs[i]["lowestAdmCodeNm"];
                            }
                        }
                        catch
                        {
                        }

                        result.Add(new AssetDongV2(
                            sigungu, bjdong, dongName,
                            Convert.ToDouble(reader["bld_map_x"].ToString()),
                            Convert.ToDouble(reader["bld_map_y"].ToString()),
                            Convert.ToInt16(reader["asset_count"].ToString())
                        ));
                    }
                }
            }
            return result;
        }

        public DongAndCount DongsInBound4(string bld_type, double top, double bottom, double left, double right, 
            Int64 hasName, Int64 hasNumber, Int64 hasGwan, Int64 fmlyMin, Int64 fmlyMax, string mainPurps, string useAprDay)
        {
            List<AssetDongV2> result = new List<AssetDongV2>();
            Int64 count = 0;

            string condition = "";

            switch (hasName)
            {
                case -1: condition += "AND (bld_name = '' OR bld_name = '(자동입력)') "; break;
                case 1: condition += "AND bld_name != '' AND bld_name != '(자동입력)' "; break;
            }
            switch (hasNumber)
            {
                case -1: condition += "AND bld_tel_owner = '' "; break;
                case 1: condition += "AND bld_tel_owner != '' "; break;
            }
            switch (hasGwan)
            {
                case -1: condition += "AND (bld_gwan = '' OR bld_gwan IS NULL) "; break;
                case 1: condition += "AND bld_gwan != '' AND bld_gwan IS NOT NULL "; break;
            }

            condition += fmlyMin >= 0 ? $"AND bld_fmly_cnt >= {fmlyMin} " : "";
            condition += fmlyMax >= 0 ? $"AND bld_fmly_cnt <= {fmlyMax} " : "";
            condition += $" AND main_purps_cd_nm LIKE '%{System.Web.HttpUtility.UrlDecode(mainPurps)}%'";
            condition += useAprDay.Trim() != "" ? $" AND useapr_day >= '{useAprDay}'" : "";

            using (MySqlConnection conn = openCon())
            {
                string countQuery =
                    $@"
                        SELECT
                            COUNT(*) count
                        FROM 
                            dd_locator_bld
                        WHERE
                            bld_type LIKE '%{bld_type}%'
                            {condition}
                    ";

                using (MySqlDataReader reader = exReader(countQuery, conn))
                {
                    if (reader.Read())
                    {
                        count = Convert.ToInt64(reader["count"].ToString());
                    }
                }
            }

            using (MySqlConnection conn = openCon())
            {

                string getAssetDongsQuery =
                    $@"
                        SELECT 
                            SIGUNGU_CD, BJDONG_CD,
                            AVG(CAST(bld_map_x AS DECIMAL(11,7))) AS bld_map_x, AVG(CAST(bld_map_y AS DECIMAL(11,7))) as bld_map_y, 
                            COUNT(BJDONG_CD) as asset_count FROM dd_locator_bld
                        WHERE
                            bld_type LIKE '%{bld_type}%'
                            AND bld_map_x > '{top}'
                            AND bld_map_x < '{bottom}'
                            AND bld_map_y < '{left}'
                            AND bld_map_y > '{right}'
                            {condition}
                        GROUP BY SIGUNGU_CD, BJDONG_CD
                        ORDER BY bld_map_y
                    ";


                using (MySqlDataReader reader = exReader(getAssetDongsQuery, conn))
                {
                    while (reader.Read())
                    {
                        string sigungu =  reader["SIGUNGU_CD"].ToString();
                        string bjdong = reader["BJDONG_CD"].ToString();
                        string dongName = "(동 이름 받기 실패)";

                        string address = "https://openapi.nsdi.go.kr/nsdi/eios/service/rest/AdmService/admDongList.json?authkey=fd14b8208e06b2f0e7ea4c&admCode="
                            + sigungu;
                        request = WebRequest.Create(address);
                        request.Method = "GET";

                        try
                        {
                            string responseStr = new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
                            JObject rspObj = JObject.Parse(responseStr);
                            JArray dongs = (JArray)((JObject)rspObj["admVOList"])["admVOList"];
                            for (int i = 0; i < dongs.Count; i++)
                            {
                                if ((string)dongs[i]["admCode"] == sigungu + bjdong.Substring(0, 3))
                                    dongName = (string)dongs[i]["lowestAdmCodeNm"];
                            }
                        }
                        catch
                        {
                        }

                        result.Add(new AssetDongV2(
                            sigungu, bjdong, dongName,
                            Convert.ToDouble(reader["bld_map_x"].ToString()),
                            Convert.ToDouble(reader["bld_map_y"].ToString()),
                            Convert.ToInt16(reader["asset_count"].ToString())
                        ));
                    }
                }
            }
            return new DongAndCount(result, count);
        }
    }
}