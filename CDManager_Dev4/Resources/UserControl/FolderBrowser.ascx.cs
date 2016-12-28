using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDManager_Dev4.Resources.UserControl
{
    public partial class FolderBrowser : System.Web.UI.UserControl
    {
        public string Folder
        {
            set { txtFTPFolder.Text = value; }
            get { return txtFTPFolder.Text; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}