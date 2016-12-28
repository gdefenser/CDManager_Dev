using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.Core;
using System.IO;

namespace CDManager_Dev4.Management.Message
{
    public partial class NoticeManagement : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //删除操作
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                string ggtm = e.CommandArgument.ToString();
                Notice delete_notice = cde.Notice.First(n => n.GGTM == ggtm);
                if (delete_notice.FJ.Value)
                {
                    string path = "/NoticeFiles/" + ggtm;
                    string server_path = Server.MapPath("~" + path);
                    DirectoryInfo dir = new DirectoryInfo(server_path);
                    if (dir.Exists)
                    {
                        if (dir.GetFiles() != null)
                        {
                            FileInfo fi = dir.GetFiles()[0];
                            fi.Delete();
                            dir.Delete();
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Management/Error.aspx");
                    }
                }
                cde.Notice.Remove(delete_notice);

                if (cde.SaveChanges() > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('删除成功!');location.reload();", true);
                }
            }
            catch
            {
                Response.Redirect("~/Management/Error.aspx");
            }
        }

        //删除确认框
        protected void gvNotice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //如果是绑定数据行 
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    LiteralControl lite = (LiteralControl)e.Row.Cells[0].Controls[0];//强制转换LiteralControl
                    Button btn = (Button)lite.FindControl("btnDelete");
                    btn.Attributes.Add("onclick", "javascript:return confirm('你确认要删除公告:" + ((HyperLink)e.Row.Cells[1].Controls[0]).Text + "吗?')");

                    //string fjmc = e.Row.Cells[7].Text;
                    //if (!String.IsNullOrEmpty(fjmc) && fjmc != "&nbsp;") { e.Row.Cells[7].Text = "是"; }
                    //else { e.Row.Cells[7].Text = "否"; }
                }
            }
        }

        protected void btnGo_Command(object sender, CommandEventArgs e)
        {
            DataPager dpNotice = (DataPager)lvNotice.FindControl("dpBook");
            TextBox txtPage = (TextBox)dpNotice.Controls[4].FindControl("txtPage");

            try
            {
                int max = dpNotice.TotalRowCount < dpNotice.PageSize ? 1 : dpNotice.TotalRowCount / dpNotice.PageSize + 1;
                int select = Convert.ToInt32(txtPage.Text);
                if (select < 1) { select = 1; }
                else if (select > max) { select = max; }

                int start_row = (select - 1) * dpNotice.PageSize;
                dpNotice.SetPageProperties(start_row, dpNotice.PageSize, false);
                lvNotice.DataBind();
            }
            catch
            { }
        }

        protected void lvNotice_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //如果是绑定数据行 
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                HyperLink linkGGBT = (HyperLink)dataItem.FindControl("linkGGBT");

                Button btn = (Button)dataItem.FindControl("btnDelete");
                btn.Attributes.Add("onclick", "javascript:return confirm('你确认要删除公告: [ " + linkGGBT.Text + " ] 吗?')");

                Label lblFJ = (Label)dataItem.FindControl("lblFJ");
                if (lblFJ.Text == "True" || lblFJ.Text == "true") { lblFJ.Text = "有附件"; }
                else { lblFJ.Text = "无附件"; }
            }
        }
    }
}