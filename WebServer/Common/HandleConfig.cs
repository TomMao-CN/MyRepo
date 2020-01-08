using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Web;

namespace Common
{
    public class HandleConfig
    {
        #region 获取配置文件中AppSetting节点的值
        /// <summary>
        /// 获取配置文件中AppSetting节点的值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static string GetAppSettingValue(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }
        #endregion

        #region 获取项目绝对路径
        public static string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                if (strPath == "/")
                    return AppDomain.CurrentDomain.BaseDirectory;
                else
                    return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }
        #endregion
    }
}
