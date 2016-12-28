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
        string roles;
        protected void Page_Load(object sender, EventArgs e)
        {
            roles = GetUserRole();
            if (roles == "2")
            {
                lblISBN.Visible = true;
                txtISBN.Visible = false;
            }
            else
            {
                lblISBN.Visible = false;
                txtISBN.Visible = true;
            }
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
                if (roles == "3") { txtISBN.Text = book.ISBN; }
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
            else if (roles == "3" && String.IsNullOrEmpty(txtISBN.Text))
            {
                valISBN.IsValid = false;
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
                    if (roles == "3")//系统管理员修改ISBN
                    {
                        string edit_isbn = txtISBN.Text;
                        if (isbn != edit_isbn)
                        {
                            if (cde.Book.Count(b => b.ISBN == edit_isbn) > 0)
                            {
                                valISBN.ErrorMessage = "ISBN已存在";
                                valISBN.IsValid = false;
                            }
                            else
                            {
                                edit_book.ISBN = edit_isbn;
                            }
                        }
                    }
                    edit_book.ZTM = ztm;
                    edit_book.DJ = dj;
                    edit_book.ZRZ = zrz;
                    edit_book.CBS = cbs;
                    edit_book.YEMA = yema;
                    edit_book.YSBMY = ysbmy;
                    if (cde.SaveChanges() > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "click", "alert('更新成功!');location.href='EditBook.aspx?ISBN=" + edit_book.ISBN + "';", true);
                    }
                }
                catch { Response.Redirect("~/Management/Error.aspx"); }
            }
        }
    }
}