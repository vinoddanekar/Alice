using Alice.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace RoomBookingLib
{
    public class BookingAliceParser
    {
        private BookingRepository _repository;
        public BookingAliceParser()
        {
            _repository = new BookingRepository();
        }

       
        public IAliceResponse Book(IAliceRequest aliceRequest)
        {
            BookingRequestsBuilder requestsBuilder = new BookingRequestsBuilder(aliceRequest.Parameters, aliceRequest.UserProfile.UserName);
            BookingRequest bookingRequest;
            bookingRequest = requestsBuilder.Build();
            
            Booking booking;
            IAliceResponse response = new AliceResponse();
            try
            {
                booking = _repository.Book(bookingRequest);
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