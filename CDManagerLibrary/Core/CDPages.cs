using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.EntityFramework;
using System.Web.Security;
using System.Data.Entity.Validation;

namespace CDManagerLibrary.Core
{
    public class CDPages : System.Web.UI.Page
    {
        protected static string CLICK = "click";
        protected CDManagerDevEntities cde;
        public CDPages() { cde = CDManagerEntitiesSingleton.GetCDManagerDevEntities(); }
        //禁用缓存
        //protected override void OnLoad(EventArgs e)
        //{
        //    Response.Cache.SetNoStore();
        //    base.OnLoad(e);
        //}

        protected string GetUserRole()
        {
            try
            {
                var ticket = Context.User.Identity as FormsIdentity;
                if (ticket != null && ticket.IsAuthenticated)
                {
                    string[] data = ticket.Ticket.UserData.Split(',');
                    return data[0];
                }
                else { return null; }
            }
            catch
            {
                return null;             
            }
        }

        protected void SaveAndRedirect_Management()
        {
            try
            {
                if (cde.SaveChanges() > 0) { SuccessRedirect_Management(false); }
                else { ErrorRedirect_Management(false); }
            }
            catch (DbEntityValidationException e)
            { ErrorRedirect_Management(false); }
        }


        protected void SuccessRedirect_Front()
        {
            Response.Redirect("~/Success.aspx");
        }
        protected void SuccessRedirect_Front(bool endResponse)
        {
            Response.Redirect("~/Success.aspx", endResponse);
        }

        protected void SuccessRedirect_Management()
        {
            Response.Redirect("~/Management/Success.aspx");
        }
        protected void SuccessRedirect_Management(bool endResponse)
        {
            Response.Redirect("~/Management/Success.aspx", endResponse);
        }

        protected void ErrorRedirect_Front()
        {
            Response.Redirect("~/Error.aspx");
        }
        protected void ErrorRedirect_Front(bool endResponse)
        {
            Response.Redirect("~/Error.aspx", endResponse);
        }

        protected void ErrorRedirect_Management()
        {
            Response.Redirect("~/Management/Error.aspx");
        }
        protected void ErrorRedirect_Management(bool endResponse)
        {
            Response.Redirect("~/Management/Error.aspx", endResponse);
        }

        protected void AuthenticationError()
        {
            Response.Redirect("~/Account/AuthenticationError.aspx");
        }
    }
}
