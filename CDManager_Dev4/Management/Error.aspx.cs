using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.Core;

namespace CDManager_Dev4.Management
{
    public partial class Error : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try { lblErrorMsg.Text = Session["ErrorMsg"].ToString(); }
                catch { }
            }
        }
    }
}