using CDManagerLibrary.Core;
using CDManagerLibrary.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDManager_Dev4
{
    public partial class Feedback : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
        }

        protected void btnClick_Click(object sender, EventArgs e)
        {
            string xx = txtXX.Text;
            if (!String.IsNullOrEmpty(xx) && xx.Length <= 300)
            {
                string ip = Request.UserHostAddress;
                List<Message> check = cde.Message.Where(m => m.YHLX == "feedback"
                    && m.XXNR.Contains(ip)).ToList();
                if (check.Count == 0 || check.Last().FSSJ.Value.AddMinutes(30) < DateTime.Now)
                {
                    //生成主键ID
                    DateTime now = DateTime.Now;
                    string id = "" + now.Year.ToString() + now.Month.ToString() + now.Day.ToString();
                    var id_count = cde.Message.Where(m => m.XXTM.Contains(id)).ToList();
                    if (id_count.Count > 0)
                    {
                        string max = id_count.Max(m => m.XXTM.Substring(m.XXTM.Length - 8));
                        int temp_id = Convert.ToInt16(max) + 1;
                        id += temp_id.ToString().PadLeft(8, '0');
                    }
                    else
                    {
                        id += "00000001";
                    }



                    string fsr = "";
                    if (Page.User.Identity.IsAuthenticated)
                    {
                        fsr = Page.User.Identity.Name;
                    }
                    else { fsr = "匿名"; }

                    Message new_msg = new Message();
                    new_msg.XXTM = id;
                    new_msg.YHLX = "feedback";
                    //new_msg.SXRTM = sxrtm;
                    new_msg.FSRTM = fsr;
                    new_msg.FSSJ = DateTime.Now;
                    new_msg.XXNR = ip + "@" + xx;
                    new_msg.YD = false;
                    cde.Message.Add(new_msg);

                    if (cde.SaveChanges() > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('建议保存成功!感谢支持光盘系统!');location.href = 'Index.aspx';", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('很抱歉!IP:" + ip + "在半小时内只能发送一次建议!');", true);
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtXX.Text = "";
        }
    }
}