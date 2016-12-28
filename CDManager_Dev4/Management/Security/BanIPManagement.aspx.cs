using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.Core;
using System.Text.RegularExpressions;

namespace CDManager_Dev4.Management.Security
{
    public partial class BanIPManagement : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string jzip = Session["JZIP"].ToString();
                    if (!String.IsNullOrEmpty(jzip))
                    {txtJZIP.Text = jzip; }
                }
                catch { }
            }
        }

        protected void btnBan_Click(object sender, EventArgs e)
        {
            string ip = txtJZIP.Text;
            if (String.IsNullOrEmpty(ip))
            {
                valJZIP.IsValid = false;
                valJZIP.ErrorMessage = "请输入IP地址";
            }
            else
            {
                if (!IsIP(ip))
                {
                    valJZIP.IsValid = false;
                    valJZIP.ErrorMessage = "请输入正确IP地址";
                }
                else if (cde.BanIP.Count(i => i.JZIP == ip) > 0)
                {
                    valJZIP.IsValid = false;
                    valJZIP.ErrorMessage = "IP地址:" + ip + "已被禁止登录";
                }
                else
                {
                    if (dropJSSJ.SelectedIndex < 1)
                    {
                        valJSSJ.IsValid = false;
                    }
                    else
                    {
                        DateTime kssj = DateTime.Now;
                        DateTime jssj = new DateTime(9999, 12, 31);
                        if (dropJSSJ.SelectedIndex != 5)
                        { jssj = kssj.AddDays(Convert.ToDouble(dropJSSJ.SelectedValue)); }

                        BanIP new_banIP = new BanIP();
                        new_banIP.IPTM = DateTime.Now.ToFileTime().ToString();
                        new_banIP.KSSJ = kssj;
                        new_banIP.JSSJ = jssj;
                        new_banIP.JZIP = ip;
                        new_banIP.JZCZY = Page.User.Identity.Name;

                        cde.BanIP.Add(new_banIP);
                        cde.SaveChanges();
                        gvBanIP.DataBind();
                    }
                }
            }
        }

        protected void gvBanIP_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //如果是绑定数据行 删除确认框
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    LiteralControl lite = (LiteralControl)e.Row.Cells[0].Controls[0];//强制转换LiteralControl
                    Button btn = (Button)lite.FindControl("btnDelete");
                    lite = (LiteralControl)e.Row.Cells[2].Controls[0];
                    Label lbl = (Label)lite.FindControl("lblJZIP");
                    btn.Attributes.Add("onclick", "javascript:return confirm('你确认要解除禁止IP:" + lbl.Text + "的登录吗?')");
                }
            }
            //如果是编辑数据行 绑定编辑时DropDownList默认值
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Edit ||
                    e.Row.RowState == (DataControlRowState.Alternate | DataControlRowState.Edit))
                {
                    DateTime kssj = Convert.ToDateTime(e.Row.Cells[3].Text);
                    DateTime jssj = Convert.ToDateTime(((HiddenField)e.Row.Cells[4].Controls[1]).Value);

                    LiteralControl lite = (LiteralControl)e.Row.Cells[4].Controls[0];//强制转换LiteralControl
                    DropDownList dropEditJSSJ = (DropDownList)lite.FindControl("dropEditJSSJ");

                    if (jssj != new DateTime(9999, 12, 31))
                    {
                        TimeSpan ts = jssj - kssj;
                        dropEditJSSJ.SelectedValue = ts.Days.ToString();
                    }
                    else
                    {
                        dropEditJSSJ.SelectedIndex = 5;
                    }
                }
            }
        }
        //Gridview更新
        protected void gvBanIP_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            LiteralControl lite;
            lite = (LiteralControl)gvBanIP.Rows[e.RowIndex].Cells[2].Controls[0];
            TextBox txtEditJZIP = (TextBox)lite.FindControl("txtEditJZIP");
            CustomValidator valEditJZIP = (CustomValidator)lite.FindControl("valEditJZIP");
            HiddenField hideIPTM = (HiddenField)lite.FindControl("hideIPTM");
            string ip = txtEditJZIP.Text;
            string iptm = hideIPTM.Value;


            if (String.IsNullOrEmpty(ip))
            {
                valEditJZIP.IsValid = false;
                valEditJZIP.ErrorMessage = "请输入IP地址";
                e.Cancel = true;
            }
            else
            {

                if (!IsIP(ip))
                {
                    valEditJZIP.IsValid = false;
                    valEditJZIP.ErrorMessage = "请输入正确IP地址";
                    e.Cancel = true;
                }
                else
                {
                    try
                    {
                        List<BanIP> list = cde.BanIP.Where(b => b.JZIP == ip || b.IPTM == iptm).ToList();

                        lite = (LiteralControl)gvBanIP.Rows[e.RowIndex].Cells[4].Controls[0];
                        DropDownList dropEditJSSJ = (DropDownList)lite.FindControl("dropEditJSSJ");
                        CustomValidator valEditJSSJ = (CustomValidator)lite.FindControl("valEditJSSJ");

                        if (dropEditJSSJ.SelectedIndex < 1)
                        {
                            valEditJSSJ.IsValid = false;
                            e.Cancel = true;
                        }
                        else
                        {
                            if (list.Count(b => b.IPTM != iptm) > 0)
                            {
                                valEditJZIP.IsValid = false;
                                valEditJZIP.ErrorMessage = "IP地址:" + ip + "已被禁止登录";
                                e.Cancel = true;
                            }
                            else
                            {
                                DateTime kssj = Convert.ToDateTime(gvBanIP.Rows[e.RowIndex].Cells[3].Text);
                                DateTime jssj = new DateTime(9999, 12, 31);
                                if (dropEditJSSJ.SelectedIndex != 5)
                                { jssj = kssj.AddDays(Convert.ToDouble(dropEditJSSJ.SelectedValue)); }
                                BanIP update_banIP = list.First();
                                update_banIP.JZIP = ip;//更新IP
                                update_banIP.JSSJ = jssj;//更新结束时间
                                cde.SaveChanges();
                                gvBanIP.DataBind();
                            }
                        }
                    }
                    catch
                    { Response.Redirect("~/Management/Error.aspx"); }
                }
            }
        }

        //验证是否IP
        private bool IsIP(string ip)
        {
            string num = @"(25[0-5]|2[0-4]\d|[0-1]\d{2}|[1-9]?\d)";
            if (Regex.IsMatch(ip, ("^" + num + "\\." + num + "\\." + num + "\\." + num + "$")))
            { return true; }
            else
            { return false; }
        }
        //删除禁止IP
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                string iptm = e.CommandArgument.ToString();
                BanIP delete_banIP = cde.BanIP.First(b => b.IPTM == iptm);
                cde.BanIP.Remove(delete_banIP);
                cde.SaveChanges();
                gvBanIP.DataBind();
            }
            catch
            { Response.Redirect("~/Management/Error.aspx"); }
        }
    }
}