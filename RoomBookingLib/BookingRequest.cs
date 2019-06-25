using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomBookingExtension
{
    public class BookingRequest
    {
        public string RoomName { get; set; }
        public DateTime BookFromUtc { get; set; }
        public DateTime BookToUtc { get; set; }
        public string BookedBy { get; set; }
        public string BookedFor { get; set; }
        public DateTime BookedOnUtc { get; }

        public bool ExplicitBooking
        {
            get
            {
                if (RoomName.ToLower() == "any room")
                    return false;

                return true;
            }
        }
        public BookingRequest()
        {
            BookedOnUtc = DateTime.Now;
        }

        public Booking ToBooking()
        {
            Booking booking = new Booking
            {
                BookedBy = BookedBy,
                BookedFor = BookedFor,
                BookedFromUtc = BookFromUtc,
                BookedOnUtc = BookedOnUtc,
                BookedToUtc = BookToUtc,
                RoomName = RoomName
            };

            return booking;
        }

        public BookingRequest Clone()
        {
            BookingRequest request = new BookingRequest
            {
                BookedBy = BookedBy,
                BookedFor = BookedFor,
                BookFromUtc = BookFromUtc,
                BookToUtc = BookToUtc,
                RoomName = RoomName
            };

            return request;
        }

    }
}
