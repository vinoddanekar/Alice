using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestWebApp.Alice.Fx;

namespace TestWebApp.Alice
{
    public partial class Alice_Chat : System.Web.UI.Page
    {   
        [WebMethod]
        public static Response Ask(string message)
        {
            Alice.Fx.Alice bot = new Alice.Fx.Alice();
            Response response = bot.Ask(message);
            return response  ;
        }
    }
}