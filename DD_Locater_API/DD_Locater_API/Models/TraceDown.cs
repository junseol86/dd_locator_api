using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DD_Locater_API.Models
{
    public class TraceDown: Trace
    {
        public TraceDown()
        {}

        public TraceDown(
            Int64 _traceIdx, string _userId, string _longitude, string _latitude, string _datetime
            )
        {
            trace_idx = _traceIdx;
            user_id = _userId;
            longitude = _longitude;
            latitude = _latitude;
            datetime = _datetime;
        }
    }
}