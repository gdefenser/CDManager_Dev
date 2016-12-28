using CDManagerLibrary.Core;
using CDManagerLibrary.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDManager_Dev4.Management.User.ReaderManager
{
    public partial class ReaderDelete : CDPages
    {
        static DateTime now = DateTime.Now;
        static string y1 = (now.Year - 4).ToString().Substring(2);
        static string y2 = (now.Year - 3).ToString().Substring(2);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (now.Month <= 6) { ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('不能在毕业生离校前进行此操作');window.history.back(-1);", true); }
            else
            {
                string where = "(it.DZLX='专科生' or it.DZLX='本科生') and (Substring(it.DZTM,1,3)='" + y1 + "4' or Substring(it.DZTM,1,3)='" + y2 + "3')";
                edsReader.Where = where;
                lvReader.DataBind();

                if (!IsPostBack)
                {
                    int count = cde.Reader.Count(r => (r.DZLX == "专科生" || r.DZLX == "本科生")
                        && (r.DZTM.Substring(0, 3) == y1 + "4" || r.DZTM.Substring(0, 3) == y2 + "3"));
                    if (count > 0) { btnDelete.Enabled = true; }
                    lblTitle.Text = now.Year + "届毕业生读者注销";
                    lblWarning.Text = "注意!以下" + count + "位学生用户一经注销即无法恢复,请谨慎操作!";
                    btnDelete.Attributes.Add("onclick", "javascript:return confirm('你确认要注销这些读者用户吗?')");
                }
            }
        }

        protected void btnGo_Command(object sender, CommandEventArgs e)
        {
            DataPager dpReader = (DataPager)lvReader.FindControl("dpReader");
            TextBox txtPage = (TextBox)dpReader.Controls[4].FindControl("txtPage");

            try
            {
                int max = dpReader.TotalRowCount < dpReader.PageSize ? 1 : dpReader.TotalRowCount / dpReader.PageSize + 1;
                int select = Convert.ToInt32(txtPage.Text);
                if (select < 1) { select = 1; }
                else if (select > max) { select = max; }

                int start_row = (select - 1) * dpReader.PageSize;
                dpReader.SetPageProperties(start_row, dpReader.PageSize, false);
                lvReader.DataBind();
            }
            catch
            { }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int count = 0;
            lvReader.Enabled = false;
            while (true)
            {
                try
                {
                    Reader delete_reader = cde.Reader.First(r => (r.DZLX == "专科生" || r.DZLX == "本科生") &&
                        (r.DZTM.Substring(0, 3) == y1 + "4" || r.DZTM.Substring(0, 3) == y2 + "3"));
                    cde.Reader.Remove(delete_reader);
                    count += cde.SaveChanges();
                }
                catch
                { break; }
            }
            if (count > 0)
            { ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('成功删除" + count + "位读者用户');window.location.href='../../Main.aspx'", true); }
            else
            { ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('没有读者用户被删除');", true); }
        }
    }
}