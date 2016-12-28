using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.Core;
using CDManagerLibrary.FTP.Serv_uAdvCon;
using System.Web.Security;

namespace CDManager_Dev4.Management.User.AdminManager
{
    public partial class AdminManagement : CDPages
    {
        string roles;
        protected void Page_Load(object sender, EventArgs e)
        {
            var ticket = Context.User.Identity as FormsIdentity;
            if (ticket != null && ticket.IsAuthenticated)
            {
                string[] data = ticket.Ticket.UserData.Split(',');
                roles = data[0];

                if (roles != "3")
                {
                    AuthenticationError();
                }
            }
        }
        //删除操作
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                string glytm = e.CommandArgument.ToString();
                Admin delete_admin = cde.Admin.First(a => a.GLYTM == glytm);
                cde.Admin.Remove(delete_admin);
                if (cde.SaveChanges() > 0)
                {
                    //编辑FTP用户
                    FTPUsers.FTPUsersSoapClient usc = new FTPUsers.FTPUsersSoapClient();
                    if (usc.DeleteUser(glytm))
                    {
                        SuccessRedirect_Management(false);
                    }
                }
            }
            catch
            {
                ErrorRedirect_Management();
            }
        }
        //查找管理员
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtGLYTM.Text))
            {
                if (roles == "2") { edsAdmin.Where = "it.GLYLX = 2 and it.GLYTM <> ''"; }
                else if (roles == "3") { edsAdmin.Where = "it.GLYTM <> ''"; }
            }
            else
            {
                if (roles == "2") { edsAdmin.Where = "it.GLYLX = 2 and it.GLYTM like '%" + txtGLYTM.Text + "%'"; }
                else if (roles == "3") { edsAdmin.Where = "it.GLYTM like '%" + txtGLYTM.Text + "%'"; }                
            }
        }

        protected void btnGo_Command(object sender, CommandEventArgs e)
        {
            DataPager dpAdmin = (DataPager)lvAdmin.FindControl("dpAdmin");
            TextBox txtPage = (TextBox)dpAdmin.Controls[4].FindControl("txtPage");

            try
            {
                int max = dpAdmin.TotalRowCount < dpAdmin.PageSize ? 1 : dpAdmin.TotalRowCount / dpAdmin.PageSize + 1;
                int select = Convert.ToInt32(txtPage.Text);
                if (select < 1) { select = 1; }
                else if (select > max) { select = max; }

                int start_row = (select - 1) * dpAdmin.PageSize;
                dpAdmin.SetPageProperties(start_row, dpAdmin.PageSize, false);
                lvAdmin.DataBind();
            }
            catch
            { }
        }

        protected void lvAdmin_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                HyperLink linkGLYTM = (HyperLink)dataItem.FindControl("linkGLYTM");
                Button btn = (Button)dataItem.FindControl("btnDelete");
                btn.Attributes.Add("onclick", "javascript:return confirm('你确认要注销管理员:" + linkGLYTM.Text + "吗?')");

                Label lblGLYLX = (Label)dataItem.FindControl("lblGLYLX");
                if (lblGLYLX.Text == "2") { lblGLYLX.Text = "图书管理员"; }
                else if (lblGLYLX.Text == "3") 
                {
                    btn.Visible = false;
                    lblGLYLX.Text = "系统管理员"; 
                }
            }
        }
    }
}