using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomBookingLib
{
    class AliceRoomListParser
    {
        public string Parse(IList<Room> rooms)
        {
            StringBuilder sb = new StringBuilder();
            if (rooms.Count == 0)
                return "Surprising! There are no rooms! Take a look at documentation.";

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
