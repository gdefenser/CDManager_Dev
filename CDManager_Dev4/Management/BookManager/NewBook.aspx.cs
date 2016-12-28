using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.Core;

namespace CDManager_Dev4.Management.BookManager
{
    public partial class NewBook : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //新建图书
        protected void btnNew_Click(object sender, EventArgs e)
        {
            string isbn = txtISBN.Text;
            if (String.IsNullOrEmpty(isbn))
            {
                valISBN.IsValid = false;
                valISBN.ErrorMessage = "请输入ISBN";
            }
            else if (cde.Book.Count(b => b.ISBN == isbn) > 1)
            {
                valISBN.IsValid = false;
                valISBN.ErrorMessage = "ISBN:" + isbn + "书目已存在";
            }
            else
            {
                string ztm = txtZTM.Text;
                if (String.IsNullOrEmpty(ztm))
                {
                    valZTM.IsValid = false;
                }
                else
                {
                    string zrz = txtZRZ.Text;
                    string cbs = txtCBS.Text;
                    decimal dj = 0;               
                    try { dj = Convert.ToDecimal(txtDJ.Text); }
                    catch { }
                    string yema = txtYEMA.Text;
                    string ysbmy = txtYSBMY.Text;
                    string kb = txtKB.Text;

                    Book new_book = new Book();
                    new_book.ISBN = isbn;
                    new_book.ZTM = ztm;
                    new_book.DJ = dj;
                    new_book.ZRZ = zrz;
                    new_book.CBS = cbs;
                    new_book.YEMA = yema;
                    new_book.YSBMY = ysbmy;
                    new_book.FFSJ = DateTime.Now;
                    new_book.FFCZY = Page.User.Identity.Name;
                    cde.Book.Add(new_book);
                    SaveAndRedirect_Management();
                }
            }
        }
        //重置输入
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtISBN.Text = "";
            txtZTM.Text = "";
            txtCBS.Text = "";
            txtDJ.Text = "";
            txtKB.Text = "";
            txtYEMA.Text = "";
            txtYSBMY.Text = "";
            txtZRZ.Text = "";
        }
    }
}