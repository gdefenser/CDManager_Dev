using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.Core;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;

namespace CDManager_Dev4.Management.CDManager
{
    public partial class CDDownloadDetail : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string cdxh = Request.QueryString["CDXH"];
                BindDownloadLog(cdxh);
            }
        }

        private void BindDownloadLog(string cdxh)
        {
            try
            {
                //计算统计的时间
                int mouth = DateTime.Now.Month;
                int year = DateTime.Now.Year;
                List<List<int>> listDate = new List<List<int>>();
                List<string> listDateShow = new List<string>();
                if (mouth >= 3 && mouth < 9)
                {
                    listDate.Add(new List<int>() { year, 2 });
                    listDate.Add(new List<int>() { year, 3 });
                    listDate.Add(new List<int>() { year, 4 });
                    listDate.Add(new List<int>() { year, 5 });
                    listDate.Add(new List<int>() { year, 6 });
                    listDate.Add(new List<int>() { year, 7 });
                    listDate.Add(new List<int>() { year, 8 });
                    listDateShow.AddRange(new string[] {
                            year+"/2",year+"/3",year+"/4",year+"/5",year+"/6",(year+1)+"/7",(year+1)+"/8"
                        });
                }
                else
                {
                    int year_1 = 0;
                    int year_2 = 0;
                    if (mouth != 1 && mouth != 2)
                    {
                        year_1 = year;
                        year_2 = year + 1;
                    }
                    else
                    {
                        year_1 = year - 1;
                        year_2 = year;
                    }

                    listDate.Add(new List<int>() { year_1, 8 });
                    listDate.Add(new List<int>() { year_1, 9 });
                    listDate.Add(new List<int>() { year_1, 10 });
                    listDate.Add(new List<int>() { year_1, 11 });
                    listDate.Add(new List<int>() { year_1, 12 });
                    listDate.Add(new List<int>() { year_2, 1 });
                    listDate.Add(new List<int>() { year_2, 2 });
                    listDateShow.AddRange(new string[] {
                            year_1+"/8",year_1+"/9",year_1+"/10",year_1+"/11",year_1+"/12",year_2+"/1",year_2+"/2"
                        });

                }
                List<int> listDownloadCount = new List<int>();

                foreach (List<int> time in listDate)
                {
                    int f_year = time[0];
                    int f_mouth = time[1];
                    listDownloadCount.Add(cde.DownloadLog.Count(d => d.CD.CDXH == cdxh && d.XZSJ.Value.Year == f_year && d.XZSJ.Value.Month == f_mouth));
                }
                //配置条形图参数
                string title = listDateShow[0] + "至" + listDateShow[6] + "下载数趋势图";
                chartDownload.Titles.Add(title);
                chartDownload.Titles[0].TextStyle = TextStyle.Frame;
                chartDownload.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chartDownload.ChartAreas[0].AxisX.Title = "时间";
                chartDownload.ChartAreas[0].AxisX.TitleAlignment = StringAlignment.Far;
                chartDownload.ChartAreas[0].AxisY.Title = "下载数";
                chartDownload.ChartAreas[0].AxisY.TitleAlignment = StringAlignment.Far;
                chartDownload.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Horizontal;
                chartDownload.Series[0].Label = "#VALY";
                chartDownload.Series[0].BorderWidth = 3;
                chartDownload.Series[0].Points.DataBindXY(listDateShow, listDownloadCount);

            }
            catch { }
        }

        protected void gvCD_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //如果是绑定数据行 删除确认框
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    string zxzt = e.Row.Cells[3].Text;
                    if (zxzt == "0")
                    { e.Row.Cells[3].Text = "未上传"; }
                    else if (zxzt == "1")
                    { e.Row.Cells[3].Text = "在线"; }
                    else if (zxzt == "2")
                    { e.Row.Cells[3].Text = "已删除"; }

                    LiteralControl lite;//强制转换LiteralControl
                    lite = (LiteralControl)e.Row.Cells[4].Controls[0];
                    Label lblCount = (Label)lite.FindControl("lblCount");
                    string cdxh = e.Row.Cells[0].Text;
                    List<DownloadLog> list = cde.DownloadLog.Where(d => d.CD.CDXH == cdxh).ToList();
                    lblCount.Text = list.Count.ToString();
                    lite = (LiteralControl)e.Row.Cells[5].Controls[0];
                    Label lblDayCount = (Label)lite.FindControl("lblDayCount");
                    lblDayCount.Text = list.Count(c => c.XZSJ.Value.Date == DateTime.Now.Date).ToString();
                    lite = (LiteralControl)e.Row.Cells[6].Controls[0];
                    Label lblMouthCount = (Label)lite.FindControl("lblMouthCount");
                    lblMouthCount.Text = (list.Count(c => c.XZSJ.Value.Year == DateTime.Now.Year) / 12.0).ToString("f2");
                }
            }
        }

        protected void btnBanIP_Command(object sender, CommandEventArgs e)
        {
            Session["JZIP"] = e.CommandArgument.ToString();
            Response.Redirect("~/Management/Security/BanIPManagement.aspx");
        }
    }
}