using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDManager_Dev4
{
    public partial class Page404 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblErrorMsg.Text = Session["Code404"].ToString();
            }
            catch { }
        }
    }
}