using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.Core;
using System.IO;
namespace CDManager_Dev4
{
    public partial class NoticeDetail : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string ggtm = Request.QueryString["GGTM"];
                if (!String.IsNullOrEmpty(ggtm))
                {
                    try
                    {//查找公告明细
                        List<Notice> list = cde.Notice.Where(n => n.DXLX == "读者" || n.DXLX == "使用教程").OrderByDescending(n => n.LKSJ).ToList();
                        int index = list.FindIndex(n => n.GGTM == ggtm);
                        Notice notice = list[index];
                        lblTitle.Text = notice.GGBT;
                        lblLKSJTitle.Text = "发布时间:" + notice.LKSJ.ToString();
                        lblGGDX.Text = notice.GGDX;
                        lblGGNR.Text = notice.GGNR;
                        lblLKR.Text = notice.LKR;
                        lblLKSJ.Text = notice.LKSJ.Value.ToLongDateString();

                        string path = "~/NoticeFiles/" + notice.GGTM;
                        string server_path = Server.MapPath( path);
                        DirectoryInfo dir = new DirectoryInfo(server_path);

                        if (dir.Exists && notice.FJ.Value)
                        {
                            if (dir.GetFiles() != null)
                            {
                                FileInfo fi = dir.GetFiles()[0];
                                panelDownload.Visible = true;
                                linkDownload.Text = fi.Name;
                                linkDownload.NavigateUrl = path + "/" + fi.Name;
                            }
                        }


                        if (notice.DXLX == "读者") { list = list.Where(n => n.DXLX == "读者").ToList(); }
                        else if (notice.DXLX == "使用教程") { list = list.Where(n => n.DXLX == "使用教程").ToList(); }


                        int prev = list.IndexOf(notice) - 1;
                        int next = list.IndexOf(notice) + 1;

                        try
                        {
                            Notice prev_notice = list[prev];
                            linkPrev.Text = prev_notice.GGBT;
                            linkPrev.NavigateUrl = "~/NoticeDetail.aspx?GGTM=" + prev_notice.GGTM;
                        }
                        catch
                        {
                            linkPrev.Text = "没有上一篇";
                        }

                        try
                        {
                            Notice next_notice = list[next];
                            linkNext.Text = next_notice.GGBT;
                            linkNext.NavigateUrl = "~/NoticeDetail.aspx?GGTM=" + next_notice.GGTM;
                        }
                        catch
                        {
                            linkNext.Text = "没有下一篇";
                        }
                    }
                    catch
                    {
                        Response.Redirect("~/Index.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/Index.aspx");
                }
            }
        }
    }
}