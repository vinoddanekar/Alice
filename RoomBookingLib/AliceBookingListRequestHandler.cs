using Alice.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomBookingExtension
{
    class AliceBookingListRequestHandler : IInternalRequestHandler
    {
        BookingRepository _bookingRepository;

        public AliceBookingListRequestHandler(BookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public IAliceResponse Handle(IAliceRequest request)
        {
            IAliceResponse response = new AliceResponse();
            DateTime date = DateTime.Now;

            if (request.Parameters.Count == 3)
                date = Utility.ConvertToDate(request.Parameters[2].Value);

            response.Message = ListBookings(date, string.Empty, request.UserProfile);

            return response;
        }

        private string ListBookings(DateTime date, string userName, IUserProfile userProfile)
        {
            DateTime localTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(date, userProfile.TimeZoneInfo.Id);
            DateTime dateUtc = localTime.Date.ToUniversalTime();

            BookingFilter filter = new BookingFilter();
            filter.BookedFromUtc = dateUtc;
            filter.BookedToUtc = filter.BookedFromUtc.AddHours(24);
            filter.BookedBy = userName;

            IList<Booking> bookings = _bookingRepository.ListBookings(filter);

            AliceBookingListParser parser = new AliceBookingListParser();
            string response = parser.Parse(bookings, userProfile);

            return response;
        }

    }
}
