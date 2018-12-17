using DD_Locater_API.Models;
using DD_Locater_API.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DD_Locater_API.Services
{
    public class PhoneNumberRepository: DBFuncs
    {
        public List<PhoneNumberDown> getNumbers(string keyword)
        {
            List<PhoneNumberDown> result = new List<PhoneNumberDown>();

            using (MySqlConnection conn = openCon())
            {
                string getNumberQuery = $@"
                    SELECT * FROM aa_dd_locator_phonenum
                    WHERE REPLACE(pn_number, '-', '') LIKE '%{keyword}%';
                ";
                using (MySqlDataReader reader = exReader(getNumberQuery, conn))
                {
                    while (reader.Read())
                    {
                        result.Add(new PhoneNumberDown(
                            Convert.ToInt64(reader["pn_idx"].ToString()),
                            reader["pn_belong"].ToString(),
                            reader["pn_number"].ToString()
                            ));
                    }
                }
            }
            using (MySqlConnection conn = openCon())
            {
                string getNumberFromAssetQuery = $@"
                    SELECT * FROM view_locatorforsearch
                    WHERE REPLACE(bld_tel_owner, '-', '') LIKE '%{keyword}%'
                    OR REPLACE(bld_tel_gwan, '-', '') LIKE '%{keyword}%';
                ";
                using (MySqlDataReader reader = exReader(getNumberFromAssetQuery, conn))
                {
                    List<string> ownerTels = new List<string>();
                    List<string> gwanTels = new List<string>();
                    while (reader.Read())
                    {
                        if (reader["bld_tel_owner"].ToString().Replace("-", "").Contains(keyword) 
                            && !ownerTels.Contains(reader["bld_tel_owner"].ToString()))
                        {
                            ownerTels.Add(reader["bld_tel_owner"].ToString());
                            result.Add(new PhoneNumberDown(
                                0,
                            $"[주]{reader["bld_name"].ToString()}",
                            reader["bld_tel_owner"].ToString()
                                ));
                        }
                        if (reader["bld_tel_gwan"].ToString().Replace("-", "").Contains(keyword) 
                            && !gwanTels.Contains(reader["bld_tel_gwan"].ToString()))
                        {
                            gwanTels.Add(reader["bld_tel_gwan"].ToString());
                            result.Add(new PhoneNumberDown(
                                0,
                            $"[관]{reader["bld_gwan"].ToString()}",
                            reader["bld_tel_gwan"].ToString()
                                ));
                        }
                    }
                }
            }
            return result;
        }

        public Int64 InsertPhonenum(PhoneNumber pn)
        {
            Int64 result = 0;

            using (MySqlConnection conn = openCon())
            {
                string insertPhonenum = $@"
                    INSERT INTO aa_dd_locator_phonenum
                        (
                            pn_belong,
                            pn_number
                        )
                    VALUES
                        (
                            '{pn.pn_belong}',
                            '{pn.pn_number}'
                        );
                ";
                result = exScalar(insertPhonenum, conn);
            }

            return result;
        }

        public Int64 DeletePhonenum(Int64 pnIdx)
        {
            Int64 result = 0;

            using (MySqlConnection conn = openCon())
            {
                string deletePhonenum = $"DELETE FROM aa_dd_locator_phonenum WHERE pn_idx = {pnIdx}";
                result = exScalar(deletePhonenum, conn);
            }

            return result;
        }

        public void UploadPhoneNumberFromFile(string pnBelong, string pnNumber)
        {
            Int64 count = 0;
            using (MySqlConnection conn = openCon())
            {
                string countPhonenum = $@"
                    SELECT COUNT(*) count FROM aa_dd_locator_phonenum
                    WHERE REPLACE(pn_number, '-', '') LIKE '%{pnNumber.Trim().Replace("-", "")}%';
                    ";
                using (MySqlDataReader reader = exReader(countPhonenum, conn))
                {
                    if (reader.Read()) {
                        count = Convert.ToInt64(reader["count"].ToString());
                    }
                }
            }

            if (count == 0)
            {
                using (MySqlConnection conn = openCon())
                {
                    string insertPhonenum = $@"
                        INSERT INTO aa_dd_locator_phonenum
                        (pn_belong, pn_number)
                        VALUES
                        ('{pnBelong}', '{pnNumber.Trim()}');
                        ";
                    System.Diagnostics.Debug.WriteLine(insertPhonenum);
                    exScalar(insertPhonenum, conn);
                }
            }
            
        }

    }
}