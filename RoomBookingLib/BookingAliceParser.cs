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

        public string ListBookings(DateTime date)
        {
            DateTime dateFrom = date.Date;
            DateTime dateTo = date.Date.AddHours(24);

            IList<Booking> bookings = _repository.ListBookings(dateFrom, dateTo);
            string response = ParseBookings(bookings);

            return response;
        }

        private string ParseBookings(IList<Booking> bookings)
        {
            StringBuilder sb = new StringBuilder();
            if (bookings.Count == 0)
                return "No bookings recorded yet!";

            sb.Append("I found something - ");
            sb.Append("<ul>");
            foreach (Booking booking in bookings)
            {
                sb.Append("<li>");
                sb.AppendFormat("{0} is booked for {1}<br/>", booking.RoomName, booking.BookRangeToString());
                sb.Append("</li>");
            }
            sb.Append("<ul>");

            return sb.ToString();
        }
        
        public IAliceResponse Book(IAliceRequest aliceRequest)
        {
            BookingRequestsBuilder requestsBuilder = new BookingRequestsBuilder(aliceRequest.Parameters);
            BookingRequest bookingRequest;
            bookingRequest = requestsBuilder.Build();
            
            Booking booking;
            IAliceResponse response = new AliceResponse();
            try
            {
                booking = _repository.Book(bookingRequest);
                response.Message = "Booked";
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