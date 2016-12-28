using CDManagerLibrary.XML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CDManager_Dev4
{
    /// <summary>
    /// NoticeDownload 的摘要说明
    /// </summary>
    public class NoticeDownload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string ggtm = context.Request.QueryString["GGTM"];
                string path = XMLHelper.getAppSettingValue("Notice_Download");
                if (File.Exists(path))
                {
                }
            }
            catch
            { }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}