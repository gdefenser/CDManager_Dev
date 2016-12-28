using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.Core;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
namespace CDManager_Dev4.Management.User.ReaderManager
{
    public partial class ApplyAndDownload : CDPages
    {
        //List<ApplyLog> listApply = new List<ApplyLog>();
        //List<DownloadLog> listDownload = new List<DownloadLog>();
        protected void Page_Load(object sender, EventArgs e)
        {

            string dztm = Request.QueryString["DZTM"];
            if (!String.IsNullOrEmpty(dztm))
            {
                panelReader.Visible = true;
                List<ApplyLog> listApply = cde.ApplyLog.Where(a => a.DZTM == dztm).ToList();
                List<DownloadLog> listDownload = cde.DownloadLog.Where(d => d.CZYTM == dztm).ToList();
                BindReader(dztm, listApply, listDownload);
                BindChartTrend(dztm, listApply, listDownload);
                BindChartPercent(dztm, listApply, listDownload);
                chartTrend.Visible = true;
                lvApply.Visible = true;
            }
            else
            {
                panelDZTM.Visible = true;
            }
        }

        //绑定读者信息
        private void BindReader(string dztm, List<ApplyLog> listApply, List<DownloadLog> listDownload)
        {
            try
            {
                Reader reader = cde.Reader.Find(dztm);
                linkDZTM.Text = reader.DZTM;
                linkDZTM.NavigateUrl = "~/Management/User/ReaderManager/EditReader.aspx?DZTM=" + reader.DZTM;
                lblXM.Text = reader.XM;
                lblApplyDownload.Text = listApply.Count + "/" + listDownload.Count;
                int mouth_apply = listApply.Count(a => a.SQSJ.Value.Year == DateTime.Now.Year && a.SQSJ.Value.Month == DateTime.Now.Month);
                int mouth_download = listDownload.Count(d => d.XZSJ.Value.Year == DateTime.Now.Year && d.XZSJ.Value.Month == DateTime.Now.Month);
                lblMouthApplyDownload.Text = mouth_apply + "/" + mouth_download;
                lblApply.Text = listApply.Max(a => a.SQSJ).ToString();
                lblDownload.Text = listDownload.Max(d => d.XZSJ).ToString();
            }
            catch
            { Response.Redirect("~/Management/User/ReaderManager/ApplyAndDownload.aspx"); }
        }
        //申请/下载趋势图
        private void BindChartTrend(string dztm, List<ApplyLog> listApply, List<DownloadLog> listDownload)
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
            List<int> listApplyCount = new List<int>();
            List<int> listDownloadCount = new List<int>();

            foreach (List<int> time in listDate)
            {
                int f_year = time[0];
                int f_mouth = time[1];
                listApplyCount.Add(listApply.Count(d => d.SQSJ.Value.Year == f_year && d.SQSJ.Value.Month == f_mouth));
            }

            foreach (List<int> time in listDate)
            {
                int f_year = time[0];
                int f_mouth = time[1];
                listDownloadCount.Add(listDownload.Count(d => d.XZSJ.Value.Year == f_year && d.XZSJ.Value.Month == f_mouth));
            }

            //配置条形图参数
            chartTrend.Titles.Clear();
            chartTrend.Legends.Clear();
            string title = dztm + "用户" + listDateShow[0] + "至" + listDateShow[6] + "光盘库申请与下载数趋势图";
            chartTrend.Titles.Add(title);
            chartTrend.Legends.Add("申请数");
            chartTrend.Legends.Add("下载数");
            chartTrend.Titles[0].TextStyle = TextStyle.Frame;
            chartTrend.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartTrend.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chartTrend.ChartAreas[0].AxisX.Title = "时间";
            chartTrend.ChartAreas[0].AxisX.TitleAlignment = StringAlignment.Far;
            chartTrend.ChartAreas[0].AxisY.Title = "统计数";
            chartTrend.ChartAreas[0].AxisY.TitleAlignment = StringAlignment.Far;
            chartTrend.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Horizontal;
            chartTrend.Series[0].Label = "#VALY";
            chartTrend.Series[0].LegendText = "申请数";
            chartTrend.Series[0].BorderWidth = 3;
            chartTrend.Series[1].Label = "#VALY";
            chartTrend.Series[1].LegendText = "下载数";
            chartTrend.Series[1].BorderWidth = 3;
            chartTrend.Series[0].Points.DataBindXY(listDateShow, listApplyCount);
            chartTrend.Series[1].Points.DataBindXY(listDateShow, listDownloadCount);
        }
        //申请/下载比例图
        private void BindChartPercent(string dztm, List<ApplyLog> listApply, List<DownloadLog> listDownload)
        {
            List<string> listText = new List<string> { "申请数", "下载数" };
            List<int> listValue = new List<int> { listApply.Count, listDownload.Count };
            string title = dztm + "用户光盘库申请与下载数比例图";
            chartPercent.Titles.Add(title);
            chartPercent.Legends.Add(title);
            chartPercent.Series[0].ShadowOffset = 1;
            chartPercent.Series[0].LegendText = "#VALX";
            chartPercent.Series[0].Label = "#PERCENT{P1}";
            chartPercent.Series[0]["PieLabelStyle"] = "Disable";
            chartPercent.Series[0].Points.DataBindXY(listText, listValue);
        }

        protected void btnClick_Click(object sender, EventArgs e)
        {
            string dztm = txtDZTM.Text;
            if (!String.IsNullOrEmpty(dztm))
            { Response.Redirect("~/Management/User/ReaderManager/ApplyAndDownload.aspx?DZTM=" + dztm); }
        }

        protected void dropType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropType.SelectedIndex == 0)
            {
                chartTrend.Visible = true;
                chartPercent.Visible = false;
            }
            else
            {
                chartTrend.Visible = false;
                chartPercent.Visible = true;
            }
        }

        protected void btnBan_Click(object sender, EventArgs e)
        {
            Session["DZTM"] = linkDZTM.Text;
            Response.Redirect("~/Management/Security/BanReaderManagement.aspx");
        }

        protected void dropLogType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropLogType.SelectedIndex == 0)
            {
                lvApply.Visible = true;
                lvDownload.Visible = false;
            }
            else
            {
                lvApply.Visible = false;
                lvDownload.Visible = true;
            }
        }

        protected void btnBanIP_Command(object sender, CommandEventArgs e)
        {
            Session["JZIP"] = e.CommandArgument.ToString();
            Response.Redirect("~/Management/Security/BanIPManagement.aspx");
        }

        protected void btnApplyGo_Command(object sender, CommandEventArgs e)
        {
            DataPager dpApply = (DataPager)lvApply.FindControl("dpApply");
            TextBox txtPage = (TextBox)dpApply.Controls[4].FindControl("txtPage");

            try
            {
                int max = dpApply.TotalRowCount < dpApply.PageSize ? 1 : dpApply.TotalRowCount / dpApply.PageSize + 1;
                int select = Convert.ToInt32(txtPage.Text);
                if (select < 1) { select = 1; }
                else if (select > max) { select = max; }

                int start_row = (select - 1) * dpApply.PageSize;
                dpApply.SetPageProperties(start_row, dpApply.PageSize, false);
                lvApply.DataBind();
            }
            catch
            { }
        }

        protected void lvApply_ItemDataBound(object sender, ListViewItemEventArgs e)
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
            }
        }

        protected void btnDownloadGo_Command(object sender, CommandEventArgs e)
        {
            DataPager dpDownload = (DataPager)lvApply.FindControl("dpDownload");
            TextBox txtPage = (TextBox)dpDownload.Controls[4].FindControl("txtPage");

            try
            {
                int max = dpDownload.TotalRowCount < dpDownload.PageSize ? 1 : dpDownload.TotalRowCount / dpDownload.PageSize + 1;
                int select = Convert.ToInt32(txtPage.Text);
                if (select < 1) { select = 1; }
                else if (select > max) { select = max; }

                int start_row = (select - 1) * dpDownload.PageSize;
                dpDownload.SetPageProperties(start_row, dpDownload.PageSize, false);
                lvApply.DataBind();
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

                HyperLink linkBookDetail = (HyperLink)dataItem.FindControl("linkZTM");
                linkBookDetail.ToolTip = linkBookDetail.Text;
                if (linkBookDetail.Text.Length > 30)
                {
                    linkBookDetail.Text = linkBookDetail.Text.Substring(0, 30) + "...";
                }

                Label lblCDMC = (Label)dataItem.FindControl("CDMCLabel");
                lblCDMC.ToolTip = lblCDMC.Text;
                if (lblCDMC.Text.Length > 30)
                {
                    lblCDMC.Text = lblCDMC.Text.Substring(0, 30) + "...";
                }
            }
        }
    }
}