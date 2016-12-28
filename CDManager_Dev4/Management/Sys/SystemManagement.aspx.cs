using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Drawing;
using System.Configuration;
using CDManagerLibrary.XML;
using System.Timers;
using CDManagerLibrary.FTP.Serv_uAdvCon;
using CDManagerLibrary.Core;

namespace CDManager_Dev4.Management.Sys
{
    public partial class SystemManagement : CDPages
    {
        FTPUsers.FTPUsersSoapClient usc = new FTPUsers.FTPUsersSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string enable = XMLHelper.getAppSettingValue("IsEnable");
                if (enable == "1")
                {
                    lblStatus.Text = "已打开";
                    lblStatus.ForeColor = Color.DarkGreen;
                    btnSetTime.Visible = true;
                    string settime = XMLHelper.getAppSettingValue("SetTime");
                    if (!String.IsNullOrEmpty(settime))
                    {
                        lblSetTime.Text = "计划定时关闭时间:" + settime;
                        btnResetTime.Visible = true;
                        btnSetTime.Text = "重置关闭时间";
                    }
                    else
                    {
                        btnStatus.Text = "关闭前台";
                        btnStatus.Visible = true;
                    }
                }
                else if (enable == "0")
                {
                    btnStatus.Visible = true;
                    lblStatus.Text = "已关闭";
                    lblStatus.ForeColor = Color.DarkRed;
                    btnStatus.Text = "打开前台";
                    string settime = XMLHelper.getAppSettingValue("SetTime");
                    if (!String.IsNullOrEmpty(settime))
                    { XMLHelper.setAppSettingValue("SetTime", ""); }
                }
                //并发下载读者数
                txtMaxUser.Text = usc.GetMaxUser();
            }
        }

        //更改前台状态
        protected void btnStatus_Click(object sender, EventArgs e)
        {
            try
            {
                //设置前台状态
                string xmlenable = XMLHelper.getAppSettingValue("IsEnable");

                if (xmlenable == "0")
                {
                    if (XMLHelper.setAppSettingValue("IsEnable", "1"))
                    { Response.Redirect("~/Management/Success.aspx", false); }
                }
                else if (xmlenable == "1")
                {
                    if (XMLHelper.setAppSettingValue("IsEnable", "0"))
                    { Response.Redirect("~/Management/Success.aspx", false); }
                }
                else
                {
                    Response.Redirect("~/Management/Error.aspx");
                }
            }
            catch { Response.Redirect("~/Management/Error.aspx"); }
        }

        protected void btnSetTime_Click(object sender, EventArgs e)
        {
            if (!timePicker.Visible && !btnClick.Visible)
            {
                timePicker.Visible = true;
                btnClick.Visible = true;
                btnStatus.Visible = false;
                btnSetTime.Text = "取消设定";
                if (!String.IsNullOrEmpty(lblSetTime.Text))
                {
                    try
                    {
                        DateTime date = Convert.ToDateTime(lblSetTime.Text.Replace("计划定时关闭时间:", ""));
                        timePicker.SelectedTime = date;
                    }
                    catch
                    { }
                }
            }
            else if (timePicker.Visible && btnClick.Visible)
            {
                Response.Redirect("~/Management/Sys/SystemManagement.aspx");
            }
        }

        protected void btnClick_Click(object sender, EventArgs e)
        {
            DateTime select = timePicker.SelectedTime;
            if (select > DateTime.Now)
            {
                if (XMLHelper.setAppSettingValue("SetTime", select.ToString()))
                { Response.Redirect("~/Management/Success.aspx"); }
                else
                { Response.Redirect("~/Management/Error.aspx"); }
            }
        }

        protected void btnSetUser_Click(object sender, EventArgs e)
        {
            string new_max = txtMaxUser.Text;
            if (usc.SetMaxUser("reader",new_max))
            {
                 SuccessRedirect_Management(); 
            }
        }

        protected void btnReSetUser_Click(object sender, EventArgs e)
        {
            txtMaxUser.Text = usc.GetMaxUser();
            Response.Redirect("~/Management/Sys/SystemManagement.aspx");
        }

        protected void btnResetTime_Click(object sender, EventArgs e)
        {
            if (XMLHelper.setAppSettingValue("SetTime", ""))
            { Response.Redirect("~/Management/Success.aspx"); }
            else
            { Response.Redirect("~/Management/Error.aspx"); }
        }
    }
}