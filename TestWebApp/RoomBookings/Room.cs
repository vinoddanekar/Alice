using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TestWebApp
{
    internal class Room
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

    internal class BookingRepository
    {
        private string DataFile
        {
            get
            {
                string virtualPath = "~/app_data/room-bookings.json";
                return HttpContext.Current.Server.MapPath(virtualPath);
            }
        }

        public IList<Booking> List()
        {
            IList<Booking> bookings;
            if (!File.Exists(DataFile))
                return new List<Booking>();

            using (StreamReader r = new StreamReader(DataFile))
            {
                string json = r.ReadToEnd();
                if (string.IsNullOrWhiteSpace(json))
                    bookings = new List<Booking>();
                else
                    bookings = JsonConvert.DeserializeObject<List<Booking>>(json);
            }

            return bookings;
        }

        private void Save(IList<Booking> bookings)
        {
            string json = JsonConvert.SerializeObject(bookings.ToArray());
            File.WriteAllText(DataFile, json);
        }

        public Booking Book(BookingRequest bookingRequest)
        {
            IList<Booking> bookings = List();

            Booking presentBooking = GetBooking(bookings, bookingRequest.BookFrom, bookingRequest.BookTo, bookingRequest.RoomName);
            if (presentBooking != null)
            {
                throw new Exception(string.Format("Room {0} is booked by {1} till {2}, already.", presentBooking.RoomName, presentBooking.BookedBy, presentBooking.BookedTo.ToString("hh:mm tt")));
            }

            Booking booking = bookingRequest.ToBooking();
            bookings.Add(booking);

            Save(bookings);

            return booking;
        }

        public Booking GetBooking(IList<Booking> bookings, DateTime dateFrom, DateTime dateTo, string roomName)
        {
            IList<Booking> bookingsForSlot = ListEngaged(bookings, dateFrom, dateTo);

            foreach (Booking booking in bookings)
            {
                if (booking.RoomName == roomName)
                {
                    return booking;
                }
            }

            return null;
        }

        public IList<Booking> ListBookings(DateTime dateFrom, DateTime dateTo)
        {            
            IList<Booking> bookings = List();
            IList<Booking> selectedBookings = ListBookings(bookings, dateFrom, dateTo);

            return selectedBookings;
        }

        private IList<Booking> ListBookings(IList<Booking> bookings, DateTime dateFrom, DateTime dateTo)
        {
            IList<Booking> bookingsForSlot = new List<Booking>();
            foreach (Booking booking in bookings)
            {
                if (booking.BookedFrom > dateFrom && booking.BookedTo < dateTo)
                {
                    if (!bookingsForSlot.Contains(booking))
                        bookingsForSlot.Add(booking);
                }
            }

            return bookingsForSlot;
        }

        private IList<Booking> ListEngaged(IList<Booking> bookings, DateTime dateFrom, DateTime dateTo)
        {
            IList<Booking> bookingsForSlot = new List<Booking>();
            foreach (Booking booking in bookings)
            {
                if (
                    (booking.BookedFrom < dateFrom && booking.BookedTo > dateFrom) ||
                    (booking.BookedFrom < dateTo && booking.BookedTo > dateTo)
                    )
                {
                    if (!bookingsForSlot.Contains(booking))
                        bookingsForSlot.Add(booking);
                }
            }

            return bookingsForSlot;
        }

    }
       
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

    public class BookingRequest
    {
        public string RoomName { get; set; }
        public DateTime BookFrom { get; set; }
        public DateTime BookTo { get; set; }
        public string BookedBy { get; set; }
        public string BookedFor { get; set; }
        public DateTime BookedOn { get; }

        public BookingRequest()
        {
            BookedOn = DateTime.Now;
        }

        public Booking ToBooking()
        {
            Booking booking = new Booking
            {
                BookedBy = BookedBy,
                BookedFor = BookedFor,
                BookedFrom = BookFrom,
                BookedOn = BookedOn,
                BookedTo = BookTo,
                RoomName = RoomName
            };

            return booking;
        }
    }

    internal class RoomRepository
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

    internal class BookingRequestProvider
    {
        public IList<BookingRequest> GetBulkRequests(string roomName, string bookie, string subject, string requestedDates, string requestedSlots)
        {
            string[] dates = requestedDates.Split(',');
            throw new NotImplementedException();
        }

        private DateTime[] GetDateTimes(string input)
        {
            string[] dates = input.Split(',');
            DateTime[] dateTimes = new DateTime[dates.Length];
            for (int i = 0; i < dates.Length; i++)
            {
                dateTimes[i] = Convert.ToDateTime(dates[i]);
            }

            return dateTimes;
        }
    }
}