using CDManagerLibrary.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDManager_Dev4
{
    public partial class Error : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            { lblMsg.Text = Session["ErrorMsg"].ToString(); }
            catch { }
        }
    }
}