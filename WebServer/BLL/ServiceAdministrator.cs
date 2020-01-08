using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ServiceAdministrator
    {
        #region 生成管理员token
        /// <summary>
        /// 生成一个管理员Token
        /// </summary>
        /// <returns></returns>
        public static string GetAdminToken()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss") + Guid.NewGuid().ToString("n").Substring(0, 6);
        }
        #endregion

    }
}
