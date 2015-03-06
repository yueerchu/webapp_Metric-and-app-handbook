using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;

namespace DevTemplateV3.WebApp.UI.Controls
{
    public interface InputInterface
    {
        String GetQueryString();

        void ApplyQueryString(NameValueCollection q);

        Exception ValidateInputs();
    }
}