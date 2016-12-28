using CDManagerLibrary.Core;
using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.Excel;
using CDManagerLibrary.XML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDManager_Dev4.Management.CDManager
{
    public partial class DownloadLogList : CDPages
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string from = Request.QueryString["From"];
            string to = Request.QueryString["To"];
            string book = Request.QueryString["Book"];
            string cd = Request.QueryString["CD"];
            string czy = Request.QueryString["CZY"];

            if (!IsPostBack)
            {
                Session["para"] = from + "_" + to + "_" + book + "_" + cd + "_" + czy;
            }

            string where = "";

            if (!String.IsNullOrEmpty(from))
            {
                string[] froms = from.Split('-');
                where += " and it.XZSJ >= CreateDateTime(" + froms[0] + "," + froms[1] + "," + froms[2] + ",0,0,0)";
                lblDate.Text = "大于" + from;
            }
            if (!String.IsNullOrEmpty(to))
            {
                string[] tos = to.Split('-');
                where += " and it.XZSJ <= CreateDateTime(" + tos[0] + "," + tos[1] + "," + tos[2] + ",0,0,0)";
                if (!String.IsNullOrEmpty(lblDate.Text))
                {
                    string lbl = lblDate.Text;
                    lbl += " 小于" + to;
                    lblDate.Text = lbl;
                }
                else { lblDate.Text = "小于" + to; }
            }
            if (!String.IsNullOrEmpty(book))
            {
                where += " and it.CD.Book.ZTM like '%" + book + "%' or it.CD.Book.ISBN like '%" + book + "%'";
                lblBook.Text = book;
            }
            if (!String.IsNullOrEmpty(cd))
            {
                where += " and it.CD.CDMC like '%" + cd + "%' or it.CD.CDXH like '%" + cd + "%'";
                lblCD.Text = cd;
            }
            if (!String.IsNullOrEmpty(czy))
            {
                where += " and it.CZYTM like '%" + czy + "%'";
                lblCZY.Text = czy;
            }

            if (String.IsNullOrEmpty(where)) { where = " and it.XZBH<>''"; }
            where = where.Substring(5);
            edsDownload.Where = where;
            lvDownload.DataBind();

        }

        protected void btnBookDetail_Command(object sender, CommandEventArgs e)
        {
            string isbn = e.CommandArgument.ToString();
            Response.Redirect("~/Management/CDManager/CDDetail.aspx?ISBN=" + isbn);
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {

        }

        protected void btnGo_Command(object sender, CommandEventArgs e)
        {
            DataPager dpBook = (DataPager)lvDownload.FindControl("dpDownload");
            TextBox txtPage = (TextBox)dpBook.Controls[4].FindControl("txtPage");

            try
            {
                int max = dpBook.TotalRowCount < dpBook.PageSize ? 1 : dpBook.TotalRowCount / dpBook.PageSize + 1;
                int select = Convert.ToInt32(txtPage.Text);
                if (select < 1) { select = 1; }
                else if (select > max) { select = max; }

                int start_row = (select - 1) * dpBook.PageSize;
                dpBook.SetPageProperties(start_row, dpBook.PageSize, false);
                lvDownload.DataBind();
            }
            catch
            { }
        }

        protected void lvDownload_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //如果是绑定数据行 
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;

                HyperLink linkCZYTM = (HyperLink)dataItem.FindControl("linkCZYTM");
                Label lblXM = (Label)dataItem.FindControl("lblXM");
                try
                {
                    Reader reader = cde.Reader.First(r => r.DZTM == linkCZYTM.Text);
                    linkCZYTM.NavigateUrl = "~/Management/User/ReaderManager/ApplyAndDownload.aspx?DZTM=" + reader.DZTM;
                    lblXM.Text = reader.XM;
                }
                catch
                {
                    try
                    {
                        Admin admin = cde.Admin.First(a => a.GLYTM == linkCZYTM.Text);
                        lblXM.Text = admin.XM;
                    }
                    catch { }
                }


                HyperLink linkCDMC = (HyperLink)dataItem.FindControl("linkCDMC");
                linkCDMC.ToolTip = linkCDMC.Text;
                if (linkCDMC.Text.Length > 30)
                {
                    linkCDMC.Text = linkCDMC.Text.Substring(0, 30) + "...";
                }

                HyperLink linkZTM = (HyperLink)dataItem.FindControl("linkZTM");
                linkZTM.ToolTip = linkZTM.Text;
                if (linkZTM.Text.Length > 30)
                {
                    linkZTM.Text = linkZTM.Text.Substring(0, 30) + "...";
                }
            }
        }

        protected void edsDownload_QueryCreated(object sender, QueryCreatedEventArgs e)
        {

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Management/CDManager/DownloadSearch.aspx");
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {

        }
    }
}