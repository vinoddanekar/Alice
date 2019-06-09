using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace RoomBookingLib
{
    internal class Config
    {
        private static Config _current;
        public static Config Current
        {
            get
            {
                if (_current == null)
                    _current = new Config();

                return _current;
            }
        }

        private Config()
        {
         
        }

        public string DataPath
        {
            get
            {
                return HttpContext.Current.Server.MapPath("~/app_data/RoomBookings");
            }
        }

        public string RoomDataFile
        {
            get
            {
                return Path.Combine(DataPath, "Rooms");
            }
        }

        public string BookingDataFile
        {
            get
            {
                return Path.Combine(DataPath, "Bookings");
            }
        }
    }
}
