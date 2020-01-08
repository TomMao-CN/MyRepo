using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ServiceUser
    {
        #region 转化用户性别
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        public string ConvertGender(bool gender)
        {
            if (gender == true)
            {
                return "男";
            }
            else
            {
                return "女";
            }
        }
        #endregion

    }
}
