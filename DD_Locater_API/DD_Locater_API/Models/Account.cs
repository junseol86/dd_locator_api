using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DD_Locater_API.Models
{

    public class Account
    {
        public string user_id { get; set; }
        public string user_pwd { get; set; }
        public string user_group { get; set; }
        public Int64 user_level { get; set; }

        public Account(string _userId, string _userPwd, string _userGroup, Int64 _userLevel)
        {
            user_id = _userId;
            user_pwd = _userPwd;
            user_group = _userGroup;
            user_level = _userLevel;
        }
    }
}