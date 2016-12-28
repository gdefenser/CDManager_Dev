using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.Core;
using System.Security.Principal;
using System.Web.Security;
using System.IO;
using CDManagerLibrary.FTP.Serv_uAdvCon;
namespace CDManager_Dev4.Account.Profile
{
    public partial class UpdatePassword : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string tm = Page.User.Identity.Name;
                if (String.IsNullOrEmpty(tm)) { AuthenticationError(); }
            }
        }

        //更新密码
        protected void btnClick_Click(object sender, EventArgs e)
        {
            string id = Page.User.Identity.Name;
            string type = (Context.User.Identity as FormsIdentity).Ticket.UserData.Split(',')[0];
            string oldPassword = txtOldPassword.Text;
            string newPassword = txtNewPassword.Text;
            string c_newPassword = txtConfirmNewPassword.Text;

            if (String.IsNullOrEmpty(oldPassword))
            {
                valOldPassword.IsValid = false;
                valOldPassword.ErrorMessage = "请输入旧密码";
            }
            else if (String.IsNullOrEmpty(newPassword))
            {
                valNewPassword.IsValid = false;
                valNewPassword.ErrorMessage = "请输入新密码";
            }
            else if (String.IsNullOrEmpty(c_newPassword))
            {
                valConfirmNewPassword.IsValid = false;
                valConfirmNewPassword.ErrorMessage = "请输入确认新密码";
            }
            else
            {
                try
                {
                    if (type == "1")
                    {
                        Reader reader = cde.Reader.Find(id);
                        string old_pwd = reader.MM;
                        if (old_pwd != oldPassword)
                        {
                            valOldPassword.IsValid = false;
                            valOldPassword.ErrorMessage = "旧密码输入错误";
                        }
                        else
                        {
                            if (newPassword == c_newPassword)
                            {
                                reader.MM = newPassword;
                                if (cde.SaveChanges() > 0) { SuccessRedirect_Front(false); }
                            }
                            else
                            {
                                valConfirmNewPassword.IsValid = false;
                                valConfirmNewPassword.ErrorMessage = "请确认两次输入新密码一致";
                            }
                        }
                    }
                    else if (type == "2" || type == "3")
                    {
                        Admin admin = cde.Admin.First(a => a.GLYTM == id);
                        string old_pwd = admin.MM;
                        if (old_pwd != oldPassword)
                        {
                            valOldPassword.IsValid = false;
                            valOldPassword.ErrorMessage = "旧密码输入错误";
                        }
                        else
                        {
                            if (newPassword == c_newPassword)
                            {
                                admin.MM = newPassword;
                                if (cde.SaveChanges() > 0)
                                {
                                    //编辑FTP用户
                                    FTPUsers.FTPUsersSoapClient usc = new FTPUsers.FTPUsersSoapClient();
                                    if (usc.UpdatePasswd(id, newPassword))
                                    {
                                        SuccessRedirect_Front(false);
                                    }
                                }
                            }
                            else
                            {
                                valConfirmNewPassword.IsValid = false;
                                valConfirmNewPassword.ErrorMessage = "请确认两次输入新密码一致";
                            }
                        }
                    }
                }
                catch { ErrorRedirect_Front(); }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtOldPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmNewPassword.Text = "";
        }
    }
}