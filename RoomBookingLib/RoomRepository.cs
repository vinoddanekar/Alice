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
                new Room("Training room", 1),
                new Room("Amsterdam", 1),
                new Room("Endoven", 1),
                new Room("Utrecht", 1)
            };

            return rooms;
        }
    }
}
