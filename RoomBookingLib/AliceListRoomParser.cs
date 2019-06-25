using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomBookingExtension
{
    class AliceListRoomParser
    {
        public string Parse(IList<Room> rooms)
        {
            StringBuilder sb = new StringBuilder();
            if (rooms.Count == 0)
                return "Surprising! There are no rooms! Take a look at documentation.";

            sb.Append("<ol>");
            string item, command;
            foreach (Room room in rooms)
            {
                sb.Append("<li>");
                command = string.Format("Book {0} on today from time to time for meeting", room.Name);
                item = Alice.Common.ResponseHelper.CreateHintCommand(room.Name, command);
                sb.Append(item);
                sb.Append("</li>");
            }
            sb.Append("<ol>");

            return sb.ToString();
        }
    }
}
