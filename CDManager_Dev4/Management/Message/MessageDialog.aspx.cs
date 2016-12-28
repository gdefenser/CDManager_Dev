using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.Core;

namespace CDManager_Dev4.Management.Message
{
    public partial class MessageDialog : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string tm = Request.QueryString["TM"];
                Session["MTM"] = Page.User.Identity.Name;
                lblTitle.Text = tm;
                List<CDManagerLibrary.EntityFramework.Message> list = cde.Message.Where(m => m.SXRTM == Page.User.Identity.Name && !m.YD.Value).ToList();
                if (list.Count > 0)
                {
                    foreach (CDManagerLibrary.EntityFramework.Message msg in list)
                    {
                        msg.YD = true;
                    }
                    cde.SaveChanges();
                }
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                string xxnr = txtXXNR.Text;
                if (String.IsNullOrEmpty(xxnr))
                {
                    valXXNR.IsValid = false;
                    valXXNR.ErrorMessage = "请输入消息内容";
                }
                else
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
                    CDManagerLibrary.EntityFramework.Message new_msg = new CDManagerLibrary.EntityFramework.Message();
                    new_msg.XXTM = id;
                    new_msg.YHLX = "admin";
                    new_msg.SXRTM = lblTitle.Text;
                    new_msg.FSRTM = Page.User.Identity.Name;
                    new_msg.FSSJ = DateTime.Now;
                    new_msg.XXNR = xxnr;
                    new_msg.YD = false;
                    cde.Message.Add(new_msg);
                    cde.SaveChanges();
                    lvMessage.DataBind();
                }
            }
            catch { }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtXXNR.Text = "";
        }
    }
}