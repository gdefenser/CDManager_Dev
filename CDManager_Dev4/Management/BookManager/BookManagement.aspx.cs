using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.Core;
using System.Drawing;

namespace CDManager_Dev4.Management.BookManager
{
    public partial class BookManagement : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string where = hideWhere.Value;
            if (String.IsNullOrEmpty(where)) { where = "it.ISBN <> ''"; }
            edsBook.Where = where;
        }
        //删除图书
        //try
        //    {
        //        string isbn = e.CommandArgument.ToString();
        //        Book delete_book = cde.Book.First(b => b.ISBN == isbn);
        //        cde.Book.Remove(delete_book);
        //        SaveAndRedirect_Management();
        //    }
        //    catch
        //    {
        //        Response.Redirect("~/Management/Error.aspx");
        //    }
        //查找图书
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string where = "";
            if (String.IsNullOrEmpty(txtISBN.Text))
            {
                where = "it.ISBN <> ''";
            }
            else
            { where = "it.ISBN like '%" + txtISBN.Text.Trim() + "%'"; }
            hideWhere.Value = where;
            edsBook.Where = where;
        }

        protected void btnCD_Command(object sender, CommandEventArgs e)
        {
            string isbn = e.CommandArgument.ToString();
            Response.Redirect("~/Management/CDManager/CDDetail.aspx?ISBN=" + isbn);
        }

        protected void lvBook_ItemDataBound(object sender, ListViewItemEventArgs e)
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

                Button btn = (Button)dataItem.FindControl("btnDelete");
                btn.Attributes.Add("onclick", "javascript:return confirm('你确认要删除图书:" + linkBookDetail.Text + "吗?')");

                Label lblDownloadApply = (Label)dataItem.FindControl("lblDownloadApply");

                string isbn = ((Label)dataItem.FindControl("ISBNLabel")).Text;
                int downlload = cde.DownloadLog.Count(d => d.CD.Book.ISBN == isbn);
                int apply = cde.ApplyLog.Count(a => a.Book.ISBN == isbn);

                lblDownloadApply.Text = downlload + "/" + apply;
                Label lblIsOnline = (Label)dataItem.FindControl("lblIsOnline");
                try
                {
                    int status = Convert.ToInt16(cde.CD.Where(i => i.Book.ISBN == isbn).First().ZXZT);
                    if (status == 1)
                    {
                        lblIsOnline.ForeColor = Color.Red;
                        lblIsOnline.Font.Bold = true;
                        lblIsOnline.Text = "在线";
                    }
                    else { lblIsOnline.Text = "不在线"; }
                }
                catch { lblIsOnline.Text = "不在线"; }
                Label lblTime = (Label)dataItem.FindControl("FFSJLabel");
                lblTime.Text = cde.CD.Where(c => c.Book.ISBN == isbn).Select(c => c.CZSJ).Max().ToString();
                if (String.IsNullOrEmpty(lblTime.Text)) { lblTime.Text = "暂无操作"; }
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

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                string isbn = e.CommandArgument.ToString();
                Book delete_book = cde.Book.First(b => b.ISBN == isbn);
                cde.Book.Remove(delete_book);
                SaveAndRedirect_Management();
            }
            catch
            {
                Response.Redirect("~/Management/Error.aspx");
            }
        }

        protected void btnCDDetail_Command(object sender, CommandEventArgs e)
        {
            string isbn = e.CommandArgument.ToString();
            Response.Redirect("~/Management/CDManager/CDDetail.aspx?ISBN=" + isbn);
        }
    }
}