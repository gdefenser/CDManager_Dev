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
            string xm = txtXM.Text;
            if (String.IsNullOrEmpty(xm))
            {
                valXM.IsValid = false;
            }
            else
            {
                if (dropYXRQ.SelectedIndex < 1)
                {
                    valYXRQ.IsValid = false;
                }
                else
                {
                    try
                    {
                        string mm = txtMM.Text;
                        if (String.IsNullOrEmpty(mm))
                        { mm = "000000"; }

                        string xb = "";
                        if (dropXB.SelectedIndex > 0) { xb = dropXB.SelectedValue; }

                        DateTime yxrq = new DateTime();
                        if (dropYXRQ.SelectedIndex > 0)
                        { yxrq = DateTime.Now.AddYears(Convert.ToInt16(dropYXRQ.SelectedValue)); }
                        string yjdw = txtYJDW.Text;
                        string ejdw = txtEJDW.Text;
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

                if (reader.YXRQ.Value > DateTime.Now)
                {
                    try
                    {
                        int y = reader.YXRQ.Value.Year - DateTime.Now.Year;
                        dropYXRQ.SelectedValue = y.ToString();
                    }
                    catch { }
                }

                txtYJDW.Text = reader.YJDW;
                txtEJDW.Text = reader.EJDW;
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
    }
}