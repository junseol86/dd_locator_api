using System;
namespace DD_Locater_API.Utils
{
    public static class ConditionSetter
    {
        public static string ByBldCtgr(string con, string bld_ctgr)
        {
            string condition = con;

            if (bld_ctgr != "0000" && bld_ctgr != "1111")
            {
                bool isFirst = true;
                condition += " AND (";

                if (bld_ctgr[0] == '1')
                {
                    condition += "main_purps_cd_nm LIKE '%단독%'";
                    isFirst = false;
                }
                if (bld_ctgr[1] == '1')
                {
                    condition += (isFirst ? "" : " OR ") + " main_purps_cd_nm LIKE '%근린생활%' ";
                    isFirst = false;
                }
                if (bld_ctgr[2] == '1')
                {
                    condition += (isFirst ? "" : " OR ") + " etc_purps LIKE '%오피스텔%' ";
                    isFirst = false;
                }
                if (bld_ctgr[3] == '1')
                {
                    condition += (isFirst ? "" : " OR ") + " etc_purps LIKE '%도시형%' ";
                }

                condition += ")";

            }
            return condition;
        }

        public static string ByBldType(string con, string bld_type) {
            string condition = con;
                       
            condition += bld_type == "ftrstr" ? " AND (bld_type LIKE 'ftr%' OR bld_type = 'str')"
                : bld_type.Contains("ftr") ? $" AND bld_type = '{bld_type}'"
                : bld_type == "ONEROOM" ? " AND (bld_type = 'ONEROOM' OR bld_type = 'ONEROOM_SU')"
                : bld_type == "su" ? " AND (bld_type = 'su' OR bld_type = 'ONEROOM_SU')"
                : bld_type == "OR_SU_BOTH" ? " AND (bld_type = 'ONEROOM' OR bld_type = 'su' OR bld_type = 'ONEROOM_SU')"
                : $" AND bld_type LIKE '%{bld_type}%'";

            return condition;
        }

        public static string ByNameNumberGwan(string con, Int64 hasName, Int64 hasNumber, Int64 hasGwan)
        {
            string condition = con;

            switch (hasName)
            {
                case -1: condition += "AND (bld_name = '' OR bld_name = '(자동입력)') "; break;
                case 1: condition += "AND bld_name != '' AND bld_name != '(자동입력)' "; break;
            }
            switch (hasNumber)
            {
                case -1: condition += "AND bld_tel_owner = '' "; break;
                case 1: condition += "AND bld_tel_owner != '' "; break;
            }
            switch (hasGwan)
            {
                case -1: condition += "AND (bld_gwan = '' OR bld_gwan IS NULL) "; break;
                case 1: condition += "AND bld_gwan != '' AND bld_gwan IS NOT NULL "; break;
            }

            return condition;
        }

        public static string ByFamilyMinMax(string con, Int64 fmlyMin, Int64 fmlyMax)
        {
            string condition = con;
            condition += fmlyMin >= 0 ? $"AND fmly_cnt >= {fmlyMin} " : "";
            condition += fmlyMax >= 0 ? $"AND fmly_cnt <= {fmlyMax} " : "";
            return condition;
        }

        public static string ByFactoryCount(string con, Int64 factory_count)
        {
            string condition = con;
            if (factory_count == 0)
            {
                condition += " AND visited = 1 AND factory_count = 0";
            }
            if (factory_count > 0)
            {
                condition += " AND visited = 1 AND factory_count > 0";
            }
            return condition;
        }
        public static string ByFloorCount(string con, Int64 floor_min) {
            string condition = con;
            if (floor_min > 0)
            {
                condition += $" AND grnd_flr_cnt >= {floor_min}";
            }
            return condition;
        }

    }
}
