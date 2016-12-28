using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDManager_Dev4.Management.CDManager
{
    public partial class DownloadSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string query = "";
            if (chkDate.Checked)
            {
                try
                {
                    DateTime from = CalendarExtenderFrom.SelectedDate;
                    DateTime to = CalendarExtenderTo.SelectedDate;

                    if (from.Year != 0001 && to.Year == 0001)
                    {
                        query += "&From=" + from.Year + "-" + from.Month + "-" + from.Day;
                    }
                    else if (from.Year == 0001 && to.Year != 0001)
                    {
                        query += "&To=" + to.Year + "-" + to.Month + "-" + to.Day;
                    }
                    else if (from.Year != 0001 && to.Year != 0001)
                    {
                        query += "&From=" + from.Year + "-" + from.Month + "-" + from.Day
                            + "&To=" + to.Year + "-" + to.Month + "-" + to.Day;
                    }

                }
                catch { }


            }

            if (chkBook.Checked)
            {
                string book = txtBook.Text;
                if (!String.IsNullOrEmpty(book))
                { query += "&Book=" + book; }
            }

            if (chkCD.Checked)
            {
                string cd = txtCD.Text;
                if (!String.IsNullOrEmpty(cd))
                { query += "&CD=" + cd; }
            }

            if (chkCZYTM.Checked)
            {
                string czy = txtCZY.Text;
                if (!String.IsNullOrEmpty(czy))
                { query += "&CZY=" + czy; }
            }



            if (!String.IsNullOrEmpty(query))
            {
                query = query.Substring(1);
                Response.Redirect("~/Management/CDManager/DownloadLogList.aspx?" + query);
            }
            else
            {
                Response.Redirect("~/Management/CDManager/DownloadLogList.aspx");
            }
        }
    }
}