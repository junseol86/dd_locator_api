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
    public class TraceController : CustomController
    {
        private TraceRepository traceRepository;
        public TraceController()
        {
            traceRepository = new TraceRepository();
        }

        [Route("api/trace")]
        [HttpGet]
        public List<TraceDown> GetTraces()
        {
            return traceRepository.GetTraces(getHdDbl("left"), getHdDbl("right"), getHdDbl("top"), getHdDbl("bottom"), getHdStr("date_from"), getHdStr("date_to"));
        }

        [Route("api/trace/single")]
        [HttpGet]
        public TraceDown GetTrace()
        {
            return traceRepository.GetTrace(getHdInt("trace_idx"));
        }

        [Route("api/trace/insert")]
        [HttpPost]
        public Int64 InsertTrace([FromBody] Trace trace)
        {
            return traceRepository.InsertTrace(trace);
        }
    }
}
