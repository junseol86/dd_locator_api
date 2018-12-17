using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DD_Locater_API.Models
{
    public class Trace
    {
        public Int64 trace_idx {get; set; }
        public string user_id {get; set;}
        public string longitude {get; set; }
        public string latitude {get; set; }
        public string datetime {get; set; }
    }
}