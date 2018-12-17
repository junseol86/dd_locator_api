using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DD_Locater_API.Models
{
    public class DongAndCount
    {
        public List<AssetDongV2> dong_list { get; set; }
        public Int64 total_count { get; set; }

        public DongAndCount(List<AssetDongV2> _dongList, Int64 _totalCount)
        {
            dong_list = _dongList;
            total_count = _totalCount;
        }
    }
}