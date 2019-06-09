using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alice.Common;

namespace RoomBookingLib
{
    public class AliceRequestHandler : Alice.Common.IAliceRequestHandler
    {
        public string RequestsDataFile { get { return "RoomBookingRequests.json"; } }

        public IAliceResponse Execute(IAliceRequest request)
        {
            IAliceResponse response;
            response = ListBookings(request);
            
            return response;
            throw new NotImplementedException("RoomBookingLib.AliceRequestHandler is not implemented");
        }

        private IAliceResponse ListBookings(IAliceRequest request)
        {
            IAliceResponse response = new AliceResponse();
            response.Message = ListBookings(DateTime.Now);
            
            return response;
        }

        public string ListBookings(DateTime date)
        {
            BookingRepository repository = new BookingRepository();

            DateTime dateFrom = date.Date;
            DateTime dateTo = date.Date.AddHours(24);

            IList<Booking> bookings = repository.ListBookings(dateFrom, dateTo);
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
    }
}
