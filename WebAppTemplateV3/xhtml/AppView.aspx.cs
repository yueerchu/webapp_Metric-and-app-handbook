using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevTemplateV3.WebApp.Common;
using System.Data.SqlClient;
using EvoPdf;

namespace WebAppTemplateV3.xhtml
{
    public partial class AppView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request.QueryString.Count > 0 && Request.QueryString["back"] != null)
            {
                String s = Request.QueryString["AppID"];
                if (Request.QueryString["back"].Equals("no"))
                    gobacknow.Visible = false;
                populate_purpose();


            }
            PopulateReport();
            PopulateMetricReport();
            gobacknow.OnClientClick = "GoB(); return false;";
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {


                if (Page.Request.QueryString["export"] == null)
                {
                    //normal page view
                }
                else if (Page.Request.QueryString["export"].Equals("excel"))
                {
                    //export to excel
                    ExportToExcel();
                }
                else if (Page.Request.QueryString["export"].Equals("print"))
                {
                    //export to printer
                    ExportToPrinter();
                }
                else if (Page.Request.QueryString["export"].Equals("adobe"))
                {
                    //export to adobe
                    ExportToAdobe();
                }
            }
        }

        protected void cmdExcel_Click(object sender, EventArgs e)
        {
            String appID = Request.QueryString["AppID"];
            String ps = "AppView.aspx?AppID=" + appID + "&back=no&export=excel";
            Page.Response.Redirect(ps);
        }

        protected void cmdSupport_Click(object sender, EventArgs e)
        {
            String ps = "http://pmcoe/support/";
            Page.Response.Redirect(ps);
        }

        protected void cmdPrint_Click(object sender, EventArgs e)
        {
            String appID = Request.QueryString["AppID"];
            String ps = "AppView.aspx?AppID=" + appID + "&back=no&export=print";
            Page.Response.Redirect(ps);
        }

        protected void cmdAdobe_Click(object sender, EventArgs e)
        {
            String appID = Request.QueryString["AppID"];
            String ps = "AppView.aspx?AppID=" + appID + "&back=no&export=adobe";
            Page.Response.Redirect(ps);
        }


        /* --- METHODS --- */
        private void ExportToExcel()
        {
            //Create Excel Document
            HtmlConverter ee = new HtmlConverter();
            ee.StartDocument();

            ee.ConvertTable(MetricTable);
            ee.EndDocument();

            //Export Excel Document
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=Help.xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/ms-excel";
            Response.Write(ee.GetText());
            Response.End();
        }


        private void ExportToPrinter()
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "Print", "window.print();", true);
        }


        private void ExportToAdobe()
        {
            PdfConverter pdfConverter = null;
            Document pdfDocument = null;
            PdfPage newPage = null;
            HtmlToPdfElement htmlToPdfURL = null;
            AddElementResult addResult = null;
            byte[] pdfBytes = null;
            String URL = "";

            URL = this.Request.Url.OriginalString;

            if (URL.Contains("&export="))
            {
                URL = URL.Replace("&export=adobe", "&export=web");
            }
            else
            {
                URL = URL + "&export=web";
            }

            URL = URL + "&userID=" + UserAuthentication.GetAuthenticatedUser(this);

            //initialize converter
            pdfConverter = new PdfConverter();
            pdfConverter.LicenseKey = EvoPdfLicense.LICENSE_KEY;
            pdfConverter.NavigationTimeout = 300;

            //make empty document
            pdfDocument = new Document();
            pdfDocument.LicenseKey = EvoPdfLicense.LICENSE_KEY;
            pdfDocument.DocumentInformation.Title = "Metric View";
            pdfDocument.DocumentInformation.CreationDate = DateTime.Now;
            pdfDocument.DocumentInformation.Author = "The Coca-Cola Company";
            pdfDocument.OpenAction.Action = new PdfActionJavaScript("this.zoom = 75;");

            //get url contents
            newPage = pdfDocument.Pages.AddNewPage();
            newPage.Orientation = PdfPageOrientation.Portrait;
            htmlToPdfURL = new HtmlToPdfElement(0, 10, URL);
            htmlToPdfURL.RenderedHtmlElementSelector = "#content";

            try
            {
                addResult = newPage.AddElement(htmlToPdfURL);
            }
            catch (Exception ex)
            {
                //ignore error, keep going...blank page will be added to document
            }

            //end pdf document and save to http response
            pdfBytes = pdfDocument.Save();

            pdfDocument.Close();

            //clear current http response
            HttpResponse httpResponse = HttpContext.Current.Response;
            httpResponse.Clear();

            // add the Content-Type and Content-Disposition HTTP headers
            httpResponse.AddHeader("Content-Type", "application/pdf");
            httpResponse.AddHeader("Content-Disposition", String.Format("attachment; filename=ManufacturingScorecard.pdf; size={0}", pdfBytes.Length.ToString()));

            // write the PDF document bytes as attachment to HTTP response 
            httpResponse.BinaryWrite(pdfBytes);

            //end response
            httpResponse.End();
        }

        private void PopulateMetricReport()
        {
            DatabaseConnection db1 = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);


            String metricSQL = "";
            metricSQL = "select m.MetricName, m.Definition, mc.Name as Formula, f.FrequencyName as Frequency, m.DataSource, m.MetricID ";
            metricSQL = metricSQL + "from COE.dbo.tblHelp_Metrics m ";
            metricSQL = metricSQL + "INNER JOIN COE.dbo.tblHelp_Application_Metrics am ON m.MetricID = am.MetricID ";
            metricSQL = metricSQL + "INNER JOIN COE.dbo.tblApplication a ON a.AppID = am.ApplicationID ";
            metricSQL = metricSQL + "INNER JOIN COE.dbo.tblHelp_Metrics_Calculation mc ON mc.MetricID = m.MetricID ";
            metricSQL = metricSQL + "INNER JOIN tblHelp_Frequency AS f ON m.FrequencyID = f.FrequencyID ";
            metricSQL = metricSQL + "where a.APPID = '" + Request.QueryString["AppID"] + "'";
            metricSQL = metricSQL + " order by m.MetricName ";


            SqlDataReader metricResults = null;

            MetricTable.Rows.Clear();

            if (db1.OpenConnection())
            {

                metricResults = db1.ExecuteQuerySQL(metricSQL);
            }

            if (metricResults != null)
            {
                MetricTableHeader(metricResults);
                PopulateMetricTableData(metricResults);
                metricResults.Close();

            }

            if (db1.CloseConnection())
            {
                return;
            }
        }

        

        private void MetricTableHeader(SqlDataReader queryResults)
        {
            TableRow row = null;
            TableCell cell = null;

            row = new TableRow();
            cell = new TableCell();
            cell.Text = "Metric Name";

            cell.Style["width"] = "15%";
            cell.CssClass = "DataTableHeaderCell";
            cell.Style["color"] = "white";
            cell.Style["height"] = "20px";
            cell.Style["font-weight"] = "bold";
            cell.Style["font-size"] = "17px";
            cell.Style["background-color"] = "#787878";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = queryResults.GetName(queryResults.GetOrdinal("Definition"));
            cell.Style["width"] = "34%";
            cell.Style["background-color"] = "#787878";
            cell.Style["color"] = "white";
            cell.Style["height"] = "20px";
            cell.Style["font-weight"] = "bold";
            cell.Style["font-size"] = "17px";
            cell.CssClass = "DataTableHeaderCell";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = queryResults.GetName(queryResults.GetOrdinal("Formula"));
            cell.Style["width"] = "20%";
            cell.Style["background-color"] = "#787878";
            cell.Style["color"] = "white";
            cell.Style["height"] = "20px";
            cell.Style["font-weight"] = "bold";
            cell.Style["font-size"] = "17px";
            cell.CssClass = "DataTableHeaderCell";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = queryResults.GetName(queryResults.GetOrdinal("Frequency"));
            cell.Style["width"] = "20%";
            cell.Style["background-color"] = "#787878";
            cell.Style["color"] = "white";
            cell.Style["height"] = "20px";
            cell.Style["font-weight"] = "bold";
            cell.Style["font-size"] = "17px";
            cell.CssClass = "DataTableHeaderCell";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Data Source";
            cell.Style["width"] = "11%";
            cell.Style["background-color"] = "#787878";
            cell.Style["color"] = "white";
            cell.Style["font-weight"] = "bold";
            cell.Style["font-size"] = "17px";
            cell.Style["height"] = "20px";
            cell.CssClass = "DataTableHeaderCell";
            row.Cells.Add(cell);



            MetricTable.Rows.Add(row);
        }


        private void PopulateMetricTableData(SqlDataReader queryResults)
        {
            TableRow row = null;
            TableCell cell = null;

            while (queryResults.Read())
            {
                row = new TableRow();

                //LinkButton
                cell = new TableCell();
                System.Web.UI.WebControls.HyperLink metricLink = new HyperLink();
                metricLink.CssClass = "linkbuttonCss";
                string url = "MetricView.aspx" + "?MetricID=" + (queryResults.GetInt32(queryResults.GetOrdinal("MetricID"))).ToString() + "&back=yes";
                metricLink.NavigateUrl = url;
                metricLink.Text = queryResults.GetString(queryResults.GetOrdinal("MetricName"));
                cell.Controls.Add(metricLink);
                row.Cells.Add(cell);

                //Metadata
                cell = new TableCell();
                cell.Text = queryResults.GetString(queryResults.GetOrdinal("Definition"));
                row.Cells.Add(cell);

                //Image Logo   
                cell = new TableCell();
                System.Web.UI.WebControls.Image logo = new System.Web.UI.WebControls.Image();
                logo.CssClass = "logo";
                logo.Style["float"] = "left";
                logo.ImageUrl = "../Data/" + (queryResults.GetString(queryResults.GetOrdinal("formula"))); ;
                cell.Controls.Add(logo);
                row.Cells.Add(cell);

                //Metadata
                cell = new TableCell();
                cell.Text = queryResults.GetString(queryResults.GetOrdinal("Frequency"));
                row.Cells.Add(cell);

                //Metadata
                cell = new TableCell();
                cell.Text = queryResults.GetString(queryResults.GetOrdinal("DataSource"));
                row.Cells.Add(cell);


                MetricTable.Rows.Add(row);
            }


        }

        private void PopulateReport()
        {
            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";
            SQL = "SELECT a.FileURL, a.FileName";
            SQL = SQL + " From COE.dbo.tblHelp_File a ";
            SQL = SQL + " Inner Join COE.dbo.tblApplication b ";
            SQL = SQL + " On a.ApplicationID = b.AppID ";
            SQL = SQL + " Where b.AppID = '" + Request.QueryString["AppID"] + "'";
            SQL = SQL + " order by a.FileName ";


            SqlDataReader queryResults = null;
            supportfiles.Rows.Clear();
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);
            }




            if (queryResults != null)
            {

                PopulateFileTableData(queryResults);

                queryResults.Close();
            }

            if (db.CloseConnection())
            {
                return;
            }
        }

        private void PopulateFileTableData(SqlDataReader queryResults)
        {
            TableRow row = null;
            TableCell cell = null;
            Int32 a = 0;

            while (queryResults.Read())
            {
                MSG.Text = "";
                if (a % 6 == 0)
                {
                    row = new TableRow();
                }

                cell = new TableCell();
                System.Web.UI.WebControls.HyperLink newlink = new HyperLink();
                newlink.CssClass = "image_hyperlinks";
                string fileURL = queryResults.GetString(queryResults.GetOrdinal("FileURL"));
                string temp = queryResults.GetString(queryResults.GetOrdinal("FileName"));
                newlink.NavigateUrl = fileURL;
                if (temp.Contains("pptx"))
                {
                    newlink.ImageUrl = "../img/page_white_powerpoint.png";
                }
                else if (temp.Contains("xlsx"))
                {
                    newlink.ImageUrl = "../img/page_white_excel.png";
                }
                else
                {
                    newlink.ImageUrl = "../img/page_white.png";
                }
                System.Web.UI.WebControls.HyperLink desc = new HyperLink();
                desc.CssClass = "name_hyperlinks";
                
                desc.NavigateUrl = "../Data/" + temp;
                desc.Text = temp;
                cell.Controls.Add(newlink);
                cell.Controls.Add(new LiteralControl("<br />"));
                cell.Controls.Add(desc);
                cell.CssClass = "cell";
                cell.Style["width"] = "20%";
                row.Cells.Add(cell);

                supportfiles.Rows.Add(row);
                a = a + 1;
            }



        }


        private void populate_purpose()
        {
            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";
            SQL = "SELECT  a.MetaData, a.AppName, a.ImageURL";
            SQL = SQL + " FROM COE.dbo.tblApplication a ";
            SQL = SQL + "Where AppID = '" + Request.QueryString["AppID"] + "'";

            SqlDataReader queryResults = null;

            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);
            }


            if (queryResults != null)
            {
                while (queryResults.Read())
                {
                    purpose.Text = queryResults.GetString(queryResults.GetOrdinal("MetaData"));
                    String temp = queryResults.GetString(queryResults.GetOrdinal("AppName"));
                    Label1.Text = temp;
                    Title.Text = temp;
                    TitleImage.ImageUrl = queryResults.GetString(queryResults.GetOrdinal("ImageURL"));
                    TitleImage.Width = 40;
                    TitleImage.Height = 40;

                }


                queryResults.Close();
            }

            if (db.CloseConnection())
            {
                return;
            }
        }

    }
}