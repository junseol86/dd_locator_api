using DD_Locater_API.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DD_Locater_API.Services
{
    public class UpDownloadRepository: DBFuncs
    {
        public string InsertPhotoData(string bldIdx, string fileName)
        {
            string result = "";

            using (MySqlConnection conn = openCon())
            {
                string insertQuery = $@"
                    UPDATE aa_dd_locator_bld
                    SET
                        photo = '{fileName}'
                    WHERE bld_idx = '{bldIdx}';

                    SELECT photo FROM aa_dd_locator_bld
                    WHERE bld_idx = '{bldIdx}';
                ";

                using (MySqlDataReader reader = exReader(insertQuery, conn))
                {
                    if (reader.Read())
                    {
                        result = reader["photo"].ToString();
                    }
                }
            }

            return result;
        }

        public string deletePhotoData(string bldIdx)
        {
            string result = "";

            using (MySqlConnection conn = openCon())
            {
                string deleteQuery = $@"
                    UPDATE aa_dd_locator_bld
                    SET
                        photo = ''
                    WHERE bld_idx = '{bldIdx}';

                    SELECT photo FROM aa_dd_locator_bld
                    WHERE bld_idx = '{bldIdx}';
                ";

                using (MySqlDataReader reader = exReader(deleteQuery, conn))
                {
                    if (reader.Read())
                    {
                        result = reader["photo"].ToString();
                    }
                }
            }
            return result;
        }
    }
}