using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.Core;
using System.Drawing;
using System.Web.Security;

namespace CDManager_Dev4
{
    public partial class BookDetail : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string isbn = Request.QueryString["ISBN"];
            if (!String.IsNullOrEmpty(isbn))
            {
                //string dztm = Page.User.Identity.Name;
                //if (cde.ApplyLog.Count(a => a.DZTM == dztm && a.ISBN == isbn && a.SQZT == 1) > 0)
                //{ lblApplyed.Visible = true; }
                //else
                //{ }
                BindBookDetail(isbn);
            }
            else
            {
                Response.Write("<script>window.history.back(-1);</script>");
            }
        }

        private void BindBookDetail(string isbn)
        {
            try
            {
                int status = 0;
                if (!IsPostBack)
                {
                    Book book = cde.Book.First(b => b.ISBN == isbn);
                    lblTitle.Text = book.ZTM + "(" + book.ISBN + ")";
                    lblISBN.Text = book.ISBN;
                    lblZTM.Text = book.ZTM;
                    linkCBS.Text = book.CBS;
                    linkCBS.NavigateUrl = "~/SearchResult.aspx?CBS=" + book.CBS;
                    linkZRZ.Text = book.ZRZ;
                    linkZRZ.NavigateUrl = "~/SearchResult.aspx?ZRZ=" + book.ZRZ;
                    lblYEMA.Text = book.YEMA;
                    lblDownload.Text = cde.DownloadLog.Where(d => d.CD.ISBN == isbn).Count().ToString();

                    lblApply.Text = book.ApplyLog.Count(a => a.ISBN == isbn).ToString();
                    List<CD> listCD = cde.CD.Where(c => c.ISBN == isbn).ToList();
                    lblOnlineCount.Text = "(光盘数量:" + listCD.Count() + "  在线数量:" + listCD.Count(c => c.ZXZT == 1) + ")";
                  
                    try
                    {
                        status = Convert.ToInt16(cde.CD.Where(i => i.ISBN == isbn).First().ZXZT);
                    }
                    catch { }

                    if (status == 1)
                    {
                        lblIsOnline.Font.Bold = true;
                        lblIsOnline.ForeColor = Color.Red;
                        lblIsOnline.Text = "在线";
                        listCD = listCD.Where(c => c.ZXZT == 1).ToList();
                        for (int i = 0; i < listCD.Count; i++)
                        {

                            CD cd = listCD[i];
                            Literal li_div_b = new Literal();
                            li_div_b.Text = "光盘" + (i + 1) + "<div style='background-color: #FFFFCC; border: 1px dotted #990000;padding-top: 3px; padding-bottom: 3px'>";

                            HyperLink link = new HyperLink();
                            link.Font.Bold = true;
                            link.Text = cd.CDMC;
                            link.NavigateUrl = "~/CDDownload.ashx?CDXH=" + cd.CDXH;
                            link.Target = "_blank";

                            Literal li = new Literal();
                            li.Text = "<br/>";

                            Literal li_div_e = new Literal();
                            li_div_e.Text = "</div><br/>";

                            panelDownload.Controls.Add(li_div_b);
                            panelDownload.Controls.Add(link);
                            panelDownload.Controls.Add(li);
                            panelDownload.Controls.Add(li_div_e);
                        }
                    }
                    else { lblIsOnline.Text = "不在线"; }

                    lblTime.Text = cde.CD.Where(c => c.ISBN == isbn).Select(c => c.CZSJ).Max().ToString();
                }

                var ticket = Context.User.Identity as FormsIdentity;
                if (ticket != null && ticket.IsAuthenticated)
                {
                    if (status == 1)
                    {

                        panelDownload.Visible = true;

                    }
                    else
                    {
                        string[] data = ticket.Ticket.UserData.Split(',');
                        string roles = data[0];

                        if (roles == "2" || roles == "3") 
                        { 
                            panelUpload.Visible = true;
                            linkUpload.NavigateUrl = "~/Management/CDManager/CDDetail.aspx?ISBN=" + isbn;
                        }
                        else
                        {
                            panelApply.Visible = true;
                            if (cde.ApplyLog.Count(a => a.DZTM == ticket.Name && a.ISBN == isbn && a.SQZT == 0) > 0)
                            { lblApplyed.Visible = true; }
                            else { btnApplySubmit.Visible = true; }
                        }
                    }
                }
                else
                {
                    panelNoLogin.Visible = true;
                }
            }
            catch
            { }
        }
        //提交申请
        protected void btnApplySubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string dztm = Page.User.Identity.Name;
                string isbn = lblISBN.Text;
                if (cde.ApplyLog.Count(a => a.DZTM == dztm && a.ISBN == isbn && a.SQZT == 0) > 0)
                {
                    if (IsPostBack)
                    {
                        btnApplySubmit.Visible = false;
                        lblApplyed.Visible = false;
                    }
                    valApplyed.IsValid = false;
                    valApplyed.ErrorMessage = "你已经提交申请,请耐心等待管理员上传资源";

                }
                else
                {
                    //生成主键ID
                    DateTime now = DateTime.Now;
                    string id = "" + now.Year.ToString() + now.Month.ToString() + now.Day.ToString();
                    var id_count = cde.ApplyLog.Where(a => a.SQBH.Contains(id)).ToList();
                    if (id_count.Count > 0)
                    {
                        string max = id_count.Max(a => a.SQBH.Substring(a.SQBH.Length - 6));
                        int temp_id = Convert.ToInt16(max) + 1;
                        id += temp_id.ToString().PadLeft(6, '0');
                    }
                    else
                    {
                        id += "000001";
                    }

                    ApplyLog new_log = new ApplyLog();
                    new_log.SQBH = id;
                    new_log.DZTM = dztm;
                    new_log.SQSJ = DateTime.Now;
                    new_log.SQZT = 0;
                    new_log.ISBN = isbn;
                    new_log.IP = Request.UserHostAddress;
                    cde.ApplyLog.Add(new_log);
                    if (cde.SaveChanges() > 0)
                    {
                        //Literal lite = new Literal();
                        //lite.Text="<script>alert('申请成功!请耐心等待管理员上传资源,切勿重复申请');</script>";
                        //this.Page.Controls.Add(lite);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('申请成功!请耐心等待管理员上传资源,切勿重复申请');location.reload();", true);
                    }
                }
            }
            catch
            { }
        }
    }
}