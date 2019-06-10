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

        private IAliceResponse DefaultResponse
        {
            get
            {
                IAliceResponse response = new AliceResponse();
                response.Message = "Booking service could not serve this request.";

                return response;
            }
        }

        public IAliceResponse Execute(IAliceRequest request)
        {
            IAliceResponse response;

            string serverAction = request.ServerAction.ToLower();
            switch (serverAction)
            {
                case "listrooms":
                    response = ListRooms();
                    break;
                case "listbookings":
                    response = ListBookings(request);
                    break;
                case "bookroom":
                    BookingAliceParser parser = new BookingAliceParser();
                    response = parser.Book(request);
                    break;
                case "cancelbooking":

                default:
                    response = DefaultResponse;
                    break;
            }
            
            return response;
        }

        private IAliceResponse ListRooms()
        {
            IAliceResponse response = new AliceResponse();
            RoomRepository roomRepository = new RoomRepository();
            IList<Room> rooms = roomRepository.List();
            response.Message = ParseRooms(rooms);

            return response;
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
                sb.AppendFormat("{0} is booked from {1}", booking.RoomName, booking.BookRangeToString());
                if(!string.IsNullOrWhiteSpace(booking.BookedFor))
                {
                    sb.AppendFormat(" for {0}", booking.BookedFor);
                }
                sb.Append("</li>");
            }
            sb.Append("<ul>");

            return sb.ToString();
        }

        private string ParseRooms(IList<Room> rooms)
        {
            StringBuilder sb = new StringBuilder();
            if (rooms.Count == 0)
                return "Surprising! There are no rooms! Take a looks at documentation.";

            sb.Append("<ol>");
            foreach (Room room in rooms)
            {
                sb.Append("<li>");
                sb.AppendFormat("{0}", room.Name);
                sb.Append("</li>");
            }
            sb.Append("<ol>");

            return sb.ToString();
        }
    }
}
