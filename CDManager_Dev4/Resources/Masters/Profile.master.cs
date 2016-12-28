using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CDManager_Dev4.Resources.Masters
{
    public partial class Profile : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var ticket = Context.User.Identity as FormsIdentity;
                if (ticket != null && ticket.IsAuthenticated)
                {
                    string[] data = ticket.Ticket.UserData.Split(',');
                    string roles = data[0];
                    if (roles == "1")
                    {
                        linkMessage.NavigateUrl = "~/Account/Profile/MyMessage.aspx";
                    }
                    else if (roles == "2"||roles == "3")
                    {
                        linkMessage.NavigateUrl = "~/Management/Message/ReceivedMessage.aspx";
                    }
                }
            }
        }
    }
}