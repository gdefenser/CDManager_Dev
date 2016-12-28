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
    public partial class BanApply : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string isbn = Request.QueryString["ISBN"];
                if (String.IsNullOrEmpty(isbn))
                { ErrorRedirect_Management(); }
                else
                {
                    try
                    {
                        //DateTime date = cde.ApplyLog.OrderBy(a => a.Book.ISBN == isbn).Max(a => a.SQSJ).Value;
                        List<ApplyLog> applylist = cde.ApplyLog.Where(a => a.SQZT == 0 && a.Book.ISBN == isbn).ToList();
                        ApplyLog applylog = applylist.Where(a => a.SQSJ == applylist.Max(a1 => a1.SQSJ)).First();
                        if (applylog != null)
                        {
                            lblISBN.Text = applylog.Book.ISBN;
                            linkZTM.Text = applylog.Book.ZTM;
                            linkZTM.NavigateUrl = "~/Management/CDManager/CDDetail.aspx?ISBN=" + applylog.Book.ISBN;
                            linkApply.Text = applylist.Count.ToString();
                            linkApply.NavigateUrl = "~/Management/CDManager/ApplyLogList.aspx?ISBN=" + applylog.Book.ISBN;
                            lblSQSJ.Text = applylog.SQSJ.ToString();
                        }
                    }
                    catch { }
                    //btnClick.Attributes.Add("onclick", "javascript:return confirm('你确认要忽略图书:" + ((HyperLink)gvApplyLog.Rows[0].Cells[1].Controls[0]).Text + "(" + gvApplyLog.Rows[0].Cells[0].Text + ")的申请吗?')");
                }
            }
        }

        protected void gvApplyLog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //如果是绑定数据行 删除确认框
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    LiteralControl lite = (LiteralControl)e.Row.Cells[2].Controls[0];
                    HyperLink linkCountApply = (HyperLink)lite.FindControl("linkCountApply");
                    string isbn = e.Row.Cells[0].Text;
                    linkCountApply.Text = cde.ApplyLog.Count(a => a.Book.ISBN == isbn && a.SQZT == 0).ToString();
                }
            }
        }

        protected void btnClick_Click(object sender, EventArgs e)
        {
            try
            {
                string isbn = Request.QueryString["ISBN"];
                List<ApplyLog> list = cde.ApplyLog.Where(a => a.Book.ISBN == isbn && a.SQZT == 0).ToList();
                List<string> listMsg = new List<string>();
                foreach (ApplyLog apply in list)
                {
                    apply.CLSJ = DateTime.Now;
                    apply.CLCZY = Page.User.Identity.Name;
                    apply.SQZT = 2;
                    //生成主键ID
                    DateTime now = DateTime.Now;
                    string id = "" + now.Year.ToString() + now.Month.ToString() + now.Day.ToString();
                    var id_count = cde.Message.Where(m => m.XXTM.Contains(id)).ToList();
                    if (id_count.Count > 0 && listMsg.Count == 0)
                    {
                        string max = id_count.Max(m => m.XXTM.Substring(m.XXTM.Length - 8));
                        int temp_id = Convert.ToInt16(max) + 1;
                        id += temp_id.ToString().PadLeft(8, '0');

                    }
                    else if (id_count.Count > 0 || listMsg.Count > 0)
                    {
                        string max = listMsg.Max().Substring(listMsg.Max().Length - 8);
                        int temp_id = Convert.ToInt16(max) + 1;
                        id += temp_id.ToString().PadLeft(8, '0');
                    }
                    else
                    {
                        id += "00000001";
                    }
                    listMsg.Add(id);
                    CDManagerLibrary.EntityFramework.Message new_msg = new CDManagerLibrary.EntityFramework.Message();

                    string msg = "抱歉,你的" + apply.Book.ZTM + "(" + apply.Book.ISBN + ")光盘资源上传资源被管理员拒绝了";
                    if (dropMessage.SelectedIndex > 0)
                    {
                        if (dropMessage.SelectedIndex == 3)
                        {
                            string i_msg = txtMessage.Text;
                            if (!String.IsNullOrEmpty(i_msg))
                            {
                                msg += ",理由:" + i_msg;
                            }
                        }
                        else
                        {
                            msg += ",理由:" + dropMessage.SelectedValue;
                        }
                    }

                    new_msg.XXTM = id;
                    new_msg.YHLX = "reader";
                    new_msg.SXRTM = apply.DZTM;
                    new_msg.FSRTM = "系统消息";
                    new_msg.FSSJ = DateTime.Now;
                    new_msg.XXNR = msg;
                    new_msg.YD = false;
                    cde.Message.Add(new_msg);

                    if (cde.SaveChanges() > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('拒绝申请成功!');location.href = document.referrer;", true);
                    }
                }
            }
            catch
            { ErrorRedirect_Management(); }
        }

        protected void dropMessage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropMessage.SelectedIndex == 4) { txtMessage.Visible = true; }
            else { txtMessage.Visible = false; }

        }
    }
}