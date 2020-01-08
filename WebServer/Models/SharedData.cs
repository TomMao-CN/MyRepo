using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Models
{
    public class SharedData
    {
        #region 服务器最小时间
        /// <summary>
        /// 服务器最小时间
        /// </summary>
        public static DateTime MinTime
        {
            get
            {
                DateTime tempTime;
                DateTime.TryParse("1900-01-01", out tempTime);
                return tempTime;
            }
        }
        #endregion

        #region 服务器最大日期
        /// <summary>
        /// 服务器最大日期
        /// </summary>
        public static DateTime MaxTime
        {
            get
            {
                DateTime tempTime;
                DateTime.TryParse("2999-01-01", out tempTime);
                return tempTime;
            }
        }
        #endregion

        #region 项目绝对路径
        /// <summary>
        /// 项目绝对路径
        /// </summary>
        public static string ProjectPath
        {
            get
            {
                return Common.HandleConfig.GetMapPath("/");
            }
        }
        #endregion

        #region 网络域名
        /// <summary>
        /// 网络域名
        /// </summary>
        public static string DomainName
        {
            get
            {
                return Common.HandleConfig.GetAppSettingValue("DomainName");
            }
        }
        #endregion
    }
}
