using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alice.Common;

namespace RoomBookingLib
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
            if (string.IsNullOrWhiteSpace(booking.BookedFor))
                sb.Append(" <i>for</i> something");
            else
                sb.AppendFormat(" <i>for</i> {0}", booking.BookedFor);

            if (string.IsNullOrWhiteSpace(booking.BookedBy))
                sb.Append(" <i>by</i> someone");
            else
                sb.AppendFormat(" <i>by</i> {0}", booking.BookedBy);
        }

    }
}
