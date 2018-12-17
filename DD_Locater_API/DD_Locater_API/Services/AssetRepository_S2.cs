﻿using DD_Locater_API.Models;
using DD_Locater_API.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DD_Locater_API.Services
{
    public class AssetRepository_S2: DBFuncs
    {

        public AssetAndCount_S2 AssetsInBound(string bld_type, double left, double right, double top, double bottom, 
            Int64 hasName, Int64 hasNumber, Int64 hasGwan, Int64 fmlyMin, Int64 fmlyMax,
            string mainPurps, string useaprDay, Int64 visited, Int64 factory_count, string doCode)
        {

            List<AssetMark_S2> result = new List<AssetMark_S2>();
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

            condition += bld_type == "ftrstr" ? " AND (bld_type LIKE 'FACTORY%' OR bld_type = 'str')"
                : bld_type.Contains("FACTORY") ? $" AND bld_type = '{bld_type}'"
                : bld_type == "ONEROOM" ? " AND (bld_type = 'ONEROOM' OR bld_type = 'ONEROOM_SU')"
                : bld_type == "su" ? " AND (bld_type = 'su' OR bld_type = 'ONEROOM_SU')"
                : bld_type == "OR_SU_BOTH" ? " AND (bld_type = 'ONEROOM' OR bld_type = 'su' OR bld_type = 'ONEROOM_SU')"
                : $" AND bld_type LIKE '%{bld_type}%'";
            condition += fmlyMin >= 0 ? $" AND fmly_cnt >= {fmlyMin} " : "";
            condition += fmlyMax >= 0 ? $" AND fmly_cnt <= {fmlyMax} " : "";
            condition += $" AND main_purps_cd_nm LIKE '%{System.Web.HttpUtility.UrlDecode(mainPurps)}%'";
            condition += useaprDay.Trim() != "" ? $" AND useapr_day >= '{useaprDay}'" : "";
            condition += visited <= -1 ? "" : $" AND visited = {visited}";
            if (factory_count == 0)
            {
                condition += " AND visited = 1 AND factory_count = 0";
            }
            if (factory_count > 0)
            {
                condition += " AND visited = 1 AND factory_count > 0";
            }
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
                        WHERE
                            bld_type LIKE '%%'
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
                string getAssetListQuery =
                    $@"
                        SELECT
                            bld_idx, 
                            bld_type, 
                            bld_name, 
                            geo_lng as bld_map_x, geo_lat as bld_map_y, 
                            bld_ipkey, bld_roomkey, 
                            bld_tel_owner,
                            bld_on_wall,
                            bld_on_parked,
                            work_requested,
                            work_request,
                            visited,
                            factory_count
                        FROM 
                            view_locatorforsearch
                        WHERE
                            geo_lng > '{left}'
                            AND geo_lng < '{right}'
                            AND geo_lat < '{top}'
                            AND geo_lat > '{bottom}'
                            {condition}
                        ORDER BY
                            geo_lat, modified ASC
                    ";

                System.Diagnostics.Debug.WriteLine(getAssetListQuery);

                using (MySqlDataReader reader = exReader(getAssetListQuery, conn))
                {
                    while(reader.Read())
                    {
                        result.Add(new AssetMark_S2(reader));
                    }
                }
            }
            return new AssetAndCount_S2(result, count);
        }

        public List<Asset_S2_Down> AssetsRequested(string bld_type, 
            Int64 hasName, Int64 hasNumber, Int64 hasGwan, Int64 fmlyMin, Int64 fmlyMax, string mainPurps, string useaprDay, string bldName, string bldMemo, Int64 visited, Int64 factory_count, string doCode)
        {

            List<Asset_S2_Down> result = new List<Asset_S2_Down>();
            using (MySqlConnection conn = openCon())
            {

                string condition = "";

                condition += bld_type == "ftrstr" ? " AND (bld_type LIKE 'FACTORY%' OR bld_type = 'str')"
                    : bld_type.Contains("FACTORY") ? $" AND bld_type = '{bld_type}'"
                    : bld_type == "ONEROOM" ? " AND (bld_type = 'ONEROOM' OR bld_type = 'ONEROOM_SU')"
                    : bld_type == "su" ? " AND (bld_type = 'su' OR bld_type = 'ONEROOM_SU')"
                    : bld_type == "OR_SU_BOTH" ? " AND (bld_type = 'ONEROOM' OR bld_type = 'su' OR bld_type = 'ONEROOM_SU')"
                    : $" AND bld_type LIKE '%{bld_type}%'";

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

                condition += fmlyMin >= 0 ? $" AND fmly_cnt >= {fmlyMin} " : "";
                condition += fmlyMax >= 0 ? $" AND fmly_cnt <= {fmlyMax} " : "";
                condition += $" AND main_purps_cd_nm LIKE '%{System.Web.HttpUtility.UrlDecode(mainPurps)}%'";
                condition += useaprDay.Trim() != "" ? $" AND useapr_day >= '{useaprDay}'" : "";
                condition += visited <= -1 ? "" : $" AND visited = {visited}";
                if (factory_count == 0)
                {
                    condition += " AND visited = 1 AND factory_count = 0";
                }
                if (factory_count > 0)
                {
                    condition += " AND visited = 1 AND factory_count > 0";
                }
                if (doCode.Length > 0)
                {
                    condition += $" AND do_cd = {doCode}";
                }

                string getAssetListQuery =
                    $@"
                        SELECT *
                        FROM 
                            view_locatorforsearch
                        WHERE
                            work_requested != ''
                            AND (bld_name LIKE '%{bldName}%' OR plat_plc LIKE '%{bldName}%')
                            AND bld_memo LIKE '%{bldMemo}%'
                            {condition}
                        ORDER BY
                            work_requested DESC
                    ";

                using (MySqlDataReader reader = exReader(getAssetListQuery, conn))
                {
                    while(reader.Read())
                    {
                        result.Add(new Asset_S2_Down(reader));
                    }
                }
            }
            return result;
        }

        public List<Asset_S2_Down> AssetsSearched(string bld_type, 
            Int64 hasName, Int64 hasNumber, Int64 hasGwan, Int64 fmlyMin, Int64 fmlyMax, string mainPurps, string useaprDay, string bldName, string bldMemo, Int64 visited, Int64 factory_count, string doCode)
        {

            List<Asset_S2_Down> result = new List<Asset_S2_Down>();
            using (MySqlConnection conn = openCon())
            {
                string condition = "";

                condition += bld_type == "ftrstr" ? " AND (bld_type LIKE 'FACTORY%' OR bld_type = 'str')"
                    : bld_type.Contains("FACTORY") ? $" AND bld_type = '{bld_type}'"
                    : bld_type == "ONEROOM" ? " AND (bld_type = 'ONEROOM' OR bld_type = 'ONEROOM_SU')"
                    : bld_type == "su" ? " AND (bld_type = 'su' OR bld_type = 'ONEROOM_SU')"
                    : bld_type == "OR_SU_BOTH" ? " AND (bld_type = 'ONEROOM' OR bld_type = 'su' OR bld_type = 'ONEROOM_SU')"
                    : $" AND bld_type LIKE '%{bld_type}%'";

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

                condition += fmlyMin >= 0 ? $" AND fmly_cnt >= {fmlyMin} " : "";
                condition += fmlyMax >= 0 ? $" AND fmly_cnt <= {fmlyMax} " : "";
                condition += $" AND main_purps_cd_nm LIKE '%{System.Web.HttpUtility.UrlDecode(mainPurps)}%'";
                condition += useaprDay.Trim() != "" ? $" AND useapr_day >= '{useaprDay}'" : "";
                condition += visited <= -1 ? "" : $" AND visited = {visited}";
                if (factory_count == 0)
                {
                    condition += " AND visited = 1 AND factory_count = 0";
                }
                if (factory_count > 0)
                {
                    condition += " AND visited = 1 AND factory_count > 0";
                }
                if (doCode.Length > 0)
                {
                    condition += $" AND do_cd = {doCode}";
                }

                string getAssetListQuery =
                    $@"
                        SELECT *
                        FROM 
                            view_locatorforsearch
                        WHERE
                            (bld_name LIKE '%{bldName}%' OR plat_plc LIKE '%{bldName}%')
                            AND bld_memo LIKE '%{bldMemo}%'
                            {condition}
                    ";

                using (MySqlDataReader reader = exReader(getAssetListQuery, conn))
                {
                    while(reader.Read())
                    {
                        result.Add(new Asset_S2_Down(reader));
                    }
                }
            }
            return result;
        }

        public Asset_S2_Down GetAnAsset(Int64 _bldIdx)
        {
            Asset_S2_Down result = new Asset_S2_Down();

            using (MySqlConnection conn = openCon())
            {
                string getAnAssetQuery = $"SELECT * FROM view_locatorforsearch WHERE bld_idx = '{_bldIdx}'";
                using (MySqlDataReader reader = exReader(getAnAssetQuery, conn))
                {
                    if (reader.Read())
                    {
                        result = new Asset_S2_Down(reader);
                    }
                }
            }

            return result;
        }

        public Int64 InsertAsset(Asset_S2 a)
        {
            Int64 result = 0;

            using (MySqlConnection conn = openCon())
            {
                string insertAssetQuery = $@"
                    INSERT INTO aa_dd_locator_bld
                        (
                            bld_type,
                            PLAT_PLC,
                            NEW_PLAT_PLC,
                            bld_name,
                            bld_fmly_cnt,
                            bld_memo,
                            bld_ipkey,
                            bld_roomkey,
                            bld_gwan,
                            bld_tel_owner,
                            bld_tel_gwan,
                            bld_map_x,
                            bld_map_y,
                            bld_on_wall,
                            bld_on_parked,
                            created,
                            work_requested
                        )
                    VALUES 
                        (
                            '{a.bld_type}', 
                            '{a.plat_plc}', 
                            '{a.new_plat_plc}', 
                            '{a.bld_name}', 
                            '{a.bld_fmly_cnt}', 
                            '{a.bld_memo}', 
                            '{a.bld_ipkey}', 
                            '{a.bld_roomkey}', 
                            '{a.bld_gwan}', 
                            '{a.bld_tel_owner}', 
                            '{a.bld_tel_gwan}', 
                            '{a.bld_map_x}', 
                            '{a.bld_map_y}', 
                            '{a.bld_on_wall}', 
                            '{a.bld_on_parked}', 
                            NOW(),
                            NOW()
                        );
                    SELECT MAX(bld_idx) bld_idx FROM view_locatorforsearch;
                ";
                using (MySqlDataReader reader = exReader(insertAssetQuery, conn))
                {
                    if (reader.Read())
                    {
                        result = Convert.ToInt64(reader["bld_idx"].ToString());
                    }
                }
            }
            return result;
        }

        public Int64 ModifyAsset(Asset_S2 a, string bldIdx)
        {
            Int64 result = 0;
            using (MySqlConnection conn = openCon())
            {
                string workRequested = a.work_requested == "true" ? "NOW()" : "''";
                string workRequest = a.work_requested == "true" ? "1" : "IF(work_request = 1, 3, NULL)";
                string fmlyCnt = (a.bld_fmly_cnt != null && a.bld_fmly_cnt.Trim().Length > 0) ? a.bld_fmly_cnt : "0";
                string modifyAssetQuery = $@"
                    UPDATE aa_dd_locator_bld
                        SET
                            bld_type = '{a.bld_type}',
                            bld_name = '{a.bld_name}',
                            bld_fmly_cnt = '{fmlyCnt}',
                            bld_memo = '{a.bld_memo}', 
                            bld_gwan = '{a.bld_gwan}',
                            bld_tel_owner = '{a.bld_tel_owner}', 
                            bld_tel_gwan = '{a.bld_tel_gwan}', 
                            bld_ipkey = '{a.bld_ipkey}',
                            bld_roomkey = '{a.bld_roomkey}',
                            bld_on_wall = '{a.bld_on_wall}',
                            bld_on_parked = '{a.bld_on_parked}',
                            visited = '{a.visited}',
                            factory_count = '{a.factory_count}',
                            modified = NOW(),
                            work_requested = {workRequested},
                            work_request = {workRequest}
                        WHERE bld_idx = '{bldIdx}'        
                ";
                System.Diagnostics.Debug.WriteLine(modifyAssetQuery);
                result = exNonQuery(modifyAssetQuery, conn);
            }
            return result;
        }

        public Int64 DeleteAsset(string bldIdx)
        {
            Int64 result = 0;

            using (MySqlConnection conn = openCon())
            {
                string deleteAssetQuery = $"DELETE FROM view_locatorforsearch WHERE bld_idx = '{bldIdx}'";
                result = exNonQuery(deleteAssetQuery, conn);
            }
            return result;
        }

    }
}