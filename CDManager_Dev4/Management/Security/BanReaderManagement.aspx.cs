using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.Core;

namespace CDManager_Dev4.Management.Security
{
    public partial class BanReaderManagement : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string dztm = Session["DZTM"].ToString();
                    if (!String.IsNullOrEmpty(dztm))
                    { txtDZTM.Text = dztm; }
                }
                catch { }
            }
        }

        //禁止用户登录
        protected void btnBan_Click(object sender, EventArgs e)
        {
            string dztm = txtDZTM.Text;
            if (String.IsNullOrEmpty(dztm))
            {
                valDZTM.IsValid = false;
                valDZTM.ErrorMessage = "请输入读者条码";
            }
            else
            {
                if (dropJSSJ.SelectedIndex < 1)
                {
                    valJSSJ.IsValid = false;
                }
                else
                {
                    Reader reader = null;
                    try
                    { reader = cde.Reader.Find(dztm); }
                    catch
                    {
                        valDZTM.IsValid = false;
                        valDZTM.ErrorMessage = "读者" + dztm + "不存在";
                        return;
                    }

                    try
                    {
                        if (reader.YHZT == 1)
                        {
                            DateTime kssj = DateTime.Now;
                            DateTime jssj = new DateTime(9999, 12, 31);
                            if (dropJSSJ.SelectedIndex != 5)
                            { jssj = kssj.AddDays(Convert.ToDouble(dropJSSJ.SelectedValue)); }

                            reader.KSSJ = kssj;
                            reader.JSSJ = jssj;
                            reader.JZCZY = Page.User.Identity.Name;
                            reader.YHZT = 0;
                            cde.SaveChanges();
                            gvReader.DataBind();
                        }
                        else
                        {
                            valDZTM.IsValid = false;
                            valDZTM.ErrorMessage = "读者" + dztm + "已被禁止登录";
                        }
                    }
                    catch
                    { Response.Redirect("~/Management/Error.aspx"); }
                }
            }
        }

        protected void gvReader_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //如果是绑定数据行 删除确认框
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    LiteralControl lite = (LiteralControl)e.Row.Cells[0].Controls[0];//强制转换LiteralControl
                    Button btn = (Button)lite.FindControl("btnDelete");
                    lite = (LiteralControl)e.Row.Cells[2].Controls[0];
                    HyperLink link = (HyperLink)lite.FindControl("linkDZTM");
                    btn.Attributes.Add("onclick", "javascript:return confirm('你确认要解除禁止用户:" + link.Text + "的登录吗?')");
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

        protected void gvReader_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            LiteralControl lite;
            lite = (LiteralControl)gvReader.Rows[e.RowIndex].Cells[2].Controls[0];
            TextBox txtEditDZTM = (TextBox)lite.FindControl("txtEditDZTM");
            CustomValidator valEditDZTM = (CustomValidator)lite.FindControl("valEditDZTM");
            HiddenField hideDZTM = (HiddenField)lite.FindControl("hideDZTM");
            string dztm_n = txtEditDZTM.Text;
            string dztm_o = hideDZTM.Value;

            if (String.IsNullOrEmpty(dztm_n))
            {
                valEditDZTM.IsValid = false;
                valEditDZTM.ErrorMessage = "请输入读者条码";
            }
            else
            {
                try
                {
                    lite = (LiteralControl)gvReader.Rows[e.RowIndex].Cells[4].Controls[0];
                    DropDownList dropEditJSSJ = (DropDownList)lite.FindControl("dropEditJSSJ");
                    CustomValidator valEditJSSJ = (CustomValidator)lite.FindControl("valEditJSSJ");
                    if (dropEditJSSJ.SelectedIndex < 1)
                    {
                        valEditJSSJ.IsValid = false;
                        e.Cancel = true;
                    }
                    else
                    {
                        try
                        {
                            DateTime kssj = Convert.ToDateTime(gvReader.Rows[e.RowIndex].Cells[3].Text);
                            DateTime jssj = new DateTime(9999, 12, 31);
                            if (dropEditJSSJ.SelectedIndex != 5)
                            { jssj = kssj.AddDays(Convert.ToDouble(dropEditJSSJ.SelectedValue)); }

                            if (dztm_o == dztm_n)
                            {
                                Reader reader = cde.Reader.First(r => r.DZTM == dztm_o);
                                reader.JSSJ = jssj;
                                cde.SaveChanges();
                                gvReader.DataBind();
                            }
                            else
                            {

                                List<Reader> reader = cde.Reader.Where(r => r.DZTM == dztm_o || r.DZTM == dztm_n).ToList();

                                Reader reader_n = reader[1];
                                if (reader_n.YHZT == 0)
                                {
                                    valEditDZTM.IsValid = false;
                                    valEditDZTM.ErrorMessage = "读者" + dztm_n + "已被禁止登录";
                                    e.Cancel = true;
                                }
                                else
                                {
                                    Reader reader_o = reader[0];
                                    reader_o.YHZT = 1;

                                    reader_n.YHZT = 0;
                                    reader_n.KSSJ = kssj;
                                    reader_n.JSSJ = jssj;
                                    reader_n.JZCZY = Page.User.Identity.Name;
                                    cde.SaveChanges();
                                    gvReader.DataBind();
                                }

                            }

                        }
                        catch
                        {
                            valEditDZTM.IsValid = false;
                            valEditDZTM.ErrorMessage = "读者" + dztm_n + "不存在";
                            e.Cancel = true;
                        }
                    }
                }
                catch
                { Response.Redirect("~/Management/Error.aspx"); }
            }
        }
        //解除禁止管理员
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                string dztm = e.CommandArgument.ToString();
                Reader reader = cde.Reader.Find(dztm);
                reader.YHZT = 1;
                cde.SaveChanges();
                gvReader.DataBind();
            }
            catch
            { Response.Redirect("~/Management/Error.aspx"); }
        }
    }
}