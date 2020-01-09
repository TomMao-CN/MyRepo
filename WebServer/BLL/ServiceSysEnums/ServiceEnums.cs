using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ServiceEnums
    {
        /// <summary>
        /// 转化管理员类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ConvertAdminType (int type)
        {
            switch (type)
            {
                case (int)Models.Enums.AdminType.SuperAdmin:
                    return "超级管理员";
                case (int)Models.Enums.AdminType.SecondaryAdmin:
                    return "次级管理员";
                default:
                    return "";
            }
        }
        public static string ConvertRoleStatus(int status)
        {
            switch (status)
            {
                case (int)Models.Enums.RoleStatus.Normal:
                    return "正常";
                case (int)Models.Enums.RoleStatus.Verify:
                    return "审核中";
                default:
                    return "";
            }
        }
    }
}
