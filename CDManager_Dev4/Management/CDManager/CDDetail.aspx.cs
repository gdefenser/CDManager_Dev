using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.Core;
using CDManagerLibrary.FTP.Serv_uAdvCon;
using System.IO;
using CDManagerLibrary.XML;

namespace CDManager_Dev4.Management.CDManager
{
    public partial class CDDetail : CDPages
    {
        CDFile.CDFileSoapClient cfc = new CDFile.CDFileSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string isbn = Request.QueryString["ISBN"];
                if (String.IsNullOrEmpty(isbn)) { Response.Redirect("~/Management/Main.aspx"); }
            }
        }

        protected void gvBook_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //如果是绑定数据行 删除确认框
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    string isbn = Request.QueryString["ISBN"];
                    LiteralControl lite;//强制转换LiteralControl
                    lite = (LiteralControl)e.Row.Cells[4].Controls[0];
                    Label lblApply = (Label)lite.FindControl("lblApply");
                    lblApply.Text = cde.ApplyLog.Count(a => a.Book.ISBN == isbn).ToString();

                    lite = (LiteralControl)e.Row.Cells[5].Controls[0];
                    Label lblDownload = (Label)lite.FindControl("lblDownload");
                    lblDownload.Text = cde.DownloadLog.Count(d => d.CD.Book.ISBN == isbn).ToString();

                    lite = (LiteralControl)e.Row.Cells[6].Controls[0];
                    Label lblCDCount = (Label)lite.FindControl("lblCDCount");
                    List<CD> list = cde.CD.Where(c => c.Book.ISBN == isbn).ToList();
                    lblCDCount.Text = list.Count.ToString();
                    lite = (LiteralControl)e.Row.Cells[7].Controls[0];
                    Label lblOnlineCount = (Label)lite.FindControl("lblOnlineCount");
                    lblOnlineCount.Text = list.Count(c => c.ZXZT == 1).ToString();
                }
            }
        }
        //光盘列表
        protected void gvCD_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //如果是绑定数据行 删除确认框
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    string glytm = Page.User.Identity.Name;
                    string isbn = Request.QueryString["ISBN"];
                    LiteralControl lite = (LiteralControl)e.Row.Cells[3].Controls[0];
                    Label lblCDXH = (Label)lite.FindControl("lblCDXH");
                    string cdxh = lblCDXH.Text;

                    lite = (LiteralControl)e.Row.Cells[0].Controls[0]; //强制转换LiteralControl
                    Button btn = (Button)lite.FindControl("btnDelete");
                    lite = (LiteralControl)e.Row.Cells[4].Controls[0];
                    Label lblCDMC = (Label)lite.FindControl("lblCDMC");
                    string ztm = ((HyperLink)gvBook.Rows[0].Cells[1].Controls[0]).Text;

                    btn.Attributes.Add("onclick", "javascript:return confirm('你确认要删除光盘信息:" + lblCDMC.Text + "(" + e.Row.Cells[3].Text + ")" + "吗?')");

                    lite = (LiteralControl)e.Row.Cells[1].Controls[0];

                    string zxzt = e.Row.Cells[7].Text;

                    try
                    {
                        string[] fileMsg = cfc.GetFile(glytm, isbn, ztm, cdxh).Split(',');
                        long fileLength = Convert.ToInt64(fileMsg[0]);
                        string extension = fileMsg[1];

                        if (zxzt == "1" && fileLength > 0)
                        {

                            Button btnFTPDelete = (Button)lite.FindControl("btnFTPDelete");
                            btnFTPDelete.Visible = true;
                            e.Row.Cells[5].Text = extension;
                            double newfileLength = fileLength / 1024.0 / 1024.0;
                            if (newfileLength >= 1024)
                            { e.Row.Cells[6].Text = (newfileLength / 1024.0).ToString("0.00") + "Gb"; }
                            else
                            { e.Row.Cells[6].Text = newfileLength.ToString("0.00") + "Mb"; }
                            e.Row.Cells[7].Text = "在线";
                            btnFTPDelete.Attributes.Add("onclick", "javascript:return confirm('你确认要在服务器中删除这张光盘:" + lblCDMC.Text + "(" + e.Row.Cells[3].Text + ")" + "吗?(此操作不会删除光盘信息)')");
                        }
                        else { throw new Exception(); }
                    }
                    catch
                    {
                        if (zxzt == "0")
                        {
                            Button btnFTPUpload = (Button)lite.FindControl("btnFTPUpload");
                            btnFTPUpload.Visible = true;
                            btnFTPUpload.Attributes.Add("onclick", "javascript:return confirm('你确认要上传好这张光盘:" + lblCDMC.Text + "(" + cdxh + ")" + "吗?')");
                            e.Row.Cells[7].Text = "未上传";
                        }
                        else if (zxzt == "2")
                        {
                            Button btnFTPUpload = (Button)lite.FindControl("btnFTPUpload");
                            btnFTPUpload.Visible = true;
                            btnFTPUpload.Attributes.Add("onclick", "javascript:return confirm('你确认要上传好这张光盘:" + lblCDMC.Text + "(" + e.Row.Cells[3].Text + ")" + "吗?')");
                            e.Row.Cells[7].Text = "已删除";
                        }
                        else
                        {
                            Label lblFTP = (Label)lite.FindControl("lblFTP");
                            lblFTP.Visible = true;
                            e.Row.Cells[5].Text = "无法获取";
                            e.Row.Cells[6].Text = "无法获取";
                            e.Row.Cells[7].Text = "无法获取";
                        }
                    }

                    lite = (LiteralControl)e.Row.Cells[8].Controls[0];
                    HyperLink linkDownloadCount = (HyperLink)lite.FindControl("linkDownloadCount");
                    linkDownloadCount.Text = cde.DownloadLog.Count(d => d.CD.CDXH == cdxh).ToString();
                }
            }
        }
        //删除光盘信息
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                string cdxh = e.CommandArgument.ToString();
                CD delete_cd = cde.CD.FirstOrDefault(c => c.CDXH == cdxh);
                string result = cfc.GetFileName(delete_cd.Book.ISBN, delete_cd.Book.ZTM, cdxh);
                if (String.IsNullOrEmpty(result))
                {
                    cde.CD.Remove(delete_cd);
                    cde.SaveChanges();
                    gvCD.DataBind();
                    gvBook.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('光盘" + delete_cd.CDMC + "已经上传,若要删除该光盘信息请先删除服务器文件!');", true);
                }
            }
            catch { Response.Redirect("~/Management/Error.aspx"); }
        }
        //FTP上传确认
        protected void btnFTPUpload_Command(object sender, CommandEventArgs e)
        {
            //FTP上传操作
            try
            {
                string cdxh = e.CommandArgument.ToString();
                string glytm = Page.User.Identity.Name;
                string isbn = Request.QueryString["ISBN"];
                CD update_cd = cde.CD.FirstOrDefault(c => c.CDXH == cdxh);
                if (update_cd != null)
                {
                    if (update_cd.ZXZT != 1)
                    {
                        try
                        {
                            string result = cfc.UploadConfirm(glytm, isbn, update_cd.Book.ZTM, cdxh);
                            if (result == "file_ok")
                            {
                                List<ApplyLog> listApply = cde.ApplyLog.Where(a => a.Book.ISBN == isbn && a.SQZT == 0).ToList();
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
                                update_cd.ZXZT = 1;
                                update_cd.CZCZY = Page.User.Identity.Name;
                                update_cd.CZSJ = DateTime.Now;
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
                                new_upload.CDID = update_cd.CDID;
                                new_upload.SCSJ = DateTime.Now;
                                new_upload.IP = Request.UserHostAddress;
                                cde.UploadLog.Add(new_upload);
                                cde.SaveChanges();
                                gvCD.DataBind();
                                gvBook.DataBind();

                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('确认成功!');", true);
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
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('无法连接光盘存储服务器!');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('未找到光盘文件!请确认是否已经上传');", true);
                        //Session["ErrorMsg"] = "未找到光盘文件!请确认是否已经上传";
                        //ErrorRedirect_Management(false);
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
                //Session["ErrorMsg"] = "FTP已关闭!无法确认上传";
                //ErrorRedirect_Management();
            }
        }
        //FTP删除
        protected void btnFTPDelete_Command(object sender, CommandEventArgs e)
        {
            //FTP删除操作
            try
            {
                string glytm = Page.User.Identity.Name;
                string cdxh = e.CommandArgument.ToString();
                //更新光盘信息
                CD update_cd = cde.CD.First(c => c.CDXH == cdxh);
                update_cd.ZXZT = 2;
                cde.SaveChanges();

                string isbn = update_cd.Book.ISBN;
                if (cfc.RemoveFile(glytm, update_cd.Book.ISBN, update_cd.Book.ZTM, cdxh))
                {
                    gvCD.DataBind();
                    gvBook.DataBind();
                }
            }
            catch { }

        }

        protected void gvCD_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            LiteralControl lite;
            lite = (LiteralControl)gvCD.Rows[e.RowIndex].Cells[3].Controls[0];
            TextBox txtEditCDXH = (TextBox)lite.FindControl("txtEditCDXH");
            CustomValidator valEditCDXH = (CustomValidator)lite.FindControl("valEditCDXH");
            HiddenField hideCDID = (HiddenField)lite.FindControl("hideCDID");
            long cdid = Convert.ToInt64(hideCDID.Value);
            string cdxh = txtEditCDXH.Text;
            if (String.IsNullOrEmpty(cdxh))
            {
                valEditCDXH.IsValid = false;
                e.Cancel = true;
            }
            else if (cde.CD.Count(c => c.CDXH == cdxh && c.CDID != cdid) > 0)
            {
                valEditCDXH.IsValid = false;
                valEditCDXH.ErrorMessage = "光盘序号为" + cdxh + "的光盘已存在!";
                e.Cancel = true;
            }
            else
            {
                lite = (LiteralControl)gvCD.Rows[e.RowIndex].Cells[4].Controls[0];
                TextBox txtEditCDMC = (TextBox)lite.FindControl("txtEditCDMC");
                CustomValidator valEditCDMC = (CustomValidator)lite.FindControl("valEditCDMC");
                string cdmc = txtEditCDMC.Text;
                if (String.IsNullOrEmpty(cdmc))
                {
                    valEditCDMC.IsValid = false;
                    e.Cancel = true;
                }
                else
                {
                    CD update_cd = cde.CD.Find(cdid);

                    if (CDString.getFileName(cdmc) != CDString.getFileName(update_cd.CDMC) || CDString.getFileName(cdxh) != CDString.getFileName(update_cd.CDXH))
                    {
                        if (update_cd.ZXZT == 1)
                        {
                            try
                            {
                                cfc.UpdateFileName(update_cd.Book.ISBN, update_cd.Book.ZTM, update_cd.CDXH, cdmc);
                            }
                            catch
                            {
                                Session["ErrorMsg"] = "FTP已关闭!无法更新!";
                                ErrorRedirect_Management();
                            }
                        }
                        update_cd.CDXH = cdxh;
                        update_cd.CDMC = cdmc;
                        cde.SaveChanges();
                        gvBook.DataBind();
                        gvCD.DataBind();
                    }
                }
            }
        }
        //添加按钮
        protected void btnNew_Click(object sender, EventArgs e)
        {

            if (panelCD.Visible)
            {
                panelCD.Visible = false;
                btnNew.Text = "添加光盘信息";
            }
            else
            {
                panelCD.Visible = true;
                btnNew.Text = "取消添加";
            }

        }
        //新建光盘信息
        protected void btnNewCD_Click(object sender, EventArgs e)
        {
            string cdmc = txtCDMC.Text;
            if (String.IsNullOrEmpty(cdmc)) { valCDMC.IsValid = false; }
            else
            {
                try
                {
                    //生成主键ID
                    DateTime now = DateTime.Now;

                    string cdxh = String.Empty;
                    try
                    {
                        int max = Convert.ToInt32(cde.CD.Where(c =>
                            !c.CDXH.ToLower().Contains("a") &&
                            !c.CDXH.ToLower().Contains("b") &&
                            !c.CDXH.Contains(".") &&
                            !c.CDXH.Contains(" ")
                            ).Max(c => c.CDXH));
                        cdxh = (max + 1).ToString();
                    }
                    catch
                    { cdxh = "未确定" + (cde.CD.Count(c => c.CDXH.Contains("未确定")) + 1); }
                    string isbn = Request.QueryString["ISBN"];
                    long bookID = cde.Book.First(b => b.ISBN == isbn).BookID;
                    CD new_cd = new CD();
                    new_cd.CDXH = cdxh;
                    new_cd.BookID = bookID;
                    new_cd.CDMC = cdmc;
                    new_cd.CZSJ = DateTime.Now;
                    new_cd.FFSJ = DateTime.Now;
                    new_cd.FFCZY = Page.User.Identity.Name;
                    new_cd.ZXZT = 0;
                    cde.CD.Add(new_cd);
                    cde.SaveChanges();
                    gvCD.DataBind();
                    gvBook.DataBind();
                    txtCDMC.Text = "";
                    panelCD.Visible = false;
                    btnNew.Text = "添加光盘信息";
                }
                catch
                { Response.Redirect("~/Management/Error.aspx"); }
            }
        }
    }
}