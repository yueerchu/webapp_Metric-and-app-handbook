using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevTemplateV3.WebApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Dashboard is default page for web app
            Response.Redirect("xhtml/LauncherPage.aspx");
        }
    }
}
