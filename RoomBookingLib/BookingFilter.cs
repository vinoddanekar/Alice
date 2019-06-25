using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomBookingExtension
{
    public class BookingFilter
    {
        public string RoomName { get; set; }
        public DateTime BookedFromUtc { get; set; }
        public DateTime BookedToUtc { get; set; }
        public string BookedBy { get; set; }
        public string BookedFor { get; set; }

        public bool Filter(Booking booking)
        {
            bool result = false;
            if (
                MatchRoomName(booking) &&
                MatchBookedBy(booking) &&
                MatchDateFrom(booking) 
                )
                result = true;

            return result;
        }

        private bool MatchRoomName(Booking booking)
        {
            bool result = false;
            if (string.IsNullOrEmpty(RoomName))
                result = true;
            else if (RoomName.Equals(booking.RoomName, StringComparison.InvariantCultureIgnoreCase))
                result = true;

            return result;
        }

        private bool MatchBookedBy(Booking booking)
        {
            bool result = false;
            if (string.IsNullOrEmpty(BookedBy))
                result = true;
            else if (BookedBy.Equals(booking.BookedBy, StringComparison.InvariantCultureIgnoreCase))
                result = true;

            return result;
        }

        private bool MatchDateFrom(Booking booking)
        {
            bool result = false;
            if (BookedFromUtc == DateTime.MinValue || BookedFromUtc == null)
                result = true;
            else if (BookedFromUtc < booking.BookedFromUtc && BookedToUtc > booking.BookedFromUtc)
                result = true;

            return result;
        }
    }
}
