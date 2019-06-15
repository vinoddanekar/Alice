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
        public static IAliceResponse Ask(string message)
        {
            RoomBookingLib.AliceRequestHandler handler = new RoomBookingLib.AliceRequestHandler();
            Alice.Framework.AliceContext.Current.Register(handler);
            Alice.Framework.AliceContext.Current.UserProfile.UserName = "Vinod";

            Alice.Framework.Alice bot = new Alice.Framework.Alice();
            IAliceResponse response = bot.Ask(message);

            return response;
        }
    }
}