using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.Core;
using CDManagerLibrary.FTP.Serv_uAdvCon;
using System.IO;
using System.Web.Security;

namespace CDManager_Dev4.Management.User.AdminManager
{
    public partial class NewAdmin : CDPages
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
                    if (roles != "3")
                    {
                        AuthenticationError();
                    }
                }
            }
        }
        //新建管理员
        protected void btnNew_Click(object sender, EventArgs e)
        {
            string glytm = txtGLYTM.Text;
            if (String.IsNullOrEmpty(glytm))
            {
                valGLYTM.IsValid = false;
                valGLYTM.ErrorMessage = "请输入管理员条码";
            }
            else if (cde.Admin.Count(a => a.GLYTM == glytm) > 0)
            {
                valGLYTM.IsValid = false;
                valGLYTM.ErrorMessage = "管理员账号" + glytm + "已存在";
            }
            else
            {
                string xm = txtXM.Text;
                if (String.IsNullOrEmpty(xm))
                {
                    valXM.IsValid = false;
                }
                else
                {
                    if (dropYXRQ.SelectedIndex < 1)
                    {
                        valYXRQ.IsValid = false;
                    }
                    else
                    {
                        string mm = txtMM.Text;
                        if (String.IsNullOrEmpty(mm))
                        { mm = "000000"; }

                        string xb = "";
                        if (dropXB.SelectedIndex > 0) { xb = dropXB.SelectedValue; }

                        DateTime yxrq = new DateTime();
                        if (dropYXRQ.SelectedIndex > 0)
                        { yxrq = DateTime.Now.AddYears(Convert.ToInt16(dropYXRQ.SelectedValue)); }

                        string yjdw = txtYJDW.Text;
                        string ejdw = txtEJDW.Text;

                        Admin new_admin = new Admin();
                        new_admin.GLYTM = glytm;
                        new_admin.XM = xm;
                        new_admin.MM = mm;
                        new_admin.XB = xb;
                        if (yxrq > DateTime.Now)
                        { new_admin.YXRQ = yxrq; }
                        new_admin.YJDW = yjdw;
                        new_admin.EJDW = ejdw;
                        new_admin.GLYLX = 2;
                        new_admin.FFSJ = DateTime.Now;
                        new_admin.FFCZY = Page.User.Identity.Name;
                        cde.Admin.Add(new_admin);

                        if (cde.SaveChanges() > 0)
                        {
                            //新建FTP用户
                            FTPUsers.FTPUsersSoapClient usc = new FTPUsers.FTPUsersSoapClient();
                            if (usc.NewUser(glytm, mm))
                            {
                                SuccessRedirect_Management(false);
                            }
                        }
                        else
                        { ErrorRedirect_Management(); }
                    }
                }
            }
        }
        //重置输入
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtGLYTM.Text = "";
            txtXM.Text = "";
            txtMM.Text = "";
            dropXB.SelectedIndex = 0;
            dropYXRQ.SelectedIndex = 0;
            txtYJDW.Text = "";
            txtEJDW.Text = "";
        }
    }
}