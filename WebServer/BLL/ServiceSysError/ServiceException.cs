using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ServiceException : SuperClass
    {
        #region 添加异常日志
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public void AddExceptionLog(Exception ex)
        {
            Models.ExceptionLog objExceptionLog = new Models.ExceptionLog
            {
                Time=DateTime.Now,
                Message=ex.Message,
                Source=ex.Source,
                StackTrace = ex.StackTrace
            };
            dataContext.ExceptionLog.InsertOnSubmit(objExceptionLog);
            dataContext.SubmitChanges();
        }
        #endregion
    }
}
