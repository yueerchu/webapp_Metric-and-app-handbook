using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using DevTemplateV3.WebApp.UI.Controls;
using DevTemplateV3.WebApp.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using WebAppTemplateV3.xhtml;
using System.Collections;


namespace DevTemplateV3.WebApp.UI.Pages
{
    public partial class LauncherPage : System.Web.UI.Page
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            filter1();
            filter2();
            filter3();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            NavigationBarControl nv = (NavigationBarControl)Master.FindControl("navBar");
            nv.SetActiveLinkHome(true);
            if (!IsPostBack) { resultsPanel.Visible = false; divLine.Visible = false; }
            else { resultsPanel.Visible = true; divLine.Visible = true; }
        }


        private void filter1()
        {
            ListItem item = null;
            pillars.Items.Clear();
            pillars.Items.Add("All Pillars");

            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";
            SQL = "SELECT PillarDesc";
            SQL = SQL + " FROM COE.dbo.tblApplication_Pillar ";
            SQL = SQL + "Where PillarDesc != 'Unknown' And PillarDesc != 'Other'";

            SqlDataReader queryResults = null;
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);
            }
            if (queryResults != null)
            {
                while (queryResults.Read())
                {
                    item = new ListItem(queryResults.GetString(queryResults.GetOrdinal("PillarDesc")));
                    pillars.Items.Add(item);
                }
                queryResults.Close();
            }

            if (db.CloseConnection())
            {
                return;
            }
        }

        private void filter2()
        {
            ListItem item = null;
            appss.Items.Clear();

            appss.Items.Add("All Apps");
            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";
            SQL = "SELECT a.AppName";
            SQL = SQL + " FROM COE.dbo.tblApplication a ";
            SQL = SQL + " INNER JOIN COE.dbo.tblApplication_Pillar b ";
            SQL = SQL + " ON a.PillarID = b.PillarID ";



            if (pillars.SelectedValue != "All Pillars" && pillars.SelectedValue != "")
            {
                SQL = SQL + " And b.PillarDesc = '" + pillars.SelectedValue + "'";
            }

            SQL = SQL + " order by a.AppName ";

            SqlDataReader queryResults = null;
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);
            }

            if (queryResults != null)
            {

                while (queryResults.Read())
                {
                    item = new ListItem(queryResults.GetString(queryResults.GetOrdinal("AppName")));
                    appss.Items.Add(item);
                }

                queryResults.Close();
            }

            if (db.CloseConnection())
            {
                return;
            }


        }

        private void filter3()
        {
            ListItem item = null;
            metrics.Items.Clear();

            metrics.Items.Add("All Metrics");
            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String metricSQL = "";
            metricSQL = "select Distinct c.MetricName";
            metricSQL = metricSQL + " from COE.dbo.tblHelp_Metrics c ";

            if (appss.SelectedValue != "All Apps" && appss.SelectedValue != "")
            {

                metricSQL = "select Distinct c.MetricName";
                metricSQL = metricSQL + " from COE.dbo.tblApplication a,  COE.dbo.tblHelp_Metrics c, COE.dbo.tblHelp_Application_Metrics b ";
                metricSQL = metricSQL + " where c.MetricID = b.MetricID AND a.AppID = b.ApplicationID ";
                metricSQL = metricSQL + " And a.AppName = '" + appss.SelectedValue + "'";
            }


            metricSQL = metricSQL + " order by c.MetricName ";

            SqlDataReader queryResults = null;

            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(metricSQL);
            }

            if (queryResults != null)
            {

                while (queryResults.Read())
                {
                    item = new ListItem(queryResults.GetString(queryResults.GetOrdinal("MetricName")));
                    metrics.Items.Add(item);
                }

                queryResults.Close();
            }

            if (db.CloseConnection())
            {
                return;
            }


        }

        protected void pillar_click(object sender, EventArgs e)
        {

            filter2();
            filter3();
            PopulateReport();

        }

        protected void app_click(object sender, EventArgs e)
        {
            filter3();
            PopulateReport();


        }

        protected void metric_click(object sender, EventArgs e)
        {

            PopulateReport();

        }



        protected void RefreshButton_Click(object sender, EventArgs e)
        {
            PopulateReport();

        }

        private void PopulateReport()
        {
            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String appSQL = "";
            appSQL = "SELECT a.ImageURL, a.AppName, a.MetaData as Definition, a.AppID";
            appSQL = appSQL + " FROM COE.dbo.tblApplication a ";
            appSQL = appSQL + " INNER JOIN COE.dbo.tblApplication_Pillar b ";
            appSQL = appSQL + " ON a.PillarID = b.PillarID ";
            appSQL = appSQL + "Where AppName Like '%" + TextBox1.Text + "%'";

            String metricSQL = "";
            metricSQL = "select m.MetricName, m.Definition, mc.Name as Formula, f.FrequencyName as Frequency, m.DataSource, m.MetricID ";
            metricSQL += "from tblHelp_Metrics AS m ";
            metricSQL += "INNER JOIN tblHelp_Metrics_Calculation AS mc ON mc.MetricID = m.MetricID ";
            metricSQL += "INNER JOIN tblHelp_Frequency AS f ON m.FrequencyID = f.FrequencyID ";
            metricSQL += "AND m.MetricName Like '%" + TextBox1.Text + "%'";


            if (pillars.SelectedValue != "All Pillars" && pillars.SelectedValue != "")
            {
                appSQL = appSQL + " And b.PillarDesc = '" + pillars.SelectedValue + "'";
            }

            if (appss.SelectedValue != "All Apps" && appss.SelectedValue != "")
            {

                appSQL = appSQL + " And a.AppName = '" + appss.SelectedValue + "'";
                metricSQL = "select m.MetricName, m.Definition, mc.Name as Formula, f.FrequencyName as Frequency, m.DataSource, a.AppName, a.AppID, m.MetricID ";
                metricSQL += "from tblHelp_Metrics AS m ";
                metricSQL += "INNER JOIN tblHelp_Application_Metrics AS am ON m.MetricID = am.MetricID ";
                metricSQL += "INNER JOIN tblApplication AS a ON a.AppID = am.ApplicationID ";
                metricSQL += "INNER JOIN tblHelp_Metrics_Calculation AS mc ON mc.MetricID = m.MetricID ";
                metricSQL += "INNER JOIN tblHelp_Frequency AS f ON m.FrequencyID = f.FrequencyID ";
                metricSQL += "AND m.MetricName Like '%" + TextBox1.Text + "%'";
                metricSQL = metricSQL + " And a.AppName = '" + appss.SelectedValue + "'";
            }

            if (metrics.SelectedValue != "All Metrics" && metrics.SelectedValue != "")
            {
                metricSQL = metricSQL + " And m.MetricName = '" + metrics.SelectedValue + "'";
            }

            appSQL = appSQL + " order by AppName ";
            metricSQL = metricSQL + "order by m.MetricName ";

            SqlDataReader appResults = null;
            SqlDataReader metricResults = null;
            AppTable.Rows.Clear();
            MetricTable.Rows.Clear();
            if (db.OpenConnection())
            {
                appResults = db.ExecuteQuerySQL(appSQL);

            }

            if (appResults != null)
            {
                AppTableHeader(appResults);
                PopulateAppTableData(appResults);

                appResults.Close();

            }


            DatabaseConnection db1 = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);
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

        private void AppTableHeader(SqlDataReader queryResults)
        {
            TableRow row = null;
            TableCell cell = null;

            row = new TableRow();
            cell = new TableCell();

            cell.Style["width"] = "0%";
            cell.CssClass = "DataTableHeaderCell";
            cell.Style["background-color"] = "#787878";
            cell.Style["height"] = "20px";
            cell.Style["font-weight"] = "bold";
            cell.Style["font-size"] = "17px";
            cell.Style["border-right-width"] = "0px";

            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Text = "Application Name";

            cell.Style["width"] = "30%";
            cell.CssClass = "DataTableHeaderCell";
            cell.Style["color"] = "white";
            cell.Style["height"] = "20px";
            cell.Style["font-weight"] = "bold";
            cell.Style["font-size"] = "17px";
            cell.Style["background-color"] = "#787878";
            cell.Style["border-left-width"] = "0px";
            row.Cells.Add(cell);
            cell = new TableCell();
            cell.Text = queryResults.GetName(queryResults.GetOrdinal("Definition"));

            cell.Style["width"] = "70%";
            cell.Style["background-color"] = "#787878";
            cell.Style["color"] = "white";
            cell.Style["height"] = "20px";
            cell.Style["font-weight"] = "bold";
            cell.Style["font-size"] = "17px";
            cell.CssClass = "DataTableHeaderCell";
            row.Cells.Add(cell);
            AppTable.Rows.Add(row);
        }

        private void PopulateAppTableData(SqlDataReader queryResults)
        {
            TableRow row = null;
            TableCell cell = null;
            Boolean hasData = false;

            while (queryResults.Read())
            {
                hasData = true;
                row = new TableRow();

                //Image Logo   
                cell = new TableCell();
                System.Web.UI.WebControls.Image logo = new System.Web.UI.WebControls.Image();
                cell.CssClass = "applogo";
                cell.Style["border-right-width"] = "0px";
                logo.CssClass = "logo";
                logo.Width = 40;
                logo.Height = 40;
                logo.ImageUrl = queryResults.GetString(queryResults.GetOrdinal("ImageURL"));
                cell.Controls.Add(logo);
                row.Cells.Add(cell);

                //LinkButtons
                cell = new TableCell();
                LinkButton lbtnNavigateToLogDetail = new LinkButton();
                cell.CssClass = "appname";
                cell.Style["border-left-width"] = "0px";
                lbtnNavigateToLogDetail.CssClass = "linkbuttonCss";
                string url = "AppView.aspx" + "?AppID=" + queryResults.GetInt32(queryResults.GetOrdinal("AppID")).ToString() + "&back=no";
                lbtnNavigateToLogDetail.OnClientClick = "Navigate('" + url + "'); return false;";
                lbtnNavigateToLogDetail.Text = queryResults.GetString(queryResults.GetOrdinal("AppName"));
                cell.Controls.Add(lbtnNavigateToLogDetail);
                row.Cells.Add(cell);

                //Metadata
                cell = new TableCell();
                cell.Text = queryResults.GetString(queryResults.GetOrdinal("Definition"));
                cell.CssClass = "appmeta";
                row.Cells.Add(cell);

                AppTable.Rows.Add(row);
            }

            if (!hasData) { AppTable.Visible = false; AppTableNoData.Visible = true; }
            else { AppTable.Visible = true; AppTableNoData.Visible = false; }

        }

        private void MetricTableHeader(SqlDataReader queryResults)
        {
            TableRow row = null;
            TableCell cell = null;

            row = new TableRow();
            cell = new TableCell();
            cell.Text = "Metric Name";

            cell.Style["width"] = "12%";
            cell.CssClass = "DataTableHeaderCell";
            cell.Style["color"] = "white";
            cell.Style["height"] = "20px";
            cell.Style["font-weight"] = "bold";
            cell.Style["font-size"] = "17px";
            cell.Style["background-color"] = "#787878";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = queryResults.GetName(queryResults.GetOrdinal("Definition"));
            cell.Style["width"] = "30%";
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
            cell.Style["width"] = "13%";
            cell.Style["background-color"] = "#787878";
            cell.Style["color"] = "white";
            cell.Style["height"] = "20px";
            cell.Style["font-weight"] = "bold";
            cell.Style["font-size"] = "17px";
            cell.CssClass = "DataTableHeaderCell";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Data Source";
            cell.Style["width"] = "10%";
            cell.Style["background-color"] = "#787878";
            cell.Style["color"] = "white";
            cell.Style["height"] = "20px";
            cell.Style["font-weight"] = "bold";
            cell.Style["font-size"] = "17px";
            cell.CssClass = "DataTableHeaderCell";
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Application Name";
            cell.Style["width"] = "15%";
            cell.Style["background-color"] = "#787878";
            cell.Style["color"] = "white";
            cell.Style["height"] = "20px";
            cell.Style["font-weight"] = "bold";
            cell.Style["font-size"] = "17px";
            cell.CssClass = "DataTableHeaderCell";
            row.Cells.Add(cell);

            MetricTable.Rows.Add(row);
        }


        private void PopulateMetricTableData(SqlDataReader queryResults)
        {
            TableRow row = null;
            TableCell cell = null;
            ArrayList temp = new ArrayList();
            while (queryResults.Read())
            {
                Int32 tempnum = (queryResults.GetInt32(queryResults.GetOrdinal("metricID")));
                if (!temp.Contains(tempnum))
                {

                    row = new TableRow();

                    //LinkButton
                    cell = new TableCell();
                    LinkButton lbtnNavigateToLogDetail = new LinkButton();
                    lbtnNavigateToLogDetail.CssClass = "linkbuttonCss";
                    String tempid = (queryResults.GetInt32(queryResults.GetOrdinal("MetricID"))).ToString();
                    string url = "MetricView.aspx" + "?MetricID=" + tempid + "&back=no";
                    lbtnNavigateToLogDetail.OnClientClick = "Navigate('" + url + "'); return false;";
                    String metricName = queryResults.GetString(queryResults.GetOrdinal("MetricName"));
                    lbtnNavigateToLogDetail.Text = metricName;
                    cell.Controls.Add(lbtnNavigateToLogDetail);
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

                    //LinkButton
                    cell = new TableCell();


                    Dictionary<string, string> related = P(metricName);

                    HyperLink applink = null;
                    Label spaced = null;
                    int count2 = related.Count;
                    int tempnum2 = 1;
                    foreach (String id in related.Keys)
                    {
                        LinkButton appbutton = new LinkButton();
                        spaced = new Label();
                        spaced.Text = ", ";
                        appbutton.CssClass = "linkbuttonCss";
                        string linkurl = "AppView.aspx" + "?AppID=" + id + "&back=no";
                        appbutton.OnClientClick = "Navigate('" + linkurl + "'); return false;";
                        appbutton.Text = related[id];
                        cell.Controls.Add(appbutton);
                        if (tempnum2 < count2)
                        {
                            cell.Controls.Add(spaced);
                        }
                        tempnum2 = tempnum2 + 1;

                    }

                    row.Cells.Add(cell);

                    MetricTable.Rows.Add(row);
                    temp.Add(tempnum);

                }
            }
            if (temp.Count == 0) { MetricTable.Visible = false; MetricTableNoData.Visible = true; }
            else { MetricTable.Visible = true; MetricTableNoData.Visible = false; }
        }

        private Dictionary<string, string> P(String metricName)
        {
            DatabaseConnection db2 = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String metricSQL = "";
            metricSQL = "select a.AppName, a.AppID";
            metricSQL = metricSQL + " from COE.dbo.tblApplication a,  COE.dbo.tblHelp_Metrics c, COE.dbo.tblHelp_Application_Metrics b ";
            metricSQL = metricSQL + " where c.MetricID = b.MetricID AND a.AppID = b.ApplicationID AND c.MetricName = '" + metricName + "'";
            metricSQL = metricSQL + " order by a.AppName ";

            SqlDataReader appResults3 = null;
            Dictionary<String, String> relateApp = new Dictionary<String, String>();

            if (db2.OpenConnection())
            {
                appResults3 = db2.ExecuteQuerySQL(metricSQL);

            }

            if (appResults3 != null)
            {

                while (appResults3.Read())
                {
                    String appName = (appResults3.GetString(appResults3.GetOrdinal("AppName")));
                    String appID = (appResults3.GetInt32(appResults3.GetOrdinal("AppID"))).ToString();
                    relateApp.Add(appID, appName);
                }

                appResults3.Close();
            }
            return relateApp;
        }

    }
}