using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using DevTemplateV3.WebApp.Common;
using System.Data.SqlClient;


namespace DevTemplateV3.WebApp.UI.Controls
{
    public partial class SampleInputsControl : System.Web.UI.UserControl, InputInterface
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                LoadYears();

                LoadMonths();
            }
        }


        public Exception ValidateInputs()
        {

            if (cmbYear.SelectedIndex == -1)
            {
                return new Exception("Choose a year");
            }

            if (cmbMonth.SelectedIndex == -1)
            {
                return new Exception("Choose a month");
            }

            return null;
        }


        public String GetQueryString()
        {
            String queryString = "?";

            queryString = queryString + "&year=" + HttpUtility.UrlEncode(this.GetYearIDs());
            queryString = queryString + "&month=" + HttpUtility.UrlEncode(this.GetMonthIDs());

            return queryString;
        }


        public void ApplyQueryString(NameValueCollection query)
        {
            String[] values = null;

            //Apply Year
            if (query["year"] != null)
            {
                LoadYears();
                this.cmbYear.SelectedValue = query["year"];
            }

            //Apply Month
            if (query["month"] != null)
            {
                LoadMonths();

                try
                {
                    values = query["month"].Split(new Char[] { ',' });
                }
                catch (Exception)
                {
                    values = null;
                }

                if (values != null)
                {
                    for (Int32 i = 0; i < values.Length; i++)
                    {
                        try
                        {
                            cmbMonth.Items.FindByValue(values[i]).Selected = true;
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
            }
        }


        private void LoadMonths()
        {
            ListItem item = null;

            cmbMonth.Items.Clear();

            for (Int32 i = 1; i <= 12; i++)
            {
                item = new ListItem(i.ToString(), i.ToString());

                cmbMonth.Items.Add(item);
            }
        }


        private void LoadYears()
        {
            ListItem item = null;

            this.cmbYear.Items.Clear();

            item = new ListItem("2012", "2012");
            this.cmbYear.Items.Add(item);

            item = new ListItem("2013", "2013");
            this.cmbYear.Items.Add(item);

            item = new ListItem("2014", "2014");
            this.cmbYear.Items.Add(item);
        }


        public String GetYearIDs()
        {
            String list = "";

            try
            {
                for (Int32 i = 0; i < this.cmbYear.Items.Count; i++)
                {
                    if (cmbYear.Items[i].Selected)
                    {
                        list = list + cmbYear.Items[i].Value + ",";
                    }
                }

                list = list.Substring(0, list.Length - 1);
            }
            catch (Exception)
            {
                list = "";
            }

            return list;
        }


        public String GetMonthIDs()
        {
            String list = "";

            try
            {
                for (Int32 i = 0; i < this.cmbMonth.Items.Count; i++)
                {
                    if (cmbMonth.Items[i].Selected)
                    {
                        list = list + cmbMonth.Items[i].Value + ",";
                    }
                }

                list = list.Substring(0, list.Length - 1);
            }
            catch (Exception)
            {
                list = "";
            }

            return list;
        }


    }
}