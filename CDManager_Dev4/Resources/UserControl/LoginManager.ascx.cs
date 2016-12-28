using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.Configuration;
using CDManagerLibrary.XML;

namespace CDManager_Dev4.Resources.UserControl
{
    public partial class LoginManager : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var ticket = Context.User.Identity as FormsIdentity;
                if (ticket != null && ticket.IsAuthenticated)
                {
                    string[] data = ticket.Ticket.UserData.Split(',');
                    string roles = data[0];
                    if (roles == "2")
                    {
                        lblLoginAccount.Text = Page.User.Identity.Name + "(图书管理员)";
                    }
                    else if (roles == "3")
                    {
                        lblLoginAccount.Text = Page.User.Identity.Name + "(系统管理员)";                      
                    }
                    else { Response.Redirect("~/Account/AuthenticationError.aspx"); }

                    string settime = XMLHelper.getAppSettingValue("SetTime");
                    if (!String.IsNullOrEmpty(settime))
                    {
                        lblSetTime.Text = "前台将于" + settime + "关闭";
                        lblSetTime.Visible = true;
                    }

                    string status = WebConfigurationManager.AppSettings["IsEnable"];
                    if (status == "0") { lblStatus.Visible = true; }//提示前台已关闭
                    linkEditAdmin.NavigateUrl = "~/Management/User/AdminManager/EditAdmin.aspx?GLYTM=" + Page.User.Identity.Name;
                    linkLogout.Attributes["onclick"] = "javascript:return confirm('" + Page.User.Identity.Name + ",您确定退出吗？');";
                }
                else { Response.Redirect("~/Account/AuthenticationError.aspx"); }
            }
        }

        //退出登录
        protected void linkLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/Index.aspx");
        }
    }
}