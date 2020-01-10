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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        public JsonResult GetExceptionLogList(string startTime, string endTime, int page = 1)
        {
            object result = new object();      
            try
            {
             
                DateTime? sTime = Common.ConvertData.StrToDateTime(startTime);
                DateTime? eTime = Common.ConvertData.StrToDateTime(endTime);

                if (sTime == null)
                    sTime = Models.SharedData.MinTime;
                if (eTime == null)
                    eTime = Models.SharedData.MaxTime;

                List<Models.ExceptionLog> exceptionLogs = dataContext.ExceptionLog.Where(m => m.Time >= sTime && m.Time < eTime).ToList();

                int pageSize = 10;
                int pagePre = page * pageSize - pageSize;
                int totalCount = exceptionLogs.Count;
                int mod = totalCount % pageSize;
                int pageCout = totalCount / pageSize + (mod == 0 ? 0 : 1);
                int totalPage = totalCount / pageSize + (totalCount % pageSize == 0 ? 0 : 1);

                exceptionLogs = exceptionLogs.OrderByDescending(m => m.Time).Skip(pagePre).Take(pageSize).ToList();
                List<object> list = new List<object>();

                if (exceptionLogs.Count > 0)
                {
                    foreach (var exceptionLog in exceptionLogs)
                    {
                        list.Add(new
                        {
                            exceptionLog.ID,
                            exceptionLog.Source,
                            exceptionLog.Message,
                            exceptionLog.StackTrace,
                            Time = Common.ConvertData.ConvertDateTime(exceptionLog.Time, 2)
                        });
                    }
                }
                result = new
                {
                    list,
                    page,
                    totalCount,
                    totalPage,
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