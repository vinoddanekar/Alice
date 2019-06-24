using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomBookingLib
{
    class BookingRequestValidator
    {
        public string ValidationError { get; set; }
        public bool IsValid { get; set; }

        private RoomRepository _roomRepository;
        public BookingRequestValidator(RoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public bool Validate(BookingRequest bookingRequest)
        {
            IsValid = true;
            ValidationError = string.Empty;

            if (bookingRequest.BookFromUtc < DateTime.UtcNow)
            {
                IsValid = false;
                ValidationError = "Booking for past time is not allowed.";
            }
            else if (bookingRequest.BookToUtc.Subtract(bookingRequest.BookFromUtc).TotalMinutes < 5)
            {
                IsValid = false;
                ValidationError = "Hey, you should book room for 5 minutes at least";
            }
            else if (bookingRequest.ExplicitBooking)
            {
                Room room = _roomRepository.Find(bookingRequest.RoomName);
                if (room == null)
                {
                    IsValid = false;
                    ValidationError = string.Format("Oh! I can't book it. There is no room with name <i>{0}</i>. Type <a {{aliceRequest}}>Show rooms</a> to show rooms.", bookingRequest.RoomName);
                }
            }

            return IsValid;
        }


    }
}
