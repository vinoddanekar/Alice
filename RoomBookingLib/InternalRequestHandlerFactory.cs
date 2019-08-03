using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomBookingExtension
{
    class InternalRequestHandlerFactory
    {
        private BookingRepository _bookingRepository;
        private RoomRepository _roomRepository;
        private Dictionary<string, Func<IInternalRequestHandler>> _handlers;

        public InternalRequestHandlerFactory(BookingRepository bookingRepository, RoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _handlers = new Dictionary<string, Func<IInternalRequestHandler>>();

            _handlers.Add("listrooms", () => { return new AliceListRoomsRequestHandler(_roomRepository); });
            _handlers.Add("listbookings", () => { return new AliceBookingListRequestHandler(_bookingRepository); });
            _handlers.Add("listmybookings", () => { return new AliceMyBookingListRequestHandler(_bookingRepository); });
            _handlers.Add("bookroom", () => { return new AliceBookRoomRequestHandler(_bookingRepository); });
            _handlers.Add("cancelbooking", () => { return new AliceCancelBookingRequestHandler(_bookingRepository); });
            _handlers.Add("suggestcancellations", () => { return new AliceCancelBookingSuggestionRequestHandler(_bookingRepository); });
            _handlers.Add("unrecognized", () => { return new AliceUnrecognizedRequestHandler(); });
        }

        public IInternalRequestHandler GetInternalRequestHandler(string requestText)
        {
            if (_handlers.ContainsKey(requestText))
                return _handlers[requestText]();

            return _handlers["unrecognized"]();
        }
    }
}
