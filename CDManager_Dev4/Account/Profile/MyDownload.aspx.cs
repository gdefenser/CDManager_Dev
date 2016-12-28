using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.Core;
using System.Drawing;

namespace CDManager_Dev4.Account.Profile
{
    public partial class MyDownload : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string czytm = Page.User.Identity.Name;
                if (!String.IsNullOrEmpty(czytm))
                {
                    Session["CZYTM"] = czytm;
                }
                else
                { AuthenticationError(); }
            }
        }

        protected void gvMyDownload_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //如果是绑定数据行 
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    //强制转换LiteralControl
                    LiteralControl lite = (LiteralControl)e.Row.Cells[2].Controls[0];
                    HyperLink linkBookDetail = (HyperLink)lite.FindControl("linkBookDetail");
                    if (linkBookDetail.Text.Length > 30)
                    {
                        linkBookDetail.Text = linkBookDetail.Text.Substring(0, 30) + "...";
                    }
                }
            }
        }

        protected void btnGo_Command(object sender, CommandEventArgs e)
        {
            DataPager dpMyDownload = (DataPager)lvMyDownload.FindControl("dpMyDownload");
            TextBox txtPage = (TextBox)dpMyDownload.Controls[4].FindControl("txtPage");

            try
            {
                int max = dpMyDownload.TotalRowCount < dpMyDownload.PageSize ? 1 : dpMyDownload.TotalRowCount / dpMyDownload.PageSize + 1;
                int select = Convert.ToInt32(txtPage.Text);
                if (select < 1) { select = 1; }
                else if (select > max) { select = max; }

                int start_row = (select - 1) * dpMyDownload.PageSize;
                dpMyDownload.SetPageProperties(start_row, dpMyDownload.PageSize, false);
                lvMyDownload.DataBind();
            }
            catch
            { }
        }

        protected void lvMyDownload_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //如果是绑定数据行 
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                HyperLink linkBookDetail = (HyperLink)dataItem.FindControl("linkZTM");
                linkBookDetail.ToolTip = linkBookDetail.Text;
                if (linkBookDetail.Text.Length > 30)
                {
                    linkBookDetail.Text = linkBookDetail.Text.Substring(0, 30) + "...";
                }

                string isbn = ((Label)dataItem.FindControl("ISBNLabel")).Text;

                Label lblIsOnline = (Label)dataItem.FindControl("lblIsOnline");
                try
                {
                    int status = Convert.ToInt16(cde.CD.Where(i => i.ISBN == isbn).First().ZXZT);
                    if (status == 1)
                    {
                        lblIsOnline.ForeColor = Color.Red;
                        lblIsOnline.Font.Bold = true;
                        lblIsOnline.Text = "在线";
                    }
                    else { lblIsOnline.Text = "不在线"; }
                }
                catch { lblIsOnline.Text = "不在线"; }
            }
        }
    }
}