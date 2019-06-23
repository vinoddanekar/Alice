﻿using Alice.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomBookingLib
{
    class AliceBookRoomRequestHandler : IInternalRequestHandler
    {
        BookingRepository _bookingRepository;

        public AliceBookRoomRequestHandler(BookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public IAliceResponse Handle(IAliceRequest aliceRequest)
        {
            BookingRequestsBuilder requestsBuilder = new BookingRequestsBuilder(aliceRequest.Parameters, aliceRequest.UserProfile);
            BookingRequest bookingRequest;
            bookingRequest = requestsBuilder.Build();

            Booking booking;
            IAliceResponse response = new AliceResponse();
            try
            {
                booking = _bookingRepository.Book(bookingRequest);
                response.Message = string.Format("Booked");
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