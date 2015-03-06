using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevTemplateV3.WebApp.UI.Controls
{
    public partial class BroadcastMessageControl : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        public void SetMessage(String msg)
        {
            lblMessage.Text = msg;
        }


    }
}