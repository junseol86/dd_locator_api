using DD_Locater_API.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DD_Locater_API.Services
{
    public class MoveBuildingRepository: DBFuncs
    {
        public void MoveBuilding()
        {
            List<BuilInfo> list = new List<BuilInfo>();

            using (MySqlConnection conn = openCon())
            {
                string getListQuery =
                    $@"
                        SELECT base_idx, _NEW_PLAT_PLC, baselist_name FROM dd_locator_building
                        WHERE baselist_name NOT LIKE '%(자동입력)%'
                        AND TRIM(baselist_name) != ''
                        AND TRIM(_NEW_PLAT_PLC) != ''
                        ;
                    ";

                System.Diagnostics.Debug.WriteLine(getListQuery);


                using (MySqlDataReader reader = exReader(getListQuery, conn))
                {
                    while (reader.Read())
                    {
                        list.Add(new BuilInfo(
                            reader["base_idx"].ToString(),
                            reader["_NEW_PLAT_PLC"].ToString(),
                            reader["baselist_name"].ToString()
                            ));
                    }
                }

            }

            list.ForEach(delegate (BuilInfo b)
            {
                using (MySqlConnection conn = openCon())
                {
                    string getMatchedQuery =
                    $@"
                        UPDATE dd_locator_bld
                        SET
                            bld_name = '{b.bldName}'
                        WHERE
                            NEW_PLAT_PLC = '{b.newPlatPlc}';
                    ";
                    //System.Diagnostics.Debug.WriteLine(getMatchedQuery);
                    exNonQuery(getMatchedQuery, conn);
                }
            });


        }
    }

    public class BuilInfo
    {
        public string bldIdx;
        public string newPlatPlc;
        public string bldName;

        public BuilInfo(string _bldIdx, string _newPlatPlc, string _bldName)
        {
            bldIdx = _bldIdx;
            newPlatPlc = _newPlatPlc;
            bldName = _bldName;
        }
    }
}