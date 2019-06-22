using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alice.Common;

namespace RoomBookingLib
{
    public class AliceRequestHandler : Alice.Common.IAliceRequestHandler
    {
        private readonly RoomRepository _roomRepository;
        private readonly BookingRepository _bookingRepository;
        public string RequestsDataFile { get { return "RoomBookingRequests.json"; } }

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

            _bookingRepository = new BookingRepository(_dataFiles["bookings"], _dataFiles["rooms"]);
            _roomRepository = new RoomRepository(_dataFiles["rooms"]);
        }

        public IAliceResponse Handle(IAliceRequest request)
        {
            string serverAction = request.ServerAction.ToLower();
            IInternalRequestHandler handler = null;

            switch (serverAction)
            {
                case "listrooms":
                    handler = new AliceListRoomsRequestHandler(_roomRepository);
                    break;
                case "listbookings":
                    handler = new AliceBookingListRequestHandler(_bookingRepository);
                    break;
                case "listmybookings":
                    handler = new AliceMyBookingListRequestHandler(_bookingRepository);
                    break;
                case "bookroom":
                    handler = new AliceBookRoomRequestHandler(_bookingRepository);
                    break;
                case "cancelbooking":
                    handler = new AliceCancelBookingRequestHandler(_bookingRepository);
                    break;
                case "suggestcancellations":
                    handler = new AliceCancelBookingSuggestionRequestHandler(_bookingRepository);
                    break;
                default:
                    handler = new AliceUnrecognizedRequestHandler();
                    break;
            }

            IAliceResponse response;
            response = handler.Handle(request);

            return response;
        }
               

    }
}
