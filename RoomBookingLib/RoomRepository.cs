using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomBookingLib
{
    public class RoomRepository
    {
        private string _dataFile;
        public RoomRepository(string dataFile)
        {
            _dataFile = dataFile;
        }

        public IList<Room> List()
        {
            List<Room> rooms;
            rooms = JsonFile<List<Room>>.Read(_dataFile);
            return rooms;
        }

        public Room Find(string roomName)
        {
            IList<Room> rooms = List();
            Room room = rooms.FirstOrDefault<Room>(o => o.Name.Equals(roomName, StringComparison.InvariantCultureIgnoreCase));
            return room;
        }
    }
}
