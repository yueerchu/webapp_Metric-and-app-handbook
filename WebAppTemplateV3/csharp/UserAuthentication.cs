using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;

namespace DevTemplateV3.WebApp.Common
{
    public class UserAuthentication
    {

        private UserAuthentication()
        {

        }


        public static String GetAuthenticatedUser(Page p, Boolean rawUserID = false)
        {
            String userID = "";
            String systemPassword = "";
            String correctSystemPassword = "";
            Boolean systemAccountFound = false;
            Boolean systemPasswordFound = false;

            userID = p.User.Identity.Name.Trim();

            //server name
            if (userID.EndsWith("$"))
            {
                systemAccountFound = true;
            }

            //app pool
            if (userID.StartsWith("IIS APPPOOL"))
            {
                systemAccountFound = true;
            }

            //network service
            if (userID.Equals("NT AUTHORITY\\NETWORK SERVICE"))
            {
                systemAccountFound = true;
            }

            //system password
            if (p.Request.QueryString["systemPassword"] != null)
            {
                systemPassword = p.Request.QueryString["systemPassword"].ToString();

                correctSystemPassword = GetSystemPassword();

                if (correctSystemPassword.Equals("") == false)
                {
                    if (systemPassword.Equals(correctSystemPassword))
                    {
                        systemPasswordFound = true;
                    }
                }
            }

            if ((systemAccountFound) || (systemPasswordFound))
            {
                if (p.Request.QueryString["userID"] != null)
                {
                    userID = p.Request.QueryString["userID"].ToString();
                }
            }
            else
            {
                if (rawUserID == false)
                {
                    if (userID.Contains('\\'))
                    {
                        userID = userID.Substring(userID.IndexOf('\\') + 1);
                    }
                }
            }


            return userID;
        }


        private static String GetSystemPassword()
        {
            String pass = "";
            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.Connections.ETCCOPSS102_COE);
            String SQL = "";
            SqlDataReader records = null;

            db.OpenConnection();

            if (db.IsConnected == false)
            {
                return "";
            }

            SQL = "EXEC uspCOE_Web @ReportType='GetImpersonationPassword'";

            records = db.ExecuteQuerySQL(SQL);

            if (records != null)
            {
                if (records.Read())
                {
                    pass = records["UserImpersonationPassword"].ToString();
                }

                records.Close();
            }


            db.CloseConnection();

            return pass;
        }


    }
}