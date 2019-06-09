using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomBookingLib
{
    public class RoomRepository
    {
        public IList<Room> List()
        {
            IList<Room> rooms = new List<Room>
            {
                new Room("Training room" ),
                new Room("Amsterdam" ),
                new Room("Endoven"),
                new Room("Utrecht")
            };

            return rooms;
        }
    }
}
