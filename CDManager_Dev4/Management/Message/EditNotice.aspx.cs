using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.Core;
using System.IO;

namespace CDManager_Dev4.Management.Message
{
    public partial class EditNotice : CDPages
    {
        string ggtm;
        protected void Page_Load(object sender, EventArgs e)
        {
            ggtm = Request.QueryString["GGTM"];
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(ggtm))
                {
                    BindNotice(ggtm);
                }
                else
                {
                    Response.Redirect("~/Management/Main.aspx");
                }
            }
        }


        protected void btnReset_Click(object sender, EventArgs e)
        {
            BindNotice(ggtm);
        }

        private void BindNotice(string ggtm)
        {
            //查找公告明细
            try
            {
                Notice notice = cde.Notice.First(n => n.GGTM == ggtm);
                txtGGBT.Text = notice.GGBT;
                txtGGDX.Text = notice.GGDX;
                txtGGNR.Text = notice.GGNR;
                txtLKR.Text = notice.LKR;
                dropDXLX.SelectedValue = notice.DXLX;
                lblTitle.Text = notice.GGBT;
                lblLKSJ.Text = notice.LKSJ.ToString();

                string path = "/NoticeFiles/" + notice.GGTM;
                string server_path = Server.MapPath("~" + path);
                DirectoryInfo dir = new DirectoryInfo(server_path);

                if (dir.Exists && notice.FJ.Value)
                {
                    if (dir.GetFiles() != null)
                    {
                        FileInfo fi = dir.GetFiles()[0];
                        linkDownload.Visible = true;
                        btnReUpload.Visible = true;
                        linkDownload.Text = fi.Name;
                        linkDownload.NavigateUrl = path + "/" + fi.Name;
                    }
                }
                else
                {
                    FileUpload.Visible = true;
                }

            }
            catch
            {
                Response.Redirect("~/Management/Main.aspx");
            }
        }
        //更新公告
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string ggbt = txtGGBT.Text;

            if (String.IsNullOrEmpty(ggbt))
            {
                valGGBT.IsValid = false;
                valGGBT.ErrorMessage = "请输入公告标题";
            }
            else
            {
                {
                    string ggnr = txtGGNR.Text;
                    if (String.IsNullOrEmpty(ggnr))
                    {
                        valGGNR.IsValid = false;
                        valGGNR.ErrorMessage = "请输入公告内容";
                    }
                    else
                    {
                        string dxlx = "";
                        if (dropDXLX.SelectedIndex > 0) { dxlx = dropDXLX.SelectedValue; }
                        else { dxlx = "读者"; }

                        string ggdx = txtGGDX.Text;
                        if (String.IsNullOrEmpty(ggdx))
                        {
                            if (dropDXLX.SelectedIndex == 2) { ggdx = "各位管理员"; }
                            else { ggdx = "各位读者"; }
                        }

                        string lkr = txtLKR.Text;
                        if (String.IsNullOrEmpty(lkr)) { lkr = "图书馆"; }
                        try
                        {
                            if (dropDXLX.SelectedValue == "前台关闭公告" &&
                                cde.Notice.Count(n => n.DXLX == "前台关闭公告") >= 1)
                            {
                                valGGBT.IsValid = false;
                                valGGBT.ErrorMessage = "前台关闭公告已存在";
                            }
                            else
                            {
                                Notice edit_notice = cde.Notice.Find(ggtm);
                                string fname = "";
                                bool isFJ = edit_notice.FJ.Value;
                                if (FileUpload.Visible && !isFJ)
                                {
                                    if (FileUpload.HasFile)
                                    {
                                        string[] type = FileUpload.FileName.Split('.');
                                        string file_type = type[type.Length - 1];
                                        if (type.Length >= 2 && 
                                            (file_type != "txt" || 
                                            file_type != "htm" || 
                                            file_type != "html" || 
                                            file_type != "xml" || 
                                            file_type != "xhtml"))
                                        {
                                            string path = Server.MapPath("~/NoticeFiles/" + ggtm);
                                            fname = FileUpload.FileName;
                                            if (!Directory.Exists(path))
                                            {
                                                Directory.CreateDirectory(path);
                                            }
                                            FileUpload.SaveAs(path + "//" + fname);
                                            isFJ = !isFJ;
                                        }
                                    }
                                }

                                edit_notice.GGBT = ggbt;
                                edit_notice.DXLX = dxlx;
                                edit_notice.GGDX = ggdx;
                                edit_notice.GGNR = ggnr;
                                edit_notice.FJ = isFJ;
                                edit_notice.LKR = lkr;
                                SaveAndRedirect_Management();
                            }
                        }
                        catch
                        {
                            Response.Redirect("~/Management/Error.aspx");
                        }
                    }
                }
            }
        }

        protected void btnReUpload_Click(object sender, EventArgs e)
        {
            string ggtm = Request.QueryString["GGTM"];
            DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/NoticeFiles/" + ggtm));
            if (dir.Exists)
            {
                try
                {
                    FileInfo fi = dir.GetFiles()[0];
                    fi.Delete();
                    Notice edit_notice = cde.Notice.First(n => n.GGTM == ggtm);
                    edit_notice.FJ = false;
                    cde.SaveChanges();
                    Response.Redirect("~/Management/Message/EditNotice.aspx?GGTM=" + ggtm, false);
                }
                catch { }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Management/Message/NoticeManagement.aspx");
        }
    }
}