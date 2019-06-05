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
        public int DisplayIndex { get; set; }

        public Room()
        {

        }

        public Room(string name, int displayIndex)
        {
            Name = name;
            DisplayIndex = displayIndex;
        }
    }
}