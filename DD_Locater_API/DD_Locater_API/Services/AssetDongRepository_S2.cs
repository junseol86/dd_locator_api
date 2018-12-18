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
    public class AssetDongRepository_S2: DBFuncs
    {
        //WebRequest request;
        
        public DongAndCount DongsInBound(string bld_type, double top, double bottom, double left, double right, 
            Int64 hasName, Int64 hasNumber, Int64 hasGwan, Int64 fmlyMin, Int64 fmlyMax,
            string mainPurps, string useAprDay, Int64 visited, Int64 factory_count, string doCode)
        {
            List<AssetDongV2> result = new List<AssetDongV2>();
            Int64 count = 0;

            string condition = "";

            condition = ConditionSetter.ByBldType(condition, bld_type);
            condition = ConditionSetter.ByNameNumberGwan(condition, hasName, hasNumber, hasGwan);
            condition = ConditionSetter.ByFamilyMinMax(condition, fmlyMin, fmlyMax);
            condition = ConditionSetter.ByFactoryCount(condition, factory_count);

            condition += $" AND main_purps_cd_nm LIKE '%{System.Web.HttpUtility.UrlDecode(mainPurps)}%'";
            condition += useAprDay.Trim() != "" ? $" AND useapr_day >= '{useAprDay}'" : "";
            condition += visited <= -1 ? "" : $" AND visited = {visited}";

            if (doCode.Length > 0)
            {
                condition += $" AND do_cd = {doCode}";
            }

            using (MySqlConnection conn = openCon())
            {
                string countQuery =
                    $@"
                        SELECT
                            COUNT(*) count
                        FROM 
                            view_locatorforsearch
                        WHERE TRUE
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
                            dongri_name,
                            AVG(CAST(geo_lng AS DECIMAL(11,7))) AS geo_lng, AVG(CAST(geo_lat AS DECIMAL(11,7))) as geo_lat, 
                            COUNT(BJDONG_CD) as asset_count FROM view_locatorforsearch
                        WHERE
                            geo_lng > '{top}'
                            AND geo_lng < '{bottom}'
                            AND geo_lat < '{left}'
                            AND geo_lat > '{right}'
                            {condition}
                        GROUP BY dongri_name
                        ORDER BY geo_lat
                    ";


                using (MySqlDataReader reader = exReader(getAssetDongsQuery, conn))
                {
                    while (reader.Read())
                    {
                        string dongRiName = reader["dongri_name"].ToString();

                        result.Add(new AssetDongV2(
                            "", "", dongRiName,
                            Convert.ToDouble(reader["geo_lng"].ToString()),
                            Convert.ToDouble(reader["geo_lat"].ToString()),
                            Convert.ToInt16(reader["asset_count"].ToString())
                        ));
                    }
                }
            }
            return new DongAndCount(result, count);
        }


        public List<AssetDongV2> DongsInBoundMobile(string bld_ctgr, string bld_type, double top, double bottom, double left, double right,
            Int64 hasName, Int64 hasNumber, Int64 hasGwan, Int64 fmlyMin, Int64 fmlyMax,
            string mainPurps, string useAprDay, Int64 visited, Int64 factory_count)
        {
            List<AssetDongV2> result = new List<AssetDongV2>();

            string condition = "";

            condition = ConditionSetter.ByBldCtgr(condition, bld_ctgr);
            condition = ConditionSetter.ByBldType(condition, bld_type);
            condition = ConditionSetter.ByNameNumberGwan(condition, hasName, hasNumber, hasGwan);
            condition = ConditionSetter.ByFamilyMinMax(condition, fmlyMin, fmlyMax);
            condition = ConditionSetter.ByFactoryCount(condition, factory_count);

            condition += $" AND main_purps_cd_nm LIKE '%{System.Web.HttpUtility.UrlDecode(mainPurps)}%'";
            condition += useAprDay.Trim() != "" ? $" AND useapr_day >= '{useAprDay}'" : "";
            condition += visited <= -1 ? "" : $" AND visited = {visited}";

            using (MySqlConnection conn = openCon())
            {

                string getAssetDongsQuery =
                    $@"
                        SELECT 
                            dongri_name,
                            AVG(CAST(geo_lng AS DECIMAL(11,7))) AS geo_lng, AVG(CAST(geo_lat AS DECIMAL(11,7))) as geo_lat, 
                            COUNT(BJDONG_CD) as asset_count FROM view_locatorforsearch
                        WHERE 
                            geo_lng > '{top}'
                            AND geo_lng < '{bottom}'
                            AND geo_lat < '{left}'
                            AND geo_lat > '{right}'
                            {condition}
                        GROUP BY dongri_name
                        ORDER BY geo_lat
                    ";

                System.Diagnostics.Debug.WriteLine(getAssetDongsQuery);


                using (MySqlDataReader reader = exReader(getAssetDongsQuery, conn))
                {
                    while (reader.Read())
                    {
                        string dongRiName = reader["dongri_name"].ToString();

                        result.Add(new AssetDongV2(
                            "", "", dongRiName,
                            Convert.ToDouble(reader["geo_lng"].ToString()),
                            Convert.ToDouble(reader["geo_lat"].ToString()),
                            Convert.ToInt16(reader["asset_count"].ToString())
                        ));
                    }
                }
            }
            return result;
        }


        public string getDongName(string sgg, string bjd)
        {
            string result = "";
            string temp = "";
            using (MySqlConnection conn = openCon())
            {
                string selectQuery = $"SELECT plat_plc FROM view_locatorforsearch WHERE sigungu_cd = '{sgg}' AND bjdong_cd = '{bjd}' ORDER BY RAND() LIMIT 10";
                using (MySqlDataReader reader = exReader(selectQuery, conn))
                {
                    while (reader.Read())
                    {
                        string platPlc = reader["plat_plc"].ToString();
                        if (temp.Length == 0)
                        {
                            foreach (string piece in platPlc.Split(' '))
                            {
                                if ("읍면동".Contains(piece[piece.Length - 1]))
                                {
                                    temp = piece;
                                }
                            }
                        }
                        else
                        {
                            if (!platPlc.Contains(temp))
                            {
                                temp = "";
                            }
                        }
                    }
                    if (temp.Length > 0)
                    {
                        result = temp;
                    }
                }
            }
            return result;
        }

    }
}