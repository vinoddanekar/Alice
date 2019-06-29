using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alice.Common;

namespace RoomBookingExtension
{
    class AliceBookingListParser
    {
        public string Parse(IList<Booking> bookings, IUserProfile userProfile)
        {
            StringBuilder sb = new StringBuilder();
            if (bookings.Count == 0)
                return "No bookings recorded yet!";

            sb.Append("I found something - ");
            sb.Append("<ol>");
            foreach (Booking booking in bookings)
            {
                sb.Append("<li>");
                AppendBookingItem(booking, userProfile, sb);
                sb.Append("</li>");
            }
            sb.Append("<ol>");

            return sb.ToString();
        }

        private void AppendBookingItem(Booking booking, IUserProfile userProfile, StringBuilder sb)
        {
            sb.AppendFormat("{0} <i>is booked from</i> {1}", booking.RoomName, booking.BookRangeLocalToString(userProfile.TimeZoneInfo));
            sb.AppendFormat(" <i>for</i> {0}", booking.BookedForToString());
            sb.AppendFormat(" <i>by</i> {0}", booking.BookedByString(userProfile));
        }
    }
}
