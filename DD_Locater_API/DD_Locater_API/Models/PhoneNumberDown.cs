using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DD_Locater_API.Models
{
    public class PhoneNumberDown: PhoneNumber
    {
        public PhoneNumberDown(Int64 _pnIdx, string _pnBelong, string _pnNumber)
        {
            pn_idx = _pnIdx;
            pn_belong = _pnBelong;
            pn_number = _pnNumber;
        }
    }
}