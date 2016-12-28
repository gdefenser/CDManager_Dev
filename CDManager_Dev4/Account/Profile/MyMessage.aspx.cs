using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.EntityFramework;
using CDManagerLibrary.Core;
using System.Web.Security;

namespace CDManager_Dev4.Account.Profile
{
    public partial class MyMessage : CDPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string tm = Page.User.Identity.Name;
                    Session["TM"] = tm;

                    List<CDManagerLibrary.EntityFramework.Message> list = cde.Message.Where(m => m.SXRTM == tm && !m.YD.Value).ToList();
                    if (list.Count > 0)
                    {
                        foreach (CDManagerLibrary.EntityFramework.Message msg in list)
                        {
                            msg.YD = true;
                        }
                        cde.SaveChanges();
                    }

                    lvMessage.Visible = true;
                }

                catch
                { AuthenticationError(); }
            }
        }
    }
}