using Alice.Common;
using Alice.Framework;
using System;
using System.Web.Services;

namespace TestWebApp.AliceInterface
{
    public partial class Alice_Chat : System.Web.UI.Page
    {
        [WebMethod]
        public static IAliceResponse Ask(UserRequest request)
        {
            RoomBookingExtension.AliceRequestHandler handler = new RoomBookingExtension.AliceRequestHandler();
            AliceContext.Current.Register(handler);
            AliceContext.Current.UserProfile.UserName = "Vinod";
            AliceContext.Current.UserProfile.TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

            Alice.Framework.Alice bot = new Alice.Framework.Alice();
            IAliceResponse response = bot.Ask(request);

            return response;
        }
    }
}