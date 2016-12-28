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
    public partial class NewMessage : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string tm = Session["FSTM"].ToString();
                    txtTM.Text = tm;
                    dropSXRLX.SelectedIndex = 1;
                }
                catch
                { }
            }
        }
        //发送消息
        protected void btnNew_Click(object sender, EventArgs e)
        {
            string sxrtm = txtTM.Text;
            if (String.IsNullOrEmpty(sxrtm))
            {
                valTM.IsValid = false;
                valTM.ErrorMessage = "请输入收信人条码";
            }
            else if (dropSXRLX.SelectedIndex == 0)
            {
                valSXRLX.IsValid = false;
                valSXRLX.ErrorMessage = "请选择收信人类型";
            }
            else
            {
                string xxnr = txtXXNR.Text;
                if (String.IsNullOrEmpty(xxnr))
                {
                    valXXNR.IsValid = false;
                    valXXNR.ErrorMessage = "请输入消息内容";
                }
                else
                {
                    string yhlx=dropSXRLX.SelectedValue;
                    if (yhlx == "reader")
                    {
                        try
                        { Reader reader = cde.Reader.First(r => r.DZTM == sxrtm); }
                        catch
                        {
                            valTM.IsValid = false;
                            valTM.ErrorMessage = "读者" + sxrtm + "不存在";
                            return;
                        }
                    }
                    else if (yhlx == "admin")
                    {
                        try
                        { Admin admin = cde.Admin.First(a => a.GLYTM == sxrtm); }
                        catch
                        {
                            valTM.IsValid = false;
                            valTM.ErrorMessage = "管理员" + sxrtm + "不存在";
                            return;
                        }
                    }
                    else { return; }

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
                    new_msg.YHLX = yhlx;
                    new_msg.SXRTM = sxrtm;
                    new_msg.FSRTM = Page.User.Identity.Name;
                    new_msg.FSSJ = DateTime.Now;
                    new_msg.XXNR = xxnr;
                    new_msg.YD = false;
                    cde.Message.Add(new_msg);

                    if (cde.SaveChanges() > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('消息发送成功!');location.href = '../Main.aspx';", true);
                    }
                }
            }
        }
        //重置输入
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtTM.Text = "";
            dropSXRLX.SelectedIndex = 0;
            txtXXNR.Text = "";
        }
        //插入URL
        //protected void btnURL_Click(object sender, EventArgs e)
        //{
        //    txtXXNR.Text += "[URL][/URL]";
        //}
    }
}