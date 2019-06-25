using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomBookingExtension
{
    public class Booking
    {
        public string RoomName { get; set; }
        public DateTime BookedFromUtc { get; set; }
        public DateTime BookedToUtc { get; set; }
        public string BookedBy { get; set; }
        public string BookedFor { get; set; }
        public DateTime BookedOnUtc { get; set; }

        public string BookRangeUtcToString()
        {
            string range = BookedFromUtc.ToString("hh:mm tt") + " to " + BookedToUtc.ToString("hh:mm tt");
            return range;
        }

        public string BookRangeLocalToString(TimeZoneInfo timeZoneInfo)
        {
            DateTime fromDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(BookedFromUtc, timeZoneInfo.Id);
            DateTime toDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(BookedToUtc, timeZoneInfo.Id);

            string range = fromDate.ToString("hh:mm tt") + " to " + toDate.ToString("hh:mm tt");
            return range;
        }

    }
}
