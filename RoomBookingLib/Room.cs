using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace RoomBookingExtension
{
    public class Room
    {
        public string Name { get; set; }
        public bool ExplicitBooking { get; set; }

        public Room()
        {

        }

        public Room(string name)
        {
            Name = name;
        }

        public Room(string name, bool canBookExplicitely)
        {
            Name = name;
            ExplicitBooking = canBookExplicitely;
        }
    }
}