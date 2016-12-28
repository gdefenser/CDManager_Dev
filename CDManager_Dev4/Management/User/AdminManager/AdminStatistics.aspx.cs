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

namespace CDManager_Dev4.Management.User.AdminManager
{
    public partial class AdminStatistics : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string glytm = Request.QueryString["GLYTM"];
            if (!String.IsNullOrEmpty(glytm))
            {
                panelAdmin.Visible = true;
                List<ApplyLog> listApply = cde.ApplyLog.Where(a => a.CLCZY == glytm).ToList();
                List<Reader> listReader = cde.Reader.Where(r => r.FFCZY == glytm || r.JZCZY == glytm).ToList();
                List<CD> listCD = cde.CD.Where(c => c.CZCZY == glytm || c.FFCZY == glytm).ToList();
                BindAdmin(glytm, listApply);
                BindChart(glytm, listApply, listCD, listReader);
            }
            else
            { panelGLYTM.Visible = true; }
        }

        protected void btnClick_Click(object sender, EventArgs e)
        {
            string glytm = txtGLYTM.Text;
            if (!String.IsNullOrEmpty(glytm))
            { Response.Redirect("~/Management/User/AdminManager/AdminStatistics.aspx?GLYTM=" + glytm); }
        }

        private void BindAdmin(string glytm, List<ApplyLog> listApply)
        {
            try
            {
                Admin admin = cde.Admin.Find(glytm);
                linkGLYTM.Text = admin.GLYTM;
                linkGLYTM.NavigateUrl = "~/Management/User/AdminManager/EditAdmin.aspx?GLYTM=" + admin.GLYTM;
                lblXM.Text = admin.XM;
                lblApply.Text = listApply.Count().ToString();
                string scrq = admin.SCRQ.ToString();
                lblLogin.Text = scrq == "" ? "暂无登录" : scrq;
            }
            catch
            { }
        }

        private void BindChart(string glytm, List<ApplyLog> listApply, List<CD> listCD, List<Reader> listReader)
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
            List<int> listCDCount = new List<int>();
            List<int> listReaderCount = new List<int>();

            foreach (List<int> time in listDate)
            {
                int f_year = time[0];
                int f_mouth = time[1];
                listApplyCount.Add(listApply.Count(d => d.CLSJ.Value.Year == f_year && d.CLSJ.Value.Month == f_mouth));
            }

            foreach (List<int> time in listDate)
            {
                int f_year = time[0];
                int f_mouth = time[1];
                listCDCount.Add(listCD.Count(c => c.CZSJ.Value.Year == f_year &&
                    c.CZSJ.Value.Month == f_mouth || c.FFSJ.Value.Year == f_year
                    && c.FFSJ.Value.Month == f_mouth));
            }

            foreach (List<int> time in listDate)
            {
                int f_year = time[0];
                int f_mouth = time[1];
                listReaderCount.Add(listReader.Count(r => r.FFSJ.Value.Year == f_year && r.FFSJ.Value.Month == f_mouth));
            }

            string title = glytm + "管理员" + listDateShow[0] + "至" + listDateShow[6] + "管理统计图";
            chartAdmin.Titles.Add(title);
            chartAdmin.Legends.Add("申请处理数");
            chartAdmin.Legends.Add("光盘管理数");
            chartAdmin.Legends.Add("读者管理数");
            chartAdmin.Titles[0].TextStyle = TextStyle.Frame;
            chartAdmin.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartAdmin.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chartAdmin.ChartAreas[0].AxisX.Title = "时间";
            chartAdmin.ChartAreas[0].AxisX.TitleAlignment = StringAlignment.Far;
            chartAdmin.ChartAreas[0].AxisY.Title = "统计数";
            chartAdmin.ChartAreas[0].AxisY.TitleAlignment = StringAlignment.Far;
            chartAdmin.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Horizontal;
            chartAdmin.Series[0].Label = "#VALY";
            chartAdmin.Series[0].LegendText = "申请处理数";
            chartAdmin.Series[0].BorderWidth = 3;
            chartAdmin.Series[1].Label = "#VALY";
            chartAdmin.Series[1].LegendText = "光盘管理数";
            chartAdmin.Series[1].BorderWidth = 3;
            chartAdmin.Series[2].Label = "#VALY";
            chartAdmin.Series[2].LegendText = "读者管理数";
            chartAdmin.Series[2].BorderWidth = 3;
            chartAdmin.Series[0].Points.DataBindXY(listDateShow, listApplyCount);
            chartAdmin.Series[1].Points.DataBindXY(listDateShow, listCDCount);
            chartAdmin.Series[2].Points.DataBindXY(listDateShow, listReaderCount);
        }
    }
}