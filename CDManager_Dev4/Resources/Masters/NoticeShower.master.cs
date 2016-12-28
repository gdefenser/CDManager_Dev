using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.Core;
using System.Drawing;
using System.Web.Security;


namespace CDManager_Dev4.Resources.Masters
{
    public partial class NoticeShower : System.Web.UI.MasterPage
    {
        CDManagerDevEntities cde = CDManagerEntitiesSingleton.GetCDManagerDevEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Notice> list = cde.Notice.Where(n => n.DXLX == "读者" || n.DXLX == "使用教程").OrderByDescending(n => n.LKSJ).ToList();
                List<Notice> listReader = list.Where(n => n.DXLX == "读者").ToList();
                List<Notice> listUsing = list.Where(n => n.DXLX == "使用教程").ToList();

                for (int i = 0; i < 10; i++)
                {
                    if (i >= listReader.Count) { break; }
                    Notice notice = listReader[i];
                    HyperLink link = new HyperLink();
                    link.Text = notice.GGBT + "(" + notice.LKSJ.Value.ToShortDateString() + ")";
                    link.NavigateUrl = "~/NoticeDetail.aspx?GGTM=" + notice.GGTM;
                    link.Visible = true;
                    Literal li_b = new Literal();
                    li_b.Text = "<li>";
                    Literal li_e = new Literal();
                    li_e.Text = "</li>";

                    panelNotice.Controls.Add(li_b);
                    panelNotice.Controls.Add(link);
                    panelNotice.Controls.Add(li_e);
                }

                for (int i = 0; i < 10; i++)
                {
                    if (i >= listUsing.Count) { break; }
                    Notice notice = listUsing[i];
                    HyperLink link = new HyperLink();
                    link.Text = notice.GGBT + "(" + notice.LKSJ.Value.ToShortDateString() + ")";
                    link.NavigateUrl = "~/NoticeDetail.aspx?GGTM=" + notice.GGTM;
                    link.Visible = true;
                    Literal li_b = new Literal();
                    li_b.Text = "<li>";
                    Literal li_e = new Literal();
                    li_e.Text = "</li>";

                    panelUsing.Controls.Add(li_b);
                    panelUsing.Controls.Add(link);
                    panelUsing.Controls.Add(li_e);
                }
            }

            lblIP.Text = Request.UserHostAddress;
            lblBrowser.Text = Request.Browser.Browser + " " + Request.Browser.Version;

            if (Request.IsAuthenticated)
            {
                lblLogin.Text = "已登录";
                lblLogin.ForeColor = Color.Green;
                lblLogin.Font.Bold = false;

                var ticket = Context.User.Identity as FormsIdentity;
                string[] data = ticket.Ticket.UserData.Split(',');
                string roles = data[0];
                if (roles == "1")
                {
                    lblApply.Visible = true;
                    lblDownload.Visible = true;
                    linkApply.Text = cde.ApplyLog.Count(a => a.DZTM == Page.User.Identity.Name).ToString();
                    linkApply.NavigateUrl = "~/Account/Profile/MyApply.aspx";
                    linkDownload.Text = cde.DownloadLog.Count(d => d.CZYTM == Page.User.Identity.Name).ToString();
                    linkDownload.NavigateUrl = "~/Account/Profile/MyDownload.aspx";
                }
            }
        }

        protected void dropLink_SelectedIndexChanged(object sender, EventArgs e)
        {
            string url = "";
            switch (dropLink.SelectedIndex)
            {
                case 1:
                    url = "www.sise.com.cn";
                    break;
                case 2:
                    url = "lib.sise.com.cn";
                    break;
                case 3:
                    url = "my.sise.com.cn";
                    break;
                case 4:
                    url = "my.scse.com.cn";
                    break;
            }

            if (!String.IsNullOrEmpty(url))
            {
                Response.Redirect("http://" + url);
            }
        }
    }
}