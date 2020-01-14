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
        #region 添加/修改管理员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formAdmin"></param>
        /// <returns></returns>
        public JsonResult ModifyAdmin(Models.Administrator formAdmin)
        {
            try
            {
                Models.Administrator objAdmin = dataContext.Administrator.Where(m => m.ID == formAdmin.ID).FirstOrDefault();
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
                        Type = formAdmin.Type,
                        Status = formAdmin.Status,
                        LoginTime = Models.SharedData.MinTime
                    };
                    dataContext.Administrator.InsertOnSubmit(objAdmin);
                }
                //否则为修改
                else
                {
                    //如果密码不为空则是修改密码
                    if (formAdmin.Password != null)
                    {
                        objAdmin.Password = Common.HandleString.MD5(formAdmin.Password+objAdmin.Token);
                    }
                    //否则为修改信息
                    else
                    {
                        objAdmin.Name = formAdmin.Name;
                        objAdmin.Avatar = formAdmin.Avatar;
                        objAdmin.Status = formAdmin.Status;
                        objAdmin.Type = formAdmin.Type;
                    }

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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">名字</param>
        /// <param name="type">管理员类型</param>
        /// <param name="status">角色状态</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        public JsonResult GetAdmins(string name = null, int type = 0, int status = 0, int page = 1)
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

                int pageSize = Models.SharedData.PageSize;
                int pagePre = page * pageSize - pageSize;
                int totalCount = dataContext.Administrator.Where(query).Count();
                int mod = totalCount % pageSize;
                int pageCout = totalCount / pageSize + (mod == 0 ? 0 : 1);
                int totalPage = totalCount / pageSize + (totalCount % pageSize == 0 ? 0 : 1);

                List<Models.Administrator> administrators = dataContext.Administrator.Where(query).OrderByDescending(m => m.ID).Skip(pagePre).Take(pageSize).ToList();
                List<object> list = new List<object>();
                if (administrators.Count > 0)
                {

                    foreach (Models.Administrator administrator in administrators)
                    {
                        list.Add(new
                        {
                            administrator.ID,
                            administrator.Name,
                            administrator.Avatar,
                            administrator.Status,
                            administrator.Type,
                            LoginTime = Common.ConvertData.ConvertDateTimeToStr(administrator.LoginTime, 2)
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

        #region 删除管理员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="adminID"></param>
        /// <returns></returns>
        public JsonResult DeleteAdmin(int adminID)
        {
            try
            {
                Models.Administrator administrator = dataContext.Administrator.Where(m => m.ID == adminID).FirstOrDefault();
                if (administrator != null)
                {
                    //删除管理员绑定的系统
                    List<Models.AdminSys> adminSyses = dataContext.AdminSys.Where(m=>m.AdminID==administrator.ID).ToList();
                    dataContext.AdminSys.DeleteAllOnSubmit(adminSyses);
                    dataContext.Administrator.DeleteOnSubmit(administrator);
                    dataContext.SubmitChanges();
                }
                else
                    errorCode = 20001;
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