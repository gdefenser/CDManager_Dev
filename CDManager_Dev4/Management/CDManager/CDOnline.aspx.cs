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
    public partial class CDOnline : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString.Count > 0)
            {
                string czsj = Request.QueryString["CZSJ"];
                string isbn = Request.QueryString["ISBN"];
                string cdxh = Request.QueryString["CDXH"];
                string where = "";

                if (!String.IsNullOrEmpty(czsj))
                {
                    where += " and SqlServer.DATEDIFF('day',cd.CZSJ,SqlServer.GETDATE())>" + czsj;
                    if (!IsPostBack) { dropUploaded.SelectedValue = czsj; }
                }
                if (!String.IsNullOrEmpty(isbn))
                {
                    where += " and cd.ISBN like '%" + isbn + "%'";
                    if (!IsPostBack) { txtISBN.Text = isbn; }
                }
                if (!String.IsNullOrEmpty(cdxh))
                {
                    where += " and cd.CDXH like '%" + cdxh + "%'";
                    if (!IsPostBack) { txtCDXH.Text = cdxh; }
                }

                if (!String.IsNullOrEmpty(where))
                {
                    string command = edsBook.CommandText;
                    edsBook.CommandText = command.Substring(0, command.Length - 1) + where + ")";
                    lvBook.DataBind();
                }
            }
        }

        protected void gvBook_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //如果是绑定数据行 删除确认框
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    LiteralControl lite;//强制转换LiteralControl
                    lite = (LiteralControl)e.Row.Cells[3].Controls[0];
                    Label lblCDCount = (Label)lite.FindControl("lblCDCount");
                    string isbn = e.Row.Cells[1].Text;
                    List<CD> list = cde.CD.Where(c => c.ISBN == isbn).ToList();
                    lblCDCount.Text = list.Count.ToString();
                    lite = (LiteralControl)e.Row.Cells[4].Controls[0];
                    Label lblOnlineCount = (Label)lite.FindControl("lblOnlineCount");
                    lblOnlineCount.Text = list.Count(c => c.ZXZT == 1).ToString();
                    lite = (LiteralControl)e.Row.Cells[5].Controls[0];
                    Label lblTime = (Label)lite.FindControl("lblTime");
                    lblTime.Text = list.Max(c => c.CZSJ).ToString();
                }
            }
        }

        protected void btnCDDetail_Command(object sender, CommandEventArgs e)
        {
            string isbn = e.CommandArgument.ToString();
            Response.Redirect("~/Management/CDManager/CDDetail.aspx?ISBN=" + isbn);
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string query = "";
            if (dropUploaded.SelectedIndex > 0) { query += "&CZSJ=" + dropUploaded.SelectedValue; }
            if (!String.IsNullOrEmpty(txtISBN.Text)) { query += "&ISBN=" + txtISBN.Text; }
            if (!String.IsNullOrEmpty(txtCDXH.Text)) { query += "&CDXH=" + txtCDXH.Text; }

            if (!String.IsNullOrEmpty(query))
            {
                query = query.Substring(1);
                Response.Redirect("~/Management/CDManager/CDOnline.aspx?" + query);
            }
            else
            { Response.Redirect("~/Management/CDManager/CDOnline.aspx"); }
        }

        protected void lvBook_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //如果是绑定数据行 
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                Label lblCount = (Label)dataItem.FindControl("lblCount");
                Label lblISBN = (Label)dataItem.FindControl("lblISBN");
                List<CD> list = cde.CD.Where(c => c.ISBN == lblISBN.Text).ToList();
                lblCount.Text = list.Count.ToString();
                Label lblOnlineCount = (Label)dataItem.FindControl("lblOnlineCount");
                lblOnlineCount.Text = list.Count(c => c.ZXZT == 1).ToString();
                Label lblTime = (Label)dataItem.FindControl("lblTime");
                lblTime.Text = list.Max(c => c.CZSJ).ToString();
            }
        }

        protected void btnGo_Command(object sender, CommandEventArgs e)
        {
            DataPager dpBook = (DataPager)lvBook.FindControl("dpBook");
            TextBox txtPage = (TextBox)dpBook.Controls[4].FindControl("txtPage");

            try
            {
                int max = dpBook.TotalRowCount < dpBook.PageSize ? 1 : dpBook.TotalRowCount / dpBook.PageSize + 1;
                int select = Convert.ToInt32(txtPage.Text);
                if (select < 1) { select = 1; }
                else if (select > max) { select = max; }

                int start_row = (select - 1) * dpBook.PageSize;
                dpBook.SetPageProperties(start_row, dpBook.PageSize, false);
                lvBook.DataBind();
            }
            catch
            { }
        }
    }
}