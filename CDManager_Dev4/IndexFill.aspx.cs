using CDManagerLibrary.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDManager_Dev4
{
    public partial class IndexFill : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            edsBook.Where = "it.FLTM like @FLTM+'%'";

            try
            {
                string Url = HttpContext.Current.Request.UrlReferrer.ToString();
                if (Url.IndexOf("IndexFill.aspx") < 0) { edsBook.Where = "it.FLTM = ''"; }
            }
            catch
            { edsBook.Where = "it.FLTM = ''"; }
            lvBook.DataBind();

        }

        protected void tvType_SelectedNodeChanged(object sender, EventArgs e)
        {
            Session["FLTM"] = tvType.SelectedNode.Value;
            lblFL.Text = tvType.SelectedNode.Text;
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


                Label lblDownloadApply = (Label)dataItem.FindControl("lblDownloadApply");

                string isbn = ((Label)dataItem.FindControl("ISBNLabel")).Text;
                int downlload = cde.DownloadLog.Count(d => d.CD.ISBN == isbn);
                int apply = cde.ApplyLog.Count(a => a.ISBN == isbn);

                lblDownloadApply.Text = downlload + "/" + apply;
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
                Label lblTime = (Label)dataItem.FindControl("FFSJLabel");
                lblTime.Text = cde.CD.Where(c => c.ISBN == isbn).Select(c => c.CZSJ).Max().ToString();
                if (String.IsNullOrEmpty(lblTime.Text)) { lblTime.Text = "暂无操作"; }
            }
        }

        protected void lvBook_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {

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

        protected void lvBook_PreRender(object sender, EventArgs e)
        {

        }
    }
}