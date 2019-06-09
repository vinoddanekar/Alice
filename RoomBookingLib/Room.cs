using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace RoomBookingLib
{
    public class Room
    {
        public string Name { get; set; }
        public bool CanBookExplicitely { get; set; }

        public Room(string name)
        {
            Name = name;
        }

        public Room(string name, bool canBookExplicitely)
        {
            Name = name;
            CanBookExplicitely = canBookExplicitely;
        }
    }
}