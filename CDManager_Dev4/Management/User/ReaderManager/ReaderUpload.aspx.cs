using CDManagerLibrary.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDManager_Dev4.Management.User.ReaderManager
{
    public partial class ReaderUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //ExcelHelper excel = new ExcelHelper(Page.User.Identity.Name);
            //string path = Server.MapPath("~/App_Data/Temp/Upload/Reader");
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}
            //FileUpload.SaveAs(path + "\\" + "temp_Reader_" + FileUpload.FileName);
            //string msg = excel.NewReaderByExcel(path, FileUpload.PostedFile);
            //Response.Write("<script>alert('"+msg+"')</script>");
            //Response.Flush();
        }
    }
}