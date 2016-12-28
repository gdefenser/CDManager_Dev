using CDManagerLibrary.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDManager_Dev4.Management.Sys
{
    public partial class FeedBackcheck : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string xxtm = Request.QueryString["XXTM"];
            if (String.IsNullOrEmpty(xxtm))
            {
                panelCalendar.Visible = true;
                lvMessage.Visible = true;
                string where=hideWhere.Value;
                if (String.IsNullOrEmpty(where)) { where = "it.YHLX='feedback'"; }
                edsMessage.Where = where;
            }
            else
            {
                try
                {
                    CDManagerLibrary.EntityFramework.Message msg = cde.Message.First(m => m.XXTM == xxtm);
                    if (msg.FSRTM == "匿名") { linkYH.Text = "匿名"; }
                    else
                    {
                        panelMessage.Visible = true;
                        linkYH.Text = msg.FSRTM;
                        linkYH.NavigateUrl = "~/Management/User/ReaderManager/EditReader.aspx?DZTM=" + msg.FSRTM;
                    }
                    lblNR.Text = msg.XXNR.Substring(msg.XXNR.IndexOf(msg.XXNR.Split('@')[1]));
                    lblFSSJ.Text = msg.FSSJ.ToString();

                    msg.YD = true;
                    cde.SaveChanges();
                    panelDetail.Visible = true;
                }
                catch
                { }
            }
            
        }

        protected void btnDetail_Command(object sender, CommandEventArgs e)
        {
            string xxtm = e.CommandArgument.ToString();
            Response.Redirect("~/Management/Sys/FeedBackcheck.aspx?XXTM=" + xxtm);
        }

        protected void btnGo_Command(object sender, CommandEventArgs e)
        {
            DataPager dpMessage = (DataPager)lvMessage.FindControl("dpMessage");
            TextBox txtPage = (TextBox)dpMessage.Controls[4].FindControl("txtPage");

            try
            {
                int max = dpMessage.TotalRowCount < dpMessage.PageSize ? 1 : dpMessage.TotalRowCount / dpMessage.PageSize + 1;
                int select = Convert.ToInt32(txtPage.Text);
                if (select < 1) { select = 1; }
                else if (select > max) { select = max; }

                int start_row = (select - 1) * dpMessage.PageSize;
                dpMessage.SetPageProperties(start_row, dpMessage.PageSize, false);
                lvMessage.DataBind();
            }
            catch
            { }
        }

        protected void lvMessage_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //如果是绑定数据行 
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                Label lblXXNR = (Label)dataItem.FindControl("lblXXNR");
                if (lblXXNR.Text.IndexOf('@') > 0)
                { lblXXNR.Text = lblXXNR.Text.Substring(lblXXNR.Text.IndexOf(lblXXNR.Text.Split('@')[1])); }
                if (lblXXNR.Text.Length > 30)
                {
                    lblXXNR.Text = lblXXNR.Text.Substring(0, 30) + "...";
                }

                Label lblYD = (Label)dataItem.FindControl("lblYD");
                if (lblYD.Text == "True") { lblYD.Text = "是"; }
                else if (lblYD.Text == "False") { lblYD.Text = "否"; }
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string where = "";
            if (CalendarExtenderFrom.SelectedDate == new DateTime(1, 1, 1) && 
                CalendarExtenderTo.SelectedDate == new DateTime(1, 1, 1))
            {
                where = " and it.YHLX='feedback'";
            }
            else
            {

                if (CalendarExtenderFrom.SelectedDate != new DateTime(1, 1, 1))
                {
                    where += String.Format(" and it.FSSJ >= CreateDateTime({0},{1},{2},0,0,0) and it.YHLX='feedback'",
                        CalendarExtenderFrom.SelectedDate.Year,
                                            CalendarExtenderFrom.SelectedDate.Month,
                                            CalendarExtenderFrom.SelectedDate.Day);

                }

                if (CalendarExtenderTo.SelectedDate != new DateTime(1, 1, 1))
                {
                    where += String.Format(" and it.FSSJ <=CreateDateTime({0},{1},{2},23,59,59) and it.YHLX='feedback'",
                           CalendarExtenderTo.SelectedDate.Year,
                           CalendarExtenderTo.SelectedDate.Month,
                           CalendarExtenderTo.SelectedDate.Day);
                }

            }
            where = where.Substring(5);
            edsMessage.Where = where;
            hideWhere.Value = where;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }

        protected void btnList_Click(object sender, EventArgs e)
        {

        }

        protected void btnMessage_Click(object sender, EventArgs e)
        {
            Session["FSTM"] = linkYH.Text;
            Response.Redirect("~/Management/Message/NewMessage.aspx");
        }


    }
}