using Alice.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alice.Framework;

namespace TestWebApp.AliceInterface
{
    public partial class Alice_Chat : System.Web.UI.Page
    {   
        [WebMethod]
        public static Response Ask(string message)
        {
            Alice.Framework.Alice bot = new Alice.Framework.Alice();
            Response response = bot.Ask(message);

            return response;
        }
    }
}