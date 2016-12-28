using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.Configuration;
using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.Core;
using System.Drawing;
using CDManagerLibrary.XML;

namespace CDManager_Dev4.Resources.UserControl
{
    public partial class LoginFront : System.Web.UI.UserControl
    {
        CDManagerDevEntities cde = CDManagerEntitiesSingleton.GetCDManagerDevEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                var ticket = Context.User.Identity as FormsIdentity;
                string status = WebConfigurationManager.AppSettings["IsEnable"];

                if (ticket != null && ticket.IsAuthenticated)
                {

                    string cache = "";
                    try { cache = Cache[Page.User.Identity.Name].ToString(); }
                    catch { }
                    if (!String.IsNullOrEmpty(cache) && cache == Request.UserHostAddress)
                    {
                        string[] data = ticket.Ticket.UserData.Split(',');
                        string roles = data[0];

                        if (roles == "1" || roles == "2" || roles == "3")//用户角色检查
                        {
                            if (status == "0")//如果前台已关闭
                            {
                                string currentFilePath = HttpContext.Current.Request.FilePath;
                                string toppic = currentFilePath.Substring(currentFilePath.LastIndexOf("/") + 1);
                                if (toppic != "SpecialLogin.aspx" && roles == "1")//注销已登录读者
                                {
                                    FormsAuthentication.SignOut();
                                    try { Cache.Remove(Page.User.Identity.Name); }
                                    catch { }
                                    Response.Redirect("~/Account/SpecialLogin.aspx");
                                }
                                else
                                {
                                    if (roles == "2")
                                    {
                                        lblLoginTitle.Text = "图书管理员：" + data[1];
                                        linkLoginManager.Visible = true;
                                        lblLoginAccount.Text = Page.User.Identity.Name;
                                    }
                                    else if (roles == "3")
                                    {
                                        lblLoginTitle.Text = "系统管理员：" + data[1];
                                        linkLoginManager.Visible = true;
                                        lblLoginAccount.Text = Page.User.Identity.Name;
                                    }

                                    int apply_count = cde.ApplyLog.Count(a => a.SQZT == 0);
                                    if (apply_count > 0)
                                    {
                                        linkApplyLog.Text = "[未处理申请" + apply_count + "条]";
                                        linkApplyLog.Font.Bold = true;
                                        linkApplyLog.ForeColor = Color.DarkSeaGreen;
                                    }
                                    linkApplyLog.Visible = true;

                                    linkLoginMessage.NavigateUrl = "~/Management/Message/ReceivedMessage.aspx";
                                    linkLogout.Attributes["onclick"] = "javascript:return confirm('" + Page.User.Identity.Name + ",您确定退出吗？');";
                                    lblStatus.Visible = true;
                                    panelLogin.Visible = true;
                                    panelNoLogin.Visible = false;
                                }
                            }
                            else
                            {
                                if (roles == "1")
                                {
                                    lblLoginTitle.Text = "读者：" + data[1];
                                    lblLoginAccount.Text = Page.User.Identity.Name;
                                    linkLoginMessage.NavigateUrl = "~/Account/Profile/MyMessage.aspx";
                                    linkLoginProfile.Visible = true;
                                }
                                else if (roles == "2" || roles == "3")
                                {
                                    if (roles == "2") { lblLoginTitle.Text = "图书管理员：" + data[1]; }
                                    else if (roles == "3") { lblLoginTitle.Text = "系统管理员：" + data[1]; }
                                    linkLoginManager.Visible = true;
                                    lblLoginAccount.Text = Page.User.Identity.Name;
                                    linkLoginMessage.NavigateUrl = "~/Management/Message/ReceivedMessage.aspx";

                                    int apply_count = cde.ApplyLog.Count(a => a.SQZT == 0);
                                    if (apply_count > 0)
                                    {
                                        linkApplyLog.Text = "[未处理申请" + apply_count + "条]";
                                        linkApplyLog.Font.Bold = true;
                                        linkApplyLog.ForeColor = Color.DarkSeaGreen;
                                    }
                                    linkApplyLog.Visible = true;
                                }

                                linkLogout.Attributes["onclick"] = "javascript:return confirm('" + Page.User.Identity.Name + ",您确定退出吗？');";
                                panelLogin.Visible = true;
                                panelNoLogin.Visible = false;
                            }

                            int msg_count = cde.Message.Count(m => m.SXRTM == ticket.Name && m.YD == false);
                            if (msg_count > 0)
                            {
                                linkLoginMessage.Text = "未读消息" + msg_count + "条";
                                linkLoginMessage.Font.Bold = true;
                                linkLoginMessage.ForeColor = Color.DarkOrange;
                            }
                        }
                    }
                    else
                    {
                        FormsAuthentication.SignOut();
                        Response.Redirect("~/Index.aspx");

                    }
                }
                else
                {
                    if (status == "0")
                    {
                        string currentFilePath = HttpContext.Current.Request.FilePath;
                        string toppic = currentFilePath.Substring(currentFilePath.LastIndexOf("/") + 1);
                        if (toppic != "SpecialLogin.aspx")
                        {
                            Response.Redirect("~/Account/SpecialLogin.aspx");
                        }
                    }
                    else
                    {
                        panelLogin.Visible = false;
                        panelNoLogin.Visible = true;
                    }
                }
            }
        }


        protected void linkLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            try { Cache.Remove(Page.User.Identity.Name); }
            catch { }
            Response.Redirect("~/Index.aspx");
        }
    }
}