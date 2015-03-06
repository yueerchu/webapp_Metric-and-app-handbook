using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevTemplateV3.WebApp.UI.Controls
{
    public partial class HeaderControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void Logo_Click(object sender, EventArgs e)
        {
            GoToTeamsite();
        }

        private void GoToTeamsite()
        {
            this.Page.Response.Redirect("https://teams.coca-cola.com/sites/SCVM/default.aspx");
        }
    }
}