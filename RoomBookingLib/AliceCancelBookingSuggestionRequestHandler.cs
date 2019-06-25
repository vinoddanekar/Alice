using Alice.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomBookingExtension
{
    class AliceCancelBookingSuggestionRequestHandler : IInternalRequestHandler
    {
        BookingRepository _bookingRepository;

        public AliceCancelBookingSuggestionRequestHandler(BookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public IAliceResponse Handle(IAliceRequest request)
        {
            IAliceResponse response = new AliceResponse();
            DateTime date = DateTime.Now;

            if (request.Parameters.Count == 3)
                date = Utility.ConvertToDate(request.Parameters[2].Value);

            response.Message = ListBookingsForCancellation(request.UserProfile);

            return response;
        }

        public string ListBookingsForCancellation(IUserProfile userProfile)
        {
            DateTime dateUtc = DateTime.UtcNow;

            BookingFilter filter = new BookingFilter();
            filter.BookedFromUtc = dateUtc;
            filter.BookedToUtc = filter.BookedFromUtc.AddDays(180);
            filter.BookedBy = userProfile.UserName;

            IList<Booking> bookings = _bookingRepository.ListBookings(filter);
            AliceBookingCancellationSuggestionsParser parser = new AliceBookingCancellationSuggestionsParser();
            string response = parser.Parse(bookings, userProfile);

            return response;
        }
    }
}
