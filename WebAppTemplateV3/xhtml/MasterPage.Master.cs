using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevTemplateV3.WebApp.Common;
using AjaxControlToolkit;

namespace DevTemplateV3.WebApp.UI.Pages
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            AppSettings settings = new AppSettings();
            String message = settings.GetBroadcastMessage();

            CoeHeaderSC.Title = settings.GetTitle();

            ActiveDirectoryCacheManager.FindUserBasedOnID(UserAuthentication.GetAuthenticatedUser(this.Page));

            if (this.Page.Request.FilePath.Contains("ErrorPage.aspx") == false)
            {
                if (!settings.IsAppFound())
                {
                    Response.Redirect("ErrorPage.aspx?errorMessage=" + HttpUtility.UrlEncode("This app is not set up in the COE Database."));
                }
                else if (!settings.IsEnabled())
                {
                    Response.Redirect("ErrorPage.aspx?errorMessage=" + HttpUtility.UrlEncode("This app is disabled."));
                }
                else
                {
                    if (message.Equals("") == false)
                    {
                        AppMessage.Visible = true;
                        AppMessage.SetMessage(message);
                    }
                    else
                    {
                        AppMessage.Visible = false;
                    }
                }
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                Logging logger = new Logging(this.Page);
                logger.AddUsageLogEntry();
            }
        }

    }
}