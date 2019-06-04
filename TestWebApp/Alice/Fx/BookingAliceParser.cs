using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TestWebApp.Alice.Common;

namespace TestWebApp.Alice.Fx
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
        public string Book(AliceCommand command)
        {
            BotExperience experience = new BotExperience();
            BookingRequest request = new BookingRequest();
            request.RoomName = command.Parameters[2].Value;
            request.BookFrom = experience.FormatTime(command.Parameters[4].Value);
            request.BookTo = experience.FormatTime(command.Parameters[6].Value);
            request.BookFrom = Convert.ToDateTime(DateTime.Now.ToString("dd-MMM-yy ") + request.BookFrom.ToString("HH:mm tt"));
            request.BookTo = Convert.ToDateTime(DateTime.Now.ToString("dd-MMM-yy ") + request.BookTo.ToString("HH:mm tt"));

            Booking booking;
            string response;
            try
            {
                booking = _repository.Book(request);
                response = "Booked";
            }
            catch (Exception ex)
            {

                response = "Error: " + ex.Message;
            }
            
            return response;
        }
        
    }
}