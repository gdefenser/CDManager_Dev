using CDManagerLibrary.Excel;
using CDManagerLibrary.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CDManager_Dev4.Management.User.ReaderManager
{
    /// <summary>
    /// ReaderUploadHandler 的摘要说明
    /// </summary>
    public class ReaderUploadHandler : CDPages,IHttpHandler
    {

        public override void ProcessRequest(HttpContext context)
        {
            HttpPostedFile file = context.Request.Files[0];
            //context.Response.ContentType = "text/plain";
            //ExcelHelper excel = new ExcelHelper(Page.User.Identity.Name);
            string path = Server.MapPath("~/App_Data/Temp/Upload/Reader");
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}
            //FileUpload.SaveAs(path + "\\" + "temp_Reader_" + FileUpload.FileName);
            //string msg = excel.NewReaderByExcel(path, FileUpload.PostedFile);
            //context.Response.Write(msg);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}