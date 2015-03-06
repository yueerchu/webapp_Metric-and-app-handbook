using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevTemplateV3.WebApp.Common;

namespace DevTemplateV3.WebApp.UI.Controls
{
    public partial class NavigationBarControl : System.Web.UI.UserControl
    {

        public NavigationBarControl()
        {
            
        }


        /* --- UI Events --- */
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        
        protected void HomeLink_Click(object sender, EventArgs e)
        {
            GoToHome();
        }


        protected void AdminLink_Click(object sender, EventArgs e)
        {
            GoToAdmin();
        }

        
        
        /* --- Methods --- */
        private void GoToHome()
        {
            this.Page.Response.Redirect("LauncherPage.aspx");
        }


        private void GoToAdmin()
        {
            this.Page.Response.Redirect("AdminPage.aspx");
        }


        public void SetActiveLinkHome(bool a)
        {
            if (a)
            {
                this.HomeLinkPanel.CssClass = "navigation_active";
            }
            else
            {
                this.HomeLinkPanel.CssClass = "navigation_inactive";
            }
        }


        public void SetActiveLinkSampleReport(bool a)
        {
            if (a)
            {
                this.AdminPanel.CssClass = "navigation_active";
            }
            else
            {
                this.AdminPanel.CssClass = "navigation_inactive";
            }
        }


    }
}