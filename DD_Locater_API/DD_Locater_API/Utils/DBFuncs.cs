using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DD_Locater_API.Utils
{
    public class DBFuncs
    {
        public MySqlConnection openConOld()
        {
            return new MySqlConnection(DBAuth.connectionString2014);
        }
        public MySqlConnection openCon()
        {
            return new MySqlConnection(DBAuth.connectionString2018);
        }

        public MySqlDataReader exReader(string query, MySqlConnection conn)
        {
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Connection.Open();
            return command.ExecuteReader();
        }

        public Int64 exScalar(string query, MySqlConnection conn)
        {
            Int64 firstRowFirstCol = -1;
            try
            {
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Connection.Open();
                firstRowFirstCol = Convert.ToInt64(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return firstRowFirstCol;
        }

        public Int64 exNonQuery(string query, MySqlConnection conn)
        {
            Int64 rowsAffected = -1;
            try
            {
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Connection.Open();
                rowsAffected = (Int64)command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return rowsAffected;
        }
    }
}