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

        }
        //新建读者用户
        protected void btnNew_Click(object sender, EventArgs e)
        {
            string dztm = txtDZTM.Text;
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
            else
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


                        Reader new_reader = new Reader();
                        new_reader.DZTM = dztm;
                        new_reader.XM = xm;
                        new_reader.MM = mm;
                        new_reader.XB = xb;
                        if (yxrq > DateTime.Now)
                        { new_reader.YXRQ = yxrq; }
                        new_reader.YJDW = yjdw;
                        new_reader.EJDW = ejdw;
                        new_reader.YHZT = 1;
                        new_reader.FFSJ = DateTime.Now;
                        new_reader.FFCZY = Page.User.Identity.Name;
                        cde.Reader.Add(new_reader);
                        SaveAndRedirect_Management();
                    }
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
            dropYXRQ.SelectedIndex = 0;
            txtYJDW.Text = "";
            txtEJDW.Text = "";
        }
    }
}