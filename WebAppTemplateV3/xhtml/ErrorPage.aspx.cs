using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;

namespace DevTemplateV3.WebApp.UI.Pages
{
    public partial class ErrorPage : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            NameValueCollection queryString = Page.Request.QueryString;

            if (queryString.Count > 0)
            {
                ErrorMessageLabel.Text = queryString["errorMessage"];
            }
        }


        protected void BackButton_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("LauncherPage.aspx");
        }

    }
}