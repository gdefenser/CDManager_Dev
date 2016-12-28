using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Security.Principal;
using System.IO;
using CDManagerLibrary.XML;
using CDManagerLibrary.EntityFramework;
using System.Timers;
using CDManagerLibrary.FTP.Serv_uAdvCon;

namespace CDManager_Dev4
{
    public class Global : System.Web.HttpApplication
    {
        CDManagerDevEntities cde = CDManagerEntitiesSingleton.GetCDManagerDevEntities();
        public Global() { AuthenticateRequest += new EventHandler(Application_AuthenticateRequest); }
        //服务器保存登录票证
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            var id = Context.User;
            if (id != null)
            {
                var tid = id.Identity as FormsIdentity;
                if (tid.IsAuthenticated)
                {
                    var roles = tid.Ticket.UserData.Split(',');
                    Context.User = new GenericPrincipal(tid, roles);
                }
            }
        }

        void Application_Start(object sender, EventArgs e)
        {
            try
            {
                FTPUsers.FTPUsersSoapClient usc = new FTPUsers.FTPUsersSoapClient();
                FTPDownloadFloder.FTPDownloadFloderSoapClient fsc = new FTPDownloadFloder.FTPDownloadFloderSoapClient();
                //统计图表缓存目录
                if (!Directory.Exists(@"C:\TempImageFiles\")) { Directory.CreateDirectory(@"C:\TempImageFiles\"); }
                //下载目录
                //if (!Directory.Exists(XMLHelper.getAppSettingValue("FTP_Home") + "\\Download\\")) { Directory.CreateDirectory(XMLHelper.getAppSettingValue("FTP_Home") + "\\Download\\"); }
                //数据导入缓存目录
                DirectoryInfo dir;
                dir = new DirectoryInfo(Server.MapPath("~/Temp/Upload/Book/"));
                if (!dir.Exists) { dir.Create(); }
                dir = new DirectoryInfo(Server.MapPath("~/Temp/Upload/CD/"));
                if (!dir.Exists) { dir.Create(); }
                dir = new DirectoryInfo(Server.MapPath("~/Temp/Upload/Reader/"));
                if (!dir.Exists) { dir.Create(); }

                List<Admin> list = cde.Admin.ToList();
                foreach (Admin admin in list)
                {
                    usc.Admin(admin.GLYTM, admin.MM);
                }

                fsc.Check();
            }
            catch {  }
            Timer myTimer = new Timer();
            myTimer.Interval = 60000; //这个时间单位毫秒,比如10秒，就写10000 
            myTimer.Enabled = true;
            myTimer.Elapsed += new ElapsedEventHandler(myTimer_Elapsed);
        }

        //定时检测前台是否关闭
        void myTimer_Elapsed(object sender, ElapsedEventArgs e)
        {

            string xmlenable = XMLHelper.getAppSettingValue("IsEnable");
            if (xmlenable == "1")
            {
                string settime = XMLHelper.getAppSettingValue("SetTime");
                if (!String.IsNullOrEmpty(settime))
                {
                    DateTime date = Convert.ToDateTime(settime);
                    if (date <= DateTime.Now)
                    {
                        XMLHelper.setAppSettingValue("IsEnable", "0");
                        XMLHelper.setAppSettingValue("SetTime", "");
                    }
                }
            }
        }

        void Application_End(object sender, EventArgs e)
        {
            //  在应用程序关闭时运行的代码

        }

        void Application_Error(object sender, EventArgs e)
        {
            // 在出现未处理的错误时运行的代码

        }

        void Session_Start(object sender, EventArgs e)
        {
            // 在新会话启动时运行的代码

        }

        void Session_End(object sender, EventArgs e)
        {
            // 在会话结束时运行的代码。 
            // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
            // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer 
            // 或 SQLServer，则不会引发该事件。

        }

    }
}
