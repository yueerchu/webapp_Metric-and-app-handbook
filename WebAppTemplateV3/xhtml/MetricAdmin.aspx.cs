using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevTemplateV3.WebApp.Common;
using System.Data.SqlClient;
using AjaxControlToolkit;

namespace WebAppTemplateV3.xhtml
{
    public partial class MetricAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            uploadmsg.Visible = false;
            if (this.Request.QueryString.Count > 1)
            {
                thankyou.Visible = true;
                mainpanel.Visible = false;
                String metricID = Request.QueryString["MetricID"];
                String ps = "MetricAdmin.aspx?MetricID=" + metricID;
                goback.NavigateUrl = ps;
                newview.NavigateUrl = "MetricView.aspx" + "?MetricID=" + metricID + "&back=yes";
            }
            else if (this.Request.QueryString.Count > 0)
            {
                mainpanel.Visible = true;
                thankyou.Visible = false;
                nextb.Visible = false;
                String s = Request.QueryString["MetricID"];
                if (!IsPostBack)
                {
                    catedd();
                    freqdd();
                    metriclb();
                    metriclb2();
                    remetriclb(s);
                    autofill(s);
                    remetriclb2(s);
                    formulas(s);
                }

            }
            else
            {
                mainpanel.Visible = true;
                thankyou.Visible = false;
                submitb.Visible = false;
                nextstep.Visible = false;
                if (!IsPostBack)
                {
                    catedd();
                    freqdd();
                    metriclb();
                    metriclb2();
                }
            }
        }


        private void deleterelated()
        {
            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";

            SQL += " Delete from coe.dbo.tblHelp_Application_Metrics ";
            SQL = SQL + " where ApplicationID = '" + related.SelectedValue + "' AND MetricID = '" + metricid.Text + "'";

            SqlDataReader queryResults = null;
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);

            }
            queryResults.Close();
            if (db.CloseConnection())
            {
                return;
            }
        }

        private void deleterelated2()
        {
            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";

            SQL += " Delete from coe.dbo.tblHelp_Metrics_Related ";
            SQL = SQL + " where (RelatedMetricID = '" + related2.SelectedValue + "' AND MetricID = '" + metricid.Text + "') OR (MetricID = '" + related2.SelectedValue + "' AND RelatedMetricID = '" + metricid.Text + "')";

            SqlDataReader queryResults = null;
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);

            }
            queryResults.Close();
            if (db.CloseConnection())
            {
                return;
            }
        }

        private void deletefile()
        {
            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";

            SQL += " update coe.dbo.tblHelp_Metrics_Calculation ";
            SQL = SQL + " set Name = 'NotAvailable.png' ";
            SQL = SQL + " where MetricID = '" + Request.QueryString["MetricID"] + "'";

            SqlDataReader queryResults = null;
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);

            }
            queryResults.Close();
            if (db.CloseConnection())
            {
                return;
            }
        }

        private void addfile(String fn)
        {
            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";

            SQL += " update coe.dbo.tblHelp_Metrics_Calculation ";
            SQL = SQL + " set Name = '" + fn + "' ";
            SQL = SQL + " where MetricID = '" + Request.QueryString["MetricID"] + "'";


            SqlDataReader queryResults = null;
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);

            }
            queryResults.Close();
            if (db.CloseConnection())
            {
                return;
            }
        }


        private void addrelated()
        {
            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";

            SQL += " Insert into coe.dbo.tblHelp_Application_Metrics (ApplicationID, MetricID) ";
            SQL = SQL + " Values(" + allmetric.SelectedValue + "," + metricid.Text + ")";

            SqlDataReader queryResults = null;
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);

            }
            queryResults.Close();
            if (db.CloseConnection())
            {
                return;
            }
        }

        private void addrelated2()
        {
            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";

            SQL += " Insert into coe.dbo.tblHelp_Metrics_Related (MetricID,RelatedMetricID) ";
            SQL = SQL + " Values(" + metricid.Text + "," + allmetric2.SelectedValue + ")";

            SqlDataReader queryResults = null;
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);

            }
            queryResults.Close();
            if (db.CloseConnection())
            {
                return;
            }
        }

        private void remetriclb(string metricids)
        {
            ListItem item = null;
            related.Items.Clear();

            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";
            SQL = "SELECT b.AppID, b.AppName";
            SQL = SQL + " from coe.dbo.tblHelp_Application_Metrics a ";
            SQL = SQL + " inner join COE.dbo.tblApplication b ";
            SQL = SQL + " on a.ApplicationID = b.AppID ";
            SQL = SQL + " where a.MetricID = '" + metricids + "'";
            SQL = SQL + " order by b.AppName ";

            SqlDataReader queryResults = null;
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);
            }
            if (queryResults != null)
            {
                while (queryResults.Read())
                {
                    String text = queryResults.GetString(queryResults.GetOrdinal("AppName"));
                    String value = queryResults.GetInt32(queryResults.GetOrdinal("AppID")).ToString();
                    item = new ListItem(text, value);
                    related.Items.Add(item);
                }
                queryResults.Close();
            }

            if (db.CloseConnection())
            {
                return;
            }
        }

        private void remetriclb2(string metricids)
        {
            ListItem item = null;
            related2.Items.Clear();

            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";
            SQL = "SELECT b.MetricID, b.MetricName";
            SQL = SQL + " from coe.dbo.tblHelp_Metrics_Related a ";
            SQL = SQL + " inner join COE.dbo.tblHelp_Metrics b ";
            SQL = SQL + " on a.RelatedMetricID = b.MetricID or a.MetricID = b.MetricID ";
            SQL = SQL + " where b.MetricID != '" + metricids + "'  AND (a.MetricID = '" + metricids + "' or a.RelatedMetricID = '" + metricids + "')";
            SQL = SQL + " order by b.MetricName ";

            SqlDataReader queryResults = null;
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);
            }
            if (queryResults != null)
            {
                while (queryResults.Read())
                {
                    String text = queryResults.GetString(queryResults.GetOrdinal("MetricName"));
                    String value = queryResults.GetInt32(queryResults.GetOrdinal("MetricID")).ToString();
                    item = new ListItem(text, value);
                    related2.Items.Add(item);
                }
                queryResults.Close();
            }

            if (db.CloseConnection())
            {
                return;
            }
        }

        private void autofill(string metricids)
        {
            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";
            SQL = "SELECT [MetricID],[MetricName],[MetaData],[MetricCategory],[Definition],[Objective],[DataSource],[IndustryAvg],[WorldClass],[Comments],[Example],[BusinessOwnerID],[MetricOwnerUserID],[FrequencyID],[CCR] ";
            SQL = SQL + " FROM [COE].[dbo].[tblHelp_Metrics]  ";
            SQL = SQL + " Where MetricID = '" + metricids + "'";

            SqlDataReader queryResults = null;
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);
            }
            if (queryResults != null)
            {
                while (queryResults.Read())
                {
                    string appidtemp = queryResults.GetInt32(queryResults.GetOrdinal("MetricID")).ToString();
                    if (appidtemp != null)
                    {
                        metricid.Text = appidtemp;
                    }
                    string appnametemp = queryResults.GetString(queryResults.GetOrdinal("MetricName"));
                    if (appnametemp != null)
                    {
                        metricname.Text = appnametemp;
                    }
                    string keywordtemp = queryResults.GetString(queryResults.GetOrdinal("DataSource"));
                    if (keywordtemp != null)
                    {
                        datas.Text = keywordtemp;
                    }
                    string desctemp = queryResults.GetString(queryResults.GetOrdinal("Definition"));
                    if (desctemp != null)
                    {
                        desc.Text = desctemp;
                    }
                    string metadatatemp = queryResults.GetString(queryResults.GetOrdinal("Objective"));
                    if (metadatatemp != null)
                    {
                        objective.Text = metadatatemp;
                    }
                    frequency.SelectedValue = queryResults.GetInt32(queryResults.GetOrdinal("FrequencyID")).ToString();
                    category.SelectedValue = queryResults.GetInt32(queryResults.GetOrdinal("MetricCategory")).ToString();
                    businessid.Text = queryResults.GetString(queryResults.GetOrdinal("BusinessOwnerID")).ToString();
                    ownerid.Text = queryResults.GetString(queryResults.GetOrdinal("MetricOwnerUserID")).ToString();
                    indave.Text = queryResults.GetString(queryResults.GetOrdinal("IndustryAvg")).ToString();
                    worldc.Text = queryResults.GetString(queryResults.GetOrdinal("WorldClass")).ToString();
                    ccr.Text = queryResults.GetString(queryResults.GetOrdinal("CCR")).ToString();
                    metadatas.Text = queryResults.GetString(queryResults.GetOrdinal("Metadata")).ToString();
                    comment.Text = queryResults.GetString(queryResults.GetOrdinal("Comments")).ToString();
                    example.Text = queryResults.GetString(queryResults.GetOrdinal("Example")).ToString();
                }
                queryResults.Close();
            }

            if (db.CloseConnection())
            {
                return;
            }
        }

        private void catedd()
        {
            ListItem item = null;
            category.Items.Clear();
            category.Items.Add(new ListItem("Select a Category", "-1"));

            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";
            SQL = "SELECT CategoryID, CategoryName";
            SQL = SQL + " FROM COE.dbo.tblHelp_Metrics_Category ";

            SqlDataReader queryResults = null;
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);
            }
            if (queryResults != null)
            {
                while (queryResults.Read())
                {
                    String text = queryResults.GetString(queryResults.GetOrdinal("CategoryName"));
                    String value = queryResults.GetInt32(queryResults.GetOrdinal("CategoryID")).ToString();
                    item = new ListItem(text, value);
                    category.Items.Add(item);
                }
                queryResults.Close();
            }

            if (db.CloseConnection())
            {
                return;
            }
        }

        private void freqdd()
        {
            ListItem item = null;
            frequency.Items.Clear();
            frequency.Items.Add(new ListItem("Select a Frequency", "-1"));

            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";
            SQL = "SELECT FrequencyID, FrequencyName";
            SQL = SQL + " FROM COE.dbo.tblHelp_Frequency ";

            SqlDataReader queryResults = null;
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);
            }
            if (queryResults != null)
            {
                while (queryResults.Read())
                {
                    String text = queryResults.GetString(queryResults.GetOrdinal("FrequencyName"));
                    String value = queryResults.GetInt32(queryResults.GetOrdinal("FrequencyID")).ToString();
                    item = new ListItem(text, value);
                    frequency.Items.Add(item);
                }
                queryResults.Close();
            }

            if (db.CloseConnection())
            {
                return;
            }
        }

        

        private void metriclb2()
        {
            ListItem item = null;
            allmetric2.Items.Clear();

            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);
            String s = "";
            if (this.Request.QueryString.Count > 0)
            {
                s = Request.QueryString["MetricID"];

            }
            String SQL = "";
            SQL = "SELECT MetricID, MetricName";
            SQL = SQL + " FROM COE.dbo.tblHelp_Metrics ";
            SQL = SQL + " where MetricID not in (select b.MetricID ";
            SQL = SQL + " from coe.dbo.tblHelp_Metrics_Related a ";
            SQL = SQL + " inner join COE.dbo.tblHelp_Metrics b ";
            SQL = SQL + " on a.RelatedMetricID = b.MetricID or a.MetricID = b.MetricID ";
            SQL = SQL + " where (a.MetricID = '" + s + "' or a.RelatedMetricID = '" + s + "'))";
            SQL = SQL + " order by MetricName ";


            SqlDataReader queryResults = null;
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);
            }
            if (queryResults != null)
            {
                while (queryResults.Read())
                {
                    String text = queryResults.GetString(queryResults.GetOrdinal("MetricName"));
                    String value = queryResults.GetInt32(queryResults.GetOrdinal("MetricID")).ToString();
                    item = new ListItem(text, value);
                    allmetric2.Items.Add(item);
                }
                queryResults.Close();
            }

            if (db.CloseConnection())
            {
                return;
            }
        }

        private void metriclb()
        {
            ListItem item = null;
            allmetric.Items.Clear();

            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);
            String s = "";
            if (this.Request.QueryString.Count > 0)
            {
                s = Request.QueryString["MetricID"];

            }
            String SQL = "";
            SQL = "SELECT AppID, AppName";
            SQL = SQL + " FROM COE.dbo.tblApplication ";
            SQL = SQL + " where AppID not in (SELECT b.AppID ";
            SQL = SQL + " from coe.dbo.tblHelp_Application_Metrics a ";
            SQL = SQL + " inner join COE.dbo.tblApplication b ";
            SQL = SQL + " on a.ApplicationID = b.AppID ";
            SQL = SQL + " where a.MetricID = '" + s + "')";
            SQL = SQL + " order by AppName ";


            SqlDataReader queryResults = null;
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);
            }
            if (queryResults != null)
            {
                while (queryResults.Read())
                {
                    String text = queryResults.GetString(queryResults.GetOrdinal("AppName"));
                    String value = queryResults.GetInt32(queryResults.GetOrdinal("AppID")).ToString();
                    item = new ListItem(text, value);
                    allmetric.Items.Add(item);
                }
                queryResults.Close();
            }

            if (db.CloseConnection())
            {
                return;
            }
        }



        protected void removebutton_Click(object sender, EventArgs e)
        {
            deleterelated();
            metriclb();
            remetriclb(Request.QueryString["MetricID"]);
        }

        protected void removebutton_Click2(object sender, EventArgs e)
        {
            deleterelated2();
            metriclb2();
            remetriclb2(Request.QueryString["MetricID"]);
        }

        protected void addbutton_Click(object sender, EventArgs e)
        {
            addrelated();
            metriclb();
            remetriclb(Request.QueryString["MetricID"]);
        }

        protected void addbutton_Click2(object sender, EventArgs e)
        {
            addrelated2();
            metriclb2();
            remetriclb2(Request.QueryString["MetricID"]);
        }

        protected void nextb_Click(object sender, EventArgs e)
        {
            createinfo();
            String metricID = findmetricid();
            createinfo2(metricID);
            String ps = "MetricAdmin.aspx?MetricID=" + metricID;
            Page.Response.Redirect(ps);
        }

        private String findmetricid()
        {

            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";
            SQL = " select top 1 MetricID ";
            SQL = SQL + " from coe.dbo.tblHelp_Metrics ";
            SQL = SQL + " order by mytimestamp desc ";

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
                    value = queryResults.GetInt32(queryResults.GetOrdinal("MetricID")).ToString();

                }
                queryResults.Close();

            }
            return value;

        }

        private void formulas(string metricids)
        {

            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";
            SQL = "select Name, HelpText ";
            SQL = SQL + " from coe.dbo.tblHelp_Metrics_Calculation ";
            SQL = SQL + " where MetricID = '" + metricids + "'";
            string name = "";
            SqlDataReader queryResults = null;
            
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);
            }
            if (queryResults != null)
            {
                while (queryResults.Read())
                {
                    name = queryResults.GetString(queryResults.GetOrdinal("Name")).ToString();
                    formula.ImageUrl = "../Data/" + name;
                    formulahelp.Text = queryResults.GetString(queryResults.GetOrdinal("HelpText")).ToString();
                }
                queryResults.Close();

            }
            

        }


        protected void submitb_Click(object sender, EventArgs e)
        {
            Updateinfo();
            String metricID = Request.QueryString["MetricID"];
            String ps = "MetricAdmin.aspx?MetricID=" + metricID + "&saved=yes";
            Page.Response.Redirect(ps);
        }

        private void Updateinfo()
        {
            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";

            SQL += " update coe.dbo.tblHelp_Metrics ";
            SQL = SQL + " set MetricName = '" + metricname.Text + "', Definition = '"
                + desc.Text + "', MetaData = '"
                + metadatas.Text + "', MetricCategory = '"
                + category.SelectedValue + "', Objective = '"
                + objective.Text + "', DataSource = '"
                + datas.Text + "', IndustryAvg = '"
                + indave.Text + "', WorldClass = '"
                + worldc.Text + "', CCR = '"
                + ccr.Text + "', Comments = '"
                + comment.Text + "', Example = '"
                + example.Text + "', BusinessOwnerID = '"
                + businessid.Text + "', FrequencyID = '"
                + frequency.SelectedValue + "', MetricOwnerUserID = '"
                + ownerid.Text + "' ";
            SQL = SQL + " where MetricID = '" + Request.QueryString["MetricID"] + "'";

            SQL += " update coe.dbo.tblHelp_Metrics_Calculation ";
            SQL = SQL + " set HelpText = '" + formulahelp.Text + "' ";
            SQL = SQL + " where MetricID = '" + Request.QueryString["MetricID"] + "'";

            SqlDataReader queryResults = null;
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);

            }
            queryResults.Close();
            if (db.CloseConnection())
            {
                return;
            }
        }

        protected void cancelb_Click(object sender, EventArgs e)
        {
            String ps = "MetricAdmin.aspx";
            Page.Response.Redirect(ps);
        }


        protected void Submit1_Click(object sender, EventArgs e)
        {
            if ((File1.PostedFile != null) && (File1.PostedFile.ContentLength > 0))
            {
                String fn = System.IO.Path.GetFileName(File1.PostedFile.FileName);
                String SaveLocation = Request.PhysicalApplicationPath + "Data\\" + fn;
                try
                {
                    File1.PostedFile.SaveAs(SaveLocation);
                    addfile(fn);
                    uploadmsg.Text = "The file has been uploaded.";
                    String metricID = Request.QueryString["MetricID"];
                    String ps = "MetricAdmin.aspx?MetricID=" + metricID;
                    Page.Response.Redirect(ps);

                }
                catch (Exception ex)
                {
                    uploadmsg.Text = "Error: " + ex.Message;
                    //Note: Exception.Message returns a detailed message that describes the current exception. 
                    //For security reasons, we do not recommend that you return Exception.Message to end users in 
                    //production environments. It would be better to put a generic error message. 
                }
            }
            else
            {
                uploadmsg.Text = "Please select a file to upload.";
            }
            uploadmsg.Visible = true;

            
        }

        private void createinfo2(string metricid)
        {
            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";

            SQL += " Insert into coe.dbo.tblHelp_Metrics_Calculation (MetricID,Name,HelpText) ";
            SQL = SQL + " Values('" + metricid + "', 'NotAvailable.png', ' ')";


            SqlDataReader queryResults = null;
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);

            }
            queryResults.Close();
            if (db.CloseConnection())
            {
                return;
            }
        }

        private void createinfo()
        {
            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";

            SQL += " Insert into coe.dbo.tblHelp_Metrics (ProcessID,Metricname, MetaData ,MetricCategory,Definition, Objective,DataSource,IndustryAvg, WorldClass,CCR, Comments , Example, BusinessOwnerID, MetricOwnerUserID, FrequencyID) ";
            SQL = SQL + " Values('-1','" + metricname.Text + "', '" + metadatas.Text + "', '" + category.SelectedValue + "', '" + desc.Text + "', '" + objective.Text + "', '" + datas.Text + "', '" + indave.Text + "', '" + worldc.Text + "', '" + ccr.Text + "', '" + comment.Text + "', '" + example.Text + "', '" + businessid.Text + "', '" + ownerid.Text + "', '" + frequency.SelectedValue + "')";


            SqlDataReader queryResults = null;
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);

            }
            queryResults.Close();
            if (db.CloseConnection())
            {
                return;
            }
        }



        protected void removefilebutton_Click(object sender, EventArgs e)
        {
            deletefile();
            String metricID = Request.QueryString["MetricID"];
            String ps = "MetricAdmin.aspx?MetricID=" + metricID;
            Page.Response.Redirect(ps);
        }

        protected void HtmlEditorExtender1_ImageUploadComplete(object sender, AjaxFileUploadEventArgs e)
        {
            string fullpath = Request.PhysicalApplicationPath + "Data\\" + e.FileName;
            var ajaxFileUpload = (AjaxFileUpload)sender;
            HtmlEditorExtender1.AjaxFileUpload.SaveAs(fullpath);
            e.PostedUrl = Page.ResolveUrl("~/Data/" + e.FileName);
        }

    }
}