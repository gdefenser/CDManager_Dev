using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.Core;
using System.Web.Security;
using CDManagerLibrary.XML;
namespace CDManager_Dev4.Resources.Masters
{
    public partial class Management : System.Web.UI.MasterPage
    {
        CDManagerDevEntities cde = CDManagerEntitiesSingleton.GetCDManagerDevEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Browser.Browser.IndexOf("IE") > 0 || Request.Browser.Browser.IndexOf("ie") > 0)
                { Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache); }
                else
                { Response.Cache.SetNoStore(); }
                var ticket = Context.User.Identity as FormsIdentity;
                if (ticket != null && ticket.IsAuthenticated)
                {
                    string[] data = ticket.Ticket.UserData.Split(',');
                    string roles = data[0];
                    if (roles == "3")
                    {
                        panelNewAdmin.Visible = true;
                        panelAdminManagement.Visible = true;
                        panelSystem.Visible = true;
                    }
                }


            }
        }
    }
}