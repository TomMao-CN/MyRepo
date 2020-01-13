using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ServiceError
    {
        public  string GetErrorInfo(int errorCode)
        {
            return ErrorData[errorCode];
        }

        #region 错误信息
        public Dictionary<int, string> ErrorData
        {
            get
            {
                Dictionary<int, string> result = new Dictionary<int, string>();
                #region 通用类错误
                result.Add(10001, "服务器异常！");
                result.Add(10002, "无访问权限！");
                #endregion

                #region 用户类错误
                result.Add(20001, "角色不存在！");
                result.Add(20002, "密码错误！");
                #endregion

                #region 权限类错误
                //result.Add(30001, "此管理员为！");
                #endregion
                return result;
            }
        }
        #endregion

    }
}
