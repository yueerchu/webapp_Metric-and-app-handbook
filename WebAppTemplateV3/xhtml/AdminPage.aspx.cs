using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevTemplateV3.WebApp.UI.Controls;
using DevTemplateV3.WebApp.Common;
using System.Data.SqlClient;

namespace WebAppTemplateV3.xhtml
{
    public partial class AdminPage : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            NavigationBarControl nv = (NavigationBarControl)Master.FindControl("navBar");
            nv.SetActiveLinkHome(true);

        }

        protected void TreeView1_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            if (e.Node.ChildNodes.Count == 0)
            {
                switch (e.Node.Depth)
                {
                    case 0:
                        PopulateApps(e.Node);
                        break;
                    case 1:
                        PopulaterelatedMetrics(e.Node);
                        break;
                }
            }
        }

        private void PopulateApps(TreeNode treeNode)
        {
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
                    String appName = queryResults.GetString(queryResults.GetOrdinal("AppName"));
                    String appID = queryResults.GetInt32(queryResults.GetOrdinal("AppID")).ToString();
                    String url = "AppAdmin.aspx" + "?AppID=" + appID;
                    TreeNode NewNode = new TreeNode(appName, appID, null, url, "operation");
                    NewNode.PopulateOnDemand = true;
                    treeNode.ChildNodes.Add(NewNode);
                }

                queryResults.Close();
            }

            if (db.CloseConnection())
            {
                return;
            }
        }

        private void PopulateMetrics(TreeNode treeNode)
        {
            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);

            String SQL = "";
            SQL = "SELECT a.MetricID, a.MetricName";
            SQL = SQL + " FROM COE.dbo.tblHelp_Metrics a ";
            SQL = SQL + " order by a.MetricName ";

            SqlDataReader queryResults = null;

            if (db.OpenConnection())
            {
                queryResults = db.ExecuteQuerySQL(SQL);
            }

            if (queryResults != null)
            {

                while (queryResults.Read())
                {
                    String appName = queryResults.GetString(queryResults.GetOrdinal("MetricName"));
                    String metricID = queryResults.GetInt32(queryResults.GetOrdinal("MetricID")).ToString();
                    String url = "MetricAdmin.aspx" + "?MetricID=" + metricID;
                    TreeNode NewNode = new TreeNode(appName, metricID, null, url, "operation");
                    NewNode.PopulateOnDemand = true;
                    treeNode.ChildNodes.Add(NewNode);
                }

                queryResults.Close();
            }

            if (db.CloseConnection())
            {
                return;
            }
        }

        protected void TreeView2_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            if (e.Node.ChildNodes.Count == 0)
            {
                switch (e.Node.Depth)
                {
                    case 0:
                        PopulateMetrics(e.Node);
                        break;
                }
            }
        }

        private void PopulaterelatedMetrics(TreeNode treeNode)
        {
            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.DEFAULT_CONNECTION);
            String metricSQL = "";
            metricSQL = "select Distinct c.MetricName, c.MetricID ";
            metricSQL = metricSQL + " from COE.dbo.tblHelp_Metrics c, COE.dbo.tblHelp_Application_Metrics b ";
            metricSQL = metricSQL + " where c.MetricID = b.MetricID ";

            if (treeNode.Value != "Metrics")
            {
                metricSQL = metricSQL + " And b.ApplicationID = '" + treeNode.Value + "'";
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
                    String metricName = queryResults.GetString(queryResults.GetOrdinal("MetricName"));
                    String metricID = queryResults.GetInt32(queryResults.GetOrdinal("MetricID")).ToString();
                    String url = "MetricAdmin.aspx" + "?MetricID=" + metricID;
                    TreeNode NewNode = new TreeNode(metricName, metricID, null, url, "operation");
                    NewNode.PopulateOnDemand = false;
                    treeNode.ChildNodes.Add(NewNode);
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