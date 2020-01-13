using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebServer.Controllers
{
    public class AuthorityController : SuperController
    {
        public JsonResult GetAdminSys(int adminID, int page = 1)
        {
            object result = new object();
            try
            {
                List<Models.AdminSys> adminSyses = dataContext.AdminSys.Where(m => m.AdminID == adminID).ToList();

                int pageSize = Models.SharedData.PageSize;
                int pagePre = page * pageSize - pageSize;
                int totalCount = adminSyses.Count;
                int mod = totalCount % pageSize;
                int pageCout = totalCount / pageSize + (mod == 0 ? 0 : 1);
                int totalPage = totalCount / pageSize + (totalCount % pageSize == 0 ? 0 : 1);

                adminSyses = adminSyses.OrderByDescending(m => m.AuthorityTime).Skip(pagePre).Take(pageSize).ToList();

                List<object> list = new List<object>();
                if (adminSyses.Count > 0)
                {

                    foreach (Models.AdminSys adminSys in adminSyses)
                    {
                        list.Add(new
                        {
                            adminSys.ID,
                            AdminName = adminSys.Administrator.Name,
                            SysName = adminSys.AuthoritySys.Name,
                            AuthorityTime=Common.ConvertData.ConvertDateTimeToStr(adminSys.AuthorityTime,2),
                            adminSys.Status
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
                error = new BLL.ServiceError().GetErrorInfo(errorCode);
                new BLL.ServiceException().AddExceptionLog(ex);
            }
            return Json(errorCode, error, result, true);
        }
    }
}