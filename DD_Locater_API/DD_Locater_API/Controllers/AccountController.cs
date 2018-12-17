using DD_Locater_API.Models;
using DD_Locater_API.Services;
using DD_Locater_API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DD_Locater_API.Controllers
{
    public class AccountController : CustomController
    {
        private AccountRepository accountRepository;

        public AccountController()
        {
            this.accountRepository = new AccountRepository();
        }

        [Route("api/account")]
        [HttpGet]
        public int login()
        {
            return accountRepository.adminLogin(getHdStr("id"), getHdStr("pw"));
        }
    }
}
