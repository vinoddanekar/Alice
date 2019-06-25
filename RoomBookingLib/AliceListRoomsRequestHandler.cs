using Alice.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomBookingExtension
{
    class AliceListRoomsRequestHandler : IInternalRequestHandler
    {
        RoomRepository _roomRepository;

        public AliceListRoomsRequestHandler(RoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public IAliceResponse Handle(IAliceRequest aliceRequest)
        {
            IAliceResponse response = new AliceResponse();
            IList<Room> rooms = _roomRepository.List();

            AliceRoomListParser parser = new AliceRoomListParser();
            response.Message = parser.Parse(rooms);

            return response;
        }

    }
}
