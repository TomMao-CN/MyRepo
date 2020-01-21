using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Models;

namespace WebServer.Controllers
{
    public class OpenController : Controller
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
    }
}