using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.UI;

namespace DevTemplateV3.WebApp.Common
{
    public class AppSettings
    {
        private String broadcastMSG;
        private Boolean enabled;
        private int appID;
        private Boolean appNotFound;
        private String coeImagePath;
        private String appTitle;


        public AppSettings()
        {
            DownloadAppSettings();
            DownloadCOEImagePath();
        }


        private void DownloadAppSettings()
        {
            //DatabaseConnection db = null;
            String SQL = "";
            SqlDataReader records = null;
            DatabaseConnection db = null;


            if (System.Configuration.ConfigurationManager.AppSettings["PMCOE_AppID"] != null)
            {
                string reportIdText = System.Configuration.ConfigurationManager.AppSettings["PMCOE_AppID"];

                try
                {
                    appID = Int32.Parse(reportIdText);
                }
                catch (Exception)
                {
                    broadcastMSG = "Invalid App ID";
                    return;
                }
            }


            db = new DatabaseConnection(DatabaseConnection.Connections.ETCCOPSS102_COE);
            SQL = "EXEC uspCOE_Web @ReportType='GetAppSettings'";
            SQL = SQL + ", @Optional1='" + appID + "'";

            db.OpenConnection();


            records = db.ExecuteQuerySQL(SQL);

            if (!records.HasRows)
            {
                appNotFound = false;
            }
            else
            {
                appNotFound = true;
            }


            if (records != null)
            {
                if (records.Read())
                {
                    broadcastMSG = records["MSG"].ToString();
                    enabled = records.GetBoolean(records.GetOrdinal("Enabled"));
                    appTitle = records["AppName"].ToString();
                }
                records.Close();
            }

            db.CloseConnection();
        }


        private String DownloadCOEImagePath()
        {
            String SQL = "";
            SqlDataReader records = null;
            DatabaseConnection db = null;

            db = new DatabaseConnection(DatabaseConnection.Connections.ETCCOPSS102_COE);
            SQL = "EXEC uspCOE_Web @ReportType='GetCOEImagePath'";

            db.OpenConnection();

            records = db.ExecuteQuerySQL(SQL);

            if (records != null)
            {
                while (records.Read())
                {
                    coeImagePath = records["imgPath"].ToString();
                }
                records.Close();
            }

            db.CloseConnection();

            return coeImagePath;
        }


        public String GetTitle()
        {
            return appTitle;
        }


        public String GetBroadcastMessage()
        {
            return broadcastMSG;
        }


        public Boolean IsEnabled()
        {
            return enabled; 
        }


        public int GetReportID()
        {
            return appID;
        }


        public Boolean IsAppFound()
        {
            return appNotFound;
        }


        public String GetCOEImagePath()
        {
            return coeImagePath;
        }

    }
}