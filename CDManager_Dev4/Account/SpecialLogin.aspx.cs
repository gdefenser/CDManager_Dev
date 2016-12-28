using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.Core;
using CDManagerLibrary.EntityFramework;
using System.Web.Configuration;
using System.Web.Security;
using CDManagerLibrary.Core.Login;
using System.IO;

namespace CDManager_Dev4.Account
{
    public partial class SpecialLogin : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Url = "";
                try
                { Url = HttpContext.Current.Request.UrlReferrer.ToString(); }
                catch { }
                if (Url.IndexOf("Account/AuthenticationError.aspx") < 0)
                { Session["Back_URL"] = Url.ToString(); }

                string status = WebConfigurationManager.AppSettings["IsEnable"];
                if (status == "1") { Response.Redirect("~/Index.aspx"); }
                try
                {
                    Notice notice = cde.Notice.First(n => n.DXLX == "前台关闭公告");
                    lblGGDX.Text = notice.GGDX;
                    lblGGNR.Text = notice.GGNR;
                    lblLKR.Text = notice.LKR;
                    lblTitle.Text = notice.GGBT;
                    lblLKSJ.Text = notice.LKSJ.ToString();
                    panelNotice.Visible = true;

                    string path = "/NoticeFiles/" + notice.GGTM;
                    string server_path = Server.MapPath("~" + path);
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
                }
                catch
                { }


            }
        }

        //登录
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (String.IsNullOrEmpty(username.Trim())) { usernameValidator.IsValid = false; }
            else if (String.IsNullOrEmpty(password.Trim())) { passwordValidator.IsValid = false; }
            else
            {
                string val = UserAuthentication.ValidateUser(username, password, Request.UserHostAddress);
                if (val.Substring(0, 1) == "1" || val.IndexOf("no_reader") >= 0)
                {
                    loginValidator.IsValid = false;
                    loginValidator.ErrorMessage = "抱歉,非管理员用户暂时不能登录";
                }
                else if (val == "no_account")//检查是否用户名或密码错误
                {
                    loginValidator.IsValid = false;
                    loginValidator.ErrorMessage = "用户名或密码错误!";
                }
                else if (val == "no_yxrq")
                {
                    loginValidator.IsValid = false;
                    loginValidator.ErrorMessage = "你的登录凭证已超过有效日期";
                }
                else
                {
                    //FormsAuthentication认证
                    //生成票证
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1,//票证版本
                        username,//票证标识
                        DateTime.Now,//票证登录时间
                        DateTime.Now.Add(FormsAuthentication.Timeout),//票证过期时间
                        false,//票证不永久保存
                        val//票证角色
                        );

                    //加密票证并保存至用户Cookie
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                    Response.Cookies.Add(cookie);
                    string back_url = Session["Back_URL"].ToString();
                    if (String.IsNullOrEmpty(lblURL.Text))
                    { Response.Redirect(FormsAuthentication.GetRedirectUrl(username, false)); }
                    else
                    { Response.Redirect(back_url, false); }
                }
            }

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
        }
    }
}