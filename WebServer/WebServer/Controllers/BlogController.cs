using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Common.Expand;

namespace WebServer.Controllers
{
    public class BlogController : SuperController
    {
        #region 添加或修改博客
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formObj"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        //当表单提交东内容是包含图片和文字是会出现表单提交错误
        public JsonResult ModifyBlog(Models.Blog formObj)
        {
            try
            {
                Models.Blog blog = dataContext.Blog.Where(m => m.ID == formObj.ID).FirstOrDefault();
                if (blog == null)
                {
                    blog = new Models.Blog
                    {
                        Title = formObj.Title,
                        Cover = formObj.Cover,
                        Content = formObj.Content,
                        CreateTime = DateTime.Now,
                        Author = formObj.Author,
                        Status = formObj.Status
                    };
                    dataContext.Blog.InsertOnSubmit(blog);
                }
                else
                {
                    blog.Title = formObj.Title;
                    blog.Cover = formObj.Cover;
                    blog.Content = formObj.Content;
                    blog.Author = formObj.Author;
                    blog.Status = formObj.Status;
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

        #region 获取博客
        public JsonResult GetBlogs(string keyword, int status = 0, int page = 1)
        {
            object result = null;
            try
            {
                var query = PredicateBuilder.True<Models.Blog>();
                if (!string.IsNullOrEmpty(keyword))
                {
                    query = query.And(m => m.Title.Contains(keyword) || m.Author.Contains(keyword));
                }
                if (status != 0)
                {
                    query = query.And(m => m.Status == status);
                }

                int pageSize = Models.SharedData.PageSize;
                int pagePre = page * pageSize - pageSize;
                int totalCount = dataContext.Blog.Where(query).Count();
                int mod = totalCount % pageSize;
                int pageCout = totalCount / pageSize + (mod == 0 ? 0 : 1);
                int totalPage = totalCount / pageSize + (totalCount % pageSize == 0 ? 0 : 1);

                List<Models.Blog> blogs = dataContext.Blog.Where(query).OrderByDescending(m => m.CreateTime).Skip(pagePre).Take(pageSize).ToList();

                List<object> list = new List<object>();
                if (blogs.Count > 0)
                {

                    foreach (Models.Blog item in blogs)
                    {
                        list.Add(new
                        {
                            item.ID,
                            item.Title,
                            item.Cover,
                            CreateTime = Common.ConvertData.ConvertDateTimeToStr(item.CreateTime, 2),
                            item.Status,
                            item.Author,
                            item.Content
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

        #region 导出博客
        public JsonResult ExportBlogs(string keyword, int status = 0)
        {
            object result = null;
            try
            {
                var query = PredicateBuilder.True<Models.Blog>();
                if (!string.IsNullOrEmpty(keyword))
                {
                    query = query.And(m => m.Title.Contains(keyword) || m.Author.Contains(keyword));
                }
                if (status != 0)
                {
                    query = query.And(m => m.Status == status);
                }

                List<Models.Blog> blogs = dataContext.Blog.Where(query).ToList();

                DataTable table = new DataTable();
                table.Columns.Add("标题", Type.GetType("System.String"));
                table.Columns.Add("作者", Type.GetType("System.String"));
                table.Columns.Add("创建时间", Type.GetType("System.String"));
                table.Columns.Add("状态", Type.GetType("System.String"));

                foreach(Models.Blog item in blogs)
                {
                    DataRow row = table.NewRow();
                    row["标题"] = item.Title;
                    row["作者"] = item.Author;
                    row["创建时间"] = Common.ConvertData.ConvertDateTimeToStr(item.CreateTime,2) ;
                    row["状态"] = BLL.ServiceEnums.ConvertSharedStatus(item.Status);
                    table.Rows.Add(row);
                }
                DateTime now = DateTime.Now;
                string xlsName = string.Format("{0}.xls", now.ToString("yyyyMMddhhmmss"));
                string folder = string.Format(@"{0}Resources\Excels\{1}",Models.SharedData.ProjectPath,now.ToString("yyyyMMdd"));
                Common.HandleFiles.CreateDirectory(folder);
                string savePath = string.Format(@"{0}\{1}", folder, xlsName);
                Common.HandleExcel.NpoiExport(table, "博客表", savePath);
                result = new
                {
                    url = string.Format(@"{0}Resources\Excels\{1}\{2}", Models.SharedData.DomainName, now.ToString("yyyyMMdd"),xlsName)
                };
            }
            catch (Exception ex)
            {
                errorCode = 10001;
                new BLL.ServiceException().AddExceptionLog(ex);
            }
            return Json(errorCode, error, result, true);
        }
        #endregion

        #region 删除博客
        public JsonResult DeleteBlog(int blogID)
        {
            try
            {
                Models.Blog blog = dataContext.Blog.Where(m => m.ID == blogID).FirstOrDefault();
                if (blog != null)
                {
                    dataContext.Blog.DeleteOnSubmit(blog);
                    dataContext.SubmitChanges();
                }
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

    }
}