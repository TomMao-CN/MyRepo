using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ConvertData
    {
        #region 转换时间格式
        /// <summary>
        /// 转换时间格式。
        /// type=1=yyyy-MM-dd
        /// type=2=yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static string ConvertDateTimeToStr(DateTime dateTime, int type)
        {
            switch (type)
            {
                case 1:
                    return dateTime.ToString("yyyy-MM-dd");
                case 2 :
                    return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
                default:
                    return null;
            }
        }
        #endregion

        #region 将时间字符串转化为DateTime类型
        public static DateTime? StrToDateTime(string date)
        {
            if (String.IsNullOrEmpty(date))
                return null;
            DateTime dt;
            if (!DateTime.TryParse(date, out dt))
                return null;
            return dt;
        }
        #endregion

    }
}
