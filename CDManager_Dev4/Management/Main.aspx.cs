using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.Core;
using System.Web.Security;

namespace CDManager_Dev4.Management
{
    public partial class Main : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var ticket = Context.User.Identity as FormsIdentity;
                if (ticket != null && ticket.IsAuthenticated)
                {
                    string[] data = ticket.Ticket.UserData.Split(',');
                    lblAdmin.Text = data[1];
                }
                List<Notice> list = cde.Notice.Where(n => n.DXLX == "管理员").OrderByDescending(n => n.LKSJ).ToList();

                for (int i = 0; i < 10; i++)
                {
                    if (i >= list.Count) { break; }
                    Notice notice = list[i];
                    HyperLink link = new HyperLink();
                    link.Text = notice.GGBT + "(" + notice.LKSJ.Value.ToShortDateString() + ")";
                    link.NavigateUrl = "~/Management/Message/NoticeDetail.aspx?GGTM=" + notice.GGTM;
                    link.Visible = true;
                    Literal li_b = new Literal();
                    li_b.Text = "<li>";
                    Literal li_e = new Literal();
                    li_e.Text = "</li>";

                    panelNotice.Controls.Add(li_b);
                    panelNotice.Controls.Add(link);
                    panelNotice.Controls.Add(li_e);
                }

                int count = cde.ApplyLog.Count(a => a.SQZT == 0);
                linkApply.Text = count.ToString();
                if (count > 0) { linkApply.NavigateUrl = "~/Management/CDManager/ApplyLogList.aspx"; }

                linkOnline.Text = cde.CD.Count(c => c.ZXZT == 1).ToString();
            }
        }
    }
}