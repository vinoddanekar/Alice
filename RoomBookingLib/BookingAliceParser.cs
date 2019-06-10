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

        /*
         *0 book am from 1 - 2
         *1 book
         *2 am
         *3 from
         *4 1
         *5 -
         *6 2
         */
        public IAliceResponse Book(IAliceRequest aliceRequest)
        {
            BookingRequest bookingRequest = new BookingRequest();
            bookingRequest.RoomName = aliceRequest.Parameters[2].Value;
            bookingRequest.BookFrom = Utility.ConvertToTime(aliceRequest.Parameters[4].Value);
            bookingRequest.BookTo = Utility.ConvertToTime(aliceRequest.Parameters[6].Value);
            bookingRequest.BookFrom = Convert.ToDateTime(DateTime.Now.ToString("dd-MMM-yy ") + bookingRequest.BookFrom.ToString("HH:mm tt"));
            bookingRequest.BookTo = Convert.ToDateTime(DateTime.Now.ToString("dd-MMM-yy ") + bookingRequest.BookTo.ToString("HH:mm tt"));

            if (aliceRequest.Parameters.Count > 7) {
                bookingRequest.BookedFor = aliceRequest.Parameters[8].Value;
            }

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