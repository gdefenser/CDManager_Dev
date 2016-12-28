using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.XML;
using CDManagerLibrary.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Net;
using Microsoft.Win32;

namespace CDManager_Dev4
{
    /// <summary>
    /// 光盘下载后台处理程序
    /// </summary>
    public class CDDownload : IHttpHandler, IRequiresSessionState
    {
        CDManagerDevEntities cde = CDManagerEntitiesSingleton.GetCDManagerDevEntities();
        public void ProcessRequest(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {

                string cdxh = context.Request.QueryString["CDXH"];
                string czytm = context.User.Identity.Name;
                if (!String.IsNullOrEmpty(cdxh) && !String.IsNullOrEmpty(czytm))
                {
                    string dztm = context.User.Identity.Name;
                    CD cd = cde.CD.FirstOrDefault(c => c.CDXH == cdxh);
                    if (cd != null)
                    {
                        //DownloadLog download = cde.DownloadLog.OrderByDescending(d => d.XZSJ).FirstOrDefault(d => d.CZYTM == dztm && d.CDID == cd.CDID);
                        //if (download != null && DateTime.Now < download.XZSJ.Value.AddSeconds(300))
                        //{
                        //    context.Response.Write("<script>alert('请勿在短时间内下载同一资源!');window.close();</script>");
                        //}
                        //else
                        //{
                            CDFile.CDFileSoapClient csc = new CDFile.CDFileSoapClient();
                            string name = csc.GetFileName(cd.Book.ISBN, cd.Book.ZTM, cdxh);

                            if (!String.IsNullOrEmpty(name))
                            {
                                string check_url = "ftp://" + XMLHelper.getAppSettingValue("FTP_IP") + "/" +
                                            CDString.getFileName(cd.Book.ISBN + cd.Book.ZTM) + "/" + CDString.getFileName(cdxh) + "/" +
                                            name;
                                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(check_url);
                                request.Credentials = new NetworkCredential("reader", "reader");
                                request.Method = WebRequestMethods.Ftp.GetFileSize;
                                try
                                {
                                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                                    //FileStream outputStream = new FileStream(file.FullName, FileMode.Open);
                                    response.Close();

                                    //string download_url = "";
                                    //if (context.Request.Browser.Browser == "IE")
                                    //{
                                    //    download_url = "ftp://reader:reader@" + XMLHelper.getAppSettingValue("FTP_IP") +
                                    //        XMLHelper.getAppSettingValue("FTP_Home").Split(':')[1].Replace("\\", "/") + @"/Download/" +
                                    //        HttpUtility.UrlEncode(CDString.getFileName(cd.ISBN + cd.Book.ZTM), Encoding.GetEncoding("gb2312")) + "/" + CDString.getFileName(cdxh) + "/" +
                                    //        HttpUtility.UrlEncode(CDString.getFileName(name), Encoding.GetEncoding("gb2312"));
                                    //}
                                    //else
                                    //{
                                    //    download_url = "ftp://reader:reader@" + XMLHelper.getAppSettingValue("FTP_IP") + "/" +
                                    //               HttpUtility.UrlEncode(CDString.getFileName(cd.ISBN + cd.Book.ZTM), Encoding.GetEncoding("gb2312"))
                                    //               + "/" + CDString.getFileName(cdxh) + "/" +
                                    //               HttpUtility.UrlEncode(CDString.getFileName(name), Encoding.GetEncoding("gb2312"));
                                    //}

                                    string download_url = "ftp://reader:reader@" + XMLHelper.getAppSettingValue("FTP_IP") + "/" +
                                                   HttpUtility.UrlEncode(CDString.getFileName(cd.Book.ISBN + cd.Book.ZTM), Encoding.GetEncoding("gb2312"))
                                                   + "/" + CDString.getFileName(cdxh) + "/" +
                                                   HttpUtility.UrlEncode(CDString.getFileName(name), Encoding.GetEncoding("gb2312"));
                                    try
                                    {
                                        context.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(name));
                                        context.Response.Redirect(download_url, false);
                                        //FTP下载
                                        DateTime now = DateTime.Now;
                                        string id = czytm + now.Year.ToString() + now.Month.ToString() + now.Day.ToString();
                                        var id_count = cde.DownloadLog.Where(d => d.XZBH.Contains(id)).ToList();
                                        if (id_count.Count > 0)
                                        {
                                            string max = id_count.Max(d => d.XZBH.Substring(d.XZBH.Length - 3));
                                            int temp_id = Convert.ToInt16(max) + 1;
                                            id += temp_id.ToString().PadLeft(3, '0');
                                        }
                                        else
                                        {
                                            id += "001";
                                        }

                                        DateTime xzsj = DateTime.Now;
                                        DownloadLog new_download = new DownloadLog();
                                        new_download.XZBH = id;
                                        new_download.CZYTM = czytm;
                                        new_download.CDID = cd.CDID;
                                        new_download.XZSJ = xzsj;
                                        new_download.IP = context.Request.UserHostAddress;
                                        cde.DownloadLog.Add(new_download);
                                        int i = cde.SaveChanges();

                                        //

                                    }
                                    catch (DbEntityValidationException dbEx)
                                    {

                                    }
                                }
                                catch
                                {
                                    context.Session["ErrorMsg"] = "无法打开下载连接";
                                    context.Response.Redirect("~/Error.aspx");
                                }
                            }
                        }
                    }
                //}
            }
            else
            {
                context.Response.Redirect("~/Account/AuthenticationError.aspx");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private string GetFileContentType(string ext)
        {
            string DEFAULT_CONTENT_TYPE = "application/unknown";
            RegistryKey regkey, fileextkey;
            string FileContentType;
            try
            {
                regkey = Registry.ClassesRoot;
                fileextkey = regkey.OpenSubKey(ext, false);
                FileContentType = fileextkey.GetValue("Content Type", DEFAULT_CONTENT_TYPE).ToString();
            }
            catch
            {
                FileContentType = DEFAULT_CONTENT_TYPE;
            }
            return FileContentType;
        }
    }
}