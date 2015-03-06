using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevTemplateV3.WebApp.Common
{
    public class ActiveDirectoryUser
    {
        private String userID;
        private String firstName;
        private String lastName;
        private String displayName;
        private String email;


        public ActiveDirectoryUser()
        {
            userID = "";
            firstName = "";
            lastName = "";
            displayName = "";
            email = "";
        }


        public String UserID
        {
            get { return userID; }
            set { userID = value; }
        }


        public String DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }


        public String FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }


        public String LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }


        public String Email
        {
            get { return email; }
            set { email = value; }
        }

    }
}