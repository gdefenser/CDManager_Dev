using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDManagerLibrary.EntityFramework;
using System.IO;

namespace CDManager_Deploy
{
    class Program
    {
        CDManagerDevEntities cde = CDManagerEntitiesSingleton.GetCDManagerDevEntities();
        static void Main(string[] args)
        {

        }
        //建立FTP文件夹
        private void FTPUser()
        {
            Console.WriteLine("开始建立FTP服务器用户文件夹");
            List<Admin> list = cde.Admin.ToList();
            int count = 0;
            foreach (Admin admin in list)
            {
                DirectoryInfo dir = new DirectoryInfo("D:\\testFTP\\" + admin.GLYTM);
                if (!dir.Exists)
                {
                    dir.Create();
                    count++;
                    Console.WriteLine("建立" + admin.GLYTM + "文件夹");
                }
                if (!File.Exists("D:\\testFTP\\" + admin.GLYTM + "\\上传的光盘文件命名请包含正确的ISBN和光盘条码.ini"))
                { File.Create("D:\\testFTP\\" + admin.GLYTM + "\\上传的光盘文件命名请包含正确的ISBN和光盘条码.ini"); }
            }

            DirectoryInfo d_dir = new DirectoryInfo("D:\\testFTP\\Download");
            if (!d_dir.Exists)
            {
                d_dir.Create();
                count++;
                Console.WriteLine("建立Download文件夹");
            }
            if (!File.Exists("D:\\testFTP\\Download\\光盘下载文件夹,请勿增加、修改或删除任何文件和文件夹.ini"))
            { File.Create("D:\\testFTP\\Download\\光盘下载文件夹,请勿增加、修改或删除任何文件和文件夹.ini"); }
            Console.WriteLine("已建立" + count + "个文件夹");
        }
    }
}
