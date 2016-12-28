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
    public partial class NewNotice : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //新建公告
        protected void btnNew_Click(object sender, EventArgs e)
        {
            string ggbt = txtGGBT.Text;

            if (String.IsNullOrEmpty(ggbt))
            {
                valGGBT.IsValid = false;
                valGGBT.ErrorMessage = "请输入公告标题";
            }
            else
            {
                //检查前台公告是否存在
                if (dropDXLX.SelectedValue == "前台关闭公告" && cde.Notice.Count(n => n.DXLX == "前台关闭公告") >= 1)
                {
                    valGGBT.IsValid = false;
                    valGGBT.ErrorMessage = "前台关闭公告已存在";
                }
                else
                {
                    string ggnr = txtGGNR.Text;
                    if (String.IsNullOrEmpty(ggnr))
                    {
                        valGGNR.IsValid = false;
                        valGGNR.ErrorMessage = "请输入公告内容";
                    }
                    else
                    {

                        //生成主键ID
                        DateTime now = DateTime.Now;
                        string id = "GG" + now.Year.ToString() + now.Month.ToString() + now.Day.ToString();
                        var id_count = cde.Notice.Where(n => n.GGTM.Contains(id)).ToList();
                        if (id_count.Count > 0)
                        {
                            string max = id_count.Max(n => n.GGTM.Substring(n.GGTM.Length - 4, 4));
                            int temp_id = Convert.ToInt16(max) + 1;
                            id += temp_id.ToString().PadLeft(4, '0');
                        }
                        else
                        {
                            id += "0001";
                        }

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
                        string fname = "";
                        bool fj = false;
                        if (FileUpload.Visible)
                        {
                            if (FileUpload.HasFile)
                            {
                                if (FileUpload.FileContent.Length < 1024 * 1024 * 3)
                                {
                                    string[] type = FileUpload.FileName.Split('.');
                                    string file_type = type[type.Length - 1];
                                    if (type.Length >= 2 && (file_type != "txt" || file_type != "htm" || file_type != "html" || file_type != "xml" || file_type != "xhtml"))
                                    {
                                        string path = Server.MapPath("~/NoticeFiles/" + id);
                                        fname = FileUpload.FileName;
                                        if (!Directory.Exists(path))
                                        {
                                            Directory.CreateDirectory(path);
                                        }
                                        FileUpload.SaveAs(path + "//" + fname);
                                        fj = !fj;
                                    }
                                }
                            }
                        }

                        Notice new_notice = new Notice();
                        new_notice.GGTM = id;
                        new_notice.GGBT = ggbt;
                        new_notice.DXLX = dxlx;
                        new_notice.GGDX = ggdx;
                        new_notice.GGNR = ggnr;
                        new_notice.FJ = fj;
                        new_notice.LKR = lkr;
                        new_notice.LKSJ = DateTime.Now;
                        new_notice.GLYTM = Page.User.Identity.Name;
                        cde.Notice.Add(new_notice);

                        if (cde.SaveChanges() > 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('新建成功!');location.href = 'NoticeManagement.aspx';", true);
                        }
                    }
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtGGBT.Text = "";
            txtGGDX.Text = "";
            txtGGNR.Text = "";
            txtLKR.Text = "";
            dropDXLX.SelectedIndex = 0;
        }
    }
}