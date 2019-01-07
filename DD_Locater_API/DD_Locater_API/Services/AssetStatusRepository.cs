using System;
using System.Collections.Generic;
using DD_Locater_API.Models;
using DD_Locater_API.Utils;
using MySql.Data.MySqlClient;

namespace DD_Locater_API.Services
{
    public class AssetStatusRepository : DBFuncs
    {
        public List<AstStatHsDown> GetAssetStatusHs(Int64 _bldIdx)
        {
            List<AstStatHsDown> result = new List<AstStatHsDown>();

            using (MySqlConnection conn = openCon())
            {
                string getAstStatHsQuery = QuerySetter.SetAstStatHsQuery(_bldIdx);
                System.Diagnostics.Debug.WriteLine(getAstStatHsQuery);
                using (MySqlDataReader reader = exReader(getAstStatHsQuery, conn))
                {
                    while (reader.Read())
                    {
                        result.Add(new AstStatHsDown(reader));
                    }
                }

            }
            return result;
        }

        public AstStatObDown GetAssetStatusOb(Int64 _bldIdx)
        {
            AstStatObDown result = null;

            using (MySqlConnection conn = openCon())
            {
                string getAstStatObQuery = QuerySetter.SetAstStatObQuery(_bldIdx);
                System.Diagnostics.Debug.WriteLine(getAstStatObQuery);
                using (MySqlDataReader reader = exReader(getAstStatObQuery, conn))
                {
                    if (reader.Read())
                    {
                        result = new AstStatObDown(reader);
                    }
                }

            }
            return result;
        }

        public Int64 updateResearchDate(String _ormBdIdx)
        {
            Int64 result = 0;
            using (MySqlConnection conn = openCon())
            {
                string updateQuery = $@"
                        UPDATE aa_orm_building
                            SET date_research = NOW(),
                                date_update = NOW()
                            WHERE orm_bd_idx = {_ormBdIdx};
                    ";
                result = exNonQuery(updateQuery, conn);
            }
            return result;
        }

        public Int64 modifyOb(AstStatOb ob)
        {
            Int64 result = 0;
            using (MySqlConnection conn = openCon())
            {
                string modifyAstHsQuery = $@"
                        UPDATE aa_orm_building
                            SET
                                memo = '{ob.memo}',
                                pwd_building = '{ob.pwd_building}',
                                picture_agree = '{ob.picture_agree}',
                                date_research = NOW(),
                                date_update = NOW()
                            WHERE orm_bd_idx = {ob.orm_bd_idx};
                    ";
                System.Diagnostics.Debug.WriteLine(modifyAstHsQuery);
                result = exNonQuery(modifyAstHsQuery, conn);
            }
            return result;
        }

        public Int64 modifyHs(AstStatHs hs)
        {
            Int64 result = 0;
            using (MySqlConnection conn = openCon())
            {
                string modifyAstHsQuery = $@"
                        UPDATE aa_orm_building_hosu
                            SET
                                kind_ad = '{hs.kind_ad}',
                                price_ad_w = '{hs.price_ad_w}',
                                price_ad_wbo = '{hs.price_ad_wbo}',
                                pwd = '{hs.pwd}',
                                pwd_open = '{hs.pwd_open}',
                                memo = '{hs.memo}',
                                memo_manager = '{hs.memo_manager}',
                                ad_yn = '{hs.ad_yn}',
                                person_tel_open = '{hs.person_tel_open}',
                                cond_blackout = '{hs.cond_blackout}',
                                cond_dirty = '{hs.cond_dirty}',
                                cond_wallpaper = '{hs.cond_wallpaper}'
                            WHERE hosu_idx = {hs.hosu_idx};
                    ";
                System.Diagnostics.Debug.WriteLine(modifyAstHsQuery);
                result = exNonQuery(modifyAstHsQuery, conn);
            }
            return result;
        }
    }
}
