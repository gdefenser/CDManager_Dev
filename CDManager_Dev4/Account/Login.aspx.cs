using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.Core.Login;
using CDManagerLibrary.Core;

namespace CDManager_Dev4.Account
{
    public partial class Login : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.User.Identity.IsAuthenticated)
            {
                if (!IsPostBack)
                {
                    string Url = "";
                    try
                    { Url = HttpContext.Current.Request.UrlReferrer.ToString(); }
                    catch { }
                    if (Url.IndexOf("Account/AuthenticationError.aspx") < 0)
                    { Session["Back_URL"] = Url.ToString(); }
                }
            }
            else
            { Response.Redirect("~/Index.aspx"); }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
        }

        //登录按钮
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string ip = Request.UserHostAddress;
            var ban = cde.BanIP.Where(b => b.JZIP == ip);
            if (ban.Count() > 0)//判断IP是否被禁止
            {
                BanIP update_ban = ban.First();
                if (update_ban.JSSJ.Value > DateTime.Now)
                {
                    loginValidator.IsValid = false;
                    loginValidator.ErrorMessage = "抱歉!IP:" + username + "被系统禁止登录!解禁时间:" + ban.First().JSSJ.Value;
                    return;
                }
                else
                {
                    cde.BanIP.Remove(update_ban);
                    cde.SaveChanges();
                }
            }


            if (String.IsNullOrEmpty(username.Trim())) { usernameValidator.IsValid = false; }
            else if (String.IsNullOrEmpty(password.Trim())) { passwordValidator.IsValid = false; }
            else
            {
                //单点登录检验
                string cache = Convert.ToString(Cache[username]);
                string val = UserAuthentication.ValidateUser(username, password, ip);
                if (!String.IsNullOrEmpty(val))
                {
                    if (val.IndexOf("no_reader") >= 0)//检查用户是否禁止登录
                    {
                        loginValidator.IsValid = false;
                        loginValidator.ErrorMessage = "抱歉!读者" + username + "被系统禁止登录!解禁时间:" + val.Substring(9);
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
                        if (!String.IsNullOrEmpty(cache))
                        { Cache.Remove(username); }
                        FormsAuthenticationTicket ticket;
                        if (val.Substring(0, 1) == "1")//读者票证
                        {
                            //FormsAuthentication认证
                            //生成票证
                            ticket = new FormsAuthenticationTicket(
                            1,//票证版本
                            username,//票证标识
                            DateTime.UtcNow,//票证登录时间

                            DateTime.UtcNow.Add(FormsAuthentication.Timeout),//票证过期时间
                            false,//票证不永久保存
                            val//票证角色
                            );


                            Cache.Insert(
                                username,
                                ip,
                                null,
                                DateTime.MaxValue,
                                FormsAuthentication.Timeout,
                                System.Web.Caching.CacheItemPriority.NotRemovable,
                                null);
                        }
                        else//管理员票证
                        {
                            ticket = new FormsAuthenticationTicket(
                                1,
                                username,
                                DateTime.UtcNow,
                                DateTime.UtcNow.AddHours(3),
                                false,
                                val);
                            Cache.Insert(
                                username,
                                ip,
                                null,
                                DateTime.MaxValue,
                                new TimeSpan(0, 3, 0, 0, 0),
                                System.Web.Caching.CacheItemPriority.NotRemovable,
                                null);
                        }


                        //加密票证并保存至用户Cookie
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                        Response.Cookies.Add(cookie);

                        try
                        {
                            //单点登录检验
                            string back_url = Session["Back_URL"].ToString();
                            if (String.IsNullOrEmpty(back_url))
                            {
                                if (!String.IsNullOrEmpty(cache))
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "load", "alert('该用户名已在IP:" + cache + "登录，此操作将会强制使用用户名" + username + "在本机登录，若非本人操作请及时修改密码！');location.href='../Index.aspx'", true);
                                }
                                else { Response.Redirect(FormsAuthentication.GetRedirectUrl(username, false)); }
                            }
                            else
                            {
                                string[] urls = back_url.Split('/');
                                if (!String.IsNullOrEmpty(cache))
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "load", "alert('该用户名已在IP:" + cache + "登录，此操作将会强制使用用户名" + username + "在本机登录，若非本人操作请及时修改密码！');location.href='../" + urls[3] + "'", true);
                                }
                                else { Response.Redirect(back_url, false); }
                            }
                        }
                        catch
                        { Response.Redirect("~/Index.aspx"); }

                    }
                }
                else
                {

                    loginValidator.IsValid = false;
                    loginValidator.ErrorMessage = "用户名或密码错误!";
                }
            }
        }
    }
}