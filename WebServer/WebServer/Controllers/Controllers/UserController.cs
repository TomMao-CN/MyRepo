using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Common.Expand;

namespace WebServer.Controllers
{
    public class UserController : SuperController
    {
        #region 获取用户列表
        /// <summary>
        /// 根据姓名，查找角色集合
        /// </summary>
        /// <param name="name">姓名</param>
        /// <returns></returns>
        public JsonResult GetUserList(string name = null, int page = 1)
        {
           
            #region 初始化参数
            int errorCode = 0;
            string error = string.Empty;
            List<object> lstResult = new List<object>();
            object result = null;
            var predicate = Common.Expand.PredicateBuilder.True<Models.User>();
            List<Models.User> lstUser = null;
            #endregion

            #region 逻辑处理
            if (!string.IsNullOrEmpty(name))
                predicate = predicate.And(m => m.Name.Contains(name));
            predicate = predicate.And(m => m.Status == (int)Models.Enums.RoleStatus.Normal);
            #endregion

            #region 分页
            int pageSize = 15;
            int pagePre = page * pageSize - pageSize;
            int totalCount = dataContext.User.Where(predicate).Count();
            int mod = totalCount % pageSize;
            int pageCout = totalCount / pageSize + (mod == 0 ? 0 : 1);
            lstUser = dataContext.User.Where(predicate).OrderByDescending(m => m.Number).Skip(pagePre).Take(pageSize).ToList();
            #endregion

            #region 数据返回
            if (lstUser.Count > 0)
            {
                foreach (var objUser in lstUser)
                {
                    lstResult.Add(new
                    {
                        objUser.ID,
                        objUser.Number,
                        Name = objUser.Name.Trim(),
                        Gender = new BLL.ServiceUser().ConvertGender(objUser.Gender),
                        objUser.Age,
                        Birthday = Common.ConvertData.ConvertDateTime(objUser.Birthday, 1),
                        objUser.Status,
                        objUser.Signature,
                        objUser.Portrait
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
            #endregion

            return Json(errorCode, error, result, true);
        }
        #endregion

        #region 获取所有用户列表
        public JsonResult GetAllUserList()
        {
            int errorCode = 0;
            string error = string.Empty;
            List<object> list = new List<object>();
            List<Models.User> lstUser = dataContext.User.Where(m => m.Status == (int)Models.Enums.RoleStatus.Normal).ToList();
            foreach (var item in lstUser)
            {
                list.Add(new
                {
                    item.ID,
                    item.Name,
                    item.Number
                });
            }
            return Json(errorCode, error, list, true);
        }
        #endregion

        #region 导出用户列表Excel表格
        public JsonResult ExportUserList(string name = "")
        {
            int errorCode = 0;
            string error = string.Empty;
            string result = string.Empty;
            try
            {
                List<Models.User> lstUser = dataContext.User.Where(m => m.Name.Contains(name) && m.Status == (int)Models.Enums.RoleStatus.Normal).ToList();

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("编号", Type.GetType("System.String"));
                dataTable.Columns.Add("姓名", Type.GetType("System.String"));
                dataTable.Columns.Add("性别", Type.GetType("System.String"));
                dataTable.Columns.Add("生日", Type.GetType("System.String"));
                dataTable.Columns.Add("年龄", Type.GetType("System.String"));
                foreach (var objUser in lstUser)
                {
                    DataRow dataRow;
                    dataRow = dataTable.NewRow();
                    dataRow["编号"] = objUser.Number;
                    dataRow["姓名"] = objUser.Name;
                    dataRow["性别"] = new BLL.ServiceUser().ConvertGender(objUser.Gender);
                    dataRow["生日"] = objUser.Birthday;
                    dataRow["年龄"] = objUser.Age;
                    dataTable.Rows.Add(dataRow);
                }

                string fileName = string.Format("{0}.xls", DateTime.Now.ToString("yyyyMMddhhmmss"));
                string path = string.Format(@"{0}Resources\Excels\User\{1}", Models.SharedData.ProjectPath, fileName);
                Common.HandleExcel.NpoiExport(dataTable, "用户表", path);
                result = string.Format(@"{0}Resources\Excels\User\{1}", Models.SharedData.DomainName, fileName);
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

        #region 删除用户
        public JsonResult DeleteUser(int id)
        {
            int errorCode = 0;
            string error = string.Empty;
            try
            {
                Models.User objUser = dataContext.User.Where(m => m.ID == id).FirstOrDefault();
                if (objUser != null)
                {
                    objUser.Status = (int)Models.Enums.RoleStatus.Delete;
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
                error = new BLL.ServiceError().GetErrorInfo(errorCode);

            return Json(errorCode, error, null, true);
        }
        #endregion

        #region 新增/编辑用户
        public JsonResult ModifyUser(Models.User formUser)
        {
            int errorCode = 0;
            string error = string.Empty;
            try
            {
                Models.User objUser = dataContext.User.Where(m => m.ID == formUser.ID && m.Status == (int)Models.Enums.RoleStatus.Normal).FirstOrDefault();
                if (objUser == null)
                {
                    objUser = new Models.User
                    {
                        ID = formUser.ID,
                        Number = formUser.Number,
                        Name = formUser.Name,
                        Gender = formUser.Gender,
                        Age = DateTime.Now.Year - formUser.Birthday.Year,
                        Birthday = formUser.Birthday,
                        Status = (int)Models.Enums.RoleStatus.Normal,
                        Portrait = formUser.Portrait,
                        Signature=formUser.Signature
                    };
                    dataContext.User.InsertOnSubmit(objUser);
                }
                else
                {
                    objUser.Name = formUser.Name;
                    objUser.Number = formUser.Number;
                    objUser.Gender = formUser.Gender;
                    objUser.Age = DateTime.Now.Year - formUser.Birthday.Year;
                    objUser.Birthday = formUser.Birthday;
                    objUser.Portrait = formUser.Portrait;
                    objUser.Signature = formUser.Signature;
                }
                dataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                errorCode = 10001;
                new BLL.ServiceException().AddExceptionLog(ex);
            }
            if (errorCode != 0)
                error = new BLL.ServiceError().GetErrorInfo(errorCode);

            return Json(errorCode, error, null, true);
        }
        #endregion

        #region 上传用户头像
        public JsonResult UploadUserPortrait(HttpPostedFileBase file)
        {
            int errorCode = 0;
            string error = string.Empty;
            string result = string.Empty;
            try
            {
                string imgName = string.Format("{0}.jpg", DateTime.Now.ToString("yyyyMMddHHmmss"));
                string localPath = Server.MapPath("~/Resources/Images/User/" + imgName);
                file.SaveAs(localPath);
                string webPath = string.Format("{0}/Resources/Images/User/{1}", Common.HandleConfig.GetAppSettingValue("DomainName"), imgName).Replace("/", "\\");
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