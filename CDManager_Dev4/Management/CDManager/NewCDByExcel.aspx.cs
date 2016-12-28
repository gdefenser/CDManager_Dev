using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.Excel;
using CDManagerLibrary.Core;
using System.IO;
namespace CDManager_Dev4.Management.CDManager
{
    public partial class NewCDByExcel : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string path = "~/Temp/Upload/CD";
                DirectoryInfo dir = new DirectoryInfo(Server.MapPath(path));
                List<FileInfo> list = dir.GetFiles().ToList();
                if (list.Count(f => f.Name.Contains("temp_CD_")) > 0)
                {
                    panelUpload.Visible = false;
                    upMessage.Visible = true;
                    btnInsert.Attributes.Add("onclick", "javascript:return confirm('是否已经确认待导入的光盘数据无误?')");
                    FileInfo fi = list.First();
                    lblMsg.Text = "待导入光盘数据共有:" + ExcelHelper.GetExcelCount(fi.FullName) + "条";
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
                    string path = Server.MapPath("~/Temp/Upload/CD/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    FileUpload.SaveAs(path + "\\" + "temp_CD_" + FileUpload.FileName);
                    Response.Redirect("~/Management/CDManager/NewCDByExcel.aspx");
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

        protected void btnReUplod_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Server.MapPath("~/Temp/Upload/CD/");
                DirectoryInfo dir = new DirectoryInfo(path);
                List<FileInfo> list = dir.GetFiles().ToList();
                if (list.Count(f => f.Name.Contains("temp_CD_")) > 0)
                {
                    File.Delete(list.First().FullName);
                }
                Response.Redirect("~/Management/CDManager/NewCDByExcel.aspx");
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
                string path = Server.MapPath("~/Temp/Upload/CD");

                msg = excel.NewCDByExcel(path, type);
            }
            catch
            { msg = "导入错误!"; }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('" + msg + "');location.reload();", true);
        }
    }
}