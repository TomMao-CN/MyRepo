using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

using Models;

namespace WebServer.Controllers
{
    public class SuperController : Controller
    {
        protected DepositoryDataContext dataContext = new DepositoryDataContext();
        protected int errorCode = 0;
        protected string error = string.Empty;


        #region 重载Json返回函数
        /// <summary>
        /// 封装Json返回
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="error"></param>
        /// <param name="info"></param>
        /// <param name="allowGet"></param>
        /// <returns></returns>
        protected JsonResult Json(int errorCode, string error, object info, bool allowGet)
        {
            if (info != null)
                return Json(new { error_code = errorCode, error, data = info }, allowGet ? JsonRequestBehavior.AllowGet : JsonRequestBehavior.DenyGet);
            else
                return Json(new { error_code = errorCode, error }, allowGet ? JsonRequestBehavior.AllowGet : JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region 重载OnAuthorization
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                //获取请求参数token
                string token = Common.HandleHttp.GetFormParams("token");
                //查询token是否属于管理员
                Models.Administrator administrator = dataContext.Administrator.Where(m => m.Token == token && m.Status == (int)Models.Enums.RoleStatus.Normal).FirstOrDefault();
                if (administrator == null)
                {
                    errorCode = 10002;
                }
            }
            catch (Exception ex)
            {
                errorCode = 10001;
                new BLL.ServiceException().AddExceptionLog(ex);
            }
            if (errorCode != 0)
            {
                error = new BLL.ServiceError().GetErrorInfo(errorCode);
                filterContext.Result = Json(errorCode, error, null, true); ;
            }
            base.OnAuthorization(filterContext);
        }
        #endregion
    }
}