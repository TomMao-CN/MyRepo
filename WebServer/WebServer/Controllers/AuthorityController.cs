using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebServer.Controllers
{
    public class AuthorityController : SuperController
    {
        #region 获取管理员系统
        /// <summary>
        /// 
        /// </summary>
        /// <param name="adminID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
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
                            AuthorityTime = Common.ConvertData.ConvertDateTimeToStr(adminSys.AuthorityTime, 2),
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
        #endregion

        #region 获取未管理的系统
        public JsonResult GetOtherSys(int adminID)
        {
            object result = new object();
            try
            {
                List<int> bindSysID = dataContext.AdminSys.Where(m => m.AdminID == adminID).Select(m => m.SysID).ToList();
                List<int> allSysID = dataContext.AuthoritySys.Where(m => m.Status == (int)Models.Enums.SharedStatus.Normal).Select(m =>m.ID).ToList();
                List<int> noBindSysID = allSysID.Except(bindSysID).ToList();
                List<Models.AuthoritySys> authoritySyses = dataContext.AuthoritySys.Where(m => noBindSysID.Contains(m.ID)&&m.Status == (int)Models.Enums.SharedStatus.Normal).ToList();
                List<object> list = new List<object>();
                if (authoritySyses.Count > 0)
                {
                    foreach (Models.AuthoritySys item in authoritySyses)
                    {
                        list.Add(new
                        {
                            item.ID,
                            item.Name
                        });
                    }
                    result = new
                    {
                        list
                    };
                }
            }
            catch (Exception ex)
            {
                errorCode = 10001;
                error = new BLL.ServiceError().GetErrorInfo(errorCode);
                new BLL.ServiceException().AddExceptionLog(ex);
            }
            return Json(errorCode, error, result, true);
        }
        #endregion

        #region 添加管理员系统
        public JsonResult AddAdminSys(Models.AdminSys formObj)
        {
            try
            {
                Models.AdminSys adminSys = dataContext.AdminSys.Where(m => m.ID == formObj.ID).FirstOrDefault();
                if (adminSys == null)
                {
                    adminSys = new Models.AdminSys
                    {
                        AdminID = formObj.AdminID,
                        SysID = formObj.SysID,
                        AuthorityTime = DateTime.Now,
                        Status = formObj.Status
                    };
                    dataContext.AdminSys.InsertOnSubmit(adminSys);
                }
                else
                {
                    adminSys.Status = formObj.Status;
                }
                dataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                errorCode = 10001;
                error = new BLL.ServiceError().GetErrorInfo(errorCode);
                new BLL.ServiceException().AddExceptionLog(ex);
            }
            return Json(errorCode, error, null, true);
        }
        #endregion

        #region 删除管理系统
        public JsonResult DeleteAdminSys(int adminSysID)
        {
            try
            {
                Models.AdminSys adminSys = dataContext.AdminSys.Where(m=>m.ID==adminSysID).FirstOrDefault();
                if (adminSys != null)
                {
                    dataContext.AdminSys.DeleteOnSubmit(adminSys);
                    dataContext.SubmitChanges();
                }
                else
                    errorCode = 30001;
            }
            catch (Exception ex)
            {
                errorCode = 10001;              
                new BLL.ServiceException().AddExceptionLog(ex);
            }
            if (errorCode != 0)
            {
                error = new BLL.ServiceError().GetErrorInfo(errorCode);
            }
            return Json(errorCode, error, null, true);
        }
        #endregion
    }
}