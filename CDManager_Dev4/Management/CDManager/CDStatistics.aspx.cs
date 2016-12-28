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

namespace CDManager_Dev4.Management.CDManager
{
    //光盘库统计
    public partial class CDStatistics : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string begin = DateTime.Now.Year + "-" + DateTime.Now.Month + "-1";
                    string end = DateTime.Now.Year + "-"
                        + DateTime.Now.Month + "-"
                        + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month).ToString();
                    linkMouthDownload.NavigateUrl = "/Management/CDManager/DownloadLogList.aspx?from=" + begin + "&to=" + end;
                    List<ApplyLog> listApply = cde.ApplyLog.ToList();
                    List<DownloadLog> listDownload = cde.DownloadLog.ToList();
                    BindBaseMsg(listApply, listDownload);
                    BindChart(listApply, listDownload);
                    BindApplyLog(listApply);
                    BindDownloadLog(listApply, listDownload);
                }
                catch { }
            }
        }
        //基本信息
        private void BindBaseMsg(List<ApplyLog> listApply, List<DownloadLog> listDownload)
        {
            lblBookCount.Text = cde.Book.Count().ToString();
            List<CD> listCD = cde.CD.ToList();
            lblCDCount.Text = listCD.Count.ToString();
            int count = listCD.Count(c => c.ZXZT == 1);
            if (count > 0)
            {
                linkOnlineCount.Text = count.ToString();
                linkOnlineCount.NavigateUrl = "~/Management/CDManager/CDOnline.aspx";
            }
            else { linkOnlineCount.Text = count.ToString(); }
            lblTime.Text = listCD.Max(c => c.FFSJ).ToString();
            linkApply.Text = listApply.Count.ToString();
            linkDownload.Text = listDownload.Count.ToString();
            lblMouthApply.Text = listApply.Count(a => a.SQSJ.Value.Year == DateTime.Now.Year && a.SQSJ.Value.Month == DateTime.Now.Month).ToString();
            linkMouthDownload.Text = listDownload.Count(d => d.XZSJ.Value.Year == DateTime.Now.Year && d.XZSJ.Value.Month == DateTime.Now.Month).ToString();
        }
        //绑定条形图数据
        private void BindChart(List<ApplyLog> listApply, List<DownloadLog> listDownload)
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
            string title = listDateShow[0] + "至" + listDateShow[6] + "光盘库申请与下载数趋势图";
            chartCD.Titles.Add(title);
            chartCD.Legends.Add("申请数");
            chartCD.Legends.Add("下载数");
            chartCD.Titles[0].TextStyle = TextStyle.Frame;
            chartCD.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartCD.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chartCD.ChartAreas[0].AxisX.Title = "时间";
            chartCD.ChartAreas[0].AxisX.TitleAlignment = StringAlignment.Far;
            chartCD.ChartAreas[0].AxisY.Title = "统计数";
            chartCD.ChartAreas[0].AxisY.TitleAlignment = StringAlignment.Far;
            chartCD.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Horizontal;
            chartCD.Series[0].Label = "#VALY";
            chartCD.Series[0].LegendText = "申请数";
            chartCD.Series[0].BorderWidth = 3;
            chartCD.Series[1].Label = "#VALY";
            chartCD.Series[1].LegendText = "下载数";
            chartCD.Series[1].BorderWidth = 3;
            chartCD.Series[0].Points.DataBindXY(listDateShow, listApplyCount);
            chartCD.Series[1].Points.DataBindXY(listDateShow, listDownloadCount);
        }
        //光盘资源申请统计
        private void BindApplyLog(List<ApplyLog> listApply)
        {
            lblApplyedCount.Text = (listApply.Count(a => a.SQZT != 0) / Convert.ToDouble(listApply.Count) * 100).ToString("f2") + "%";
            lblSuccessCount.Text = (listApply.Count(a => a.SQZT == 1) / Convert.ToDouble(listApply.Count) * 100).ToString("f2") + "%";
            lblBanCount.Text = (listApply.Count(a => a.SQZT == 2) / Convert.ToDouble(listApply.Count) * 100).ToString("f2") + "%";

            //最多申请资源
            var listMaxApply = listApply.GroupBy(a1 => a1.Book.ISBN).OrderBy(a2 => a2.Count(a => a.Book.ISBN != null));
            if (listMaxApply.Count() < 1)
            { linkMaxApply.Text = "没有申请记录"; }
            else
            {
                var max_apply = listMaxApply.Last().First();
                linkMaxApply.ToolTip = max_apply.Book.ZTM;
                if (max_apply.Book.ZTM.Length > 20)
                {
                    linkMaxApply.Text = max_apply.Book.ZTM.Substring(0, 20) + "...";
                }
                else { linkMaxApply.Text = max_apply.Book.ZTM; }
            }

            //本月最多申请资源
            var listMouthMaxApply = listApply.Where(a1 => a1.SQSJ.Value.Year == DateTime.Now.Year &&
                    a1.SQSJ.Value.Month == DateTime.Now.Month)
                    .GroupBy(a1 => a1.Book.ISBN).OrderBy(a2 => a2.Count(a => a.Book.ISBN != null));

            if (listMouthMaxApply.Count() < 1)
            { linkMouthMaxApply.Text = "本月没有申请记录"; }
            else
            {
                var mouth_max_apply = listMouthMaxApply.Last().First();
                linkMaxApply.ToolTip = mouth_max_apply.Book.ZTM;
                if (mouth_max_apply.Book.ZTM.Length > 20)
                {
                    linkMouthMaxApply.Text = mouth_max_apply.Book.ZTM.Substring(0, 20) + "...";
                }
                else { linkMouthMaxApply.Text = mouth_max_apply.Book.ZTM; }
                //linkMouthMaxApply.NavigateUrl = "~/Management/CDManager/CDDetail.aspx?ISBN=" + mouth_max_apply.ISBN;
            }

            //未处理申请
            int no_apply = listApply.Count(a => a.SQZT == 0);
            linkNoApply.Text = no_apply.ToString(); ;
            if (no_apply > 0) { linkNoApply.NavigateUrl = "~/Management/CDManager/ApplyLogList.aspx"; }

            //发送申请最多读者
            var listMaxReader = listApply.GroupBy(a1 => a1.DZTM).OrderBy(a2 => a2.Count(a => a.DZTM != null));
            if (listMaxReader.Count() < 1)
            { linkMaxReader.Text = "没有读者申请资源"; }
            else
            {
                var max_reader = listMaxReader.Last().First();
                linkMaxReader.Text = max_reader.DZTM.ToString();
                linkMaxReader.NavigateUrl = "~/Management/User/ReaderManager/ApplyAndDownload.aspx?DZTM=" + max_reader.DZTM;
            }

            //本月发送申请最多读者
            var listMouthMaxReader = listApply.Where(a1 => a1.SQSJ.Value.Year == DateTime.Now.Year &&
                    a1.SQSJ.Value.Month == DateTime.Now.Month)
                .GroupBy(a1 => a1.DZTM).OrderBy(a2 => a2.Count(a => a.DZTM != null));
            if (listMouthMaxReader.Count() < 1)
            { linkMonthMaxReader.Text = "本月没有读者申请资源"; }
            else
            {
                var mouth_max_reader = listMouthMaxReader.Last().First();
                linkMonthMaxReader.Text = mouth_max_reader.Reader.DZTM;
                linkMonthMaxReader.NavigateUrl = "~/Management/User/ReaderManager/ApplyAndDownload.aspx?DZTM=" + mouth_max_reader.Reader.DZTM;
            }

            //日均申请数
            DateTime begin = listApply.Min(a => a.SQSJ).Value;
            DateTime end = DateTime.Now;
            int day_count = 0;
            if (begin.Date == end.Date) { day_count = 1; }
            else
            {
                TimeSpan days = end - begin;
                day_count = days.Days;
            }
            double avg_apply = listApply.Count / Convert.ToDouble(day_count);
            lblDayCount.Text = avg_apply.ToString("f2");

            //最近24小时申请数
            int count_24 = listApply.Count(a => a.SQSJ.Value.AddDays(1) > DateTime.Now);
            lbl24Count.Text = count_24.ToString();

            //处理最多申请管理员
            var listMaxAdmin = listApply.GroupBy(a1 => a1.CLCZY).OrderBy(a2 => a2.Count(a => a.CLCZY != null));
            if (listMaxAdmin.Count() < 1)
            { linkMaxAdmin.Text = "没有管理员处理申请"; }
            else
            {
                var max_admin = listMaxAdmin.Last().First();
                linkMaxAdmin.Text = max_admin.CLCZY;
                linkMaxAdmin.NavigateUrl = "~/Management/User/AdminManager/AdminStatistics.aspx?GLYTM=" + max_admin.CLCZY;
            }

            //最近申请资源
            try
            {
                ApplyLog newly_apply = listApply.First(a => a.SQSJ == listApply.Max(al => al.SQSJ));
                if (newly_apply.Book.ZTM.Length > 20)
                { linkNewlyApply.Text = newly_apply.Book.ZTM.Substring(0, 20) + "..."; }
                else
                { linkNewlyApply.Text = newly_apply.Book.ZTM; }
                linkNewlyApply.ToolTip = newly_apply.Book.ZTM;
                linkNewlyApply.NavigateUrl = "~/Management/CDManager/CDDetail.aspx?ISBN=" + newly_apply.Book.ISBN;
            }
            catch { linkNewlyApply.Text = "没有人申请资源"; }
        }
        //光盘资源下载统计
        private void BindDownloadLog(List<ApplyLog> listApply, List<DownloadLog> listDownload)
        {
            //最多下载资源
            var listMaxDownload = listDownload.GroupBy(d1 => d1.CD.Book.ISBN).OrderBy(a2 => a2.Count(d3 => d3.CD.Book.ISBN != null));
            if (listMaxDownload.Count() < 1)
            { linkMaxDownload.Text = "没有下载记录"; }
            else
            {
                var max_download = listMaxDownload.Last().First();
                if (max_download.CD.Book.ZTM.Length > 20)
                { linkMaxDownload.Text = max_download.CD.Book.ZTM.Substring(0, 20) + "..."; }
                else
                { linkMaxDownload.Text = max_download.CD.Book.ZTM; }
                linkMaxDownload.ToolTip = max_download.CD.Book.ZTM;
                linkMaxDownload.NavigateUrl = "~/Management/CDManager/CDDetail.aspx?ISBN=" + max_download.CD.Book.ISBN;
            }

            //本月最多下载资源
            var listMouthMaxDownload = listDownload.Where(d1 => d1.XZSJ.Value.Year == DateTime.Now.Year &&
                    d1.XZSJ.Value.Month == DateTime.Now.Month)
                    .GroupBy(d2 => d2.CD.Book.ISBN).OrderBy(d3 => d3.Count(d4 => d4.CD.Book.ISBN != null));
            if (listMouthMaxDownload.Count() < 1)
            { linkMouthMaxDownload.Text = "本月没有下载记录"; }
            else
            {
                var mouth_max_download = listMouthMaxDownload.Last().First();
                if (mouth_max_download.CD.Book.ZTM.Length > 20)
                { linkMouthMaxDownload.Text = mouth_max_download.CD.Book.ZTM.Substring(0, 20) + "..."; }
                else
                { linkMouthMaxDownload.Text = mouth_max_download.CD.Book.ZTM; }
                linkMouthMaxDownload.ToolTip = mouth_max_download.CD.Book.ZTM;
                linkMouthMaxDownload.NavigateUrl = "~/Management/CDManager/CDDetail.aspx?ISBN=" + mouth_max_download.CD.Book.ISBN;
            }

            //最近下载资源
            try
            {
                DownloadLog newly_download = listDownload.First(d => d.XZSJ == listDownload.Max(d1 => d1.XZSJ));
                if (newly_download.CD.Book.ZTM.Length > 20)
                { linkNewlyDownload.Text = newly_download.CD.Book.ZTM.Substring(0, 20) + "..."; }
                else
                { linkNewlyDownload.Text = newly_download.CD.Book.ZTM; }
                linkNewlyDownload.ToolTip = newly_download.CD.Book.ZTM;
                linkNewlyDownload.NavigateUrl = "~/Management/CDManager/CDDetail.aspx?ISBN=" + newly_download.CD.Book.ISBN;
            }
            catch { linkNewlyDownload.Text = "没有资源被下载"; }

            //最多下载人
            var listMaxDownLoadReader = listDownload.GroupBy(d1 => d1.CZYTM).OrderBy(d2 => d2.Count(d3 => d3.CZYTM != null));
            if (listMaxDownLoadReader.Count() < 1)
            { linkMaxDownloadReader.Text = "没有人下载资源"; }
            else
            {
                var max_download_reader = listMaxDownLoadReader.Last().First();
                linkMaxDownloadReader.Text = max_download_reader.CZYTM;
                linkMaxDownloadReader.NavigateUrl = "~/Management/User/ReaderManager/ApplyAndDownload.aspx?DZTM=" + max_download_reader.CZYTM;
            }

            //本月最多下载人
            var listMonthMaxDownLoadReader = listDownload
                .Where(d => d.XZSJ.Value.Year == DateTime.Now.Year && d.XZSJ.Value.Month == DateTime.Now.Month)
                .GroupBy(d1 => d1.CZYTM).OrderBy(d2 => d2.Count(d3 => d3.CZYTM != null));
            if (listMonthMaxDownLoadReader.Count() < 1)
            { linkMonthMaxDownloadReader.Text = "本月没有人下载资源资源"; }
            else
            {
                var month_max_download_reader = listMaxDownLoadReader.Last().First();
                linkMonthMaxDownloadReader.Text = month_max_download_reader.CZYTM;
                linkMonthMaxDownloadReader.NavigateUrl = "~/Management/User/ReaderManager/ApplyAndDownload.aspx?DZTM=" + month_max_download_reader.CZYTM;
            }

            //日均下载数
            DateTime begin = listDownload.Min(d => d.XZSJ).Value;
            DateTime end = DateTime.Now;
            int day_count = 0;
            if (begin.Date == end.Date) { day_count = 1; }
            else
            {
                TimeSpan days = end - begin;
                day_count = days.Days;
            }
            double avg_download = listDownload.Count / Convert.ToDouble(day_count);
            lblDayDownloadCount.Text = avg_download.ToString("f2");

            //最近24小时下载数
            int count_24 = listDownload.Count(d => d.XZSJ.Value.AddDays(1) > DateTime.Now);
            lbl24DownloadCount.Text = count_24.ToString();

            //申请/下载比
            double apply = listApply.Count;
            double download = listDownload.Count;
            double apply_download = 0.0;
            if (apply > 0.0 && download > 0.0)
            { apply_download = apply / download; }
            lblApplyDownload.Text = apply_download.ToString("f2");

            double count_dztm = cde.Reader.Count();
            double count_czytm = listDownload.Select(d => d.CZYTM).Distinct().Count();
            double download_total = count_czytm / count_dztm * 100;
            lblDownloadTotal.Text = download_total.ToString("f2") + "%";
        }
    }
}