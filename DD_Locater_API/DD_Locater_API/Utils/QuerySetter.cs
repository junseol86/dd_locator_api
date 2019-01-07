using System;
namespace DD_Locater_API.Utils
{
    public static class QuerySetter
    {
        public static string SetAssetsQuery(string condition, Int64 hasGongsil, Int64 hasHosuPic) 
        {
            return $@"

                SELECT
                  `nat`.`bd_nation_idx`    AS `bd_nation_idx`,
                  `nat`.`geo_lat`          AS `bld_map_y`,
                  `nat`.`geo_lng`          AS `bld_map_x`,
                  `nat`.`USEAPR_DAY`       AS `useapr_day`,
                  `nat`.`PLAT_PLC`         AS `plat_plc`,
                  `nat`.`NEW_PLAT_PLC`     AS `new_plat_plc`,
                  `nat`.`SIGUNGU_CD`       AS `sigungu_cd`,
                  `nat`.`BJDONG_CD`        AS `bjdong_cd`,
                  `nat`.`MAIN_PURPS_CD_NM` AS `main_purps_cd_nm`,
                  `nat`.`ETC_PURPS`        AS `etc_purps`,
                  `nat`.`GRND_FLR_CNT`     AS `grnd_flr_cnt`,
                  `nat`.`FMLY_CNT`         AS `fmly_cnt`,
                  `loc`.`bld_idx`          AS `bld_idx`,
                  `loc`.`bld_type`         AS `bld_type`,
                  `loc`.`bld_name`         AS `bld_name`,
                  `loc`.`bld_fmly_cnt`     AS `bld_fmly_cnt`,
                  `loc`.`bld_ipkey`        AS `bld_ipkey`,
                  `loc`.`bld_roomkey`      AS `bld_roomkey`,
                  `loc`.`bld_memo`         AS `bld_memo`,
                  `loc`.`bld_gwan`         AS `bld_gwan`,
                  `loc`.`bld_tel_gwan`     AS `bld_tel_gwan`,
                  `loc`.`bld_tel_owner`    AS `bld_tel_owner`,
                  `loc`.`bld_on_wall`      AS `bld_on_wall`,
                  `loc`.`bld_on_parked`    AS `bld_on_parked`,
                  `loc`.`created`          AS `created`,
                  `loc`.`modified`         AS `modified`,
                  `loc`.`work_requested`   AS `work_requested`,
                  `loc`.`work_request`     AS `work_request`,
                  `loc`.`photo`            AS `photo`,
                  `loc`.`factory_count`    AS `factory_count`,
                  `loc`.`visited`          AS `visited`,
                  `loc`.`do_cd`            AS `do_cd`,
                  `loc`.`dongri_name`      AS `dongri_name`,
                  `ob`.`picture_agree`      AS `picture_agree`,
                  `ob`.`manager_idx`        AS `manager_idx`,
                  `ob`.`date_research`        AS `date_research`,
                  `hp`.`hosu_count`          AS `hosu_count`
                {SetFrom(hasGongsil, hasHosuPic)}
                WHERE TRUE
                {OuterByGongsil(hasGongsil)}
                {condition}
                GROUP BY bd_nation_idx
                ORDER BY
                    `nat`.geo_lat, `loc`.modified ASC;
            ";
        }

        public static string SetDongsQuery(string condition, Int64 hasGongsil, Int64 hasHosuPic)
        {
            return $@"
                SELECT 
                    `loc`.`dongri_name`,
                    AVG(CAST(`nat`.`geo_lng` AS DECIMAL(11,7))) AS geo_lng, AVG(CAST(`nat`.`geo_lat` AS DECIMAL(11,7))) as geo_lat, 
                    COUNT(`nat`.`BJDONG_CD`) as asset_count
                {SetFrom(hasGongsil, hasHosuPic)}
                WHERE TRUE
                {OuterByGongsil(hasGongsil)}
                {condition}
                GROUP BY `loc`.`dongri_name`
                ORDER BY `nat`.`geo_lat`;
            ";
        }

        public static string SetFrom(Int64 hasGongsil, Int64 hasHosuPic) {
            return $@"
                FROM (`aa_aa_dd_meta_building_nation` `nat`
                   JOIN (`aa_dd_locator_bld` `loc`
                            LEFT JOIN `aa_aa_dd_meta_building` `mb`
                              ON (`loc`.`bd_nation_idx` = `mb`.`bd_nation_idx`)
                           LEFT JOIN `aa_orm_building` `ob`
                             ON (`mb`.`bd_gubun_deprecated` = 'ONEROOM'
                                  AND `mb`.`meta_bd_idx` = `ob`.`meta_bd_idx`)
                           LEFT JOIN (
                             SELECT orm_bd_idx, COUNT(*) AS hosu_count FROM `aa_orm_building_hosu` `h`
                              LEFT JOIN `aa_orm_building_hosu_picture` `p`
                              ON `h`.`hosu_idx` = `p`.`hosu_idx`
                                {InnerByGongsilAndHosuPic(hasGongsil, hasHosuPic)}
                                GROUP BY orm_bd_idx) `hp`
                             ON (`ob`.`orm_bd_idx` = `hp`.`orm_bd_idx`)

                           LEFT JOIN `aa_orm_manager` `mgr`
                             ON (`ob`.`manager_idx` = `mgr`.`manager_idx`)
                ) ON (`nat`.`bd_nation_idx` = `loc`.`bd_nation_idx`))
            ";
        }

        public static string InnerByGongsilAndHosuPic (Int64 hasGongsil, Int64 hasHosuPic) {
            string result = "";
            // 공실무관이 아니라면 공실인 방의 수가 필요함
            result += hasGongsil == -1 ? "" : " WHERE `h`.`ad_yn` = 1 ";
            if (hasGongsil == 1) {
                // 공실있음이고 사진무관이 아니라면 공실이면서 사진이 없는 방의 수가 필요함
                result += (hasHosuPic == -1 ? "" : " AND `p`.`meta_bd_pic_idx` IS " + (hasHosuPic == 1 ? " NOT " : "") + " NULL ");
            }
            return result;
        }

        public static string OuterByGongsil(Int64 hasGongsil)
        {
            string result = "";
            if (hasGongsil != -1) {
                result += " AND `hosu_count` " + (hasGongsil > 0 ? " > 0 " : " IS NULL ");
            }
            return result;
        }

        public static string SetAstStatObQuery(Int64 bld_idx)
        {
            return $@"
                        SELECT
                          `loc`.`bld_idx`          AS `bld_idx`,
                          `loc`.`bld_type`         AS `bld_type`,
                          `loc`.`bld_name`         AS `bld_name`,
                          `nat`.`PLAT_PLC`         AS `plat_plc`,
                          `nat`.`NEW_PLAT_PLC`     AS `new_plat_plc`,
                          `ob`.`orm_bd_idx`        AS `orm_bd_idx`,
                          `ob`.`host_mobile`       AS `host_mobile`,
                          `ob`.`host_mobile2`      AS `host_mobile2`,
                          `ob`.`memo`              AS `memo`,
                          `ob`.`pwd_building`      AS `pwd_building`,
                          `ob`.`picture_agree`     AS `picture_agree`,
                          `ob`.`date_research`     AS `date_research`,
                          COUNT(`bp`.`meta_bd_pic_idx`)            AS `bd_pic_count`
                        FROM (`aa_aa_dd_meta_building_nation` `nat`
                           JOIN (`aa_dd_locator_bld` `loc`
                                    LEFT JOIN `aa_aa_dd_meta_building` `mb`
                                      ON (`loc`.`bd_nation_idx` = `mb`.`bd_nation_idx`)
                                   LEFT JOIN `aa_orm_building` `ob`
                                     ON (`mb`.`bd_gubun_deprecated` = 'ONEROOM'
                                          AND `mb`.`meta_bd_idx` = `ob`.`meta_bd_idx`)
                        ) ON (`nat`.`bd_nation_idx` = `loc`.`bd_nation_idx`))
                          LEFT JOIN `aa_aa_dd_meta_building_picture` `bp`
                          ON (`mb`.`meta_bd_idx` = `bp`.`meta_bd_idx`)
                        WHERE bld_idx = {bld_idx}
                        GROUP BY `loc`.bld_idx;
                    ";
        }

        public static string SetAstStatHsQuery(Int64 bld_idx)
        {
            return $@"
                        SELECT
                            `h`.`hosu_idx`,
                            `h`.`ad_yn`,
                            `p`.`meta_bd_pic_idx`,
                            `h`.`hosu`,
                            `h`.`hosu_type`,
                            `h`.`floor`,
                            `h`.`kind_ad`,
                            `h`.`price_ad_w`,
                            `h`.`price_ad_wbo`,
                            `h`.`price_ad_j`,
                            `h`.`pwd`,
                            `h`.`pwd_open`,
                            `h`.`memo`,
                            `h`.`memo_manager`,
                            `h`.`cond_blackout`,
                            `h`.`cond_dirty`,
                            `h`.`cond_wallpaper`,
                            `h`.`person_tel_open`
                        FROM (`aa_aa_dd_meta_building_nation` `nat`
                           JOIN (`aa_dd_locator_bld` `loc`
                                    LEFT JOIN `aa_aa_dd_meta_building` `mb`
                                      ON (`loc`.`bd_nation_idx` = `mb`.`bd_nation_idx`)
                                   LEFT JOIN `aa_orm_building` `ob`
                                     ON (`mb`.`bd_gubun_deprecated` = 'ONEROOM'
                                          AND `mb`.`meta_bd_idx` = `ob`.`meta_bd_idx`)
                                    LEFT JOIN `aa_orm_building_hosu` `h`
                                      ON `ob`.`orm_bd_idx` = `h`.`orm_bd_idx`
                                      LEFT JOIN `aa_orm_building_hosu_picture` `p`
                                      ON `h`.`hosu_idx` = `p`.`hosu_idx`
                            ) ON (`nat`.`bd_nation_idx` = `loc`.`bd_nation_idx`))
                        WHERE bld_idx = {bld_idx}
                        GROUP BY `hosu_idx`;";
        }
    }
}
