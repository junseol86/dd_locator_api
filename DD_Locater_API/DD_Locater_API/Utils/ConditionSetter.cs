using System;
namespace DD_Locater_API.Utils
{
    public static class ConditionSetter
    {
        public static string ByBound(string con, string prefix, double left, double right, double top, double bottom) {
            string condition = con;
            condition += $@"
                AND {prefix}geo_lng > '{left}'
                AND {prefix}geo_lng < '{right}'
                AND {prefix}geo_lat < '{top}'
                AND {prefix}geo_lat > '{bottom}'
            ";
            return condition;
        }

        public static string ByBldCtgr(string con, string prefix, string bld_ctgr)
        {
            string condition = con;

            if (bld_ctgr != "0000" && bld_ctgr != "1111")
            {
                bool isFirst = true;
                condition += " AND (";

                if (bld_ctgr[0] == '1')
                {
                    condition += $"{prefix}main_purps_cd_nm LIKE '%단독%'";
                    isFirst = false;
                }
                if (bld_ctgr[1] == '1')
                {
                    condition += (isFirst ? "" : " OR ") + $" {prefix}main_purps_cd_nm LIKE '%근린생활%' ";
                    isFirst = false;
                }
                if (bld_ctgr[2] == '1')
                {
                    condition += (isFirst ? "" : " OR ") + $" {prefix}etc_purps LIKE '%오피스텔%' ";
                    isFirst = false;
                }
                if (bld_ctgr[3] == '1')
                {
                    condition += (isFirst ? "" : " OR ") + $" {prefix}etc_purps LIKE '%도시형%' ";
                }

                condition += ")";

            }
            return condition;
        }

        public static string ByBldType(string con, string prefix, string bld_type) {
            string condition = con;
                       
            condition += bld_type == "ftrstr" ? $" AND ({prefix}bld_type LIKE 'ftr%' OR {prefix}bld_type = 'str')"
                : bld_type.Contains("ftr") ? $" AND {prefix}bld_type = '{bld_type}'"
                          : bld_type == "ONEROOM" ? $" AND ({prefix}bld_type = 'ONEROOM' OR {prefix}bld_type = 'ONEROOM_SU')"
                          : bld_type == "su" ? $" AND ({prefix}bld_type = 'su' OR {prefix}bld_type = 'ONEROOM_SU')"
                          : bld_type == "OR_SU_BOTH" ? $" AND ({prefix}bld_type = 'ONEROOM' OR {prefix}bld_type = 'su' OR {prefix}bld_type = 'ONEROOM_SU')"
                          : $" AND {prefix}bld_type LIKE '%{bld_type}%'";

            return condition;
        }

        public static string ByNameNumberGwan(string con, string prefix, Int64 hasName, Int64 hasNumber, Int64 hasGwan)
        {
            string condition = con;

            switch (hasName)
            {
                case -1: condition += $" AND ({prefix}bld_name = '' OR {prefix}bld_name = '(자동입력)') "; break;
                case 1: condition += $" AND {prefix}bld_name != '' AND {prefix}bld_name != '(자동입력)' "; break;
            }
            switch (hasNumber)
            {
                case -1: condition += $" AND {prefix}bld_tel_owner = '' "; break;
                case 1: condition += $" AND {prefix}bld_tel_owner != '' "; break;
            }
            switch (hasGwan)
            {
                case -1: condition += $" AND ({prefix}bld_gwan = '' OR {prefix}bld_gwan IS NULL) "; break;
                case 1: condition += $" AND {prefix}bld_gwan != '' AND {prefix}bld_gwan IS NOT NULL "; break;
            }

            return condition;
        }

        public static string ByFamilyMinMax(string con, string prefix, Int64 fmlyMin, Int64 fmlyMax)
        {
            string condition = con;
            condition += fmlyMin >= 0 ? $" AND {prefix}fmly_cnt >= {fmlyMin} " : "";
            condition += fmlyMax >= 0 ? $" AND {prefix}fmly_cnt <= {fmlyMax} " : "";
            return condition;
        }

        public static string ByFactoryCount(string con, string prefix, Int64 factory_count)
        {
            string condition = con;
            if (factory_count == 0)
            {
                condition += $" AND {prefix}visited = 1 AND {prefix}factory_count = 0 ";
            }
            if (factory_count > 0)
            {
                condition += $" AND {prefix}visited = 1 AND {prefix}factory_count > 0 ";
            }
            return condition;
        }

        public static string ByFloorCount(string con, string prefix, Int64 floor_min) {
            string condition = con;
            if (floor_min > 0)
            {
                condition += $" AND {prefix}grnd_flr_cnt >= {floor_min}";
            }
            return condition;
        }

        public static string ByApprovePic(string con, string prefix, Int64 approvedPic)
        {
            string condition = con;
            if (approvedPic > -1) {
                condition += $" AND ({prefix}picture_agree " + (approvedPic == 1 ? " = 1" : $" != 1 OR {prefix}picture_agree IS NULL ") + ") ";
            }
            return condition;
        }

        public static string ByAgency(string con, string prefix, Int64 hasAgency)
        {
            string condition = con;
            if (hasAgency > -1)
            {
                condition += $" AND {prefix}manager_idx IS " + (hasAgency == 1 ? "NOT" : "") + " NULL ";
            }
            return condition;
        }

        public static string BySms(string con, string prefix, Int64 smsNotSent)
        {
            string condition = con;
            if (smsNotSent == 1)
            {
                condition += $" AND ({prefix}sms_flag != 1 OR {prefix}sms_flag IS NULL) ";
            }
            return condition;
        }


    }
}
