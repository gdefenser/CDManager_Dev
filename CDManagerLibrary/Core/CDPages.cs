using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CDManagerLibrary.EntityFramework;

namespace CDManagerLibrary.Core
{
    public class CDPages : System.Web.UI.Page
    {
        protected CDManagerDevEntities cde;
        public CDPages() { cde = CDManagerEntitiesSingleton.GetCDManagerDevEntities(); }
        //禁用缓存
        //protected override void OnLoad(EventArgs e)
        //{
        //    Response.Cache.SetNoStore();
        //    base.OnLoad(e);
        //}

        protected void SaveAndRedirect_Management()
        {
            if (cde.SaveChanges() > 0) { SuccessRedirect_Management(false); }
            else { ErrorRedirect_Management(false); }
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
