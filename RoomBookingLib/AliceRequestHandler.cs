using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alice.Common;

namespace RoomBookingLib
{
    public class AliceRequestHandler : Alice.Common.IAliceRequestHandler
    {
        private RoomRepository _roomRepository;
        private BookingRepository _bookingRepository;
        
        public string RequestsDataFile { get { return "RoomBookingRequests.json"; } }

        public RoomRepository RoomRepository
        {
            get
            {
                if (_roomRepository == null)
                {
                    string roomDataFile = DataFiles["rooms"];
                    _roomRepository = new RoomRepository(roomDataFile);
                }

                return _roomRepository;
            }
        }

        public BookingRepository BookingRepository
        {
            get
            {
                if (_bookingRepository == null)
                {
                    string roomDataFile = DataFiles["rooms"];
                    string bookingDataFile = DataFiles["bookings"];

                    _bookingRepository = new BookingRepository(bookingDataFile, roomDataFile);
                }

                return _bookingRepository;
            }
        }

        private Dictionary<string,string> _dataFiles;
        public Dictionary<string, string> DataFiles
        {
            get
            {
                return _dataFiles;
            }
        }

        public AliceRequestHandler()
        {
            _dataFiles = new Dictionary<string, string>();
            _dataFiles.Add("rooms", Config.Current.RoomDataFile);
            _dataFiles.Add("bookings", Config.Current.BookingDataFile);
        }

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
                    response = Book(request);
                    break;
                case "cancelbooking":

                default:
                    response = DefaultResponse;
                    break;
            }
            
            return response;
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
                booking = BookingRepository.Book(bookingRequest);
                response.Message = string.Format("Booked");
            }
            catch (Exception ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Message = "Error: " + ex.Message;
            }

            return response;
        }

        private IAliceResponse ListRooms()
        {
            IAliceResponse response = new AliceResponse();
            IList<Room> rooms = RoomRepository.List();
            response.Message = ParseRooms(rooms);

            return response;
        }

        private IAliceResponse ListBookings(IAliceRequest request)
        {
            IAliceResponse response = new AliceResponse();
            DateTime date = DateTime.Now;

            if (request.Parameters.Count == 3)
                date = Utility.ConvertToDate(request.Parameters[2].Value);

            response.Message = ListBookings(date);
            
            return response;
        }

        public string ListBookings(DateTime date)
        {
            DateTime dateFrom = date.Date;
            DateTime dateTo = date.Date.AddHours(24);

            BookingFilter filter = new BookingFilter();
            filter.BookedFrom = dateFrom;
            filter.BookedTo = dateTo;
            
            IList<Booking> bookings = BookingRepository.ListBookings(filter);
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
                AppendBookingItem(booking, sb);
                sb.Append("</li>");
            }
            sb.Append("<ul>");

            return sb.ToString();
        }

        private void AppendBookingItem(Booking booking, StringBuilder sb)
        {
            sb.AppendFormat("{0} is booked from {1}", booking.RoomName, booking.BookRangeToString());
            if (string.IsNullOrWhiteSpace(booking.BookedFor))
                sb.Append(" for something");
            else
                sb.AppendFormat(" for {0}", booking.BookedFor);

            if (string.IsNullOrWhiteSpace(booking.BookedBy))
                sb.Append(" by someone");
            else
                sb.AppendFormat(" by {0}", booking.BookedBy);

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
