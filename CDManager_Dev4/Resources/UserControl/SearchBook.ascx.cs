using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDManager_Dev4.Resources.UserControl
{
    public partial class SearchBook : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Count > 0)
                {
                    string type = Request.QueryString["Type"];
                    if (!String.IsNullOrEmpty(type))
                    {
                        if (type == "Book")
                        {
                            rdoBook.Checked = true;
                            trBook.Visible = true;
                            rdoCD.Checked = false;
                            trCD.Visible = false;

                            if (Request.QueryString[1] != "OrderBy" || Request.QueryString[1] != "OrderByDesc")
                            {
                                txtKeyword.Text = Request.QueryString[1];
                                if (!String.IsNullOrEmpty(Request.QueryString["ISBN"])) { chkISBN.Checked = true; }
                                if (!String.IsNullOrEmpty(Request.QueryString["ZTM"])) { chkZTM.Checked = true; }
                                if (!String.IsNullOrEmpty(Request.QueryString["CBS"])) { chkCBS.Checked = true; }
                                if (!String.IsNullOrEmpty(Request.QueryString["ZRZ"])) { chkZRZ.Checked = true; }
                            }

                            if (!String.IsNullOrEmpty(Request.QueryString["OrderBy"]))
                            {
                                string order = Request.QueryString["OrderBy"];
                                if (order == "ISBN") { dropSelect.SelectedIndex = 1; }
                                else if (order == "ZTM") { dropSelect.SelectedIndex = 2; }
                                else if (order == "CBS") { dropSelect.SelectedIndex = 3; }
                                else if (order == "ZRZ") { dropSelect.SelectedIndex = 4; }
                                rdoUp.Checked = true;
                            }
                            else if (!String.IsNullOrEmpty(Request.QueryString["OrderByDesc"]))
                            {
                                string order = Request.QueryString["OrderByDesc"];
                                if (order == "ISBN") { dropSelect.SelectedIndex = 1; }
                                else if (order == "ZTM") { dropSelect.SelectedIndex = 2; }
                                else if (order == "CBS") { dropSelect.SelectedIndex = 3; }
                                else if (order == "ZRZ") { dropSelect.SelectedIndex = 4; }
                                rdoDown.Checked = true;
                            }
                        }
                        else if (type == "CD")
                        {
                            rdoBook.Checked = false;
                            trBook.Visible = false;
                            rdoCD.Checked = true;
                            trCD.Visible = true;

                            if (Request.QueryString[1] != "OrderBy" || Request.QueryString[1] != "OrderByDesc")
                            {
                                if (Request.QueryString.GetKey(1) != "ZXZT") { txtKeyword.Text = Request.QueryString[1]; }
                                if (!String.IsNullOrEmpty(Request.QueryString["CDXH"])) { chkCDXH.Checked = true; }
                                if (!String.IsNullOrEmpty(Request.QueryString["CDMC"])) { chkCDMC.Checked = true; }
                                if (!String.IsNullOrEmpty(Request.QueryString["ZXZT"]))
                                {
                                    string zxzt = Request.QueryString["ZXZT"];
                                    if (zxzt == "0") { dropZXZT.SelectedValue = "不在线"; }
                                    else if (zxzt == "1") { dropZXZT.SelectedValue = "在线"; }
                                }
                            }

                            if (!String.IsNullOrEmpty(Request.QueryString["OrderBy"]))
                            {
                                string order = Request.QueryString["OrderBy"];
                                if (order == "CDXH") { dropSelectCD.SelectedIndex = 1; }
                                else if (order == "CDMC") { dropSelectCD.SelectedIndex = 2; }
                                else if (order == "ZXZT") { dropSelectCD.SelectedIndex = 3; }
                                rdoCDUp.Checked = true;
                            }
                            else if (!String.IsNullOrEmpty(Request.QueryString["OrderByDesc"]))
                            {
                                string order = Request.QueryString["OrderByDesc"];
                                if (order == "CDXH") { dropSelectCD.SelectedIndex = 1; }
                                else if (order == "CDMC") { dropSelectCD.SelectedIndex = 2; }
                                else if (order == "ZXZT") { dropSelectCD.SelectedIndex = 3; }
                                rdoCDDown.Checked = true;
                            }
                        }
                    }
                }
            }
        }

        //检索图书按钮
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string txt = txtKeyword.Text.Trim();
            if (String.IsNullOrEmpty(txt))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('请输入搜索关键字');", true);
            }
            else
            {
                DateTime last_time = new DateTime();
                try
                {
                    last_time = Convert.ToDateTime(Session["LastTime"]);
                }
                catch
                { }
                if (last_time.Year == 1 || last_time.AddSeconds(5) < DateTime.Now)
                {

                    string query = "";
                    if (rdoBook.Checked)
                    {
                        if (chkISBN.Checked) { query += "&ISBN=" + txt; }
                        if (chkZTM.Checked) { query += "&ZTM=" + Server.UrlEncode(txt); }
                        if (chkCBS.Checked) { query += "&CBS=" + txt; }
                        if (chkZRZ.Checked) { query += "&ZRZ=" + txt; }

                        if (String.IsNullOrEmpty(query)) { query = "&key=" + Server.UrlEncode(txt); }

                        query = "Type=Book&" + query.Substring(1);

                        if (dropSelect.SelectedIndex > 0)
                        {
                            if (rdoUp.Checked)
                            {
                                if (dropSelect.SelectedIndex == 1) { query += "&OrderBy=ISBN"; }
                                else if (dropSelect.SelectedIndex == 2) { query += "&OrderBy=ZTM"; }
                                else if (dropSelect.SelectedIndex == 3) { query += "&OrderBy=CBS"; }
                                else if (dropSelect.SelectedIndex == 4) { query += "&OrderBy=ZRZ"; }
                            }
                            else if (rdoDown.Checked)
                            {
                                if (dropSelect.SelectedIndex == 1) { query += "&OrderByDesc=ISBN"; }
                                else if (dropSelect.SelectedIndex == 2) { query += "&OrderByDesc=ZTM"; }
                                else if (dropSelect.SelectedIndex == 3) { query += "&OrderByDesc=CBS"; }
                                else if (dropSelect.SelectedIndex == 4) { query += "&OrderByDesc=ZRZ"; }
                            }
                        }
                    }
                    else if (rdoCD.Checked)
                    {
                        if (chkCDXH.Checked) { query += "&CDXH=" + txt; }
                        if (chkCDMC.Checked) { query += "&CDMC=" + Server.UrlEncode(txt); }
                        if (String.IsNullOrEmpty(query)) { query = "&key=" + Server.UrlEncode(txt); }

                        if (dropZXZT.SelectedIndex == 1) { query += "&ZXZT=1"; }
                        else if (dropZXZT.SelectedIndex == 2) { query += "&ZXZT=0"; }

                        if (dropSelectCD.SelectedIndex > 0)
                        {
                            if (rdoCDUp.Checked)
                            {
                                if (dropSelectCD.SelectedIndex == 1) { query += "&OrderBy=CDXH"; }
                                else if (dropSelectCD.SelectedIndex == 2) { query += "&OrderBy=CDMC"; }
                                else if (dropSelectCD.SelectedIndex == 3) { query += "&OrderBy=ZXZT"; }
                            }
                            else if (rdoCDDown.Checked)
                            {
                                if (dropSelectCD.SelectedIndex == 1) { query += "&OrderByDesc=CDXH"; }
                                else if (dropSelectCD.SelectedIndex == 2) { query += "&OrderByDesc=CDMC"; }
                                else if (dropSelectCD.SelectedIndex == 3) { query += "&OrderByDesc=ZXZT"; }
                            }
                        }
                        query = "Type=CD" + query;
                    }

                    if (!String.IsNullOrEmpty(query))
                    {
                        Session["LastTime"] = DateTime.Now;
                        Response.Redirect("~/SearchResult.aspx?" +query);

                    }
                    else { Response.Redirect("~/SearchResult.aspx"); }
                }
                else//检索时间间隔过短
                { Response.Redirect("~/TimeSpanTooShort.aspx"); }
            }
        }

        protected void chkCDXH_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void rdoBook_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoBook.Checked)
            {
                trBook.Visible = true;
                trCD.Visible = false;
            }
            else
            {
                trBook.Visible = false;
                trCD.Visible = true;
            }
        }

        protected void rdoCD_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCD.Checked)
            {
                trBook.Visible = false;
                trCD.Visible = true;
            }
            else
            {
                trBook.Visible = true;
                trCD.Visible = false;
            }
        }
    }
}