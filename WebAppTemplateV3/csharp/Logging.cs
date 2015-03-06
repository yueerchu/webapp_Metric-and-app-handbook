using System;
using System.Collections.Generic;
using System.Web.UI;

namespace DevTemplateV3.WebApp.Common
{
    public class Logging
    {
        private Page page;


        public Logging(Page p)
        {
            page = p;
        }


        public Boolean AddUsageLogEntry()
        {
            String remoteName = GetHostName();
            Int32 appID = GetAppID();
            String url = GetURL();
            String user = UserAuthentication.GetAuthenticatedUser(page);

            DatabaseConnection db = null;
            String SQL = "";
            Boolean result = false;

            //Record Log
            db = new DatabaseConnection(DatabaseConnection.Connections.ETCCOPSS102_COE);
            SQL = "EXEC uspCOE_System_Log_Web_Usage @AppID=" + appID + ", @UserID='" + user + "', @Source='" + remoteName + "', @URL='" + url + "'";

            db.OpenConnection();

            if (db.IsConnected == false)
            {
                return false;
            }

            result = db.ExecuteActionSQL(SQL);

            db.CloseConnection();

            return result;
        }


        private String GetURL()
        {
            if (page == null)
            {
                return "";
            }

            if (page.Request == null)
            {
                return "";
            }

            return page.Request.Url.AbsoluteUri;
        }


        private String GetHostName()
        {
            String remoteIP = "";
            String remoteName = "";

            if (page == null)
            {
                return "";
            }

            if (page.Request == null)
            {
                return "";
            }

            if (page.Request.ServerVariables["remote_addr"] != null)
            {
                remoteIP = page.Request.ServerVariables["remote_addr"];
            }

            remoteName = remoteIP;

            return remoteName;
        }


        private Int32 GetAppID()
        {
            Int32 ReportID = -1;
            String ReportIDRaw = "";

            if (System.Configuration.ConfigurationManager.AppSettings["PMCOE_AppID"] != null)
            {
                ReportIDRaw = System.Configuration.ConfigurationManager.AppSettings["PMCOE_AppID"];

                try
                {
                    ReportID = Int32.Parse(ReportIDRaw);
                }
                catch (Exception)
                {
                    ReportID = -1;
                }
            }

            return ReportID;
        }


    }
}
