using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevTemplateV3.WebApp.Common;
using System.Data.SqlClient;
using System.IO;
using System.Collections;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.text.html.simpleparser;
using EvoPdf;


namespace WebAppTemplateV3.xhtml
{
    public partial class MetricView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request.QueryString.Count > 0 && Request.QueryString["back"] != null)
            {
                PopulateMetricReport();
                if (Request.QueryString["back"].Equals("no"))
                    gobacknow.Visible = false;

            }
            gobacknow.OnClientClick = "GoB(); return false;";
            timestamps.Text = "Upadated:   "+timestamp();

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
            String metricID = Request.QueryString["MetricID"];
            String ps = "MetricView.aspx?MetricID=" + metricID + "&back=no&export=excel";
            Page.Response.Redirect(ps);
        }

        protected void cmdSupport_Click(object sender, EventArgs e)
        {
            String ps = "http://pmcoe/support/";
            Page.Response.Redirect(ps);
        }

        protected void cmdPrint_Click(object sender, EventArgs e)
        {
            String metricID = Request.QueryString["MetricID"];
            String ps = "MetricView.aspx?MetricID=" + metricID + "&back=no&export=print";
            Page.Response.Redirect(ps);
        }

        protected void cmdAdobe_Click(object sender, EventArgs e)
        {
            String metricID = Request.QueryString["MetricID"];
            String ps = "MetricView.aspx?MetricID=" + metricID + "&back=no&export=adobe";
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
            metricSQL = "select c.MetricName, c.Definition,c.Objective, c.DataSource, mc.CategoryName,c.Comments, c.CCR, c.IndustryAvg,c.Example, c.WorldClass, d.Name,d.HelpText,fr.FrequencyName as Frequency, c.DataSource, c.MetricID, a.AppName, a.AppID, f.PillarDesc, u.DisplayName, c.MetricOwnerUserID ";
            metricSQL = metricSQL + "from COE.dbo.tblHelp_Metrics c ";
            metricSQL = metricSQL + "Left JOIN COE.dbo.tblHelp_Application_Metrics b on c.MetricID = b.MetricID ";
            metricSQL = metricSQL + "Left JOIN COE.dbo.tblApplication a ON a.AppID = b.ApplicationID ";
            metricSQL = metricSQL + "INNER JOIN COE.dbo.tblHelp_Metrics_Calculation d ON d.MetricID = c.MetricID ";
            metricSQL = metricSQL + "INNER JOIN COE.dbo.tblApplication_Pillar f ON f.PillarID = a.PillarID ";
            metricSQL = metricSQL + "INNER JOIN COE.dbo.tblHelp_Frequency AS fr ON c.FrequencyID = fr.FrequencyID ";
            metricSQL = metricSQL + "INNER JOIN tblHelp_Metrics_Category  AS mc ON c.MetricCategory = mc.CategoryID ";
            metricSQL = metricSQL + "Left JOIN COE.dbo.tblUser u on u.UserID = c.BusinessOwnerID ";
            metricSQL = metricSQL + "where c.MetricID = '" + Request.QueryString["MetricID"] + "'";

            String SQL = "";
            SQL = "SELECT b.MetricID, b.MetricName";
            SQL = SQL + " from coe.dbo.tblHelp_Metrics_Related a ";
            SQL = SQL + " inner join COE.dbo.tblHelp_Metrics b ";
            SQL = SQL + " on a.RelatedMetricID = b.MetricID or a.MetricID = b.MetricID ";
            SQL = SQL + " where b.MetricID != '" + Request.QueryString["MetricID"] + "'  AND (a.MetricID = '" + Request.QueryString["MetricID"] + "' or a.RelatedMetricID = '" + Request.QueryString["MetricID"] + "')";
            SQL = SQL + " order by b.MetricName ";

            SqlDataReader metricResults = null;
            //SqlDataReader relatedResults = null;

            if (db1.OpenConnection())
            {

                metricResults = db1.ExecuteQuerySQL(metricSQL);
            }

            if (metricResults != null)
            {
                generateTitle(metricResults);

                metricResults.Close();

                metricResults = db1.ExecuteQuerySQL(SQL);
                Dictionary<String, String> relatedString = generateRelated(metricResults);
                metricResults.Close();

                metricResults = db1.ExecuteQuerySQL(metricSQL);

                //relatedResults = db1.ExecuteQuerySQL(rSQL);
                //relatedResults.Close();
                PopulateMetricTableData(metricResults, relatedString);
                metricResults.Close();


            }

            if (db1.CloseConnection())
            {
                return;
            }
        }

        private Dictionary<string, string> generateRelated(SqlDataReader queryResults)
        {
            Dictionary<String, String> related = new Dictionary<String, String>();
            while (queryResults.Read())
            {
                String temp = (queryResults.GetString(queryResults.GetOrdinal("MetricName")));
                String temp2 = (queryResults.GetInt32(queryResults.GetOrdinal("MetricID"))).ToString();
                if (!related.ContainsKey(temp))
                    related.Add(temp2, temp);
            }
            return related;
        }

        private void generateTitle(SqlDataReader queryResults)
        {
            while (queryResults.Read())
            {
                Label1.Text = (queryResults.GetString(queryResults.GetOrdinal("MetricName")));

            }
        }

        private void PopulateMetricTableData(SqlDataReader queryResults, Dictionary<String, String> related)
        {
            String pillarname = "", appName = null, MetricCategory = null, definition = null, objective = null, datasource = null, CollectionFrequency = null, IndustryAvg = null,
                WorldClass = null, Comments = null, Example = null, bowner = null, mowner = null, CCR = null, formulaName = null, formulaHelp = null;
            Dictionary<String, String> relateApp = new Dictionary<String, String>();
            while (queryResults.Read())
            {
                String temp = (queryResults.GetString(queryResults.GetOrdinal("PillarDesc")));
                if (!pillarname.Contains(temp))
                    pillarname = pillarname + temp + ", ";


                appName = (queryResults.GetString(queryResults.GetOrdinal("AppName")));
                String appID = (queryResults.GetInt32(queryResults.GetOrdinal("AppID"))).ToString();
                relateApp.Add(appID, appName);
                formulaHelp = (queryResults.GetString(queryResults.GetOrdinal("HelpText")));
                formulaName = (queryResults.GetString(queryResults.GetOrdinal("Name")));
                MetricCategory = (queryResults.GetString(queryResults.GetOrdinal("CategoryName")));
                definition = (queryResults.GetString(queryResults.GetOrdinal("Definition")));
                objective = (queryResults.GetString(queryResults.GetOrdinal("Objective")));
                datasource = (queryResults.GetString(queryResults.GetOrdinal("DataSource")));
                CollectionFrequency = (queryResults.GetString(queryResults.GetOrdinal("Frequency")));
                IndustryAvg = (queryResults.GetString(queryResults.GetOrdinal("IndustryAvg")));
                WorldClass = (queryResults.GetString(queryResults.GetOrdinal("WorldClass")));
                CCR = (queryResults.GetString(queryResults.GetOrdinal("CCR")));
                Comments = (queryResults.GetString(queryResults.GetOrdinal("Comments")));
                Example = (queryResults.GetString(queryResults.GetOrdinal("Example")));
                bowner = (queryResults.GetString(queryResults.GetOrdinal("DisplayName")));
                mowner = (queryResults.GetString(queryResults.GetOrdinal("MetricOwnerUserID")));


            }

            TableRow row = null;
            TableCell cell = null;

            //a row
            row = new TableRow();

            cell = new TableCell();
            cell.Text = "PMCOE Pillar";
            cell.Style["width"] = "20%";
            cell.Style["background-color"] = "#F6F6F1";

            cell.Style["font-weight"] = "bold";

            row.Cells.Add(cell);

            cell = new TableCell();
            if (pillarname.Length > 3)
            {
                cell.Text = pillarname.Substring(0, pillarname.Length - 2);
            }
            else
            {
                cell.Text = pillarname;
            }
            row.Cells.Add(cell);
            cell.Style["width"] = "80%";


            MetricTable.Rows.Add(row);

            //a row
            row = new TableRow();

            cell = new TableCell();
            cell.Text = "Scorecard";
            cell.Style["width"] = "20%";
            cell.Style["background-color"] = "#F6F6F1";
            cell.Style["font-weight"] = "bold";
            row.Cells.Add(cell);

            cell = new TableCell();

            HyperLink applink = null;
            Label spaced = null;
            int count = relateApp.Count;
            int tempnum = 1;
            foreach (String id in relateApp.Keys)
            {
                applink = new HyperLink();
                spaced = new Label();
                spaced.Text = ", ";
                applink.CssClass = "name_hyperlinks";
                applink.NavigateUrl = "AppView.aspx" + "?AppID=" + id + "&back=yes";
                applink.Text = relateApp[id];
                cell.Controls.Add(applink);
                if(tempnum<count)
                {
                    cell.Controls.Add(spaced);
                }
                tempnum = tempnum + 1;


            }

            row.Cells.Add(cell);
            cell.Style["width"] = "80%";


            MetricTable.Rows.Add(row);

            //a row
            row = new TableRow();

            cell = new TableCell();
            cell.Text = "Metric Category";
            cell.Style["width"] = "20%";
            cell.Style["background-color"] = "#F6F6F1";
            cell.Style["font-weight"] = "bold";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = MetricCategory;
            row.Cells.Add(cell);
            cell.Style["width"] = "80%";


            MetricTable.Rows.Add(row);

            //a row
            row = new TableRow();

            cell = new TableCell();
            cell.Text = "Definition";
            cell.Style["width"] = "20%";
            cell.Style["background-color"] = "#F6F6F1";
            cell.Style["font-weight"] = "bold";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = definition;
            row.Cells.Add(cell);
            cell.Style["width"] = "80%";


            MetricTable1.Rows.Add(row);

            //a row
            row = new TableRow();

            cell = new TableCell();
            cell.Text = "Objective";
            cell.Style["width"] = "20%";
            cell.Style["background-color"] = "#F6F6F1";
            cell.Style["font-weight"] = "bold";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = objective;
            row.Cells.Add(cell);
            cell.Style["width"] = "80%";


            MetricTable1.Rows.Add(row);

            //a row
            row = new TableRow();

            cell = new TableCell();
            cell.Text = "Calculation";
            cell.Style["width"] = "20%";
            cell.Style["background-color"] = "#F6F6F1";
            cell.Style["font-weight"] = "bold";
            row.Cells.Add(cell);

            cell = new TableCell();


            Table formulaTable = new Table();
            formulaTable.CssClass = "bench";

            TableRow formulaimage = new TableRow();
            TableCell formulacell = new TableCell();

            System.Web.UI.WebControls.Image logo = new System.Web.UI.WebControls.Image();
            logo.CssClass = "logo";
            logo.Style["float"] = "left";
            logo.ImageUrl = "../Data/" + formulaName;
            formulacell.Controls.Add(logo);


            formulaimage.Cells.Add(formulacell);
            formulaTable.Rows.Add(formulaimage);
            formulacell.Style["border-style"] = "hidden";

            TableRow helptextrow = new TableRow();
            TableCell helptextcell = new TableCell();
            helptextcell.Style["border-style"] = "hidden";
            helptextcell.Text = formulaHelp;
            helptextrow.Cells.Add(helptextcell);
            formulaTable.Rows.Add(helptextrow);

            cell.Controls.Add(formulaTable);

            row.Cells.Add(cell);
            cell.Style["width"] = "80%";


            MetricTable1.Rows.Add(row);

            //a row
            row = new TableRow();

            cell = new TableCell();
            cell.Text = "Data Source";
            cell.Style["width"] = "20%";
            cell.Style["background-color"] = "#F6F6F1";
            cell.Style["font-weight"] = "bold";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = datasource;
            row.Cells.Add(cell);
            cell.Style["width"] = "80%";


            MetricTable1.Rows.Add(row);

            //a row
            row = new TableRow();

            cell = new TableCell();
            cell.Text = "Frequency";
            cell.Style["width"] = "20%";
            cell.Style["background-color"] = "#F6F6F1";
            cell.Style["font-weight"] = "bold";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = CollectionFrequency;
            row.Cells.Add(cell);
            cell.Style["width"] = "80%";


            MetricTable1.Rows.Add(row);

            //a row
            row = new TableRow();

            cell = new TableCell();
            cell.Text = "Related Supporting Metrics";
            cell.Style["width"] = "20%";
            cell.Style["background-color"] = "#F6F6F1";
            cell.Style["font-weight"] = "bold";
            row.Cells.Add(cell);

            cell = new TableCell();
            HyperLink desc = null;
            Label space = null;
            int count2 = related.Count;
            int tempnum2 = 1;
            foreach (String id in related.Keys)
            {
                desc = new HyperLink();
                space = new Label();
                space.Text = ", ";
                desc.CssClass = "name_hyperlinks";
                desc.NavigateUrl = "MetricView.aspx" + "?MetricID=" + id + "&back=yes";
                desc.Text = related[id];
                cell.Controls.Add(desc);
                if (tempnum2 < count2)
                {
                    cell.Controls.Add(space);
                }
                tempnum2 = tempnum2 + 1;


            }
            row.Cells.Add(cell);
            cell.Style["width"] = "80%";


            MetricTable1.Rows.Add(row);

            //a row
            row = new TableRow();

            cell = new TableCell();
            cell.Text = "Benchmark";
            cell.Style["width"] = "20%";
            cell.Style["background-color"] = "#F6F6F1";
            cell.Style["font-weight"] = "bold";
            row.Cells.Add(cell);

            cell = new TableCell();

            Table benchTable = new Table();
            benchTable.CssClass = "bench";

            TableRow industry = new TableRow();
            TableCell indtitle = new TableCell();
            indtitle.Style["border-style"] = "hidden";
            indtitle.Text = "Industry Average";
            industry.Cells.Add(indtitle);
            TableCell indata = new TableCell();
            indata.Text = IndustryAvg;
            industry.Cells.Add(indata);
            benchTable.Rows.Add(industry);
            indata.Style["border-style"] = "hidden";
            indtitle.Style["width"] = "40%";

            TableRow worldrow = new TableRow();
            TableCell wortitle = new TableCell();
            wortitle.Style["border-style"] = "hidden";
            wortitle.Style["width"] = "40%";
            wortitle.Text = "World Class";
            worldrow.Cells.Add(wortitle);
            TableCell wordata = new TableCell();
            wordata.Style["border-style"] = "hidden";
            wordata.Text = WorldClass;
            worldrow.Cells.Add(wordata);
            benchTable.Rows.Add(worldrow);

            TableRow ccrrow = new TableRow();
            TableCell ccrtitle = new TableCell();
            ccrtitle.Style["border-style"] = "hidden";
            ccrtitle.Style["width"] = "40%";
            ccrtitle.Text = "CCR";
            ccrrow.Cells.Add(ccrtitle);
            TableCell ccrdata = new TableCell();
            ccrdata.Style["border-style"] = "hidden";
            ccrdata.Text = CCR;
            ccrrow.Cells.Add(ccrdata);
            benchTable.Rows.Add(ccrrow);

            cell.Controls.Add(benchTable);
            row.Cells.Add(cell);
            cell.Style["width"] = "80%";


            MetricTable1.Rows.Add(row);

            //a row
            row = new TableRow();

            cell = new TableCell();
            cell.Text = "Comments";
            cell.Style["width"] = "20%";
            cell.Style["background-color"] = "#F6F6F1";
            cell.Style["font-weight"] = "bold";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = Comments;
            row.Cells.Add(cell);
            cell.Style["width"] = "80%";


            MetricTable2.Rows.Add(row);

            //a row
            row = new TableRow();

            cell = new TableCell();
            cell.Text = "Example";
            cell.Style["width"] = "20%";
            cell.Style["background-color"] = "#F6F6F1";
            cell.Style["font-weight"] = "bold";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = Example;
            row.Cells.Add(cell);
            cell.Style["width"] = "80%";


            MetricTable2.Rows.Add(row);

            //a row
            row = new TableRow();

            cell = new TableCell();
            cell.Text = "Business Owner";
            cell.Style["width"] = "20%";
            cell.Style["background-color"] = "#F6F6F1";
            cell.Style["font-weight"] = "bold";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = bowner;
            row.Cells.Add(cell);
            cell.Style["width"] = "80%";


            MetricTable3.Rows.Add(row);

            //a row
            row = new TableRow();

            cell = new TableCell();
            cell.Text = "Support Center";
            cell.Style["width"] = "20%";
            cell.Style["background-color"] = "#F6F6F1";
            cell.Style["font-weight"] = "bold";
            row.Cells.Add(cell);

            cell = new TableCell();
            HyperLink s = new HyperLink();
            s.Text = "Support Center";
            s.NavigateUrl = "http://pmcoe/support/";
            cell.Controls.Add(s);
            row.Cells.Add(cell);
            cell.Style["width"] = "80%";


            MetricTable4.Rows.Add(row);
        }

        // only create time
        private String timestamp()
        {

            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";
            SQL = "DECLARE @myDateTime DATETIME ";
            SQL = SQL + " SET @myDateTime =  ";
            SQL = SQL + " ( ";
            SQL = SQL + " select MyTimeStamp ";
            SQL = SQL + " from coe.dbo.tblHelp_Metrics ";
            SQL = SQL + " where MetricID = '" + Request.QueryString["MetricID"] + "'";
            SQL = SQL + " ) ";
            SQL = SQL + " SELECT Left(CONVERT(VARCHAR, @myDateTime, 120), 19) AS times ";
            SqlDataReader queryResults = null;
            string value = "";
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);
            }
            if (queryResults != null)
            {
                while (queryResults.Read())
                {
                    value = queryResults.GetString(queryResults.GetOrdinal("times"));

                }
                queryResults.Close();

            }
            return value;

        }

    }
}