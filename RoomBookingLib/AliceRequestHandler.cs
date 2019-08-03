using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alice.Common;

namespace RoomBookingExtension
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

            InternalRequestHandlerFactory factory = new InternalRequestHandlerFactory(_bookingRepository, _roomRepository);
            handler = factory.GetInternalRequestHandler(serverAction);

            IAliceResponse response;
            response = handler.Handle(request);

            return response;
        }
               

    }
}
