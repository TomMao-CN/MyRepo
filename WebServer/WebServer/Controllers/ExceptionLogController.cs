using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Common.Expand;

namespace WebServer.Controllers
{
    public class ExceptionLogController : SuperController
    {
        #region 异常日志列表
        public JsonResult GetExceptionLogList(string startTime, string endTime, int page = 1)
        {
            int errorCode = 0;
            string error = string.Empty;
            object result = new object();      
            try
            {
                List<object> lstResult = new List<object>();
                DateTime? sTime = Common.ConvertData.StrToDateTime(startTime);
                DateTime? eTime = Common.ConvertData.StrToDateTime(endTime);

                if (sTime == null)
                    sTime = Models.SharedData.MinTime;
                if (eTime == null)
                    eTime = Models.SharedData.MaxTime;

                List<Models.ExceptionLog> lstExceptionLog = dataContext.ExceptionLog.Where(m => m.Time >= sTime && m.Time < eTime).ToList();

                int pageSize = 10;
                int pagePre = page * pageSize - pageSize;
                int totalCount = lstExceptionLog.Count;
                int mod = totalCount % pageSize;
                int pageCout = totalCount / pageSize + (mod == 0 ? 0 : 1);
                lstExceptionLog = lstExceptionLog.OrderByDescending(m => m.Time).Skip(pagePre).Take(pageSize).ToList();

                if (lstExceptionLog.Count > 0)
                {
                    foreach (var objExceptionLog in lstExceptionLog)
                    {
                        lstResult.Add(new
                        {
                            objExceptionLog.ID,
                            objExceptionLog.Source,
                            objExceptionLog.Message,
                            objExceptionLog.StackTrace,
                            Time = Common.ConvertData.ConvertDateTime(objExceptionLog.Time, 2)
                        });
                    }
                }
                result = new
                {
                    list = lstResult,
                    page,
                    totalCount,
                    totalPage = totalCount / pageSize + (totalCount % pageSize == 0 ? 0 : 1),
                    pageSize
                };
            }
            catch (Exception ex)
            {
                errorCode = 10001;
                new BLL.ServiceException().AddExceptionLog(ex);
                error = new BLL.ServiceError().GetErrorInfo(errorCode);
            }
            return Json(errorCode, error, result, true);
        }
        #endregion
    }
}