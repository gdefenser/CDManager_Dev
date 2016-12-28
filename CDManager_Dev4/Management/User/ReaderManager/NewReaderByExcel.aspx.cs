using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.Excel;
using CDManagerLibrary.Core;
using System.IO;
namespace CDManager_Dev4.Management.User.ReaderManager
{
    public partial class NewReaderByExcel : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string path = "~/Temp/Upload/Reader";
                DirectoryInfo dir = new DirectoryInfo(Server.MapPath(path));
                List<FileInfo> list = dir.GetFiles().ToList();
                if (list.Count(f => f.Name.Contains("temp_Reader_")) > 0)
                {
                    panelUpload.Visible = false;
                    upMessage.Visible = true;
                    btnInsert.Attributes.Add("onclick", "javascript:return confirm('是否已经确认待导入的读者数据无误?')");
                    FileInfo fi = list.First();
                    lblMsg.Text = "待导入读者数据共有:" + ExcelHelper.GetExcelCount(fi.FullName) + "条";
                    linkCheck.NavigateUrl = path + "/" + fi.Name;
                }
                else
                {
                    panelUpload.Visible = true;
                    upMessage.Visible = false;
                }
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload.HasFile)
            {
                string mime = FileUpload.PostedFile.ContentType;
                if (ExcelHelper.IsExcel(mime))
                {
                    string path = Server.MapPath("~/Temp/Upload/Reader/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    FileUpload.SaveAs(path + "\\" + "temp_Reader_" + FileUpload.FileName);
                    Response.Redirect("~/Management/User/ReaderManager/NewReaderByExcel.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('请选择类型为.xls或者.xlsx的Excel文件');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('你没有选择文件');", true);
            }
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {

        }

        protected void btnReUplod_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Server.MapPath("~/Temp/Upload/Reader/");
                DirectoryInfo dir = new DirectoryInfo(path);
                List<FileInfo> list = dir.GetFiles().ToList();
                if (list.Count(f => f.Name.Contains("temp_Reader_")) > 0)
                {
                    File.Delete(list.First().FullName);
                }
                Response.Redirect("~/Management/User/ReaderManager/NewReaderByExcel.aspx");
            }
            catch { }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            string msg = "";
            try
            {
                string type = "continue";
                if (rdoReset.Checked) { type = "reset"; }

                ExcelHelper excel = new ExcelHelper(Page.User.Identity.Name);
                string path = Server.MapPath("~/Temp/Upload/Reader");

                msg = excel.NewReaderByExcel(path,type);
            }
            catch
            { msg = "导入错误!"; }
            //Response.Write("<script>alert('" + msg + "');</script>");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('" + msg + "');location.reload();", true);
        }
    }
}