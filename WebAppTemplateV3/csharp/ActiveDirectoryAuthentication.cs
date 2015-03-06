using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices.AccountManagement;
using DevTemplateV3.WebApp.UI;
using AjaxControlToolkit;

namespace DevTemplateV3.WebApp.Common
{
    public class ActiveDirectoryAuthentication
    {
        public static readonly String URL_DEFAULT = "../Default.aspx";

        private Page page;


        public ActiveDirectoryAuthentication(Page p)
        {
            page = p;
            page.Session.Timeout = 240;
        }


        public String DestinationPage
        {
            get
            {
                String URL = "";

                if (page.Session["auth_destpage"] == null)
                {
                    return URL_DEFAULT;
                }

                URL = page.Session["auth_destpage"].ToString();

                return URL;
            }
            set
            {
                page.Session["auth_destpage"] = value;
            }
        }


        public String SourcePage
        {
            get
            {
                String URL = "";

                if (page.Session["auth_sourcepage"] == null)
                {
                    return URL_DEFAULT;
                }

                URL = page.Session["auth_sourcepage"].ToString();

                return URL;
            }
            set
            {
                page.Session["auth_sourcepage"] = value;
            }
        }


        public String Username
        {
            get
            {
                String username = "";

                if (page.Session["auth_username"] == null)
                {
                    return "";
                }

                username = page.Session["auth_username"].ToString();

                return username;
            }
            set
            {
                page.Session["auth_username"] = value;
            }
        }


        public Boolean IsAuthenticated
        {
            get
            {
                String username = "";
                Boolean result = false;

                username = this.Username;

                if (username.Equals(""))
                {
                    result = false;
                }
                else
                {
                    result = true;
                }

                return result;
            }
        }


        public Boolean AuthenticateUser(String username, String password)
        {
            Boolean result = false;

            //Authenticates with Active Directory domain.  PrincipalContext describes the context of viewing the directory
            //such as Domain, Machine, Application Directory
            //Authenticates in specified domain, "na.cokecce.com"
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "na.cokecce.com"))
            {
                result = pc.ValidateCredentials(username, password);
            }

            if (result)
            {
                this.Username = username.ToUpper();
            }
            //else
            //{
            //    //Try KO
            //    using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "ko.com"))
            //    {
            //        result = pc.ValidateCredentials(username, password);
            //    }

            //    if (result)
            //    {
            //        this.Username = username.ToUpper();
            //    }
            //}

            return result;
        }

    }
}