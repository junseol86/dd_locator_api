using DD_Locater_API.Models;
using DD_Locater_API.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DD_Locater_API.Services
{
    public class AssetRepositoryV2: DBFuncs
    {

        public AssetAndCount AssetsInBound4(string bld_type, double left, double right, double top, double bottom, 
            Int64 hasName, Int64 hasNumber, Int64 hasGwan, Int64 fmlyMin, Int64 fmlyMax, string mainPurps, string useaprDay)
        {

            List<AssetMarkV2> result = new List<AssetMarkV2>();
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

            condition += fmlyMin >= 0 ? $" AND bld_fmly_cnt >= {fmlyMin} " : "";
            condition += fmlyMax >= 0 ? $" AND bld_fmly_cnt <= {fmlyMax} " : "";
            condition += $" AND main_purps_cd_nm LIKE '%{System.Web.HttpUtility.UrlDecode(mainPurps)}%'";
            condition += useaprDay.Trim() != "" ? $" AND useapr_day >= '{useaprDay}'" : "";

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
                string getAssetListQuery =
                    $@"
                        SELECT
                            bld_idx, 
                            bld_type, 
                            bld_name, 
                            bld_map_x, bld_map_y, 
                            bld_ipkey, bld_roomkey, 
                            bld_tel_owner,
                            work_requested
                        FROM 
                            dd_locator_bld
                        WHERE
                            bld_type LIKE '%{bld_type}%'
                            AND bld_map_x > '{left}'
                            AND bld_map_x < '{right}'
                            AND bld_map_y < '{top}'
                            AND bld_map_y > '{bottom}'
                            {condition}
                        ORDER BY
                            bld_map_y
                    ";

                using (MySqlDataReader reader = exReader(getAssetListQuery, conn))
                {
                    while(reader.Read())
                    {
                        result.Add(new AssetMarkV2(
                            Convert.ToInt64(reader["bld_idx"].ToString()),
                            reader["bld_type"].ToString(),
                            reader["bld_map_x"].ToString(),
                            reader["bld_map_y"].ToString(),
                            reader["bld_name"].ToString(),
                            reader["bld_ipkey"].ToString(),
                            reader["bld_roomkey"].ToString(),
                            reader["bld_tel_owner"].ToString(),
                            reader["work_requested"].ToString()
                            ));
                    }
                }
            }
            return new AssetAndCount(result, count);
        }

        public List<AssetV2Down> AssetsRequested2(string bld_type, 
            Int64 hasName, Int64 hasNumber, Int64 hasGwan, Int64 fmlyMin, Int64 fmlyMax, string mainPurps, string useaprDay, string bldName, string bldMemo)
        {

            List<AssetV2Down> result = new List<AssetV2Down>();
            using (MySqlConnection conn = openCon())
            {

                string condition = "";

                condition += $" AND bld_type LIKE '%{bld_type}%'";
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

                condition += fmlyMin >= 0 ? $" AND bld_fmly_cnt >= {fmlyMin} " : "";
                condition += fmlyMax >= 0 ? $" AND bld_fmly_cnt <= {fmlyMax} " : "";
                condition += $" AND main_purps_cd_nm LIKE '%{System.Web.HttpUtility.UrlDecode(mainPurps)}%'";
                condition += useaprDay.Trim() != "" ? $" AND useapr_day >= '{useaprDay}'" : "";

                string getAssetListQuery =
                    $@"
                        SELECT *
                        FROM 
                            dd_locator_bld
                        WHERE
                            work_requested != ''
                            AND (bld_name LIKE '%{bldName}%' OR PLAT_PLC LIKE '%{bldName}%')
                            AND bld_memo LIKE '%{bldMemo}%'
                            {condition}
                        ORDER BY
                            work_requested DESC
                    ";

                using (MySqlDataReader reader = exReader(getAssetListQuery, conn))
                {
                    while(reader.Read())
                    {
                        result.Add(new AssetV2Down(reader));
                    }
                }
            }
            return result;
        }

        public List<AssetV2Down> AssetsSearched2(string bld_type, 
            Int64 hasName, Int64 hasNumber, Int64 hasGwan, Int64 fmlyMin, Int64 fmlyMax, string mainPurps, string useaprDay, string bldName, string bldMemo)
        {

            List<AssetV2Down> result = new List<AssetV2Down>();
            using (MySqlConnection conn = openCon())
            {
                string condition = "";

                condition += $" AND bld_type LIKE '%{bld_type}%'";
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

                condition += fmlyMin >= 0 ? $" AND bld_fmly_cnt >= {fmlyMin} " : "";
                condition += fmlyMax >= 0 ? $" AND bld_fmly_cnt <= {fmlyMax} " : "";
                condition += $" AND main_purps_cd_nm LIKE '%{System.Web.HttpUtility.UrlDecode(mainPurps)}%'";
                condition += useaprDay.Trim() != "" ? $" AND useapr_day >= '{useaprDay}'" : "";

                string getAssetListQuery =
                    $@"
                        SELECT *
                        FROM 
                            dd_locator_bld
                        WHERE
                            (bld_name LIKE '%{bldName}%' OR PLAT_PLC LIKE '%{bldName}%')
                            AND bld_memo LIKE '%{bldMemo}%'
                            {condition}
                    ";

                using (MySqlDataReader reader = exReader(getAssetListQuery, conn))
                {
                    while(reader.Read())
                    {
                        result.Add(new AssetV2Down(reader));
                    }
                }
            }
            return result;
        }

        public AssetV2Down GetAnAsset(Int64 _bldIdx)
        {
            AssetV2Down result = new AssetV2Down();

            using (MySqlConnection conn = openCon())
            {
                string getAnAssetQuery = $"SELECT * FROM dd_locator_bld WHERE bld_idx = '{_bldIdx}'";
                using (MySqlDataReader reader = exReader(getAnAssetQuery, conn))
                {
                    if (reader.Read())
                    {
                        result = new AssetV2Down(reader);
                    }
                }
            }

            return result;
        }

        public Int64 InsertAssetV2(AssetV2 a)
        {
            Int64 result = 0;

            using (MySqlConnection conn = openCon())
            {
                string insertAssetQuery = $@"
                    INSERT INTO dd_locator_bld
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
                    SELECT MAX(bld_idx) bld_idx FROM dd_locator_bld;
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

        public Int64 ModifyAsset(AssetV2 a, string bldIdx)
        {
            Int64 result = 0;
            using (MySqlConnection conn = openCon())
            {
                string workRequested = a.work_requested == "true" ? "NOW()" : "''";
                string modifyAssetQuery = $@"
                    UPDATE dd_locator_bld
                        SET
                            bld_type = '{a.bld_type}',
                            bld_name = '{a.bld_name}',
                            bld_memo = '{a.bld_memo}', 
                            bld_gwan = '{a.bld_gwan}',
                            bld_tel_owner = '{a.bld_tel_owner}', 
                            bld_tel_gwan = '{a.bld_tel_gwan}', 
                            bld_ipkey = '{a.bld_ipkey}',
                            bld_roomkey = '{a.bld_roomkey}',
                            bld_on_wall = '{a.bld_on_wall}',
                            bld_on_parked = '{a.bld_on_parked}',
                            modified = NOW(),
                            work_requested = {workRequested}
                        WHERE bld_idx = '{bldIdx}'        
                ";
                result = exNonQuery(modifyAssetQuery, conn);
            }
            return result;
        }

        public Int64 ModifyAsset2(AssetV2 a, string bldIdx)
        {
            Int64 result = 0;
            using (MySqlConnection conn = openCon())
            {
                string workRequested = a.work_requested == "true" ? "NOW()" : "''";
                string modifyAssetQuery = $@"
                    UPDATE dd_locator_bld
                        SET
                            bld_type = '{a.bld_type}',
                            bld_name = '{a.bld_name}',
                            bld_fmly_cnt = '{a.bld_fmly_cnt}',
                            bld_memo = '{a.bld_memo}', 
                            bld_gwan = '{a.bld_gwan}',
                            bld_tel_owner = '{a.bld_tel_owner}', 
                            bld_tel_gwan = '{a.bld_tel_gwan}', 
                            bld_ipkey = '{a.bld_ipkey}',
                            bld_roomkey = '{a.bld_roomkey}',
                            bld_on_wall = '{a.bld_on_wall}',
                            bld_on_parked = '{a.bld_on_parked}',
                            modified = NOW(),
                            work_requested = {workRequested}
                        WHERE bld_idx = '{bldIdx}'        
                ";
                result = exNonQuery(modifyAssetQuery, conn);
            }
            return result;
        }

        public Int64 DeleteAssetV2(string bldIdx)
        {
            Int64 result = 0;

            using (MySqlConnection conn = openCon())
            {
                string deleteAssetQuery = $"DELETE FROM dd_locator_bld WHERE bld_idx = '{bldIdx}'";
                result = exNonQuery(deleteAssetQuery, conn);
            }
            return result;
        }

    }
}