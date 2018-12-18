using DD_Locater_API.Models;
using DD_Locater_API.Utils;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DD_Locater_API.Services
{
    public class BuildingRepository2 : DBFuncs
    {
        string addrDo;
        string addrCity;
        string addrDong;
        string addrEtc;
        string addrSan;
        string addrBun1;
        string addrBun2;

        Int64 inserted = 0;

        //public void InsertBuilding(string x, string y, JObject datum)
        //{

        //    using (MySqlConnection conn = openCon())
        //    {
        //        string insertAssetQuery = $@"
        //            INSERT INTO dd_locator_bld
        //                (
        //                    bld_type,

        //                    PLAT_PLC, SIGUNGU_CD, BJDONG_CD, PLAT_GB_CD, BUN, JI, 
        //                    MGM_BLDRGST_PK, 
        //                    REGSTR_GB_CD, REGSTR_GB_CD_NM, REGSTR_KIND_CD, REGSTR_KIND_CD_NM, 
        //                    NEW_PLAT_PLC, BLD_NM, SPLOT_NM, BLOCK_COL, LOT, BYLOT_CNT, 
        //                    NA_ROAD_CD, NA_BJDONG_CD, NA_UGRND_CD, NA_MAIN_BUN, NA_SUB_BUN, 
        //                    DONG_NM, 
        //                    MAIN_ATCH_GB_CD, MAIN_ATCH_GB_CD_NM, 
        //                    PLAT_AREA, ARCH_AREA, BC_RAT, TOTAREA, 
        //                    VL_RAT_ESTM_TOTAREA, VL_RAT, 
        //                    STRCT_CD, STRCT_CD_NM, ETC_STRCT, 
        //                    MAIN_PURPS_CD, main_purps_cd_nm, ETC_PURPS, 
        //                    ROOF_CD, ROOF_CD_NM, ETC_ROOF, 
        //                    HHLD_CNT, FMLY_CNT, HEIT, GRND_FLR_CNT, UGRND_FLR_CNT, 
        //                    RIDE_USE_ELVT_CNT, EMGEN_USE_ELVT_CNT, 
        //                    ATCH_BLD_CNT, ATCH_BLD_AREA, TOT_DONG_TOTAREA, 
        //                    INDR_MECH_AREA, INDR_MECH_UTCNT, 
        //                    OUDR_MECH_AREA, OUDR_MECH_UTCNT, 
        //                    INDR_AUTO_AREA, INDR_AUTO_UTCNT, 
        //                    OUDR_AUTO_AREA, OUDR_AUTO_UTCNT, 
        //                    PMS_DAY, STCNS_DAY, useapr_day, 
        //                    PMSNO_YEAR, PMSNO_KIK_CD, PMSNO_KIK_CD_NM, PMSNO_GB_CD, PMSNO_GB_CD_NM, 
        //                    HO_CNT, 
        //                    ENGR_GRADE, ENGR_RAT, ENGR_EPI, 
        //                    GN_BLD_GRADE, GN_BLD_CERT, 
        //                    ITG_BLD_GRADE, ITG_BLD_CERT, 
        //                    CRTN_DAY, 

        //                    bld_name,
        //                    bld_map_x,
        //                    bld_map_y,
        //                    created
        //                )
        //            VALUES 
        //                (
        //                    '', 
        //                    '{(string)datum["PLAT_PLC"]}', '{(string)datum["SIGUNGU_CD"]}', '{(string)datum["BJDONG_CD"]}', '{(string)datum["PLAT_GB_CD"]}', '{(string)datum["BUN"]}', '{(string)datum["JI"]}', '{(string)datum["MGM_BLDRGST_PK"]}', '{(string)datum["REGSTR_GB_CD"]}', '{(string)datum["REGSTR_GB_CD_NM"]}', '{(string)datum["REGSTR_KIND_CD"]}', '{(string)datum["REGSTR_KIND_CD_NM"]}', '{(string)datum["NEW_PLAT_PLC"]}', '{(string)datum["BLD_NM"]}', '{(string)datum["SPLOT_NM"]}', '{(string)datum["BLOCK"]}', '{(string)datum["LOT"]}', '{(string)datum["BYLOT_CNT"]}', '{(string)datum["NA_ROAD_CD"]}', '{(string)datum["NA_BJDONG_CD"]}', '{(string)datum["NA_UGRND_CD"]}', '{(string)datum["NA_MAIN_BUN"]}', '{(string)datum["NA_SUB_BUN"]}', '{(string)datum["DONG_NM"]}', '{(string)datum["MAIN_ATCH_GB_CD"]}', '{(string)datum["MAIN_ATCH_GB_CD_NM"]}', '{(string)datum["PLAT_AREA"]}', '{(string)datum["ARCH_AREA"]}', '{(string)datum["BC_RAT"]}', '{(string)datum["TOTAREA"]}', '{(string)datum["VL_RAT_ESTM_TOTAREA"]}', '{(string)datum["VL_RAT"]}', '{(string)datum["STRCT_CD"]}', '{(string)datum["STRCT_CD_NM"]}', '{(string)datum["ETC_STRCT"]}', '{(string)datum["MAIN_PURPS_CD"]}', '{(string)datum["main_purps_cd_nm"]}', '{(string)datum["ETC_PURPS"]}', '{(string)datum["ROOF_CD"]}', '{(string)datum["ROOF_CD_NM"]}', '{(string)datum["ETC_ROOF"]}', '{(string)datum["HHLD_CNT"]}', '{(string)datum["FMLY_CNT"]}', '{(string)datum["HEIT"]}', '{(string)datum["GRND_FLR_CNT"]}', '{(string)datum["UGRND_FLR_CNT"]}', '{(string)datum["RIDE_USE_ELVT_CNT"]}', '{(string)datum["EMGEN_USE_ELVT_CNT"]}', '{(string)datum["ATCH_BLD_CNT"]}', '{(string)datum["ATCH_BLD_AREA"]}', '{(string)datum["TOT_DONG_TOTAREA"]}', '{(string)datum["INDR_MECH_AREA"]}', '{(string)datum["INDR_MECH_UTCNT"]}', '{(string)datum["OUDR_MECH_AREA"]}', '{(string)datum["OUDR_MECH_UTCNT"]}', '{(string)datum["INDR_AUTO_AREA"]}', '{(string)datum["INDR_AUTO_UTCNT"]}', '{(string)datum["OUDR_AUTO_AREA"]}', '{(string)datum["OUDR_AUTO_UTCNT"]}', '{(string)datum["PMS_DAY"]}', '{(string)datum["STCNS_DAY"]}', '{(string)datum["useapr_day"]}', '{(string)datum["PMSNO_YEAR"]}', '{(string)datum["PMSNO_KIK_CD"]}', '{(string)datum["PMSNO_KIK_CD_NM"]}', '{(string)datum["PMSNO_GB_CD"]}', '{(string)datum["PMSNO_GB_CD_NM"]}', '{(string)datum["HO_CNT"]}', '{(string)datum["ENGR_GRADE"]}', '{(string)datum["ENGR_RAT"]}', '{(string)datum["ENGR_EPI"]}', '{(string)datum["GN_BLD_GRADE"]}', '{(string)datum["GN_BLD_CERT"]}', '{(string)datum["ITG_BLD_GRADE"]}', '{(string)datum["ITG_BLD_CERT"]}', '{(string)datum["CRTN_DAY"]}',

        //                    '(자동입력)', 
        //                    '{x}', '{y}',
        //                    NOW()
        //                );
        //        ";
        //        exReader(insertAssetQuery, conn);
        //    }

        //}


        public Int64 CheckAddrExist(string platPlc)
        {
            Int64 count = 0;
            using (MySqlConnection conn = openCon())
            {
                string countQuery = $"SELECT COUNT(*) count FROM dd_locator_bld WHERE PLAT_PLC = '{platPlc}'";
                using (MySqlDataReader reader = exReader(countQuery, conn))
                {
                    if (reader.Read())
                    {
                        count = Convert.ToInt64(reader["count"].ToString());
                    }
                }
            }
            //System.Diagnostics.Debug.WriteLine(count);
            return count;
        }

        public void InsertMissingBuilding(string x, string y, JObject datum)
        {

            using (MySqlConnection conn = openCon())
            {
                string stnsDay = datum["STCNS_DAY"] == null ? "" : (string)datum["STCNS_DAY"];
                string naBjdongCd = datum["NA_BJDONG_CD"] == null ? "" : (string)datum["NA_BJDONG_CD"];
                string naRoadCd = datum["NA_ROAD_CD"] == null ? "" : (string)datum["NA_ROAD_CD"];
                string naMainBun = datum["NA_MAIN_BUN"] == null ? "" : (string)datum["NA_MAIN_BUN"];
                string naSubBun = datum["NA_SUB_BUN"] == null ? "" : (string)datum["NA_SUB_BUN"];
                string dongNm = datum["DONG_NM"] == null ? "" : (string)datum["DONG_NM"];

                string insertAssetQuery = $@"
                    INSERT INTO dd_locator_bld
                        (
                            bld_type,

                            PLAT_PLC, SIGUNGU_CD, BJDONG_CD, PLAT_GB_CD, BUN, JI, 
                            MGM_BLDRGST_PK, 
                            REGSTR_GB_CD, REGSTR_GB_CD_NM, REGSTR_KIND_CD, REGSTR_KIND_CD_NM, 
                            NEW_PLAT_PLC, BLD_NM, SPLOT_NM, BLOCK_COL, LOT, BYLOT_CNT, 
                            NA_ROAD_CD, NA_BJDONG_CD, NA_UGRND_CD, NA_MAIN_BUN, NA_SUB_BUN, 
                            DONG_NM, 
                            MAIN_ATCH_GB_CD, MAIN_ATCH_GB_CD_NM, 
                            PLAT_AREA, ARCH_AREA, BC_RAT, TOTAREA, 
                            VL_RAT_ESTM_TOTAREA, VL_RAT, 
                            STRCT_CD, STRCT_CD_NM, ETC_STRCT, 
                            MAIN_PURPS_CD, main_purps_cd_nm, ETC_PURPS, 
                            ROOF_CD, ROOF_CD_NM, ETC_ROOF, 
                            HHLD_CNT, FMLY_CNT, HEIT, GRND_FLR_CNT, UGRND_FLR_CNT, 
                            RIDE_USE_ELVT_CNT, EMGEN_USE_ELVT_CNT, 
                            ATCH_BLD_CNT, ATCH_BLD_AREA, TOT_DONG_TOTAREA, 
                            INDR_MECH_AREA, INDR_MECH_UTCNT, 
                            OUDR_MECH_AREA, OUDR_MECH_UTCNT, 
                            INDR_AUTO_AREA, INDR_AUTO_UTCNT, 
                            OUDR_AUTO_AREA, OUDR_AUTO_UTCNT, 
                            PMS_DAY, STCNS_DAY, useapr_day, 
                            PMSNO_YEAR, PMSNO_KIK_CD, PMSNO_KIK_CD_NM, PMSNO_GB_CD, PMSNO_GB_CD_NM, 
                            HO_CNT, 
                            ENGR_GRADE, ENGR_RAT, ENGR_EPI, 
                            GN_BLD_GRADE, GN_BLD_CERT, 
                            ITG_BLD_GRADE, ITG_BLD_CERT, 
                            CRTN_DAY, 
                            
                            bld_name,
                            bld_map_x,
                            bld_map_y,
                            created
                        )
                    VALUES 
                        (
                            '', 
                            '{(string)datum["PLAT_PLC"]}', 
                            '{(string)datum["SIGUNGU_CD"]}', '{(string)datum["BJDONG_CD"]}', 
                            '{(string)datum["PLAT_GB_CD"]}', '{(string)datum["BUN"]}', '{(string)datum["JI"]}', 
                            '{(string)datum["MGM_BLDRGST_PK"]}', 
                            '{(string)datum["REGSTR_GB_CD"]}', '{(string)datum["REGSTR_GB_CD_NM"]}', '{(string)datum["REGSTR_KIND_CD"]}', '{(string)datum["REGSTR_KIND_CD_NM"]}', 
                            '{(string)datum["NEW_PLAT_PLC"]}', '{(string)datum["BLD_NM"]}', 
                            '{(string)datum["SPLOT_NM"]}', 
                            '{(string)datum["BLOCK"]}', '{(string)datum["LOT"]}', '{(string)datum["BYLOT_CNT"]}', 
                            '{naRoadCd}', '{naBjdongCd}', '{(string)datum["NA_UGRND_CD"]}', 
                            '{naMainBun}', '{naSubBun}', 
                            '{dongNm}', 
                            '{(string)datum["MAIN_ATCH_GB_CD"]}', '{(string)datum["MAIN_ATCH_GB_CD_NM"]}', 
                            '{(string)datum["PLAT_AREA"]}', '{(string)datum["ARCH_AREA"]}', 
                            '{(string)datum["BC_RAT"]}', '{(string)datum["TOTAREA"]}', 
                            '{(string)datum["VL_RAT_ESTM_TOTAREA"]}', '{(string)datum["VL_RAT"]}', 
                            '{(string)datum["STRCT_CD"]}', '{(string)datum["STRCT_CD_NM"]}', '{(string)datum["ETC_STRCT"]}', 
                            '{(string)datum["MAIN_PURPS_CD"]}', '{(string)datum["main_purps_cd_nm"]}', 
                            '{(string)datum["ETC_PURPS"]}', 
                            '{(string)datum["ROOF_CD"]}', '{(string)datum["ROOF_CD_NM"]}', '{(string)datum["ETC_ROOF"]}', 
                            '{(string)datum["HHLD_CNT"]}', '{(string)datum["FMLY_CNT"]}', 
                            '{(string)datum["HEIT"]}', 
                            '{(string)datum["GRND_FLR_CNT"]}', '{(string)datum["UGRND_FLR_CNT"]}', 
                            '{(string)datum["RIDE_USE_ELVT_CNT"]}', '{(string)datum["EMGEN_USE_ELVT_CNT"]}', 
                            '{(string)datum["ATCH_BLD_CNT"]}', '{(string)datum["ATCH_BLD_AREA"]}', 
                            '{(string)datum["TOT_DONG_TOTAREA"]}', 
                            '{(string)datum["INDR_MECH_AREA"]}', '{(string)datum["INDR_MECH_UTCNT"]}', '{(string)datum["OUDR_MECH_AREA"]}', '{(string)datum["OUDR_MECH_UTCNT"]}', 
                            '{(string)datum["INDR_AUTO_AREA"]}', '{(string)datum["INDR_AUTO_UTCNT"]}', '{(string)datum["OUDR_AUTO_AREA"]}', '{(string)datum["OUDR_AUTO_UTCNT"]}', 
                            '{(string)datum["PMS_DAY"]}', 
                            '{stnsDay}', '{(string)datum["useapr_day"]}', 
                            '{(string)datum["PMSNO_YEAR"]}', 
                            '{(string)datum["PMSNO_KIK_CD"]}', '{(string)datum["PMSNO_KIK_CD_NM"]}', 
                            '{(string)datum["PMSNO_GB_CD"]}', '{(string)datum["PMSNO_GB_CD_NM"]}', 
                            '{(string)datum["HO_CNT"]}', 
                            '{(string)datum["ENGR_GRADE"]}', '{(string)datum["ENGR_RAT"]}', '{(string)datum["ENGR_EPI"]}', 
                            '{(string)datum["GN_BLD_GRADE"]}', '{(string)datum["GN_BLD_CERT"]}', 
                            '{(string)datum["ITG_BLD_GRADE"]}', '{(string)datum["ITG_BLD_CERT"]}', '{(string)datum["CRTN_DAY"]}',

                            '(자동입력)', 
                            '{x}', '{y}',
                            NOW()
                        );
                ";
                //System.Diagnostics.Debug.Write(insertAssetQuery);
                //System.Diagnostics.Debug.WriteLine(++inserted);
                //System.Diagnostics.Debug.WriteLine(insertAssetQuery);
                exReader(insertAssetQuery, conn);
            }

        }


        public void InsertOnlyBuilding(JObject datum)
        {

            using (MySqlConnection conn = openCon())
            {
                string stnsDay = datum["STCNS_DAY"] == null ? "" : (string)datum["STCNS_DAY"];
                string naBjdongCd = datum["NA_BJDONG_CD"] == null ? "" : (string)datum["NA_BJDONG_CD"];
                string naRoadCd = datum["NA_ROAD_CD"] == null ? "" : (string)datum["NA_ROAD_CD"];
                string naMainBun = datum["NA_MAIN_BUN"] == null ? "" : (string)datum["NA_MAIN_BUN"];
                string naSubBun = datum["NA_SUB_BUN"] == null ? "" : (string)datum["NA_SUB_BUN"];
                string dongNm = datum["DONG_NM"] == null ? "" : (string)datum["DONG_NM"];

                string insertAssetQuery = $@"
                    INSERT INTO dd_locator_bld
                        (
                            bld_type,

                            PLAT_PLC, SIGUNGU_CD, BJDONG_CD, PLAT_GB_CD, BUN, JI, 
                            MGM_BLDRGST_PK, 
                            REGSTR_GB_CD, REGSTR_GB_CD_NM, REGSTR_KIND_CD, REGSTR_KIND_CD_NM, 
                            NEW_PLAT_PLC, BLD_NM, SPLOT_NM, BLOCK_COL, LOT, BYLOT_CNT, 
                            NA_ROAD_CD, NA_BJDONG_CD, NA_UGRND_CD, NA_MAIN_BUN, NA_SUB_BUN, 
                            DONG_NM, 
                            MAIN_ATCH_GB_CD, MAIN_ATCH_GB_CD_NM, 
                            PLAT_AREA, ARCH_AREA, BC_RAT, TOTAREA, 
                            VL_RAT_ESTM_TOTAREA, VL_RAT, 
                            STRCT_CD, STRCT_CD_NM, ETC_STRCT, 
                            MAIN_PURPS_CD, main_purps_cd_nm, ETC_PURPS, 
                            ROOF_CD, ROOF_CD_NM, ETC_ROOF, 
                            HHLD_CNT, FMLY_CNT, HEIT, GRND_FLR_CNT, UGRND_FLR_CNT, 
                            RIDE_USE_ELVT_CNT, EMGEN_USE_ELVT_CNT, 
                            ATCH_BLD_CNT, ATCH_BLD_AREA, TOT_DONG_TOTAREA, 
                            INDR_MECH_AREA, INDR_MECH_UTCNT, 
                            OUDR_MECH_AREA, OUDR_MECH_UTCNT, 
                            INDR_AUTO_AREA, INDR_AUTO_UTCNT, 
                            OUDR_AUTO_AREA, OUDR_AUTO_UTCNT, 
                            PMS_DAY, STCNS_DAY, useapr_day, 
                            PMSNO_YEAR, PMSNO_KIK_CD, PMSNO_KIK_CD_NM, PMSNO_GB_CD, PMSNO_GB_CD_NM, 
                            HO_CNT, 
                            ENGR_GRADE, ENGR_RAT, ENGR_EPI, 
                            GN_BLD_GRADE, GN_BLD_CERT, 
                            ITG_BLD_GRADE, ITG_BLD_CERT, 
                            CRTN_DAY, 
                            
                            bld_name,
                            created
                        )
                    VALUES 
                        (
                            '', 
                            '{(string)datum["PLAT_PLC"]}', 
                            '{(string)datum["SIGUNGU_CD"]}', '{(string)datum["BJDONG_CD"]}', 
                            '{(string)datum["PLAT_GB_CD"]}', '{(string)datum["BUN"]}', '{(string)datum["JI"]}', 
                            '{(string)datum["MGM_BLDRGST_PK"]}', 
                            '{(string)datum["REGSTR_GB_CD"]}', '{(string)datum["REGSTR_GB_CD_NM"]}', '{(string)datum["REGSTR_KIND_CD"]}', '{(string)datum["REGSTR_KIND_CD_NM"]}', 
                            '{(string)datum["NEW_PLAT_PLC"]}', '{(string)datum["BLD_NM"]}', 
                            '{(string)datum["SPLOT_NM"]}', 
                            '{(string)datum["BLOCK"]}', '{(string)datum["LOT"]}', '{(string)datum["BYLOT_CNT"]}', 
                            '{naRoadCd}', '{naBjdongCd}', '{(string)datum["NA_UGRND_CD"]}', 
                            '{naMainBun}', '{naSubBun}', 
                            '{dongNm}', 
                            '{(string)datum["MAIN_ATCH_GB_CD"]}', '{(string)datum["MAIN_ATCH_GB_CD_NM"]}', 
                            '{(string)datum["PLAT_AREA"]}', '{(string)datum["ARCH_AREA"]}', 
                            '{(string)datum["BC_RAT"]}', '{(string)datum["TOTAREA"]}', 
                            '{(string)datum["VL_RAT_ESTM_TOTAREA"]}', '{(string)datum["VL_RAT"]}', 
                            '{(string)datum["STRCT_CD"]}', '{(string)datum["STRCT_CD_NM"]}', '{(string)datum["ETC_STRCT"]}', 
                            '{(string)datum["MAIN_PURPS_CD"]}', '{(string)datum["main_purps_cd_nm"]}', 
                            '{(string)datum["ETC_PURPS"]}', 
                            '{(string)datum["ROOF_CD"]}', '{(string)datum["ROOF_CD_NM"]}', '{(string)datum["ETC_ROOF"]}', 
                            '{(string)datum["HHLD_CNT"]}', '{(string)datum["FMLY_CNT"]}', 
                            '{(string)datum["HEIT"]}', 
                            '{(string)datum["GRND_FLR_CNT"]}', '{(string)datum["UGRND_FLR_CNT"]}', 
                            '{(string)datum["RIDE_USE_ELVT_CNT"]}', '{(string)datum["EMGEN_USE_ELVT_CNT"]}', 
                            '{(string)datum["ATCH_BLD_CNT"]}', '{(string)datum["ATCH_BLD_AREA"]}', 
                            '{(string)datum["TOT_DONG_TOTAREA"]}', 
                            '{(string)datum["INDR_MECH_AREA"]}', '{(string)datum["INDR_MECH_UTCNT"]}', '{(string)datum["OUDR_MECH_AREA"]}', '{(string)datum["OUDR_MECH_UTCNT"]}', 
                            '{(string)datum["INDR_AUTO_AREA"]}', '{(string)datum["INDR_AUTO_UTCNT"]}', '{(string)datum["OUDR_AUTO_AREA"]}', '{(string)datum["OUDR_AUTO_UTCNT"]}', 
                            '{(string)datum["PMS_DAY"]}', 
                            '{stnsDay}', '{(string)datum["useapr_day"]}', 
                            '{(string)datum["PMSNO_YEAR"]}', 
                            '{(string)datum["PMSNO_KIK_CD"]}', '{(string)datum["PMSNO_KIK_CD_NM"]}', 
                            '{(string)datum["PMSNO_GB_CD"]}', '{(string)datum["PMSNO_GB_CD_NM"]}', 
                            '{(string)datum["HO_CNT"]}', 
                            '{(string)datum["ENGR_GRADE"]}', '{(string)datum["ENGR_RAT"]}', '{(string)datum["ENGR_EPI"]}', 
                            '{(string)datum["GN_BLD_GRADE"]}', '{(string)datum["GN_BLD_CERT"]}', 
                            '{(string)datum["ITG_BLD_GRADE"]}', '{(string)datum["ITG_BLD_CERT"]}', '{(string)datum["CRTN_DAY"]}',
                            '(자동입력)', 
                            NOW()
                        );
                ";
                //System.Diagnostics.Debug.Write(insertAssetQuery);
                //System.Diagnostics.Debug.WriteLine(++inserted);
                //System.Diagnostics.Debug.WriteLine(insertAssetQuery);
                exReader(insertAssetQuery, conn);
            }

        }

        public List<string[]> selectBldsMissingCoords ()
        {
            List<string[]> result = new List<string[]>();

            using (MySqlConnection conn = openCon())
            {
                string select100 = "SELECT bld_idx, PLAT_PLC  FROM dd_locator_bld WHERE bld_map_x IS NULL LIMIT 100";

                using (MySqlDataReader reader = exReader(select100, conn))
                {
                    while (reader.Read())
                    {
                        string[] item = new string[2];
                        item[0] = reader["bld_idx"].ToString();
                        item[1] = reader["PLAT_PLC"].ToString();
                        result.Add(item);
                    }
                }
            }
            return result;
        }

        public void fillCoords (string idx, string x, string y)
        {
            using (MySqlConnection conn = openCon())
            {
                string update = $"UPDATE dd_locator_bld SET bld_map_x = '{x}', bld_map_y = '{y}' WHERE bld_idx = '{idx}'";
                //System.Diagnostics.Debug.WriteLine(update);
                
                exNonQuery(update, conn);
            }
        }

        public void InsertFactory(string[] cols)
        {

            using (MySqlConnection conn = openCon())
            {

                string insertAssetQuery = $@"
                    INSERT INTO dd_locator_factory
                        (
                            company, institute, danji, found_code_nm, yongji_area, geonchuk_area, employees, scale_code_nm,
                            regist_date, yongdo_region, jimok, upjong, product, air_pollution, water_pollution, noise,
                            life_water_usage, ind_water_usage, self_capital, other_capital, phone, website,
                            zip_code, addr_jibun, addr_doro, latitude, longitude,
                            created
                        ) VALUES (
                            '{cols[0]}', '{cols[1]}', '{cols[2]}', '{cols[3]}', '{cols[4]}', '{cols[5]}', '{cols[6]}', '{cols[7]}', '{cols[8]}', 
                            '{cols[9]}', '{cols[10]}', '{cols[11]}', '{cols[12]}', '{cols[13]}', '{cols[14]}', '{cols[15]}', '{cols[16]}', '{cols[17]}', 
                            '{cols[18]}', '{cols[19]}', '{cols[20]}', '{cols[21]}', '{cols[22]}', '{cols[23]}', '{cols[24]}', '{cols[25]}', '{cols[26]}',
                            NOW()
                        )
                    ";
                exReader(insertAssetQuery, conn);
            }
        }

        public List<Asset_S2> SelectFactories(Int64 page)
        {
            List<Asset_S2> result = new List<Asset_S2>();

            using (MySqlConnection conn = openCon())
            {
                string selectFactory = $@"SELECT * FROM dd_locator_bld 
                                        WHERE PLAT_PLC LIKE '%경기도%' AND bld_type = 'ftr' AND has_factory_tel > 0 
                                        LIMIT 100 OFFSET {page * 100}";
                using (MySqlDataReader reader = exReader(selectFactory, conn))
                {
                    while(reader.Read())
                    {
                        result.Add(new Asset_S2_Down(reader));
                    }
                }
            }
            return result;
        }

        public List<Factory> SelectMatchedFactories(Asset_S2 asset)
        {
            List<Factory> result = new List<Factory>();

            using (MySqlConnection conn = openCon())
            {
                string selectFactory = $"SELECT * FROM dd_locator_factory WHERE addr_jibun LIKE '%{asset.plat_plc}%'";
                using (MySqlDataReader reader = exReader(selectFactory, conn))
                {
                    while(reader.Read())
                    {
                        result.Add(new Factory_Down(reader));
                    }
                }
            }
            return result;

        }

        public void ApplyFactoryMatch(Int64 bldIdx, List<Factory> list)
        {
            if (list.Count > 0)
            {
                string phone = "";
                foreach (Factory ftr in list)
                {
                    if (ftr.phone.Length > 0 && phone.Length == 0)
                    {
                        phone += $"{ftr.company}: {ftr.phone}";                        
                    }
                }
                if (phone.Length > 0 && list.Count > 1)
                {
                    phone += $" 외 {list.Count - 1}";
                }

                try
                {
                    using (MySqlConnection conn = openCon())
                    {
                        string updateQuery = $@"
                                                UPDATE dd_locator_bld
                                                SET 
                                                    bld_on_parked = '{phone}'
                                                WHERE bld_idx = {bldIdx}";
                        exNonQuery(updateQuery, conn);
                    }
                    
                } catch { }

            }
        }

        public void NotFactories(int which, string purps)
        {
            if (purps.Length < 3) return;
            using (MySqlConnection conn = openCon())
            {

                string updateQuery = $@"
                                        UPDATE dd_locator_bld
                                        SET bld_type = 'su'
                                        WHERE 
                                            bld_type = 'ftr2'
                                            AND main_purps_cd_nm LIKE '%제{which}종근린생활시설%'
                                            AND ETC_PURPS LIKE '{purps}%'
                                    ";
                exNonQuery(updateQuery, conn);
            }
        }

        public void NotNotFactories(int which, string purps)
        {
            if (purps.Length < 3) return;
            using (MySqlConnection conn = openCon())
            {
                string updateQuery = $@"
                                        UPDATE dd_locator_bld
                                        SET bld_type = 'ftr2'
                                        WHERE 
                                            bld_type = 'su'
                                            AND main_purps_cd_nm LIKE '%제{which}종근린생활시설%'
                                            AND ETC_PURPS LIKE '%{purps}%'
                                    ";
                exNonQuery(updateQuery, conn);
            }
        }

        public string[] selectSggBjdWithoutDongriName()
        {
            string[] result = {"", "", ""};
            using (MySqlConnection conn = openCon())
            {
                string selectQuery = @"
                            SELECT SIGUNGU_CD, BJDONG_CD, PLAT_PLC
                            FROM dd_locator_bld
                            WHERE dongri_name = ''
                            ORDER BY RAND()
                            LIMIT 1;
                        ";

                using (MySqlDataReader reader = exReader(selectQuery, conn))
                {
                    while (reader.Read())
                    {
                        result[0] = reader["SIGUNGU_CD"].ToString();
                        result[1] = reader["BJDONG_CD"].ToString();
                        foreach (string piece in reader["PLAT_PLC"].ToString().Split(' '))
                        {
                            if (piece[piece.Length - 1] == '리')
                            {
                                result[2] = piece;
                            }
                        }
                        if (result[2].Length == 0)
                        {
                            foreach (string piece in reader["PLAT_PLC"].ToString().Split(' '))
                            {
                                if ("읍면동가".Contains(piece[piece.Length - 1]))
                                {
                                    result[2] = piece;
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        public void saveDongName(string sgg, string bjd, string dongRiName)
        {
            using (MySqlConnection conn = openCon())
            {
                string updateQuery = $@"
                    UPDATE dd_locator_bld
                    SET dongri_name = '{dongRiName}'
                    WHERE SIGUNGU_CD = '{sgg}' AND BJDONG_CD = '{bjd}';
                ";
                exReader(updateQuery, conn);
            }
        }

    }
}