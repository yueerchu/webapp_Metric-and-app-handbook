using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevTemplateV3.WebApp.Common;
using System.Data.SqlClient;
using System.Diagnostics;

namespace WebAppTemplateV3.xhtml
{
    public partial class RemoveItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            msg.Visible = false;
            if (this.Request.QueryString.Count > 0)
            {
                if (!IsPostBack)
                {
                    String s = Request.QueryString["page"];
                    if (s.Equals("apps"))
                    {
                        listboxtitle.Text = "Application List";
                        applb();

                    }
                    else
                    {
                        listboxtitle.Text = "Metric List";
                        metriclb();
                    }
                }
            }
        }

        private void metriclb()
        {
            ListItem item = null;
            content.Items.Clear();

            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";
            SQL = "SELECT MetricID, MetricName";
            SQL = SQL + " FROM COE.dbo.tblHelp_Metrics ";

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
                    content.Items.Add(item);
                }
                queryResults.Close();
            }

            if (db.CloseConnection())
            {
                return;
            }
        }

        private void applb()
        {
            ListItem item = null;
            content.Items.Clear();

            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";
            SQL = "SELECT a.AppName, a.AppID";
            SQL = SQL + " FROM COE.dbo.tblApplication a ";
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
                    String text = queryResults.GetString(queryResults.GetOrdinal("AppName"));
                    String value = queryResults.GetInt32(queryResults.GetOrdinal("AppID")).ToString();
                    item = new ListItem(text, value);
                    content.Items.Add(item);
                }
                queryResults.Close();
            }

            if (db.CloseConnection())
            {
                return;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String d = Request.QueryString["page"];
            if (d.Equals("metrics"))
            {
                deletemetric();
                metriclb();
            }
            else
            {
                deleteapp();
                applb();
            }
        }

        private void deleteapp()
        {
            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";
            String temp = content.SelectedValue;
            String temp2 = content.SelectedItem.Text;
            
            SQL += " Delete from coe.dbo.tblHelp_Application_Metrics ";
            SQL = SQL + " where ApplicationID = '" + temp + "'";
            SQL += " Delete from coe.dbo.tblHelp_Application ";
            SQL = SQL + " where AppID = '" + temp + "'";
            SQL += " Delete from coe.dbo.tblApplication ";
            SQL = SQL + " where AppID = '" + temp + "'";

            SqlDataReader queryResults = null;
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);

                msg.Text = temp2 + " has been removed.";
                msg.Visible = true;
            }
            queryResults.Close();
            if (db.CloseConnection())
            {
                return;
            }
        }


        private void deletemetric()
        {            
            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";
            SQL += "Delete from coe.dbo.tblHelp_Metrics_Related";
            String temp = content.SelectedValue;
            String temp2 = content.SelectedItem.Text;
            SQL = SQL + " where MetricID = '" + temp + "' OR RelatedMetricID = '" + temp + "'";
            SQL += " Delete from coe.dbo.tblHelp_Metrics_Calculation ";
            SQL = SQL + " where MetricID = '" + temp + "'";
            SQL += " Delete from coe.dbo.tblHelp_Application_Metrics ";
            SQL = SQL + " where MetricID = '" + temp + "'";
            SQL += " Delete from coe.dbo.tblHelp_Metrics ";
            SQL = SQL + " where MetricID = '" + temp + "'";

            SqlDataReader queryResults = null;
            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);

                msg.Text = temp2 + " has been removed.";
                msg.Visible = true;
            }
                queryResults.Close();
            if (db.CloseConnection())
            {
                return;
            }
        }
    }
}