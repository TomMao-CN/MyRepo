using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebServer.Controllers
{
    public class SharedController : OpenController
    {
        #region 管理员登陆
        public JsonResult LoginSystem(string name, string password)
        {
            int errorCode = 0;
            string error = string.Empty;
            object result = null;
            try
            {
                Models.Administrator objAdmin = dataContext.Administrator.Where(m => m.Name == name && m.Status == (int)Models.Enums.RoleStatus.Normal).FirstOrDefault();
                if (objAdmin != null)
                {
                    //验证用户密码
                    if (objAdmin.Password != Common.HandleString.MD5(password + objAdmin.Token))
                    {
                        errorCode = 20002;
                    }
                }
                else
                    errorCode = 20001;

                if(errorCode==0)
                {
                    result = new
                    {
                        name=objAdmin.Name,
                        token=objAdmin.Token,
                        type=objAdmin.Type,
                        menus=new BLL.ServiceMenu().GetAdminMenu(objAdmin.ID)
                    };
                }
            }
            catch (Exception ex)
            {
                errorCode = 10001;
                new BLL.ServiceException().AddExceptionLog(ex);
            }

            if (errorCode != 0)
                error = new BLL.ServiceError().GetErrorInfo(errorCode);

            return Json(errorCode, error, result, true);
        }
        #endregion
    }
}