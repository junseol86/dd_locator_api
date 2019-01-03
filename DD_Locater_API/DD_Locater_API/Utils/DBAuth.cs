using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DD_Locater_API.Utils
{
    public class DBAuth
    {
        public static string connectionString2014 = "server = 115.68.114.50; uid = kklocatorr; pwd = skekdavid; database = ddhouse2014_mig;";

        //배포용
        public static string connectionString2018 = "server = 115.68.114.50; uid = kklocatorr; pwd = skekdavid; database = ddhouse2018;";

        //테스트용
        //public static string connectionString2018 = "server = 115.68.114.46; uid = kkfaceddev; pwd = skekdavid; database = ddhouse2014_mig_dev;";
    }
}