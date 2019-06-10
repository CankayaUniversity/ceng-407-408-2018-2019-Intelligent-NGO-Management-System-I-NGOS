using System;

namespace Ilkyar.Web
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CurrentUser"] == null)
                Response.Redirect("Login.aspx");
        }
    }
}