using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevTemplateV3.WebApp.Common;
using System.Data.SqlClient;

namespace WebAppTemplateV3.xhtml
{
    public partial class AppAdmin : System.Web.UI.Page 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            uploadmsg.Visible = false;
            if (this.Request.QueryString.Count > 1)
            {
                
                thankyou.Visible = true;
                mainpanel.Visible = false;
                String appID = Request.QueryString["AppID"];
                String ps = "AppAdmin.aspx?AppID=" + appID;
                goback.NavigateUrl = ps;
                newview.NavigateUrl = "AppView.aspx" + "?AppID=" + appID + "&back=yes";
            }
            else if (this.Request.QueryString.Count > 0)
            {
                mainpanel.Visible = true;
                thankyou.Visible = false;
                nextb.Visible = false;
                String s = Request.QueryString["AppID"];
                if (!IsPostBack)
                {
                    pillardd();
                    processdd();
                    classdd();
                    freqdd();
                    audiendd();
                    formatdd();
                    metriclb();
                    filelb();
                    remetriclb(s);
                    autofill(s);
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
                    pillardd();
                    processdd();
                    classdd();
                    freqdd();
                    audiendd();
                    formatdd();
                    metriclb();
                    filelb();
                }
            }
        }

        private void deleterelated()
        {
            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";

            SQL += " Delete from coe.dbo.tblHelp_Application_Metrics ";
            SQL = SQL + " where MetricID = '" + related.SelectedValue + "' AND ApplicationID = '" + appid.Text + "'";

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

            SQL += " Delete from coe.dbo.tblHelp_File ";
            SQL = SQL + " where FileID = '" + files.SelectedValue + "'";

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

        private void addfile(String fn, String fl)
        {
            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";

            SQL += " Insert into coe.dbo.tblHelp_File (FileName, FileURL, ApplicationID) ";
            SQL = SQL + " Values('" + fn + "', '" + fl + "', '" + appid.Text + "')";

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
            SQL = SQL + " Values(" + appid.Text + "," + allmetric.SelectedValue + ")";

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

        private void remetriclb(string appids)
        {
            ListItem item = null;
            related.Items.Clear();

            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";
            SQL = "SELECT b.MetricID, b.MetricName";
            SQL = SQL + " from coe.dbo.tblHelp_Application_Metrics a ";
            SQL = SQL + " inner join COE.dbo.tblHelp_Metrics b ";
            SQL = SQL + " on a.MetricID = b.MetricID ";
            SQL = SQL + " where a.ApplicationID = '" + appids + "'";
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
                    related.Items.Add(item);
                }
                queryResults.Close();
            }

            if (db.CloseConnection())
            {
                return;
            }
        }

        private void autofill(string appids)
        {
            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";
            SQL = "SELECT a.AppName, a.MetaData, a.AppID, a.AppDesc, a.ImageURL, a.OwnerUserID, a.PillarID, a.SupportOwnerID, a.URL, b.AudienceID, b.BusinessOwnerID, b.ClassificationID, b.FormatID, b.FrequencyID, b.KeyWord, b.ProcessID ";
            SQL = SQL + " FROM COE.dbo.tblApplication a  ";
            SQL = SQL + " Left JOIN COE.dbo.tblHelp_Application b  ";
            SQL = SQL + " ON a.AppID = b.AppID   ";
            SQL = SQL + " Where a.AppID = '" + appids + "'";

            SqlDataReader queryResults = null;
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);
            }
            if (queryResults != null)
            {
                while (queryResults.Read())
                {
                    string appidtemp = queryResults.GetInt32(queryResults.GetOrdinal("AppID")).ToString();
                    if (appidtemp != null)
                    {
                        appid.Text = appidtemp;
                    }
                    string appnametemp = queryResults.GetString(queryResults.GetOrdinal("AppName"));
                    if (appnametemp != null)
                    {
                        appname.Text = appnametemp;
                    }
                    string keywordtemp = queryResults.GetString(queryResults.GetOrdinal("KeyWord"));
                    if (keywordtemp != null)
                    {
                        keyword.Text = keywordtemp;
                    }
                    string desctemp = queryResults.GetString(queryResults.GetOrdinal("AppDesc"));
                    if (desctemp != null)
                    {
                        desc.Text = desctemp;
                    }
                    string metadatatemp = queryResults.GetString(queryResults.GetOrdinal("MetaData"));
                    if (metadatatemp != null)
                    {
                        metadata.Text = metadatatemp;
                    }
                    string pillartemp = queryResults.GetInt32(queryResults.GetOrdinal("PillarID")).ToString();
                    if (pillartemp != null)
                    {
                        pillar.SelectedValue = pillartemp;
                    }
                    process.SelectedValue = queryResults.GetInt32(queryResults.GetOrdinal("ProcessID")).ToString();
                    classification.SelectedValue = queryResults.GetInt32(queryResults.GetOrdinal("ClassificationID")).ToString();
                    frequency.SelectedValue = queryResults.GetInt32(queryResults.GetOrdinal("FrequencyID")).ToString();
                    audience.SelectedValue = queryResults.GetInt32(queryResults.GetOrdinal("AudienceID")).ToString();
                    format.SelectedValue = queryResults.GetInt32(queryResults.GetOrdinal("FormatID")).ToString();
                    businessid.Text = queryResults.GetString(queryResults.GetOrdinal("BusinessOwnerID")).ToString();
                    ownerid.Text = queryResults.GetString(queryResults.GetOrdinal("OwnerUserID")).ToString();
                    supportid.Text = queryResults.GetString(queryResults.GetOrdinal("SupportOwnerID")).ToString();
                    appurl.Text = queryResults.GetString(queryResults.GetOrdinal("URL")).ToString();
                    imageurl.Text = queryResults.GetString(queryResults.GetOrdinal("ImageURL")).ToString();
                }
                queryResults.Close();
            }

            if (db.CloseConnection())
            {
                return;
            }
        }

        private void formatdd()
        {
            ListItem item = null;
            format.Items.Clear();
            format.Items.Add(new ListItem("Select a Format", "-1"));

            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";
            SQL = "SELECT FormatID, FormatName";
            SQL = SQL + " FROM COE.dbo.tblHelp_Format ";

            SqlDataReader queryResults = null;
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);
            }
            if (queryResults != null)
            {
                while (queryResults.Read())
                {
                    String text = queryResults.GetString(queryResults.GetOrdinal("FormatName"));
                    String value = queryResults.GetInt32(queryResults.GetOrdinal("FormatID")).ToString();
                    item = new ListItem(text, value);
                    format.Items.Add(item);
                }
                queryResults.Close();
            }

            if (db.CloseConnection())
            {
                return;
            }
        }

        private void audiendd()
        {
            ListItem item = null;
            audience.Items.Clear();
            audience.Items.Add(new ListItem("Select an Audience", "-1"));

            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";
            SQL = "SELECT AudienceID, AudienceName";
            SQL = SQL + " FROM COE.dbo.tblHelp_Audience ";

            SqlDataReader queryResults = null;
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);
            }
            if (queryResults != null)
            {
                while (queryResults.Read())
                {
                    String text = queryResults.GetString(queryResults.GetOrdinal("AudienceName"));
                    String value = queryResults.GetInt32(queryResults.GetOrdinal("AudienceID")).ToString();
                    item = new ListItem(text, value);
                    audience.Items.Add(item);
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

        private void classdd()
        {
            ListItem item = null;
            classification.Items.Clear();
            classification.Items.Add(new ListItem("Select a Classification", "-1"));

            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";
            SQL = "SELECT ClassificationID, ClassificationName";
            SQL = SQL + " FROM COE.dbo.tblHelp_Classification ";

            SqlDataReader queryResults = null;
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);
            }
            if (queryResults != null)
            {
                while (queryResults.Read())
                {
                    String text = queryResults.GetString(queryResults.GetOrdinal("ClassificationName"));
                    String value = queryResults.GetInt32(queryResults.GetOrdinal("ClassificationID")).ToString();
                    item = new ListItem(text, value);
                    classification.Items.Add(item);
                }
                queryResults.Close();
            }

            if (db.CloseConnection())
            {
                return;
            }
        }

        private void processdd()
        {
            ListItem item = null;
            process.Items.Clear();
            process.Items.Add(new ListItem("Select a Process", "-1"));

            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";
            SQL = "SELECT ProcessID, ProcessName";
            SQL = SQL + " FROM COE.dbo.tblHelp_Processes ";

            SqlDataReader queryResults = null;
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);
            }
            if (queryResults != null)
            {
                while (queryResults.Read())
                {
                    String text = queryResults.GetString(queryResults.GetOrdinal("ProcessName"));
                    String value = queryResults.GetInt32(queryResults.GetOrdinal("ProcessID")).ToString();
                    item = new ListItem(text, value);
                    process.Items.Add(item);
                }
                queryResults.Close();
            }

            if (db.CloseConnection())
            {
                return;
            }
        }

        private void filelb()
        {
            ListItem item = null;
            files.Items.Clear();

            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);
            String s = "";
            if (this.Request.QueryString.Count > 0)
            {
                s = Request.QueryString["AppID"];

            }
            String SQL = "";
            SQL = "SELECT FileID, FileName";
            SQL = SQL + " FROM COE.dbo.tblHelp_File ";
            SQL = SQL + " where ApplicationID = '" + s + "'";


            SqlDataReader queryResults = null;
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);
            }
            if (queryResults != null)
            {
                while (queryResults.Read())
                {
                    String text = queryResults.GetString(queryResults.GetOrdinal("FileName"));
                    String value = queryResults.GetInt32(queryResults.GetOrdinal("FileID")).ToString();
                    item = new ListItem(text, value);
                    files.Items.Add(item);
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
                s = Request.QueryString["AppID"];

            }
            String SQL = "";
            SQL = "SELECT MetricID, MetricName";
            SQL = SQL + " FROM COE.dbo.tblHelp_Metrics ";
            SQL = SQL + " where MetricID not in (select b.MetricID ";
            SQL = SQL + " from coe.dbo.tblHelp_Application_Metrics a ";
            SQL = SQL + " inner join COE.dbo.tblHelp_Metrics b ";
            SQL = SQL + " on a.MetricID = b.MetricID ";
            SQL = SQL + " where a.ApplicationID = '" + s + "')";
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
                    allmetric.Items.Add(item);
                }
                queryResults.Close();
            }

            if (db.CloseConnection())
            {
                return;
            }
        }

        private void pillardd()
        {
            ListItem item = null;
            pillar.Items.Clear();
            pillar.Items.Add(new ListItem("Select a Pillar", "-1"));

            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";
            SQL = "SELECT PillarID, PillarDesc";
            SQL = SQL + " FROM COE.dbo.tblApplication_Pillar ";

            SqlDataReader queryResults = null;
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);
            }
            if (queryResults != null)
            {
                while (queryResults.Read())
                {
                    String text = queryResults.GetString(queryResults.GetOrdinal("PillarDesc"));
                    String value = queryResults.GetInt32(queryResults.GetOrdinal("PillarID")).ToString();
                    item = new ListItem(text, value);
                    pillar.Items.Add(item);
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
            remetriclb(Request.QueryString["AppID"]);
        }

        protected void addbutton_Click(object sender, EventArgs e)
        {
            addrelated();
            metriclb();
            remetriclb(Request.QueryString["AppID"]);
        }

        protected void nextb_Click(object sender, EventArgs e)
        {
            createinfo();
            String appID = findappid();
            createinfo2(appID);
            String ps = "AppAdmin.aspx?AppID=" + appID;
            Page.Response.Redirect(ps);
        }

        private String findappid()
        {

            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";
            SQL = "select top 1 appid ";
            SQL = SQL + " from coe.dbo.tblApplication ";
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
                    value = queryResults.GetInt32(queryResults.GetOrdinal("appid")).ToString();
                    
                }
                queryResults.Close();
                
            }
            return value;

        }


        protected void submitb_Click(object sender, EventArgs e)
        {
            Updateinfo();
            String appID = Request.QueryString["AppID"];
            String ps = "AppAdmin.aspx?AppID=" + appID + "&saved=yes";
            Page.Response.Redirect(ps);
        }

        private void Updateinfo()
        {
            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";

            SQL += " update coe.dbo.tblApplication ";
            SQL = SQL + " set AppName = '" + appname.Text + "', AppDesc = '"
                + desc.Text + "', MetaData = '" + metadata.Text + "', OwnerUserID = '"
                + ownerid.Text + "', SupportOwnerID = '" + supportid.Text + "', URL = '" + appurl.Text
                + "', ImageURL = '" + imageurl.Text + "', PillarID = '" + pillar.SelectedValue + "' ";
            SQL = SQL + " where AppID = '" + Request.QueryString["AppID"] + "'";

            SQL += " update coe.dbo.tblHelp_Application ";
            SQL = SQL + " set KeyWord = '" + keyword.Text + "', ProcessID = '"
                + process.SelectedValue + "', BusinessOwnerID = '" + businessid.Text + "', FormatID = '"
                + format.SelectedValue + "', FrequencyID = '" + frequency.SelectedValue + "', ClassificationID = '" + classification.SelectedValue
                + "', AudienceID = '" + audience.SelectedValue + "' ";
            SQL = SQL + " where AppID = '" + Request.QueryString["AppID"] + "'";
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
            String ps = "AppAdmin.aspx";
            Page.Response.Redirect(ps);
        }


        protected void Submit1_Click(object sender, EventArgs e)
        {
            if ((File1.PostedFile != null) && (File1.PostedFile.ContentLength > 0))
            {
                String fn = System.IO.Path.GetFileName(File1.PostedFile.FileName);
                String SaveLocation = Request.PhysicalApplicationPath + "Data\\"  + fn;
                try
                {
                    File1.PostedFile.SaveAs(SaveLocation);
                    addfile(fn, SaveLocation);
                    uploadmsg.Text = "The file has been uploaded.";
                    filelb();
                    
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


        private void createinfo()
        {
            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";

            SQL += " Insert into coe.dbo.tblApplication (AppName, AppDesc,MetaData,OwnerUserID,SupportOwnerID,URL, ImageURL, BroadcastMessage,Enabled,IsInDev, PillarID, ContentDiv,IsLegacyApp, IsExternalApp , AppGroupID, DefaultPageOrientationID, ShowHeaderInCachePDF) ";
            SQL = SQL + " Values('" + appname.Text + "', '" + desc.Text + "', '" + metadata.Text + "', '" + ownerid.Text + "', '" + supportid.Text + "', '" + appurl.Text + "', '" + imageurl.Text + "', '" + "" + "', '" + "" + "', '" + "" + "', '" + pillar.SelectedValue + "', '" + "" + "', '" + "" + "', '" + "" + "', '" + "" + "', '" + "" + "', '" + "" + "')";

            
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


        private void createinfo2(String applicationid)
        {
            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";

            SQL += " insert into coe.dbo.tblHelp_Application(AppID,AudienceID,BusinessOwnerID,ClassificationID,FormatID,FrequencyID,IsCONA,KeyWord,ProcessID) ";
            SQL = SQL + " Values('" + applicationid + "', '" + audience.SelectedValue + "', '" + businessid.Text + "', '" + classification.SelectedValue + "', '" + format.SelectedValue + "', '" + frequency.SelectedValue + "', '" + "" + "', '" + keyword.Text + "', '" + process.SelectedValue + "')";


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
            filelb();
        }

    }
}