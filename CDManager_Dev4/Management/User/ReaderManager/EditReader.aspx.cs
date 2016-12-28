using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.Core;
using System.IO;
using CDManagerLibrary.FTP.Serv_uAdvCon;

namespace CDManager_Dev4.Management.User.ReaderManager
{
    public partial class EditReader : CDPages
    {
        string dztm;
        protected void Page_Load(object sender, EventArgs e)
        {
            dztm = Request.QueryString["DZTM"];
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(dztm))
                {
                    BindReader(dztm);
                }
                else
                { Response.Redirect("~/Management/Error.aspx"); }
            }
        }
        //编辑读者
        protected void btnEdit_Click(object sender, EventArgs e)
        {
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

            string xm = txtXM.Text;
            if (String.IsNullOrEmpty(xm))
            {
                valXM.IsValid = false;
            }
            else if (CalendarExtenderYXRQ.SelectedDate <= DateTime.Now)
            {
                valYXRQ.IsValid = false;
            }
            else if (valDZLX.IsValid && valYJDW.IsValid && valEJDW.IsValid)
            {

                try
                {
                    string mm = txtMM.Text;
                    if (String.IsNullOrEmpty(mm))
                    { mm = "000000"; }

                    string xb = "";
                    if (dropXB.SelectedIndex > 0) { xb = dropXB.SelectedValue; }

                    DateTime yxrq = new DateTime();
                    if (CalendarExtenderYXRQ.SelectedDate > DateTime.Now)
                    { yxrq = CalendarExtenderYXRQ.SelectedDate; }
                    Reader edit_reader = cde.Reader.First(r => r.DZTM == dztm);
                    edit_reader.XM = xm;
                    edit_reader.MM = mm;
                    edit_reader.XB = xb;
                    if (yxrq > DateTime.Now)
                    { edit_reader.YXRQ = yxrq; }
                    edit_reader.YJDW = yjdw;
                    edit_reader.EJDW = ejdw;
                    SaveAndRedirect_Management();
                }
                catch { Response.Redirect("~/Management/Error.aspx"); }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            BindReader(dztm);
        }
        //绑定读者信息
        private void BindReader(string dztm)
        {
            try
            {
                Reader reader = cde.Reader.First(r => r.DZTM == dztm);
                lblDZTM.Text = reader.DZTM;
                txtXM.Text = reader.XM;
                txtMM.Text = reader.MM;

                if (!String.IsNullOrEmpty(reader.XB)) { dropXB.SelectedValue = reader.XB; }

                //if (reader.YXRQ.Value > DateTime.Now)
                //{
                    CalendarExtenderYXRQ.SelectedDate = reader.YXRQ.Value;
                //}

                BindDZLX();
                try { dropDZLX.SelectedValue = reader.DZLX; }
                catch { }
                BindYJDW(reader.DZLX);
                try { dropYJDW.SelectedValue = reader.YJDW; }
                catch { }
                BindEJDW(reader.DZLX, reader.YJDW);
                try { dropEJDW.SelectedValue = reader.EJDW; }
                catch { }

                txtDZLX.Visible = false;
                txtYJDW.Visible = false;
                txtEJDW.Visible = false;
            }
            catch { }
        }
        //禁止登录
        protected void btnBan_Click(object sender, EventArgs e)
        {
            Session["DZTM"] = lblDZTM.Text;
            Response.Redirect("~/Management/Security/BanReaderManagement.aspx");
        }
        //发送消息
        protected void btnMessage_Click(object sender, EventArgs e)
        {
            Session["FSTM"] = lblDZTM.Text;
            Response.Redirect("~/Management/Message/NewMessage.aspx");
        }

        protected void btnApplyDownload_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Management/User/ReaderManager/ApplyAndDownload.aspx?DZTM=" + lblDZTM.Text);
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