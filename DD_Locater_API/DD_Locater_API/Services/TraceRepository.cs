using DD_Locater_API.Models;
using DD_Locater_API.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DD_Locater_API.Services
{
    public class TraceRepository: DBFuncs
    {
        public Int64 InsertTrace(Trace trace)
        {
            Int64 result = 0;

            using (MySqlConnection conn = openCon())
            {
                string insertTraceQuery = $@"
                    INSERT INTO aa_dd_locator_trace (user_id, longitude, latitude, datetime)
                    VALUES ('{trace.user_id}', '{trace.longitude}', '{trace.latitude}', NOW());
                    SELECT MAX(trace_idx) trace_idx FROM dd_locator_trace;
                ";
                using (MySqlDataReader reader = exReader(insertTraceQuery, conn))
                {
                    if (reader.Read())
                    {
                        result = Convert.ToInt64(reader["trace_idx"].ToString());
                    }
                }
            }
            return result;
        }

        public TraceDown GetTrace(Int64 traceIdx)
        {
            TraceDown result = new TraceDown();

            using (MySqlConnection conn = openCon())
            {
                string getTraceQuery = $@"
                    SELECT * FROM aa_dd_locator_trace
                    WHERE trace_idx = {traceIdx}
                ";
                using (MySqlDataReader reader = exReader(getTraceQuery, conn))
                {
                    if (reader.Read())
                    {
                        result = new TraceDown(
                            Convert.ToInt64(reader["trace_idx"].ToString()),
                            reader["user_id"].ToString(),
                            reader["longitude"].ToString(),
                            reader["latitude"].ToString(),
                            reader["datetime"].ToString()
                        );
                    }
                }
            }

            return result;
        }

        public List<TraceDown> GetTraces(double left, double right, double top, double bottom, string dateFrom, string dateTo)
        {
            int max = 800;
            Int64 count = 0;
            List<TraceDown> result = new List<TraceDown>();

            using (MySqlConnection conn = openCon())
            {
                string countTracesQuery = $@"
                    SELECT COUNT(*) AS count FROM aa_dd_locator_trace
                    WHERE
                        longitude > '{left}' AND longitude < '{right}'
                        AND latitude < '{top}' AND latitude > '{bottom}'
                        AND datetime > '{ dateFrom }' AND datetime < '{ dateTo }'
                ";
                using (MySqlDataReader reader = exReader(countTracesQuery, conn))
                {
                    if (reader.Read())
                    {
                        count = Convert.ToInt64(reader["count"].ToString());
                    }
                }
            }
            using (MySqlConnection conn = openCon())
            {
                if (count > 0)
                {
                    Int64 skip = count <= max ? 1 : count / max;
                    string getTracesQuery = $@"
                        SELECT * FROM aa_dd_locator_trace
                        WHERE
                            trace_idx % {skip} = 0
                            AND longitude > '{left}' AND longitude < '{right}'
                            AND latitude < '{top}' AND latitude > '{bottom}'
                            AND datetime > '{ dateFrom }' AND datetime < '{ dateTo }'
                            ORDER BY user_id, trace_idx ASC
                    ";
                    using (MySqlDataReader reader = exReader(getTracesQuery, conn))
                    {
                        while (reader.Read())
                        {
                            result.Add(new TraceDown(
                                Convert.ToInt64(reader["trace_idx"].ToString()),
                                reader["user_id"].ToString(),
                                reader["longitude"].ToString(),
                                reader["latitude"].ToString(),
                                reader["datetime"].ToString()
                                ));
                        }
                    }
                }
            }
            return result;
        }
    }
}