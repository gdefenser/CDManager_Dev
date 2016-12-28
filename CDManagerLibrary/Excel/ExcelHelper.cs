using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using CDManagerLibrary.EntityFramework;
using Microsoft.Office.Interop.Excel;
using System.Data.Entity.Validation;
using CDManagerLibrary.Core;

namespace CDManagerLibrary.Excel
{
    public class ExcelHelper
    {
        private CDManagerDevEntities cde;
        private string czy = "";
        public ExcelHelper(string czy)
        {
            this.czy = czy;
            cde = CDManagerEntitiesSingleton.GetCDManagerDevEntities();
        }

        //根据Excel导入图书信息
        public string NewBookByExcel(string floder)
        {
            DirectoryInfo dir = new DirectoryInfo(floder);
            List<FileInfo> list = dir.GetFiles().ToList();
            if (list.Count(f => f.Name.Contains("temp_Book_")) > 0)
            {
                FileInfo fi = list.First();
                string exet = Path.GetExtension(fi.FullName).ToLower();
                if (exet == ".xls" || exet == ".xlsx")
                {

                    Application excel = null;
                    Workbook work_book = null;
                    Worksheet sheet = null;
                    object missing = null;

                    try
                    {
                        //实例化Excel对象
                        string fileName = @fi.FullName;
                        missing = System.Reflection.Missing.Value;
                        excel = new Application();
                        work_book = excel.Workbooks.Open(fileName, missing, true, missing, missing,
                            missing, missing, missing, missing, missing, missing, missing, false, missing, missing);
                        sheet = (Worksheet)work_book.Worksheets.get_Item(1);
                        Range range = sheet.UsedRange.Cells;
                        if (range[1, 1].Value == "ISBN")
                        {
                            if (range.Rows.Count > 1)
                            {
                                Book new_book;
                                int count = 0;
                                for (int i = 2; i <= range.Rows.Count; i++)
                                {

                                    string isbn = range[i, 1].Value;
                                    if (String.IsNullOrEmpty(isbn)) { continue; }
                                    //检查数据库中是否已有当前记录
                                    else if (cde.Book.Count(b => b.ISBN == isbn) > 0) { continue; }
                                    else
                                    {
                                        try
                                        {
                                            isbn = isbn.Trim();
                                            string ztm = CDString.getItemValue(range[i, 2].Value);
                                            string fltm = CDString.getItemValue(range[i, 3].Value);
                                            decimal dj = 0.0M;
                                            try { dj = range[i, 4].Value; }
                                            catch { }
                                            string zrz = CDString.getItemValue(range[i, 5].Value);
                                            string cbs = CDString.getItemValue(range[i, 6].Value);
                                            string yema = CDString.getItemValue(range[i, 7].Value);
                                            string ysbmy = CDString.getItemValue(range[i, 8].Value);
                                            string kb = CDString.getItemValue(range[i, 9].Value);
                                            DateTime fssj = DateTime.Now;

                                            new_book = new Book();
                                            new_book.ISBN = isbn.Trim();
                                            new_book.ZTM = ztm;
                                            new_book.FLTM = fltm;
                                            new_book.DJ = dj;
                                            new_book.ZRZ = zrz;
                                            new_book.CBS = cbs;

                                            new_book.YEMA = yema;
                                            new_book.YSBMY = ysbmy;
                                            new_book.KB = kb;
                                            new_book.FFSJ = fssj;
                                            new_book.FFCZY = czy;

                                            cde.Book.Add(new_book);
                                            count += cde.SaveChanges();
                                        }
                                        catch (DbEntityValidationException dbEx) { continue; }
                                        catch { continue; }
                                    }
                                }


                                if (count > 0)
                                {
                                    return "共成功导入图书数据" + count + "条!";
                                }
                                else
                                {
                                    return "没有图书数据被导入!";
                                }
                            }
                            else
                            { return "没有图书数据被导入!"; }
                        }
                        else
                        { return "请检查待导入图书信息是否正确!"; }
                    }
                    catch
                    {

                        return "图书数据导入失败!";
                    }
                    finally
                    {
                        excel.Quit();
                        File.Delete(fi.FullName);//删除文件
                    }
                }
                else
                { return "请检查待导入图书信息是否正确!"; }
            }
            else
            { return "没有找到待导入的图书数据"; }
        }

        //根据Excel导入光盘信息
        public string NewCDByExcel(string floder)
        {
            DirectoryInfo dir = new DirectoryInfo(floder);
            List<FileInfo> list = dir.GetFiles().ToList();
            if (list.Count(f => f.Name.Contains("temp_CD_")) > 0)
            {
                FileInfo fi = list.First();
                string exet = Path.GetExtension(fi.FullName).ToLower();
                if (exet == ".xls" || exet == ".xlsx")
                {
                    Application excel = null;
                    Workbook work_book = null;
                    Worksheet sheet = null;
                    object missing = null;

                    try
                    {
                        //实例化Excel对象
                        string fileName = @fi.FullName;
                        missing = System.Reflection.Missing.Value;
                        excel = new Application();
                        work_book = excel.Workbooks.Open(fileName, missing, true, missing, missing,
                            missing, missing, missing, missing, missing, missing, missing, false, missing, missing);
                        sheet = (Worksheet)work_book.Worksheets.get_Item(1);
                        Range range = sheet.UsedRange.Cells;
                        if (range[1, 1].Value == "CDXH")
                        {
                            CD new_cd;
                            int count = 0;
                            for (int i = 2; i <= range.Rows.Count; i++)
                            {

                                string cdxh = Convert.ToString(range[i, 1].Value);
                                if (String.IsNullOrEmpty(cdxh)) { continue; }
                                else
                                {
                                    try
                                    {
                                        cdxh = cdxh.PadLeft(5, '0');
                                    }
                                    catch { }
                                    string isbn = CDString.getItemValue(range[i, 2].Value);
                                    string cdmc = CDString.getItemValue(range[i, 3].Value);


                                    if (String.IsNullOrEmpty(isbn)) { continue; }
                                    else if (cde.CD.Count(c => c.CDXH == cdxh) > 0) { continue; }
                                    else
                                    {
                                        try
                                        {
                                            new_cd = new CD();
                                            new_cd.CDXH = cdxh;
                                            new_cd.ISBN = isbn.Trim().Replace("-", "");
                                            new_cd.CDMC = cdmc;
                                            new_cd.FFCZY = czy;
                                            new_cd.FFSJ = DateTime.Now;
                                            new_cd.CZSJ = DateTime.Now;
                                            new_cd.ZXZT = 0;
                                            cde.CD.Add(new_cd);
                                            count += cde.SaveChanges();
                                        }
                                        catch { continue; }
                                    }
                                }
                            }

                            if (count > 0)
                            {
                                return "共成功导入光盘数据" + count + "条!";
                            }
                            else
                            {
                                return "没有光盘数据被导入!";
                            }
                        }
                        else
                        { return "请检查待导入光盘数据是否正确!"; }
                    }
                    catch { return "光盘数据导入失败!"; }
                    finally
                    {
                        excel.Quit();
                        File.Delete(fi.FullName);//删除文件
                    }
                }
                else
                { return "请检查待导入光盘数据是否正确!"; }
            }
            else
            { return "没有找到待导入的光盘数据"; }
        }

        //根据Excel导入读者信息
        public string NewReaderByExcel(string floder)
        {
            DirectoryInfo dir = new DirectoryInfo(floder);
            List<FileInfo> list = dir.GetFiles().ToList();
            if (list.Count(f => f.Name.Contains("temp_Reader_")) > 0)
            {
                FileInfo fi = list.First();
                string exet = Path.GetExtension(fi.Name).ToLower();
                if (exet == ".xls" || exet == ".xlsx")
                {
                    if (!Directory.Exists(floder))
                    {
                        Directory.CreateDirectory(floder);
                    }

                    Application excel = null;
                    Workbook work_book = null;
                    Worksheet sheet = null;
                    object missing = null;

                    try
                    {
                        //实例化Excel对象
                        string excel_file = @fi.FullName;
                        missing = System.Reflection.Missing.Value;
                        excel = new Application();
                        work_book = excel.Workbooks.Open(excel_file, missing, true, missing, missing,
                            missing, missing, missing, missing, missing, missing, missing, false, missing, missing);

                        sheet = (Worksheet)work_book.Worksheets.get_Item(1);
                        Range range = sheet.UsedRange.Cells;

                        if (range[1, 1].Value == "DZTM")
                        {
                            if (range.Rows.Count > 2)
                            {
                                int count = 0;
                                Reader new_reader;
                                List<string> listDZTM = new List<string>();
                                for (int i = 2; i <= range.Rows.Count; i++)
                                {

                                    string dztm = Convert.ToString(range[i, 1].Value);
                                    if (String.IsNullOrEmpty(dztm)) { continue; }
                                    else
                                    {

                                        string xm = CDString.getItemValue(range[i, 2].Value);
                                        string mm = "000000";
                                        DateTime ffsj = DateTime.Now;
                                        DateTime bzrq = new DateTime();
                                        if (range[i, 3].Value == null) { bzrq = ffsj; }
                                        else
                                        {
                                            try { bzrq = Convert.ToDateTime(range[i, 3].Value); }
                                            catch { }
                                        }
                                        DateTime yxrq = new DateTime();
                                        if (range[i, 4].Value == null) { yxrq = ffsj.AddYears(5); ; }
                                        else
                                        {
                                            try { yxrq = Convert.ToDateTime(range[i, 4].Value); }
                                            catch { }
                                        }

                                        string xb = CDString.getItemValue(range[i, 5].Value);
                                        if (xb == "暂无信息")
                                        {
                                            xb = "男";
                                        }

                                        string dzlx = CDString.getItemValue(range[i, 6].Value);
                                        if (dzlx == "本科生" || dzlx == "专科生")
                                        {
                                            if (dztm.Length >= 10) { dztm = dztm.PadLeft(10, '0'); }
                                        }
                                        else
                                        {
                                            dzlx = "本科生";
                                            if (dztm.Length >= 4) { dztm = dztm.PadLeft(4, '0'); }
                                        }

                                        string yjdw = CDString.getItemValue(range[i, 7].Value);
                                        string ejdw = CDString.getItemValue(range[i, 8].Value);

                                        string ffczy = czy;
                                        int yhlx = 1;


                                        //检查数据库中是否已有当前记录
                                        if (cde.Reader.Count(r => r.DZTM == dztm) > 0) { continue; }
                                        else
                                        {
                                            try
                                            {
                                                new_reader = new Reader();
                                                new_reader.DZTM = dztm.Trim();
                                                new_reader.XM = xm.Trim();
                                                new_reader.MM = mm;
                                                new_reader.BZRQ = bzrq;
                                                new_reader.YXRQ = yxrq;
                                                new_reader.XB = xb;
                                                new_reader.DZLX = dzlx;
                                                new_reader.YJDW = yjdw;
                                                new_reader.EJDW = ejdw;
                                                new_reader.FFCZY = ffczy;
                                                new_reader.FFSJ = ffsj;
                                                new_reader.YHZT = yhlx;
                                                cde.Reader.Add(new_reader);
                                                count += cde.SaveChanges();
                                            }
                                            catch (DbEntityValidationException dbEx) { continue; }
                                            catch { continue; }
                                        }
                                    }
                                }


                                if (count > 0)
                                {
                                    return "共成功导入读者数据" + count + "条!";
                                }
                                else
                                {
                                    return "没有读者数据被导入!";
                                }
                            }
                            else
                            { return "没有读者数据被导入!"; }
                        }
                        else
                        { return "请检查待导入读者数据是否正确!"; }
                    }
                    catch
                    {

                        return "读者数据导入失败!";
                    }
                    finally
                    {
                        excel.Quit();
                        File.Delete(fi.FullName);//删除文件
                    }
                }
                else
                { return "请检查待导入读者数据是否正确!"; }
            }
            else
            { return "没有找到待导入的读者数据"; }
        }

        public static int GetExcelCount(string fileName)
        {
            if (File.Exists(fileName))
            {
                Application excel = null;
                Workbook work_book = null;
                Worksheet sheet = null;
                object missing = null;

                try
                {
                    //实例化Excel对象
                    string excel_file = fileName;
                    missing = System.Reflection.Missing.Value;
                    excel = new Application();
                    work_book = excel.Workbooks.Open(excel_file, missing, true, missing, missing,
                        missing, missing, missing, missing, missing, missing, missing, false, missing, missing);

                    sheet = (Worksheet)work_book.Worksheets.get_Item(1);
                    Range range = sheet.UsedRange.Cells;
                    int count = range.Rows.Count - 1;
                    return count;
                }
                catch
                { }
                finally
                {
                    excel.Quit();
                }
            }
            return 0;
        }

        public static bool IsExcel(string mime)
        {
            if (mime == "application/vnd.ms-excel" ||
                mime == "application/x-msexcel" ||
                mime == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            { return true; }
            else { return false; }
        }
    }
}
