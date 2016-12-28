using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.Core;

namespace CDManager_Dev4.Management.CDManager
{
    public partial class ApplyLogList : CDPages
    {
        //protected override void OnLoad(EventArgs e)
        //{
        //    Response.Cache.SetNoStore();
        //    base.OnLoad(e);
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string isbn = Request.QueryString["ISBN"];
                if (String.IsNullOrEmpty(isbn))
                {
                    lvApplyLogList.Visible = true;
                }
                else
                {
                    try
                    {
                        panelLogDetail.Visible = true;
                        List<ApplyLog> list = cde.ApplyLog.Where(a => a.Book.ISBN == isbn && a.SQZT == 0).ToList();
                        ApplyLog apply = list.First();
                        lblCount.Text = "截至" + DateTime.Now + "图书:" + apply.Book.ZTM + "(" + apply.Book.ISBN + ")的光盘资源申请数: " + list.Count;
                    }
                    catch
                    { Response.Redirect("~/Management/Error.aspx", false); }
                }
            }
        }

        protected void btnBan_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect("~/Management/CDManager/BanApply.aspx?ISBN=" + e.CommandArgument.ToString());
        }

        protected void btnUpload_Command(object sender, CommandEventArgs e)
        {
            Upload(e.CommandArgument.ToString());
        }

        protected void btnBanDetail_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Management/CDManager/BanApply.aspx?ISBN=" + Request.QueryString["ISBN"]);
        }

        protected void btnUploadDetail_Click(object sender, EventArgs e)
        {
            Upload(Request.QueryString["ISBN"]);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Management/CDManager/ApplyLogList.aspx");
        }

        //光盘明细
        private void Upload(string isbn)
        { Response.Redirect("~/Management/CDManager/CDDetail.aspx?ISBN=" + isbn); }

        protected void btnBanIP_Command(object sender, CommandEventArgs e)
        {
            Session["JZIP"] = e.CommandArgument.ToString();
            Response.Redirect("~/Management/Security/BanIPManagement.aspx");
        }

        protected void btnListGo_Command(object sender, CommandEventArgs e)
        {
            DataPager dpApply = (DataPager)lvApplyLogList.FindControl("dpApply");
            TextBox txtPage = (TextBox)dpApply.Controls[4].FindControl("txtPage");

            try
            {
                int max = dpApply.TotalRowCount < dpApply.PageSize ? 1 : dpApply.TotalRowCount / dpApply.PageSize + 1;
                int select = Convert.ToInt32(txtPage.Text);
                if (select < 1) { select = 1; }
                else if (select > max) { select = max; }

                int start_row = (select - 1) * dpApply.PageSize;
                dpApply.SetPageProperties(start_row, dpApply.PageSize, false);
                lvApplyLogList.DataBind();
            }
            catch
            { }
        }

        protected void lvApplyLogList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //如果是绑定数据行 
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                HyperLink linkCountApply = (HyperLink)dataItem.FindControl("linkCountApply");
                Label lblISBN = (Label)dataItem.FindControl("lblISBN");
                linkCountApply.Text = cde.ApplyLog.Count(a => a.Book.ISBN == lblISBN.Text && a.SQZT == 0).ToString();
            }
        }

        protected void btnDetailGo_Command(object sender, CommandEventArgs e)
        {
            DataPager dpApply = (DataPager)lvApplyLogDetail.FindControl("dpApply");
            TextBox txtPage = (TextBox)dpApply.Controls[4].FindControl("txtPage");

            try
            {
                int max = dpApply.TotalRowCount < dpApply.PageSize ? 1 : dpApply.TotalRowCount / dpApply.PageSize + 1;
                int select = Convert.ToInt32(txtPage.Text);
                if (select < 1) { select = 1; }
                else if (select > max) { select = max; }

                int start_row = (select - 1) * dpApply.PageSize;
                dpApply.SetPageProperties(start_row, dpApply.PageSize, false);
                lvApplyLogList.DataBind();
            }
            catch
            { }
        }
    }
}