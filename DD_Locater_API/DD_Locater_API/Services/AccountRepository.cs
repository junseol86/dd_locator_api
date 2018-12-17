using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DD_Locater_API.Models;
using DD_Locater_API.Utils;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DD_Locater_API.Services
{
    public class AccountRepository: DBFuncs
    {
        public int adminLogin(string id, string pw)
        {
            int result = 0;

            using (MySqlConnection conn = openConOld())
            {
                string checkAccountQuery = $@"
                    SELECT COUNT(*) count FROM dd_user

                    WHERE USER_ID = '{id}'
                    AND USER_PWD = '{pw}'
                    AND USER_GROUP = 'ADMIN'
                    AND USER_LEVEL = 1
                    ";
                using (MySqlDataReader reader = exReader(checkAccountQuery, conn))
                {
                    if (reader.Read())
                    {
                        result = Convert.ToInt16(reader["count"].ToString());
                    }
                }
            }
            return result;
        }

    }
}