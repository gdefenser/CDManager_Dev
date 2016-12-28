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
    public partial class EditBook : CDPages
    {
        string isbn;
        protected void Page_Load(object sender, EventArgs e)
        {
            isbn = Request.QueryString["ISBN"];
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(isbn))
                {
                    BindBook(isbn);
                }
                else
                { Response.Redirect("~/Management/Error.aspx"); }
            }
        }

        private void BindBook(string isbn)
        {
            try
            {
                Book book = cde.Book.First(b => b.ISBN == isbn);
                lblISBN.Text = book.ISBN;
                txtZTM.Text = book.ZTM;
                txtCBS.Text = book.CBS;
                txtDJ.Text = book.DJ.ToString();
                txtKB.Text = book.KB;
                txtYEMA.Text = book.YEMA;
                txtYSBMY.Text = book.YSBMY;
                txtZRZ.Text = book.ZRZ;

            }
            catch { }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            BindBook(isbn);
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string ztm = txtZTM.Text;
            if (String.IsNullOrEmpty(ztm))
            {
                valZTM.IsValid = false;
            }
            else
            {
                try
                {
                    string zrz = txtZRZ.Text;
                    string cbs = txtCBS.Text;
                    decimal dj = 0;
                    try { dj = Convert.ToDecimal(txtDJ.Text); }
                    catch { }
                    string yema = txtYEMA.Text;
                    string ysbmy = txtYSBMY.Text;
                    string kb = txtKB.Text;

                    Book edit_book = cde.Book.First(b => b.ISBN == isbn);
                    edit_book.ZTM = ztm;
                    edit_book.DJ = dj;
                    edit_book.ZRZ = zrz;
                    edit_book.CBS = cbs;
                    edit_book.YEMA = yema;
                    edit_book.YSBMY = ysbmy;
                    SaveAndRedirect_Management();
                }
                catch { Response.Redirect("~/Management/Error.aspx"); }
            }
        }
    }
}