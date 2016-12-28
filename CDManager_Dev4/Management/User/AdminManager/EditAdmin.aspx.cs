using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.Core;
using System.IO;
using CDManagerLibrary.FTP.Serv_uAdvCon;
using System.Web.Security;

namespace CDManager_Dev4.Management.User.AdminManager
{
    public partial class EditAdmin : CDPages
    {
        string glytm;
        protected void Page_Load(object sender, EventArgs e)
        {
            glytm = Request.QueryString["GLYTM"];
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(glytm))
                {
                    BindAdmin(glytm);
                }
                else
                { Response.Redirect("~/Management/Error.aspx"); }
            }
        }

        private void BindAdmin(string glytm)
        {
            try
            {
                Admin admin = cde.Admin.First(a => a.GLYTM == glytm);
                lblGLYTM.Text = admin.GLYTM;
                txtXM.Text = admin.XM;

                var ticket = Context.User.Identity as FormsIdentity;
                string[] data = ticket.Ticket.UserData.Split(',');
                string roles = data[0];
                if (data[0] == "3")
                {
                    linkMM.Visible = false;
                    txtMM.Visible = true;
                    txtMM.Text = admin.MM;
                }

                if (!String.IsNullOrEmpty(admin.XB)) { dropXB.SelectedValue = admin.XB; }

                if (admin.YXRQ.Value > DateTime.Now)
                {
                    try
                    {
                        int y = admin.YXRQ.Value.Year - DateTime.Now.Year;
                        dropYXRQ.SelectedValue = y.ToString();
                    }
                    catch { }
                }

                txtYJDW.Text = admin.YJDW;
                txtEJDW.Text = admin.EJDW;
            }
            catch { }
        }

        //重置输入
        protected void btnReset_Click(object sender, EventArgs e)
        {
            BindAdmin(glytm);
        }
        //编辑管理员
        protected void btnEdit_Click(object sender, EventArgs e)
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
                    try
                    {
                        string xb = "";
                        if (dropXB.SelectedIndex > 0) { xb = dropXB.SelectedValue; }

                        DateTime yxrq = new DateTime();
                        if (dropYXRQ.SelectedIndex > 0)
                        { yxrq = DateTime.Now.AddYears(Convert.ToInt16(dropYXRQ.SelectedValue)); }

                        string yjdw = txtYJDW.Text;
                        string ejdw = txtEJDW.Text;

                        Admin edit_admin = cde.Admin.First(a => a.GLYTM == glytm);
                        edit_admin.XM = xm;
                        edit_admin.XB = xb;
                        if (yxrq > DateTime.Now)
                        { edit_admin.YXRQ = yxrq; }
                        edit_admin.YJDW = yjdw;
                        edit_admin.EJDW = ejdw;
                        if (txtMM.Visible && !linkMM.Visible && txtMM.Text != edit_admin.MM)
                        {
                            string mm = txtMM.Text;
                            edit_admin.MM = mm;
                            if (cde.SaveChanges() > 0)
                            {
                                //编辑FTP用户密码
                                FTPUsers.FTPUsersSoapClient usc = new FTPUsers.FTPUsersSoapClient();
                                if (usc.UpdatePasswd(glytm, mm))
                                {
                                    SuccessRedirect_Management(false);
                                }
                            }
                        }
                        else
                        { SaveAndRedirect_Management(); }
                    }
                    catch { ErrorRedirect_Management(); }
                }
            }
        }

        protected void btnStatistics_Click(object sender, EventArgs e)
        {
            string glytm = Request.QueryString["GLYTM"];
            Response.Redirect("~/Management/User/AdminManager/AdminStatistics.aspx?GLYTM=" + glytm);
        }
    }
}