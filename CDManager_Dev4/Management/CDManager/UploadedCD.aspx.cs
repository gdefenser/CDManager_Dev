using CDManagerLibrary.Core;
using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.FTP.Serv_uAdvCon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDManager_Dev4.Management.CDManager
{
    public partial class UploadedCD : CDPages
    {
        CDFile.CDFileSoapClient cfc = new CDFile.CDFileSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    List<string> list = cfc.GetUploadedFiles(Page.User.Identity.Name);
                    lvUploadedCD.DataSource = list;
                    lvUploadedCD.DataBind();
                }
                catch
                {
                    lvUploadedCD.Visible = false;
                    panelEdit.Visible = false;
                    lblFTP.Visible = true;
                }
            }
        }

        protected void lvUploadedCD_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //如果是绑定数据行 
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                string[] values = dataItem.DataItem.ToString().Split(',');
                Label lblCDMC = (Label)dataItem.FindControl("lblCDMC");
                Label lblCDGS = (Label)dataItem.FindControl("lblCDGS");
                Label lblCDDX = (Label)dataItem.FindControl("lblCDDX");
                Label lblTime = (Label)dataItem.FindControl("lblTime");
                Button btnDelete = (Button)dataItem.FindControl("btnDelete");
                Button btnConfirm = (Button)dataItem.FindControl("btnConfirm");
                Button btnEdit = (Button)dataItem.FindControl("btnEdit");
                //Label lblISBN = (Label)dataItem.FindControl("lblISBN");
                btnDelete.CommandArgument = values[0] + values[1];
                btnDelete.Attributes.Add("onclick", "javascript:return confirm('你确认要删除文件:" + values[0] + values[1] + "吗?')");
                btnConfirm.CommandArgument = values[0];
                btnEdit.CommandArgument = dataItem.DataItem.ToString();
                lblCDMC.Text = values[0];
                lblCDGS.Text = values[1];
                lblCDDX.Text = values[2];
                lblTime.Text = values[3];
            }
        }

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                string name = e.CommandArgument.ToString();
                if (cfc.RemoveUploadedFile(Page.User.Identity.Name, name))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('删除成功!');location.reload();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('删除失败!');", true);
                }
            }
            catch
            {
                lvUploadedCD.Visible = false;
                panelEdit.Visible = false;
                lblFTP.Visible = true;
            }
        }

        protected void btnConfirm_Command(object sender, CommandEventArgs e)
        {

            string name = e.CommandArgument.ToString();
            try
            {
                string cdxh = name.Split('_')[1];
                CD cd = cde.CD.FirstOrDefault(c => c.CDXH == cdxh);
                if (cd != null)
                {
                    if (cd.ZXZT != 1)
                    {
                        try
                        {
                            string result = cfc.UploadConfirm(Page.User.Identity.Name, cd.Book.ISBN, cd.Book.ZTM, cd.CDXH);
                            if (result == "file_ok")
                            {
                                List<ApplyLog> listApply = cde.ApplyLog.Where(a => a.Book.ISBN == cd.Book.ISBN && a.SQZT == 0).ToList();
                                List<string> listMsg = new List<string>();
                                foreach (ApplyLog apply in listApply)
                                {
                                    //更新申请状态
                                    apply.CLSJ = DateTime.Now;
                                    apply.CLCZY = Page.User.Identity.Name;
                                    apply.SQZT = 1;

                                    //发送消息
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
                                    new_msg.XXTM = id;
                                    new_msg.YHLX = "reader";
                                    new_msg.SXRTM = apply.DZTM;
                                    new_msg.FSRTM = "系统消息";
                                    new_msg.FSSJ = DateTime.Now;
                                    new_msg.XXNR = "你申请的" + apply.Book.ZTM + "(" + apply.Book.ISBN + ")光盘资源已经上传完成";
                                    new_msg.YD = false;
                                    cde.Message.Add(new_msg);
                                }
                                //更新光盘信息
                                cd.ZXZT = 1;
                                cd.CZCZY = Page.User.Identity.Name;
                                cd.CZSJ = DateTime.Now;
                                //update_cd.CDLJ = moved;

                                //新建上传记录
                                DateTime ul_now = DateTime.Now;
                                string ul_id = Page.User.Identity.Name + ul_now.Year + ul_now.Month + ul_now.Day;
                                var ul_id_count = cde.UploadLog.Where(u => u.SCBH.Contains(ul_id)).ToList();
                                if (ul_id_count.Count > 0)
                                {
                                    string max = ul_id_count.Max(u => u.SCBH.Substring(u.SCBH.Length - 5));
                                    int temp_id = Convert.ToInt16(max) + 1;
                                    ul_id += temp_id.ToString().PadLeft(5, '0');
                                }
                                else
                                { ul_id += "00001"; }

                                UploadLog new_upload = new UploadLog();
                                new_upload.SCBH = ul_id;
                                new_upload.CZYTM = Page.User.Identity.Name;
                                new_upload.CDID = cd.CDID;
                                new_upload.SCSJ = DateTime.Now;
                                new_upload.IP = Request.UserHostAddress;
                                cde.UploadLog.Add(new_upload);

                                cde.SaveChanges();
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('确认成功!');location.href='CDDetail.aspx?ISBN=" + cd.Book.ISBN + "';", true);
                            }
                            else if (result == "file_null")
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('文件不存在!');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('确认失败!请耐心等待文件上传文件或重新上传文件!');", true);
                            }
                        }
                        catch
                        {
                            lvUploadedCD.Visible = false;
                            panelEdit.Visible = false;
                            lblFTP.Visible = true;
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('无法连接光盘存储服务器!');", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('光盘序号错误!');", true);
                }

            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('确认失败!请查看文件名是否以[ISBN_光盘序号_光盘名称]命名!');", true);
            }

        }

        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            string[] values = e.CommandArgument.ToString().Split(',');
            lblCDMC.Text = values[0];
            lblCDGS.Text = values[1];
            lblCDDX.Text = values[2];
            lblTime.Text = values[3];
            panelEdit.Visible = true;
            lvUploadedCD.Visible = false;
        }

        protected void btnGo_Command(object sender, CommandEventArgs e)
        {
            DataPager dpUploadedCD = (DataPager)lvUploadedCD.FindControl("dpUploadedCD");
            TextBox txtPage = (TextBox)dpUploadedCD.Controls[4].FindControl("txtPage");

            try
            {
                int max = dpUploadedCD.TotalRowCount < dpUploadedCD.PageSize ? 1 : dpUploadedCD.TotalRowCount / dpUploadedCD.PageSize + 1;
                int select = Convert.ToInt32(txtPage.Text);
                if (select < 1) { select = 1; }
                else if (select > max) { select = max; }

                int start_row = (select - 1) * dpUploadedCD.PageSize;
                dpUploadedCD.SetPageProperties(start_row, dpUploadedCD.PageSize, false);
                lvUploadedCD.DataBind();
            }
            catch
            { }
        }

        protected void btnClick_Click(object sender, EventArgs e)
        {
            string newName = txtNewCDMC.Text;
            if (!String.IsNullOrEmpty(newName))
            {
                string glytm = Page.User.Identity.Name;
                string name = lblCDMC.Text;
                string ext = lblCDGS.Text;

                if (cfc.UpdateUploadedFile(glytm, name, newName, ext))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('修改成功!');location.href='UploadedCD.aspx';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('修改失败!');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('请输入文件名!');", true);
            }

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            panelEdit.Visible = false;
            lvUploadedCD.Visible = true;
        }
    }
}