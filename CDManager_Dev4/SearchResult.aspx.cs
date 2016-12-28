using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.Core;
using CDManagerLibrary.EntityFramework;
using System.Drawing;
using System.Web.Security;
namespace CDManager_Dev4
{
    public partial class SearchResult : CDPages
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["Type"]))
            {
                string orderby = "";
                string orderby_str = "";
                string type = Request.QueryString["Type"];
                if (!String.IsNullOrEmpty(Request.QueryString["OrderBy"]))
                {
                    orderby = Request.QueryString["OrderBy"];
                    if (type == "Book")
                    {
                        if (orderby == "ISBN") { orderby_str = "it.ISBN"; }
                        else if (orderby == "ZTM") { orderby_str = "it.ZTM"; }
                        else if (orderby == "CBS") { orderby_str = "it.CBS"; }
                        else if (orderby == "ZRZ") { orderby_str = "it.ZRZ"; }
                    }
                    else if (type == "CD")
                    {
                        if (orderby == "CDXH") { orderby_str = "it.CDXH"; }
                        else if (orderby == "CDMC") { orderby_str = "it.CDMC"; }
                        else if (orderby == "ZXZT") { orderby_str = "it.ZXZT"; }
                    }
                }
                else if (!String.IsNullOrEmpty(Request.QueryString["OrderByDesc"]))
                {
                    orderby = Request.QueryString["OrderByDesc"];
                    if (type == "Book")
                    {
                        if (orderby == "ISBN") { orderby_str = "it.ISBN desc"; }
                        else if (orderby == "ZTM") { orderby_str = "it.ZTM desc"; }
                        else if (orderby == "CBS") { orderby_str = "it.CBS desc"; }
                        else if (orderby == "ZRZ") { orderby_str = "it.ZRZ desc"; }
                    }
                    else if (type == "CD")
                    {
                        if (orderby == "CDXH") { orderby_str = "it.CDXH desc"; }
                        else if (orderby == "CDMC") { orderby_str = "it.CDMC desc"; }
                        else if (orderby == "ZXZT") { orderby_str = "it.ZXZT desc"; }
                    }
                }

                try
                {
                    if (type == "Book")
                    {
                        if (!String.IsNullOrEmpty(orderby_str)) { edsBook.OrderBy = orderby_str; }
                        edsBook.Where = baseBookStr();

                        if (!IsPostBack)
                        {
                            listBook.Visible = true;
                            DataPager dpBook = (DataPager)listBook.FindControl("dpBookTop");
                            Label lblPageCount = (Label)listBook.FindControl("lblPageCount");
                            Label lblPage = (Label)listBook.FindControl("lblPage");
                            lblPage.Text = "1";
                            lblPageCount.Text = (dpBook.TotalRowCount / dpBook.PageSize + 1).ToString();
                            listBook.DataBind();
                        }
                    }
                    else if (type == "CD")
                    {
                        if (!String.IsNullOrEmpty(orderby_str)) { edsCD.OrderBy = orderby_str; }
                        edsCD.Where = baseCDStr();

                        if (!IsPostBack)
                        {
                            listCD.Visible = true;
                            DataPager dpCD = (DataPager)listCD.FindControl("dpCDTop");
                            Label lblPageCount = (Label)listCD.FindControl("lblPageCount");
                            Label lblPage = (Label)listCD.FindControl("lblPage");
                            lblPage.Text = "1";
                            lblPageCount.Text = (dpCD.TotalRowCount / dpCD.PageSize + 1).ToString();
                            listCD.DataBind();
                        }
                    }
                }
                catch { }
            }

        }

        private string baseBookStr()
        {
            string where = "";
            if (Request.QueryString.Count <= 2)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["key"]))
                { where = "Replace(it.ISBN,'-','') like '%'+@Key+'%' or it.ZTM like '%'+@Key+'%' or it.CBS like '%'+@Key+'%' or it.ZRZ like '%'+@Key+'%'"; }
                else if (!String.IsNullOrEmpty(Request.QueryString["ISBN"]))
                { where = "Replace(it.ISBN,'-','') like '%'+@ISBN+'%'"; }
                else if (!String.IsNullOrEmpty(Request.QueryString["ZTM"]))
                { where = "it.ZTM like '%'+@ZTM+'%'"; }
                else if (!String.IsNullOrEmpty(Request.QueryString["CBS"]))
                { where = "it.CBS like '%'+@CBS+'%'"; }
                else if (!String.IsNullOrEmpty(Request.QueryString["ZRZ"]))
                { where = "it.ZRZ like '%'+@ZRZ+'%'"; }
                else
                { where = "it.ISBN=''"; }
                return where;
            }
            else if (Request.QueryString.Count > 2)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["ISBN"]))
                { where += " or Replace(it.ISBN,'-','') like '%'+@ISBN+'%'"; }
                if (!String.IsNullOrEmpty(Request.QueryString["ZTM"]))
                { where += " or it.ZTM like '%'+@ZTM+'%'"; }
                if (!String.IsNullOrEmpty(Request.QueryString["CBS"]))
                { where += " or it.CBS like '%'+@CBS+'%'"; }
                if (!String.IsNullOrEmpty(Request.QueryString["ZRZ"]))
                { where += " or it.ZRZ like '%'+@ZRZ+'%'"; }
                if (where == "") { where = " or it.ISBN=''"; }
                where = where.Substring(4);
                return where;
            }
            else
            {
                return null;
            }
        }

        private string baseCDStr()
        {
            string where = "";
            if (Request.QueryString.Count <= 3)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["key"]))
                { where = "(it.CDXH like '%'+@key+'%' or it.CDMC like '%'+@key+'%')"; }
                else if (!String.IsNullOrEmpty(Request.QueryString["CDXH"]))
                { where = "it.CDXH like '%'+@CDXH+'%'"; }
                else if (!String.IsNullOrEmpty(Request.QueryString["CDMC"]))
                { where = "it.CDMC like '%'+@CDMC+'%'"; }

                if (!String.IsNullOrEmpty(Request.QueryString["ZXZT"]))
                {
                    if (!String.IsNullOrEmpty(where)) { where += " and "; }
                    where += "it.ZXZT=@ZXZT";
                }

                if (where == "") { where = "it.CDXH=''"; }
                return where;

            }
            else if (Request.QueryString.Count > 3)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["key"]))
                { where = "(it.CDXH like '%'+@key+'%' or it.CDMC like '%'+@key+'%')"; }
                else
                {
                    if (!String.IsNullOrEmpty(Request.QueryString["CDXH"]))
                    { where += " or it.CDXH like '%'+@CDXH+'%'"; }
                    if (!String.IsNullOrEmpty(Request.QueryString["CDMC"]))
                    { where += " or it.CDMC like '%'+@CDMC+'%'"; }
                    if (!String.IsNullOrEmpty(where)) { where = where.Substring(4); }
                }

                if (!String.IsNullOrEmpty(Request.QueryString["ZXZT"]))
                {
                    if (!String.IsNullOrEmpty(where)) { where += " and "; }
                    where += "it.ZXZT=@ZXZT";
                }

                if (where == "") { where = " or it.CDXH=''"; }
                return where;
            }
            else
            {
                return null;
            }
        }

        protected void edsBook_QueryCreated(object sender, QueryCreatedEventArgs e)
        {
            var books = e.Query.Cast<Book>();
            Label lblCount = (Label)listBook.FindControl("lblCount");
            lblCount.Text = books.Count().ToString();
        }

        protected void listBook_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                LiteralControl lite = (LiteralControl)e.Item.Controls[0];
                Label lblISBN = (Label)lite.FindControl("lblISBN");
                Label lblCDStatus = (Label)lite.FindControl("lblCDStatus");
                Label lblCDCount = (Label)lite.FindControl("lblCDCount");
                Label lblCDOnline = (Label)lite.FindControl("lblCDOnline");
                string isbn = lblISBN.Text;
                List<CD> listCD = cde.CD.Where(c => c.Book.ISBN == isbn).ToList();

                if (listCD.Count(c => c.ZXZT == 1) > 0)
                {
                    lblCDStatus.ForeColor = Color.Red;
                    lblCDStatus.Font.Bold = true;
                    lblCDStatus.Text = " 在 线 ";
                }
                else { lblCDStatus.Text = "不在线"; }

                lblCDCount.Text = listCD.Count.ToString();
                lblCDOnline.Text = listCD.Count(c => c.ZXZT == 1).ToString();
            }
        }

        protected void listBook_PagePropertiesChanged(object sender, EventArgs e)
        {
            DataPager dpBook = (DataPager)listBook.FindControl("dpBookTop");
            Label lblPage = (Label)listBook.FindControl("lblPage");
            lblPage.Text = (dpBook.StartRowIndex / dpBook.PageSize + 1).ToString();
        }

        protected void edsCD_QueryCreated(object sender, QueryCreatedEventArgs e)
        {
            var cd = e.Query.Cast<CD>();
            Label lblCount = (Label)listCD.FindControl("lblCount");
            lblCount.Text = cd.Count().ToString();
        }

        protected void listCD_PagePropertiesChanged(object sender, EventArgs e)
        {
            DataPager dpCD = (DataPager)listCD.FindControl("dpCDTop");
            Label lblPage = (Label)listCD.FindControl("lblPage");
            lblPage.Text = (dpCD.StartRowIndex / dpCD.PageSize + 1).ToString();
        }

        protected void listCD_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                LiteralControl lite = (LiteralControl)e.Item.Controls[0];
                Label lblISBN = (Label)lite.FindControl("lblISBN");
                Label lblCDXH = (Label)lite.FindControl("lblCDXH");
                Label lblCDStatus = (Label)lite.FindControl("lblCDStatus");
                Label lblApplyed = (Label)lite.FindControl("lblApplyed");
                Button btnApplySubmit = (Button)lite.FindControl("btnApplySubmit");
                Button btnDownload = (Button)lite.FindControl("btnDownload");
                HyperLink linkCDMC = (HyperLink)lite.FindControl("linkCDMC");
                HyperLink linkUpload = (HyperLink)lite.FindControl("linkUpload");
                Panel panelNoLogin = (Panel)lite.FindControl("panelNoLogin");
                Panel panelApply = (Panel)lite.FindControl("panelApply");
                Panel panelDownload = (Panel)lite.FindControl("panelDownload");
                Panel panelUpload = (Panel)lite.FindControl("panelUpload");
                HiddenField hideBookID = (HiddenField)lite.FindControl("hideBookID");

                long bookID = Convert.ToInt64(hideBookID.Value);
                string isbn = cde.Book.First(b => b.BookID == bookID).ISBN;

                linkCDMC.NavigateUrl = "~/BookDetail.aspx?ISBN=" + isbn;
                linkUpload.NavigateUrl = "~/Management/CDManager/CDDetail.aspx?ISBN=" + isbn;

                if (lblCDStatus.Text == "1")
                {
                    lblCDStatus.Font.Bold = true;
                    lblCDStatus.ForeColor = Color.Red;
                    lblCDStatus.Text = "在线";
                }
                else
                { lblCDStatus.Text = "不在线"; }

                var ticket = Context.User.Identity as FormsIdentity;
                if (ticket != null && ticket.IsAuthenticated)
                {
                    if (lblCDStatus.Text == "在线")
                    {
                        panelDownload.Visible = true;
                        btnDownload.Attributes.Add("onclick", "this.form.target='_newName'");
                    }
                    else
                    {
                        string[] data = ticket.Ticket.UserData.Split(',');
                        string roles = data[0];

                        if (roles == "2" || roles == "3") { panelUpload.Visible = true; }
                        else
                        {
                            panelApply.Visible = true;
                            if (cde.ApplyLog.Count(a => a.DZTM == ticket.Name && a.Book.ISBN == isbn && a.SQZT == 0) > 0)
                            { lblApplyed.Visible = true; }
                            else
                            {
                                btnApplySubmit.Attributes.Add("onclick", "javascript:return confirm('你确认提交光盘序号为" + lblCDXH.Text + "的申请吗?')");
                                btnApplySubmit.Visible = true;
                            }
                        }
                    }
                }
                else
                {
                    panelNoLogin.Visible = true;
                }

            }
        }
        //下载资源
        protected void btnDownload_Command(object sender, CommandEventArgs e)
        {
            string CDXH = e.CommandArgument.ToString();
            Response.Redirect("~/CDDownload.ashx?CDXH=" + CDXH);
        }
        //提交申请
        protected void btnApplySubmit_Command(object sender, CommandEventArgs e)
        {
            try
            {
                string dztm = Page.User.Identity.Name;
                long bookID = Convert.ToInt64(e.CommandArgument.ToString());
                if (cde.ApplyLog.Count(a => a.DZTM == dztm && a.BookID == bookID && a.SQZT == 0) == 0)
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
                    new_log.BookID = bookID;
                    new_log.IP = Request.UserHostAddress;
                    cde.ApplyLog.Add(new_log);
                    if (cde.SaveChanges() > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('申请成功!请耐心等待管理员上传资源,切勿重复申请');location.reload();", true);
                    }
                }
                else
                { ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('你已经申请该资源!');location.reload();", true); }
            }
            catch
            { }
        }

        protected void btnBTGo_Command(object sender, CommandEventArgs e)
        {
            DataPager dpBook = (DataPager)listBook.FindControl("dpBookTop");
            TextBox txtPage = (TextBox)dpBook.Controls[3].FindControl("txtPage");
            Label lblPage = (Label)listBook.FindControl("lblPage");
            try
            {
                int max = dpBook.TotalRowCount < dpBook.PageSize ? 1 : dpBook.TotalRowCount / dpBook.PageSize + 1;
                int select = Convert.ToInt32(txtPage.Text);
                if (select < 1) { select = 1; }
                else if (select > max) { select = max; }

                int start_row = (select - 1) * dpBook.PageSize;
                dpBook.SetPageProperties(start_row, dpBook.PageSize, false);
                listBook.DataBind();
                lblPage.Text = (dpBook.StartRowIndex / dpBook.PageSize + 1).ToString();
            }
            catch
            { }
        }

        protected void btnBBGo_Command(object sender, CommandEventArgs e)
        {
            DataPager dpBook = (DataPager)listBook.FindControl("dpBookBottom");
            TextBox txtPage = (TextBox)dpBook.Controls[3].FindControl("txtPage");
            Label lblPage = (Label)listBook.FindControl("lblPage");
            try
            {
                int max = dpBook.TotalRowCount < dpBook.PageSize ? 1 : dpBook.TotalRowCount / dpBook.PageSize + 1;
                int select = Convert.ToInt32(txtPage.Text);
                if (select < 1) { select = 1; }
                else if (select > max) { select = max; }

                int start_row = (select - 1) * dpBook.PageSize;
                dpBook.SetPageProperties(start_row, dpBook.PageSize, false);
                listBook.DataBind();
                lblPage.Text = (dpBook.StartRowIndex / dpBook.PageSize + 1).ToString();
            }
            catch
            { }
        }

        protected void btnCTGo_Command(object sender, CommandEventArgs e)
        {
            DataPager dpCD = (DataPager)listCD.FindControl("dpCDTop");
            TextBox txtPage = (TextBox)dpCD.Controls[3].FindControl("txtPage");
            Label lblPage = (Label)listCD.FindControl("lblPage");
            try
            {
                int max = dpCD.TotalRowCount < dpCD.PageSize ? 1 : dpCD.TotalRowCount / dpCD.PageSize + 1;
                int select = Convert.ToInt32(txtPage.Text);
                if (select < 1) { select = 1; }
                else if (select > max) { select = max; }

                int start_row = (select - 1) * dpCD.PageSize;
                dpCD.SetPageProperties(start_row, dpCD.PageSize, false);
                listCD.DataBind();

                lblPage.Text = (dpCD.StartRowIndex / dpCD.PageSize + 1).ToString();
            }
            catch
            { }
        }

        protected void btnCBGo_Command(object sender, CommandEventArgs e)
        {
            DataPager dpCD = (DataPager)listCD.FindControl("dpCDBottom");
            TextBox txtPage = (TextBox)dpCD.Controls[34].FindControl("txtPage");
            Label lblPage = (Label)listCD.FindControl("lblPage");
            try
            {
                int max = dpCD.TotalRowCount < dpCD.PageSize ? 1 : dpCD.TotalRowCount / dpCD.PageSize + 1;
                int select = Convert.ToInt32(txtPage.Text);
                if (select < 1) { select = 1; }
                else if (select > max) { select = max; }

                int start_row = (select - 1) * dpCD.PageSize;
                dpCD.SetPageProperties(start_row, dpCD.PageSize, false);
                listCD.DataBind();
                lblPage.Text = (dpCD.StartRowIndex / dpCD.PageSize + 1).ToString();
            }
            catch
            { }
        }
    }
}