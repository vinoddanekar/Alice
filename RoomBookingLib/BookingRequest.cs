using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomBookingLib
{
    public class BookingRequest
    {
        public string RoomName { get; set; }
        public DateTime BookFrom { get; set; }
        public DateTime BookTo { get; set; }
        public string BookedBy { get; set; }
        public string BookedFor { get; set; }
        public DateTime BookedOn { get; }

        public BookingRequest()
        {
            BookedOn = DateTime.Now;
        }

        public Booking ToBooking()
        {
            Booking booking = new Booking
            {
                BookedBy = BookedBy,
                BookedFor = BookedFor,
                BookedFrom = BookFrom,
                BookedOn = BookedOn,
                BookedTo = BookTo,
                RoomName = RoomName
            };

            return booking;
        }
    }
}
