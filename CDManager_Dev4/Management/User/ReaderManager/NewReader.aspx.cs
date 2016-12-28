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
    public partial class NewReader : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDZLX();
            }
        }
        //新建读者用户
        protected void btnNew_Click(object sender, EventArgs e)
        {
            string dztm = txtDZTM.Text;

            string dzlx = "";
            string yjdw = "";
            string ejdw = "";

            if (dropDZLX.SelectedIndex == dropDZLX.Items.Count - 1 &&
                txtDZLX.Visible && !String.IsNullOrEmpty(txtDZLX.Text))
            { dzlx = txtDZLX.Text; }
            else if (dropDZLX.SelectedIndex > 0) { dzlx = dropDZLX.SelectedValue; }
            else { valDZLX.IsValid = false; }

            if (dropYJDW.SelectedIndex == dropYJDW.Items.Count - 1 &&
                txtYJDW.Visible && !String.IsNullOrEmpty(txtYJDW.Text))
            { yjdw = txtYJDW.Text; }
            else if (dropYJDW.SelectedIndex > 0) { yjdw = dropYJDW.SelectedValue; }
            else { valYJDW.IsValid = false; }

            if (dropEJDW.SelectedIndex == dropEJDW.Items.Count - 1 &&
                txtEJDW.Visible && !String.IsNullOrEmpty(txtEJDW.Text))
            { ejdw = txtEJDW.Text; }
            else if (dropEJDW.SelectedIndex > 0) { ejdw = dropEJDW.SelectedValue; }
            else { valEJDW.IsValid = false; }

            if (String.IsNullOrEmpty(dztm))
            {
                valDZTM.IsValid = false;
                valDZTM.ErrorMessage = "请输入读者条码";
            }
            else if (cde.Admin.Count(a => a.GLYTM == dztm) > 0)
            {
                valDZTM.IsValid = false;
                valDZTM.ErrorMessage = "读者账号" + dztm + "已存在";
            }
            else if (CalendarExtenderYXRQ.SelectedDate <= DateTime.Now)
            {
                valYXRQ.IsValid = false;
            }
            else if (valDZLX.IsValid && valYJDW.IsValid && valEJDW.IsValid)
            {

                string xm = txtXM.Text;
                if (String.IsNullOrEmpty(xm))
                {
                    valXM.IsValid = false;
                }
                else
                {
                    string mm = txtMM.Text;
                    if (String.IsNullOrEmpty(mm))
                    { mm = "0000"; }

                    string xb = "";
                    if (dropXB.SelectedIndex > 0) { xb = dropXB.SelectedValue; }

                    DateTime yxrq = new DateTime();
                    if (CalendarExtenderYXRQ.SelectedDate > DateTime.Now)
                    { yxrq = CalendarExtenderYXRQ.SelectedDate; }

                    try
                    {
                        Reader new_reader = new Reader();
                        new_reader.DZTM = dztm;
                        new_reader.XM = xm;
                        new_reader.MM = mm;
                        new_reader.XB = xb;
                        if (yxrq > DateTime.Now)
                        { new_reader.YXRQ = yxrq; }
                        new_reader.DZLX = dzlx;
                        new_reader.YJDW = yjdw;
                        new_reader.EJDW = ejdw;
                        new_reader.YHZT = 1;
                        new_reader.FFSJ = DateTime.Now;
                        new_reader.FFCZY = Page.User.Identity.Name;
                        cde.Reader.Add(new_reader);
                        if (cde.SaveChanges() > 0)
                        { Response.Redirect("~/Management/User/ReaderManager/ReaderManagement.aspx?DZTM=" + dztm, false); }
                    }
                    catch
                    { ErrorRedirect_Management(); }
                }
            }
        }
        //重置输入
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtDZTM.Text = "";
            txtXM.Text = "";
            txtMM.Text = "";
            dropXB.SelectedIndex = 0;
            CalendarExtenderYXRQ.Clear();
            dropDZLX.SelectedIndex = 0;
            dropYJDW.Items.Clear();
            dropEJDW.Items.Clear();
        }

        protected void dropDZLX_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropDZLX.SelectedIndex == dropDZLX.Items.Count - 1)
            {
                txtDZLX.Visible = true;
                txtYJDW.Visible = true;
                txtEJDW.Visible = true;

                dropYJDW.Items.Clear();
                dropEJDW.Items.Clear();
                dropYJDW.Items.Add(new ListItem("自定义", "自定义"));
                dropEJDW.Items.Add(new ListItem("自定义", "自定义"));
            }
            else if (dropDZLX.SelectedIndex > 0)
            {
                txtDZLX.Visible = false;
                txtYJDW.Visible = false;
                txtEJDW.Visible = false;
                string dzlx = dropDZLX.SelectedValue;
                BindYJDW(dzlx);
            }
            else
            {
                txtDZLX.Visible = false;
                txtYJDW.Visible = false;
                txtEJDW.Visible = false;
                valDZLX.IsValid = false;
            }
        }

        protected void dropYJDW_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropYJDW.SelectedIndex == dropYJDW.Items.Count - 1 && dropDZLX.SelectedIndex > 0)
            {
                txtYJDW.Visible = true;
                dropEJDW.Items.Clear();
                dropEJDW.Items.Add(new ListItem("自定义", "自定义"));
            }
            else if (dropYJDW.SelectedIndex > 0 && dropDZLX.SelectedIndex > 0)
            {
                txtYJDW.Visible = false;
                string dzlx = dropDZLX.SelectedValue;
                string yjdw = dropYJDW.SelectedValue;
                BindEJDW(dzlx, yjdw);
            }
            else
            {
                txtYJDW.Visible = false;
                valYJDW.IsValid = false;
            }
        }

        private void BindDZLX()
        {
            dropDZLX.Items.Clear();
            dropDZLX.Items.Add(new ListItem("=选择读者类型=", "=选择读者类型="));
            List<string> listDZLX = cde.Reader.Select(r => r.DZLX).Distinct().ToList();
            foreach (string l_dzlx in listDZLX)
            {
                dropDZLX.Items.Add(new ListItem(l_dzlx, l_dzlx));
            }
            dropDZLX.Items.Add(new ListItem("自定义", "自定义"));
        }

        private void BindYJDW(string dzlx)
        {
            dropYJDW.Items.Clear();
            dropYJDW.Items.Add(new ListItem("=选择一级单位="));
            List<Reader> listReader = cde.Reader.Where(r => r.DZLX == dzlx).ToList();
            List<string> listYJDW = listReader.OrderBy(r => r.YJDW).Select(r => r.YJDW).Distinct().ToList();
            foreach (string yjdw in listYJDW)
            {
                dropYJDW.Items.Add(new ListItem(yjdw, yjdw));
            }
            dropYJDW.Items.Add(new ListItem("自定义", "自定义"));
        }

        private void BindEJDW(string dzlx, string yjdw)
        {
            dropEJDW.Items.Clear();
            dropEJDW.Items.Add(new ListItem("=选择二级单位="));
            List<Reader> listReader = cde.Reader.Where(r => r.DZLX == dzlx && r.YJDW == yjdw).ToList();
            List<string> listEJDW = listReader.OrderBy(r => r.EJDW).Select(r => r.EJDW).Distinct().ToList();

            foreach (string ejdw in listEJDW)
            {
                dropEJDW.Items.Add(new ListItem(ejdw, ejdw));
            }
            dropEJDW.Items.Add(new ListItem("自定义", "自定义"));
        }

        protected void dropEJDW_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropEJDW.SelectedIndex == dropEJDW.Items.Count - 1)
            {
                txtEJDW.Visible = true;
            }
            else if (dropEJDW.SelectedIndex > 0)
            {
                txtEJDW.Visible = false;
            }
            else
            {
                txtEJDW.Visible = false;
                valEJDW.IsValid = false;
            }
        }
    }
}