using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.Core;
namespace CDManager_Dev4.Management.CDManager
{
    public partial class ApplyStatistics : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<ApplyLog> listApply = cde.ApplyLog.ToList();

                lblCount.Text = listApply.Count.ToString();
                lblMouthCount.Text = listApply.Count(a => a.SQSJ.Value.Year == DateTime.Now.Year && a.SQSJ.Value.Month == DateTime.Now.Month).ToString();
                lblApplyedCount.Text = (listApply.Count(a => a.SQZT != 0) / Convert.ToDouble(listApply.Count) * 100).ToString("f2") + "%";
                lblSuccessCount.Text = (listApply.Count(a => a.SQZT == 1) / Convert.ToDouble(listApply.Count) * 100).ToString("f2") + "%";
                lblBanCount.Text = (listApply.Count(a => a.SQZT == 2) / Convert.ToDouble(listApply.Count) * 100).ToString("f2") + "%";
            }
        }


    }
}