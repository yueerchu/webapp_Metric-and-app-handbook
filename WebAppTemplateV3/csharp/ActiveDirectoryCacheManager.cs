using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace DevTemplateV3.WebApp.Common
{
    public class ActiveDirectoryCacheManager
    {

        private ActiveDirectoryCacheManager()
        {

        }


        public static ActiveDirectoryUser FindUserBasedOnID(String ID)
        {
            ActiveDirectoryUser result = null;

            //Search Local Database
            result = FindCachedUserBasedOnID(ID);

            //If Not Found Search Active Directory
            if (result == null)
            {
                result = ActiveDirectorySearcher.FindUserBasedOnID(ID);
                AddUserToCache(result);
            }

            return result;
        }


        private static ActiveDirectoryUser FindCachedUserBasedOnID(String ID)
        {
            ActiveDirectoryUser result = null;
            DatabaseConnection db = null;
            String SQL = "";
            SqlDataReader records = null;

            db = new DatabaseConnection(DatabaseConnection.Connections.ETCCOPSS102_COE);

            SQL = "EXEC uspCOE_Web @ReportType='ActiveDirectory_Cache_Find_User_On_ID', @Optional1='" + ID + "'";

            db.OpenConnection();

            if (db.IsConnected == false)
            {
                return null;
            }

            records = db.ExecuteQuerySQL(SQL);

            if (records != null)
            {
                if (records.Read())
                {
                    result = new ActiveDirectoryUser();

                    result.UserID = records["UserID"].ToString();
                    result.Email = records["Email"].ToString();
                    result.DisplayName = records["DisplayName"].ToString();
                    result.FirstName = records["FirstName"].ToString();
                    result.LastName = records["LastName"].ToString();
                }

                records.Close();
            }

            db.CloseConnection();

            return result;
        }


        public static void AddUserToCache(ActiveDirectoryUser user)
        {
            DatabaseConnection db = null;
            String SQL = "";

            if (user == null)
            {
                return;
            }

            if (ActiveDirectoryCacheManager.IsUserInCache(user.UserID))
            {
                return;
            }

            db = new DatabaseConnection(DatabaseConnection.Connections.ETCCOPSS102_COE);

            SQL = "EXEC uspCOE_Web @ReportType='ActiveDirectory_Cache_Add_User', ";
            SQL = SQL + "@Optional1='" + user.UserID + "', ";
            SQL = SQL + "@Optional2='" + DatabaseConnection.MakeQuerySafeString(DatabaseConnection.MakeQuerySafeString(user.FirstName.Trim())) + "', ";
            SQL = SQL + "@Optional3='" + DatabaseConnection.MakeQuerySafeString(DatabaseConnection.MakeQuerySafeString(user.LastName.Trim())) + "', ";
            SQL = SQL + "@Optional4='" + DatabaseConnection.MakeQuerySafeString(DatabaseConnection.MakeQuerySafeString(user.DisplayName.Trim())) + "', ";
            SQL = SQL + "@Optional5='" + DatabaseConnection.MakeQuerySafeString(user.Email) + "'";

            db.OpenConnection();

            if (db.IsConnected == false)
            {
                return;
            }

            db.ExecuteActionSQL(SQL);

            db.CloseConnection();
        }


        public static Boolean IsUserInCache(String ID)
        {
            Boolean result = false;
            ActiveDirectoryUser user = null;

            user = FindCachedUserBasedOnID(ID);

            if (user != null)
            {
                return true;
            }

            return result;
        }


    }
}