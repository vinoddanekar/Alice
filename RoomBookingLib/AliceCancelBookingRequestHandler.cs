using Alice.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomBookingLib
{
    class AliceCancelBookingRequestHandler : IInternalRequestHandler
    {
        BookingRepository _bookingRepository;

        public AliceCancelBookingRequestHandler(BookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public IAliceResponse Handle(IAliceRequest aliceRequest)
        {
            CancellationRequestsBuilder requestsBuilder = new CancellationRequestsBuilder(aliceRequest.Parameters, aliceRequest.UserProfile);
            BookingRequest bookingRequest;
            bookingRequest = requestsBuilder.Build();

            Booking booking;
            IAliceResponse response = new AliceResponse();
            try
            {
                booking = _bookingRepository.Cancel(bookingRequest);
                response.Message = string.Format("Cancelled");
            }
            catch (Exception ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Message = "Error: " + ex.Message;
            }

            return response;
        }

    }
}
