using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomBookingLib
{
    public class Booking
    {
        public string RoomName { get; set; }
        public DateTime BookedFrom { get; set; }
        public DateTime BookedTo { get; set; }
        public string BookedBy { get; set; }
        public string BookedFor { get; set; }
        public DateTime BookedOn { get; set; }

        public string BookRangeToString()
        {
            string range = BookedFrom.ToString("hh:mm tt") + " - " + BookedTo.ToString("hh:mm tt");
            return range;
        }
    }
}
