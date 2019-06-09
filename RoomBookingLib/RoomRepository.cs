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
            List<Room> rooms;
            rooms = JsonFile<List<Room>>.Read(Config.Current.RoomDataFile);
            return rooms;
        }
    }
}
