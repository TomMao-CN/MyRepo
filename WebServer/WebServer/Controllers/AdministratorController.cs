using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Common.Expand;

namespace WebServer.Controllers
{
    public class AdministratorController : SuperController
    {
        #region 添加管理员
        public JsonResult ModifyAdmin(Models.Administrator formAdmin)
        {
            try
            {
                Models.Administrator objAdmin = dataContext.Administrator.Where(m => m.ID == formAdmin.ID && m.Status == (int)Models.Enums.RoleStatus.Normal).FirstOrDefault();
                //对象为空，则增加
                if (objAdmin == null)
                {
                    //生成管理员Token
                    string token = BLL.ServiceAdministrator.GetAdminToken();
                    objAdmin = new Models.Administrator
                    {
                        Name = formAdmin.Name,
                        Token = token,
                        //密码进行md5加密
                        Password = Common.HandleString.MD5(formAdmin.Password + token),
                        Avatar = formAdmin.Avatar,
                        Type = 1,
                        Status = 1,
                        LoginTime = DateTime.Now
                    };
                    dataContext.Administrator.InsertOnSubmit(objAdmin);
                }
                //否则为修改
                else
                {
                    objAdmin.Name = formAdmin.Name;
                    objAdmin.Avatar = formAdmin.Avatar;
                    objAdmin.Status = formAdmin.Status;
                    objAdmin.Type = formAdmin.Type;
                }
                //数据入库
                dataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                errorCode = 10001;
                new BLL.ServiceException().AddExceptionLog(ex);
                error = new BLL.ServiceError().GetErrorInfo(errorCode);
            }
            return Json(errorCode, error, null, true);
        }
        #endregion

        #region 获取管理员
        public JsonResult GetAdmin(string name, int type, int status)
        {
            object result = null;
            try
            {
                var query = PredicateBuilder.True<Models.Administrator>();
                if (!string.IsNullOrEmpty(name))
                {
                    query = query.And(m => m.Name.Contains(name));
                }
                if (type != 0)
                {
                    query = query.And(m => m.Type == type);
                }
                if (status != 0)
                {
                    query = query.And(m => m.Status == status);
                }
            }
            catch (Exception ex)
            {

            }
            return Json(errorCode,error,result,true);
        }
        #endregion
    }
}