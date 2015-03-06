using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DevTemplateV3.WebApp.Common
{
    public class DatabaseConnection
    {
        public static String DEFAULT_STORED_PROCEDURE = "";                                 //Default Stored Procedure Name
        public static Connections DEFAULT_CONNECTION = Connections.ETCBQPSS201_COE;         //Default Connection

        public enum Connections {   ETCSDSQL325_CCE_SC_OPEXModel, 
                                    ETCSPSQL140_SCORECARDS,
                                    ETCSPSQL140_CCE_SC_OPEXModel,
                                    ETCBPSQL149_MASTER_DATA, 
                                    ETCBPSQL149_PRODUCTIVITY,
                                    ETCBPSQL149_CUSTOMER_SERVICE,
                                    ETCBPSQL149_FINANCIALS,
                                    ETCBPSQL149_PI,
                                    ETCBQPSS201_COE, 
                                    ETCCOPSS102_COE
        };  //Connections (names must line up to config.web file)

        private Connections connectionStringName;
        private SqlConnection connection;


        public DatabaseConnection(Connections c)
        {
            connectionStringName = c;

            String stringName = connectionStringName.ToString();

            connection = new SqlConnection(ConfigurationManager.ConnectionStrings[stringName].ToString());
        }


        public Boolean OpenConnection()
        {
            //make sure we have a connection to open
            if (connection == null)
            {
                return false;
            }

            //open the connection
            try
            {
                connection.Open();
            }
            catch (Exception)
            {
                return false;
            }

            return IsConnected;
        }


        public Boolean CloseConnection()
        {
            //make sure we have a connection to close
            if (connection == null)
            {
                return true;
            }

            connection.Close();

            //return negative of IsConnected
            return !IsConnected;
        }


        public SqlDataReader ExecuteQuerySQL(String sql)
        {
            SqlDataReader records = null;
            SqlCommand cmd = null;

            //make sure we have a valid connection
            if (connection == null)
            {
                return null;
            }

            //make sure connection is open
            if (IsConnected == false)
            {
                return null;
            }

            //create new command for SQL
            cmd = new SqlCommand(sql, connection);

            //create recordset from SQL
            records = cmd.ExecuteReader();

            return records;
        }


        public Boolean ExecuteCommand(SqlCommand cmd)
        {
            //make sure we have a valid connection
            if (connection == null)
            {
                return false;
            }

            //make sure connection is open
            if (IsConnected == false)
            {
                return false;
            }

            //execute SQL
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            return true;
        }


        public Boolean ExecuteActionSQL(String sql)
        {
            SqlCommand cmd = null;

            //make sure we have a valid connection
            if (connection == null)
            {
                return false;
            }

            //make sure connection is open
            if (IsConnected == false)
            {
                return false;
            }

            //create new command for SQL
            cmd = new SqlCommand(sql, connection);

            //execute SQL
            cmd.ExecuteNonQuery();

            return true;
        }


        public Boolean IsConnected
        {
            get
            {
                //make sure we have a connection to test
                if (connection == null)
                {
                    return false;
                }

                //Return false for anything except an open connection
                if (connection.State != ConnectionState.Open)
                {
                    return false;
                }

                return true;
            }
        }


        public static String MakeQuerySafeString(String oldValue)
        {
            String newValue = "";

            newValue = oldValue;

            newValue = newValue.Replace("'", "''");

            return newValue;
        }


        public static String MakeBooleanSafe(Boolean value)
        {
            String newValue = "";

            if (value)
            {
                newValue = "1";
            }
            else
            {
                newValue = "0";
            }

            return newValue;
        }


    }
}
