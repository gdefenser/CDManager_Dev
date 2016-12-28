using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CDManagerLibrary.EntityFramework;
namespace CDManagerLibrary.Core.Login
{
    public class UserAuthentication
    {

        public static string ValidateUser(string username, string password, string ip)
        {
            CDManagerDevEntities cde = CDManagerEntitiesSingleton.GetCDManagerDevEntities();
            try
            {
                Reader reader = cde.Reader.First(r => r.DZTM == username && r.MM == password);
                if (reader.JSSJ != null)
                {
                    if (reader.JSSJ.Value > DateTime.Now && reader.YHZT == 0) { return "no_reader" + reader.JSSJ.Value; }
                    else if (reader.YXRQ < DateTime.Now) { return "no_yxrq"; }
                    else
                    {
                        reader.JSSJ = null;
                        reader.YHZT = 1;
                        cde.SaveChanges();
                    }
                }
                return "1," + reader.XM;
            }
            catch
            {
                try
                {
                    if (String.IsNullOrEmpty(ip)) { ip = "无法获取"; }
                    Admin admin = cde.Admin.First(a => a.GLYTM == username && a.MM == password);
                    if (admin.YXRQ < DateTime.Now) { return "no_yxrq"; }
                    else
                    {
                        int glylx = admin.GLYLX;
                        if (glylx == 2)
                        {
                            admin.SCRQ = DateTime.Now;
                            admin.SCIP = ip;
                            cde.SaveChanges();
                            return "2," + admin.XM;
                        }
                        else if (glylx == 3)
                        {
                            admin.SCRQ = DateTime.Now;
                            admin.SCIP = ip;
                            cde.SaveChanges();
                            return "3," + admin.XM;
                        }
                        else { return "no_account"; }
                    }
                }
                catch
                {
                    return "no_account";
                }
            }
        }
    }
}
