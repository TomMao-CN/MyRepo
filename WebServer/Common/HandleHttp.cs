using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft;
using Newtonsoft.Json.Linq;

namespace Common
{
    public class HandleHttp
    {
        #region 获取请求参数的值
        /// <summary>
        /// 将请求参数转化为动态类型返回
        /// </summary>
        /// <returns></returns>
        public static dynamic GetRequestParamValue()
        {
            Stream s = HttpContext.Current.Request.InputStream;
            byte[] b = new byte[s.Length];
            s.Read(b, 0, (int)s.Length);
            string paramStr = Encoding.UTF8.GetString(b);
            //将json字符串转化为动态类型
            return JToken.Parse(paramStr) as dynamic;
        }
        #endregion

        #region 获取表单请求的参数
        public static string GetFormParams(string strName)
        {
            if (HttpContext.Current.Request.Form[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.Form[strName];
        }
        #endregion
    }
}
