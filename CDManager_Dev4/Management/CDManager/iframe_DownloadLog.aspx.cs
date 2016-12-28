using CDManagerLibrary.Excel;
using CDManagerLibrary.XML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDManager_Dev4.Management.CDManager
{
    public partial class iframe_DownloadLog : System.Web.UI.Page
    {
        string para = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try { para = Session["para"].ToString(); }
            catch { }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(para))
            {
                string temp_floder = XMLHelper.getAppSettingValue("DataTempFolder") + "\\DownloadLog";
                string temp_name = "下载日志" + DateTime.Now.ToFileTime() + ".xls";
                string temp_path = temp_floder + "\\" + temp_name;
                if (!Directory.Exists(temp_floder)) { Directory.CreateDirectory(temp_floder); }

                string[] paras = para.Split('_');
                string from = "", to = "", book = "", cd = "", czy = "";

                try { from = paras[0]; }
                catch { }
                try { to = paras[1]; }
                catch { }
                try { book = paras[2]; }
                catch { }
                try { cd = paras[3]; }
                catch { }
                try { czy = paras[4]; }
                catch { }

                ExcelHelper download = new ExcelHelper("");
                string result = download.DownloadLogToExcel(temp_path, from, to, book, cd, czy);
                if (result == "download")
                {
                    FileStream downloadStream = File.OpenRead(temp_path);
                    byte[] FileData = new byte[downloadStream.Length];
                    downloadStream.Read(FileData, 0, (int)downloadStream.Length);
                    Response.Clear();
                    Response.AddHeader("Content-Type ", "application/msword");
                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(temp_name));
                    Response.AppendHeader("Content-Length", downloadStream.Length.ToString());
                    Response.BinaryWrite(FileData);
                    downloadStream.Close();
                    new FileInfo(temp_path).Delete();
                }
                else if (result == "error")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('列表导出错误!');", true);
                    new FileInfo(temp_path).Delete();
                }
                else if (result == "listnull")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('导出列表为空!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('"+result+"');", true);
                }
            }
        }
    }
}