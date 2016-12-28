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
using CDManagerLibrary.XML;

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
        public string NewBookByExcel(string floder, string type)
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

                                int count = 0;
                                for (int i = 2; i <= range.Rows.Count; i++)
                                {

                                    string isbn = range[i, 1].Value;
                                    if (String.IsNullOrEmpty(isbn)) { continue; }
                                    //检查数据库中是否已有当前记录
                                    else
                                    {
                                        Book book = cde.Book.FirstOrDefault(b => b.ISBN == isbn);
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

                                            //Book book = new Book();;
                                            if (type == "reset")
                                            {
                                                if (book != null)
                                                {
                                                    //book.ISBN = isbn.Trim();
                                                    book.ZTM = ztm;
                                                    book.FLTM = fltm;
                                                    book.DJ = dj;
                                                    book.ZRZ = zrz;
                                                    book.CBS = cbs;
                                                    book.YEMA = yema;
                                                    book.YSBMY = ysbmy;
                                                    book.KB = kb;
                                                    book.FFSJ = fssj;
                                                    book.FFCZY = czy;
                                                }
                                                else
                                                {
                                                    book = new Book();
                                                    book.ISBN = isbn.Trim();
                                                    book.ZTM = ztm;
                                                    book.FLTM = fltm;
                                                    book.DJ = dj;
                                                    book.ZRZ = zrz;
                                                    book.CBS = cbs;
                                                    book.YEMA = yema;
                                                    book.YSBMY = ysbmy;
                                                    book.KB = kb;
                                                    book.FFSJ = fssj;
                                                    book.FFCZY = czy;
                                                    cde.Book.Add(book);
                                                }
                                            }
                                            else if (type == "continue")
                                            {
                                                if (book != null) { continue; }
                                                else
                                                {
                                                    book = new Book();
                                                    book.ISBN = isbn.Trim();
                                                    book.ZTM = ztm;
                                                    book.FLTM = fltm;
                                                    book.DJ = dj;
                                                    book.ZRZ = zrz;
                                                    book.CBS = cbs;
                                                    book.YEMA = yema;
                                                    book.YSBMY = ysbmy;
                                                    book.KB = kb;
                                                    book.FFSJ = fssj;
                                                    book.FFCZY = czy;
                                                    cde.Book.Add(book);
                                                }
                                            }
                                            count += cde.SaveChanges();
                                        }
                                        //catch (DbEntityValidationException dbEx) { continue; }
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
        public string NewCDByExcel(string floder, string type)
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
                                    string isbn = CDString.getItemValue(range[i, 2].Value).Trim().Replace("-", "");
                                    string cdmc = CDString.getItemValue(range[i, 3].Value);

                                    if (String.IsNullOrEmpty(isbn)) { continue; }
                                    else
                                    {
                                        try
                                        {
                                            CD cd = cde.CD.FirstOrDefault(c => c.CDXH == cdxh);
                                            if (type == "reset")
                                            {
                                                if (cd != null)
                                                {
                                                    long bookID = cde.Book.First(b => b.ISBN == isbn).BookID;
                                                    //cd.CDXH = cdxh;
                                                    cd.BookID = bookID;
                                                    cd.CDMC = cdmc;
                                                    cd.FFCZY = czy;
                                                    cd.FFSJ = DateTime.Now;
                                                    cd.CZSJ = DateTime.Now;
                                                    cd.ZXZT = 0;
                                                }
                                                else
                                                {
                                                    cd = new CD();
                                                    long bookID = cde.Book.First(b => b.ISBN == isbn).BookID;
                                                    cd.CDXH = cdxh;
                                                    cd.BookID = bookID;
                                                    cd.CDMC = cdmc;
                                                    cd.FFCZY = czy;
                                                    cd.FFSJ = DateTime.Now;
                                                    cd.CZSJ = DateTime.Now;
                                                    cd.ZXZT = 0;
                                                    cde.CD.Add(cd);
                                                }
                                            }
                                            else if (type == "continue")
                                            {
                                                if (cd != null) { continue; }
                                                else
                                                {
                                                    cd = new CD();
                                                    long bookID = cde.Book.First(b => b.ISBN == isbn).BookID;
                                                    cd.CDXH = cdxh;
                                                    cd.BookID = bookID;
                                                    cd.CDMC = cdmc;
                                                    cd.FFCZY = czy;
                                                    cd.FFSJ = DateTime.Now;
                                                    cd.CZSJ = DateTime.Now;
                                                    cd.ZXZT = 0;
                                                    cde.CD.Add(cd);
                                                }
                                            }
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
        public string NewReaderByExcel(string floder, string type)
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
                                        try
                                        {
                                            string xm = CDString.getItemValue(range[i, 2].Value);
                                            string mm = "0000";
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
                                            else if (!String.IsNullOrEmpty(dzlx))
                                            {
                                                if (dztm.Length >= 4) { dztm = dztm.PadLeft(4, '0'); }
                                            }
                                            else
                                            {
                                                dzlx = "本科生";
                                            }

                                            string yjdw = CDString.getItemValue(range[i, 7].Value);
                                            string ejdw = CDString.getItemValue(range[i, 8].Value);

                                            string ffczy = czy;
                                            int yhlx = 1;

                                            Reader reader = cde.Reader.FirstOrDefault(r => r.DZTM == dztm);

                                            if (type == "reset")
                                            {
                                                if (reader != null)
                                                {
                                                    //reader.DZTM = dztm.Trim();
                                                    reader.XM = xm.Trim();
                                                    reader.MM = mm;
                                                    reader.BZRQ = bzrq;
                                                    reader.YXRQ = yxrq;
                                                    reader.XB = xb;
                                                    reader.DZLX = dzlx;
                                                    reader.YJDW = yjdw;
                                                    reader.EJDW = ejdw;
                                                    reader.FFCZY = ffczy;
                                                    reader.FFSJ = ffsj;
                                                    reader.YHZT = yhlx;
                                                }
                                                else
                                                {
                                                    reader = new Reader();
                                                    reader.DZTM = dztm.Trim();
                                                    reader.XM = xm.Trim();
                                                    reader.MM = mm;
                                                    reader.BZRQ = bzrq;
                                                    reader.YXRQ = yxrq;
                                                    reader.XB = xb;
                                                    reader.DZLX = dzlx;
                                                    reader.YJDW = yjdw;
                                                    reader.EJDW = ejdw;
                                                    reader.FFCZY = ffczy;
                                                    reader.FFSJ = ffsj;
                                                    reader.YHZT = yhlx;
                                                    cde.Reader.Add(reader);
                                                }
                                            }
                                            else if (type == "continue")
                                            {
                                                if (reader != null) { continue; }
                                                else
                                                {
                                                    reader = new Reader();
                                                    reader.DZTM = dztm.Trim();
                                                    reader.XM = xm.Trim();
                                                    reader.MM = mm;
                                                    reader.BZRQ = bzrq;
                                                    reader.YXRQ = yxrq;
                                                    reader.XB = xb;
                                                    reader.DZLX = dzlx;
                                                    reader.YJDW = yjdw;
                                                    reader.EJDW = ejdw;
                                                    reader.FFCZY = ffczy;
                                                    reader.FFSJ = ffsj;
                                                    reader.YHZT = yhlx;
                                                    cde.Reader.Add(reader);
                                                }
                                            }
                                            count += cde.SaveChanges();
                                        }
                                        catch { continue; }
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

        //导出下载列表
        public string DownloadLogToExcel(string path, string from, string to, string book, string cd, string czy)
        {

            List<DownloadLog> listDownload = cde.DownloadLog.ToList();
            int ini_count = 0;
            if (!String.IsNullOrEmpty(from))
            {
                try
                {
                    string[] froms = from.Split('-');
                    DateTime d_from = new DateTime(Convert.ToInt16(froms[0]),
                        Convert.ToInt16(froms[1]), Convert.ToInt16(froms[2]));
                    listDownload = listDownload.Where(d => d.XZSJ >= d_from).ToList();
                }
                catch { }
            }

            if (!String.IsNullOrEmpty(to))
            {
                try
                {
                    string[] tos = to.Split('-');
                    DateTime d_to = new DateTime(Convert.ToInt16(tos[0]),
                        Convert.ToInt16(tos[1]), Convert.ToInt16(tos[2]));
                    listDownload = listDownload.Where(d => d.XZSJ <= d_to).ToList();
                }
                catch { }
            }

            if (!String.IsNullOrEmpty(book))
            {
                listDownload = listDownload.Where(d => d.CD.Book.ISBN.Contains(book) ||
                   d.CD.Book.ZTM.Contains(book)).ToList();
            }

            if (!String.IsNullOrEmpty(cd))
            {
                listDownload = listDownload.Where(d => d.CD.CDXH.Contains(cd) ||
                   d.CD.CDMC.Contains(cd)).ToList();
            }

            if (!String.IsNullOrEmpty(czy))
            {
                listDownload = listDownload.Where(d => d.CZYTM.Contains(czy)).ToList();
            }


            if (listDownload.Count > 0)
            {
                _Application excel = null;
                Workbook work_book = null;
                object missing = null;
                try
                {
                    object fileName = @path;
                    missing = System.Reflection.Missing.Value;
                    excel = new Application();
                    work_book = excel.Workbooks.Add();
                    excel.Cells[1, 1] = "下载人";
                    excel.Cells[1, 2] = "光盘序号";
                    excel.Cells[1, 3] = "光盘名称";
                    excel.Cells[1, 4] = "ISBN";
                    excel.Cells[1, 5] = "图书名称";
                    excel.Cells[1, 6] = "下载时间";
                    excel.Cells[1, 7] = "下载IP";

                    //excel.Columns[missing,1].NumberFormat = "@";
                    //excel.Columns[missing, 2].NumberFormat = "@";
                    //excel.Columns[missing, 3].NumberFormat = "@";
                    //excel.Columns[missing, 4].NumberFormat = "@";
                    //excel.Columns[missing, 5].NumberFormat = "@";
                    //excel.Columns[missing, 7].NumberFormat = "@";

                    excel.Rows[1, missing].Font.Bold = true;

                    for (int i = 0; i < listDownload.Count; i++)
                    {
                        DownloadLog downloadlog = listDownload[i];
                        excel.Cells[i + 2, 1] = downloadlog.CZYTM;
                        //excel.Cells[i + 2, 1].NumberFormatLocal = "@";
                        excel.Cells[i + 2, 2] = downloadlog.CD.CDXH;
                        excel.Cells[i + 2, 3] = downloadlog.CD.CDMC;
                        excel.Cells[i + 2, 4] = downloadlog.CD.Book.ISBN;
                        excel.Cells[i + 2, 5] = downloadlog.CD.Book.ZTM;
                        excel.Cells[i + 2, 6] = downloadlog.XZSJ;
                        excel.Cells[i + 2, 7] = downloadlog.IP;
                    }

                    //Worksheet sheet = (Worksheet)work_book.Worksheets.get_Item(1);
                    //Range range = sheet.UsedRange.Cells;
                    //range.EntireRow.AutoFit();

                    excel.ActiveWorkbook.SaveAs(fileName, missing,
                        missing, missing, missing, missing, XlSaveAsAccessMode.xlExclusive,
                        missing, missing, missing, missing, missing);
                    work_book.Close(missing, missing, missing);
                    excel.Quit();

                    return "download";
                }
                catch (Exception e) { return e.Message; }
            }
            else { return "listnull"; }
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
