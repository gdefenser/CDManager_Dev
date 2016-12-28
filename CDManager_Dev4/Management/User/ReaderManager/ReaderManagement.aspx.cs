using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.Core;

namespace CDManager_Dev4.Management.User.ReaderManager
{
    public partial class ReaderManagement : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dropDZLX.Items.Clear();
                dropDZLX.Items.Add(new ListItem("=选择读者类型=", "=选择读者类型="));
                List<string> listDZLX = cde.Reader.Select(r => r.DZLX).Distinct().ToList();
                foreach (string l_dzlx in listDZLX)
                {
                    dropDZLX.Items.Add(new ListItem(l_dzlx, l_dzlx));
                }

                DateTime now = DateTime.Now;
                if (now.Month > 6)
                {
                    btnReaderDelete.Visible = true;
                    btnReaderDelete.Text = now.Year + "届毕业生读者注销";
                }
                //
                //
                //    linkReaderDelete.Visible = true;
                //    linkReaderDelete.Text = 
                //}              
            }

            string dztm = Server.UrlDecode(Request.QueryString["DZTM"]);
            string dzlx = Server.UrlDecode(Request.QueryString["DZLX"]);
            if (!String.IsNullOrEmpty(dztm) || !String.IsNullOrEmpty(dzlx))
            {
                string where = "";
                if (!String.IsNullOrEmpty(dztm))
                {
                    if (!IsPostBack)
                    {
                        rdoSelect.Checked = false;
                        rdoDZTM.Checked = true;
                        panelSelect.Visible = false;
                        panelDZTM.Visible = true;
                        txtDZTM.Text = dztm;
                    }
                    where = "it.DZTM like '%" + dztm + "%'";
                }
                else if (!String.IsNullOrEmpty(dzlx))
                {

                    if (!IsPostBack)
                    {
                        rdoSelect.Checked = true;
                        rdoDZTM.Checked = false;
                        panelSelect.Visible = true;
                        panelDZTM.Visible = false;
                        dropDZLX.SelectedValue = dzlx;
                    }

                    where = "it.DZLX='" + dzlx + "'";
                    if (dzlx == "本科生" || dzlx == "专科生" || dzlx == "学生")
                    {
                        if (!IsPostBack)
                        {
                            trXS.Visible = true;
                            trOther.Visible = false;
                            BindXS(dzlx);
                        }

                        string grade = Server.UrlDecode(Request.QueryString["Grade"]);
                        if (!String.IsNullOrEmpty(grade))
                        {
                            try
                            {
                                if (!IsPostBack)
                                {
                                    BindXSYJDW(dzlx, grade);
                                    dropXSGrade.SelectedValue = grade;
                                }
                                where += " and Substring(it.DZTM,1,2)='" + grade + "'";
                            }
                            catch { }
                        }
                        string yjdw = Server.UrlDecode(Request.QueryString["YJDW"]);
                        if (!String.IsNullOrEmpty(yjdw))
                        {
                            if (!IsPostBack)
                            {
                                BindXSEJDW(dzlx, grade, yjdw);
                                dropXSYJDW.SelectedValue = yjdw;
                            }
                            where += " and it.YJDW='" + yjdw + "'";
                        }
                        string ejdw = Server.UrlDecode(Request.QueryString["EJDW"]);
                        if (!String.IsNullOrEmpty(ejdw))
                        {
                            if (!IsPostBack)
                            { dropXSEJDW.SelectedValue = ejdw; }
                            where += " and it.EJDW='" + ejdw + "'";
                        }
                    }
                    else
                    {
                        if (!IsPostBack)
                        {
                            trXS.Visible = false;
                            trOther.Visible = true;
                            BindOther(dzlx);
                        }
                        string yjdw = Server.UrlDecode(Request.QueryString["YJDW"]);
                        if (!String.IsNullOrEmpty(yjdw))
                        {
                            if (!IsPostBack)
                            {
                                BindOtherEJDW(dzlx, yjdw);
                                dropOtherYJDW.SelectedValue = yjdw;
                            }
                            where += " and it.YJDW='" + yjdw + "'";
                        }
                        string ejdw = Server.UrlDecode(Request.QueryString["EJDW"]);
                        if (!String.IsNullOrEmpty(ejdw))
                        {
                            if (!IsPostBack)
                            { dropOtherEJDW.SelectedValue = ejdw; }
                            where += " and it.EJDW='" + ejdw + "'";
                        }
                    }
                }
                lvReader.Visible = true;
                edsReader.Where = where;
                lvReader.DataBind();
            }
        }
        //查找读者
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            if (rdoDZTM.Checked)
            {
                if (!String.IsNullOrEmpty(txtDZTM.Text))
                { Response.Redirect("~/Management/User/ReaderManager/ReaderManagement.aspx?DZTM=" + txtDZTM.Text); }
            }
            else if (rdoSelect.Checked)
            {
                if (dropDZLX.SelectedIndex > 0)
                {
                    string dzlx = dropDZLX.SelectedValue;
                    string query = "DZLX=" + dzlx;
                    if (dzlx == "本科生" || dzlx == "专科生" || dzlx == "学生")
                    {
                        if (dropXSGrade.SelectedIndex > 0) { query += "&Grade=" + dropXSGrade.SelectedValue; }
                        if (dropXSYJDW.SelectedIndex > 0) { query += "&YJDW=" + dropXSYJDW.SelectedValue; }
                        if (dropXSEJDW.SelectedIndex > 0) { query += "&EJDW=" + dropXSEJDW.SelectedValue; }
                    }
                    else
                    {
                        if (dropOtherYJDW.SelectedIndex > 0) { query += "&YJDW=" + dropOtherYJDW.SelectedValue; }
                        if (dropOtherEJDW.SelectedIndex > 0) { query += "&EJDW=" + dropOtherEJDW.SelectedValue; }
                    }
                    Response.Redirect("~/Management/User/ReaderManager/ReaderManagement.aspx?" + query);
                }
            }
        }
        //删除读者
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                string dztm = e.CommandArgument.ToString();
                Reader delete_reader = cde.Reader.First(r => r.DZTM == dztm);
                cde.Reader.Remove(delete_reader);
                SaveAndRedirect_Management();
            }
            catch
            {
                Response.Redirect("~/Management/Error.aspx");
            }
        }

        protected void dropDZLX_SelectedIndexChanged(object sender, EventArgs e)
        {
            string select = dropDZLX.SelectedValue;
            if (select == "本科生" || select == "专科生" || select == "学生")
            {
                trXS.Visible = true;
                trOther.Visible = false;
                BindXS(select);

            }
            else if (select != "=选择读者类型=")
            {
                trXS.Visible = false;
                trOther.Visible = true;
                BindOther(select);
            }
            else
            {
                lvReader.Visible = false;
            }
        }

        protected void rdoDZTM_CheckedChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (rdoDZTM.Checked)
                {
                    panelDZTM.Visible = true;
                    panelSelect.Visible = false;
                }
                else
                {
                    panelDZTM.Visible = false;
                    panelSelect.Visible = true;
                }
            }
        }

        protected void rdoSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (rdoSelect.Checked)
                {
                    panelDZTM.Visible = false;
                    panelSelect.Visible = true;
                }
                else
                {
                    panelDZTM.Visible = true;
                    panelSelect.Visible = false;
                }
            }
        }

        protected void dropXSGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dzlx = dropDZLX.SelectedValue;
            string grade = dropXSGrade.SelectedValue;
            BindXSYJDW(dzlx, grade);
        }

        protected void dropXSYJDW_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dzlx = dropDZLX.SelectedValue;
            string grade = dropXSGrade.SelectedValue;
            string yjdw = dropXSYJDW.SelectedValue;
            BindXSEJDW(dzlx, grade, yjdw);
        }

        protected void dropOtherYJDW_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dzlx = dropDZLX.SelectedValue;
            string yjdw = dropOtherYJDW.SelectedValue;
            BindOtherEJDW(dzlx, yjdw);
        }

        private string QueryString(string request)
        {
            string query = "";
            if (!String.IsNullOrEmpty(Request.QueryString["DZLX"])) { query = "&DZLX=" + Request.QueryString["DZLX"]; }
            if (!String.IsNullOrEmpty(Request.QueryString["Grade"])) { query = "&Grade=" + Request.QueryString["Grade"]; }
            if (!String.IsNullOrEmpty(Request.QueryString["YJDW"])) { query = "&YJDW=" + Request.QueryString["YJDW"]; }
            if (!String.IsNullOrEmpty(Request.QueryString["EJDW"])) { query = "&EJDW=" + Request.QueryString["EJDW"]; }
            query = request + query;
            return query;
        }

        private void BindXS(string dzlx)
        {
            dropXSGrade.Items.Clear();
            dropXSGrade.Items.Add(new ListItem("=选择年级="));
            List<Reader> listReader = cde.Reader.Where(r => r.DZLX == dzlx).ToList();
            List<string> listGrade = listReader.Select(r => r.DZTM.Substring(0, 2)).Distinct().ToList();
            foreach (string grade in listGrade)
            {
                try
                {
                    if (Convert.ToInt64("20" + grade) <= DateTime.Now.Year)
                    { dropXSGrade.Items.Add(new ListItem("20" + grade + "级", grade)); }
                }
                catch { continue; }
            }
        }

        private void BindOther(string dzlx)
        {
            dropOtherYJDW.Items.Clear();
            dropOtherYJDW.Items.Add(new ListItem("=选择一级单位="));

            List<Reader> listReader = cde.Reader.Where(r => r.DZLX == dzlx).ToList();
            List<string> listYJDW = listReader.Select(r => r.YJDW).Distinct().ToList();

            foreach (string yjdw in listYJDW)
            {
                dropOtherYJDW.Items.Add(new ListItem(yjdw, yjdw));
            }
        }

        private void BindXSYJDW(string dzlx, string grade)
        {
            dropXSYJDW.Items.Clear();
            dropXSYJDW.Items.Add(new ListItem("=选择一级单位="));
            List<Reader> listReader = cde.Reader.Where(r => r.DZLX == dzlx && r.DZTM.Substring(0, 2) == grade).ToList();
            List<string> listYJDW = listReader.OrderBy(r => r.YJDW).Select(r => r.YJDW).Distinct().ToList();
            foreach (string yjdw in listYJDW)
            {
                dropXSYJDW.Items.Add(new ListItem(yjdw, yjdw));
            }
        }

        private void BindXSEJDW(string dzlx, string grade, string yjdw)
        {
            dropXSEJDW.Items.Clear();
            dropXSEJDW.Items.Add(new ListItem("=选择二级单位="));
            List<Reader> listReader = cde.Reader.Where(r => r.DZLX == dzlx && r.DZTM.Substring(0, 2) == grade && r.YJDW == yjdw).ToList();
            List<string> listEJDW = listReader.OrderBy(r => r.EJDW).Select(r => r.EJDW).Distinct().ToList();

            foreach (string ejdw in listEJDW)
            {
                dropXSEJDW.Items.Add(new ListItem(ejdw, ejdw));
            }
        }

        private void BindOtherEJDW(string dzlx, string yjdw)
        {
            dropOtherEJDW.Items.Clear();
            dropOtherEJDW.Items.Add(new ListItem("=选择二级单位="));
            List<Reader> listReader = cde.Reader.Where(r => r.DZLX == dzlx && r.YJDW == yjdw).ToList();
            List<string> listEJDW = listReader.OrderBy(r => r.EJDW).Select(r => r.EJDW).Distinct().ToList();

            foreach (string ejdw in listEJDW)
            {
                dropOtherEJDW.Items.Add(new ListItem(ejdw, ejdw));
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
        //删除确认框
        protected void lvReader_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //如果是绑定数据行 
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                HyperLink linkDZTM = (HyperLink)dataItem.FindControl("linkDZTM");
                Button btn = (Button)dataItem.FindControl("btnDelete");
                btn.Attributes.Add("onclick", "javascript:return confirm('你确认要删除图书:" + linkDZTM.Text + "吗?')");
            }
        }

        protected void btnReaderDelete_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Management/User/ReaderManager/ReaderDelete.aspx");
        }
    }
}