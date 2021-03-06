﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

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
                Models.Administrator administrator = dataContext.Administrator.Where(m => m.Name == name && m.Status == (int)Models.Enums.RoleStatus.Normal).FirstOrDefault();
                if (administrator != null)
                {
                    //验证用户密码
                    if (administrator.Password != Common.HandleString.MD5(password + administrator.Token))
                    {
                        errorCode = 20002;
                    }
                }
                else
                    errorCode = 20001;

                if (errorCode == 0)
                {
                    //更新管理员登陆时间
                    administrator.LoginTime = DateTime.Now;
                    dataContext.SubmitChanges();
                    result = new
                    {
                        administrator.ID,
                        administrator.Name,
                        administrator.Token,
                        administrator.Type,
                        administrator.Avatar,
                        administrator.Status,
                        administrator.Password,
                        Menus = administrator.Type == (int)Models.Enums.AdminType.SuperAdmin ? null : new BLL.ServiceAuthority().GetAdminMenu(administrator.ID)
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

        #region 图片上传
        public JsonResult UploadImage(HttpPostedFileBase file)
        {
            int errorCode = 0;
            string error = string.Empty;
            string result = string.Empty;
            try
            {
                DateTime now = DateTime.Now;
                string imgName = string.Format("{0}.jpg", now.ToString("yyyyMMddHHmmss"));


                string folder = string.Format(@"{0}Resources\Images\{1}", Models.SharedData.ProjectPath, now.ToString("yyyyMMdd"));
                //检查是否有该文件夹，没有则创建
                Common.HandleFiles.CreateDirectory(folder);

                string localPath = string.Format(@"{0}\{1}", folder, imgName);

                file.SaveAs(localPath);
                string webPath = string.Format(@"{0}\Resources\Images\{1}\{2}", Models.SharedData.DomainName, now.ToString("yyyyMMdd"), imgName);
                result = webPath;
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
    }
}
