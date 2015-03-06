using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevTemplateV3.WebApp.UI.Controls;

namespace DevTemplateV3.WebApp.UI.Pages
{
    public partial class SampleReportPage : System.Web.UI.Page
    {

        protected void Page_PreInit(object sender, EventArgs e)
        {

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            NavigationBarControl nv = (NavigationBarControl)Master.FindControl("navBar");
            nv.SetActiveLinkSampleReport(true);

            this.ErrorLabel.Text = "";

            SpacerPanel.Visible = true;
            ReportPanel.Visible = false;  
        }


        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                InputChoice.ApplyQueryString(Page.Request.QueryString);

                if (IsLoadReport())
                {
                    if (ValidateInputs())
                    {
                        LoadReport();
                        DealWithExport();
                    }
                }
            }
        }


        protected void cmdRefresh_Click(object sender, EventArgs e)
        {
            String ps = "SampleReportPage.aspx" + InputChoice.GetQueryString() + "&export=web";
            Page.Response.Redirect(ps);
        }


        private Boolean IsLoadReport()
        {
            if (Page.Request.QueryString.Count == 0)
            {
                return false;
            }

            if (Page.Request.QueryString["export"] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        private void DealWithExport()
        {
            if (Page.Request.QueryString.Count == 0)
            {
                return;
            }

            if (Page.Request.QueryString["export"] == null)
            {
                //no report view
            }
            else if (Page.Request.QueryString["export"].Equals("web"))
            {
                //normal view
            }
        }


        private Boolean ValidateInputs()
        {
            Exception error = null;

            error = InputChoice.ValidateInputs();

            if (error != null)
            {
                ErrorLabel.Text = error.Message;

                return false;
            }
            else
            {
                return true;
            }
        }


        private void LoadReport()
        {
            //Get Inputs From Input Control

            //Report Logic Goes Here

            SpacerPanel.Visible = false;
            ReportPanel.Visible = true;
        }


    }
}