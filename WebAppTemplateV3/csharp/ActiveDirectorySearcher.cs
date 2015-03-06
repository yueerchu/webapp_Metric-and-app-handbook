using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices.AccountManagement;


namespace DevTemplateV3.WebApp.Common
{
    public class ActiveDirectorySearcher
    {

        private ActiveDirectorySearcher()
        {

        }


        public static ActiveDirectoryUser FindUserBasedOnID(String ID)
        {
            List<String> directories = new List<String>();
            ActiveDirectoryUser result = null;
            String value = "";
            PrincipalContext domainContext = null;
            UserPrincipal user = null;
            PrincipalSearcher pS = null;
            PrincipalSearchResult<Principal> searchResults = null;
            UserPrincipal searchResult = null;

            directories.Add("na.cokecce.com");
            directories.Add("na.ko.com");

            result = null;
            for (Int32 i = 0; i < directories.Count; i++)
            {
                if (result == null)
                {
                    domainContext = new PrincipalContext(ContextType.Domain, directories[i]);
                    user = new UserPrincipal(domainContext);
                    user.Name = ID;

                    pS = new PrincipalSearcher();
                    pS.QueryFilter = user;

                    searchResults = pS.FindAll();

                    if (searchResults != null)
                    {
                        if (searchResults.Count() > 0)
                        {
                            searchResult = (UserPrincipal)searchResults.ElementAt(0);

                            result = new ActiveDirectoryUser();

                            value = searchResult.Name;
                            result.UserID = value;

                            value = searchResult.GivenName;
                            result.FirstName = value;

                            value = searchResult.DisplayName;
                            result.DisplayName = value;

                            value = searchResult.Surname;
                            result.LastName = value;

                            value = searchResult.EmailAddress;
                            result.Email = value;
                        }
                    }
                }
            }


            return result;
        }



        public static Boolean IsValidUserID(String ID)
        {
            String testValue = "";
            Int32 testInt = 0;

            //Test For Length of 6
            if (ID.Length != 6)
            {
                return false;
            }

            //Test For Letter as First Value
            testValue = ID.Substring(0, 1);
            if (Int32.TryParse(testValue, out testInt) == true)
            {
                return false;
            }

            //Test For Numbers as last 5 values
            testValue = ID.Substring(1, 5);
            if (Int32.TryParse(testValue, out testInt) == false)
            {
                return false;
            }

            return true;
        }


    }
}