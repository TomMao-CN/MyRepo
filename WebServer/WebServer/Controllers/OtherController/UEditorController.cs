using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UEditor.Core;

namespace WebServer.Controllers
{
    public class UEditorController : Controller
    {
        //UEditor图片上传相关控制器
        public ContentResult Upload()
        {
            var response = UEditorService.Instance.UploadAndGetResponse(HttpContext.ApplicationInstance.Context);
            return Content(response.Result,response.ContentType);
        }
    }
}