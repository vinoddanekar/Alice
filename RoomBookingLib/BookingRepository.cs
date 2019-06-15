using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace RoomBookingLib
{
    public class BookingRepository
    {
        private string _dataFile;
        private string _roomsDataFile;

        public BookingRepository(string dataFile, string roomsDataFile)
        {
            _dataFile = dataFile;
            _roomsDataFile = roomsDataFile;
        }

        public IList<Booking> List()
        {
            IList<Booking> bookings;
            if (!File.Exists(_dataFile))
                return new List<Booking>();

            using (StreamReader r = new StreamReader(_dataFile))
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
            File.WriteAllText(_dataFile, json);
        }

        public Booking Book(BookingRequest bookingRequest)
        {
            IList<Booking> bookings = List();
            bookingRequest.BookTo = bookingRequest.BookTo.AddMilliseconds(-1);
            ValidateBooking(bookings, bookingRequest);

            Booking booking = bookingRequest.ToBooking();
            bookings.Add(booking);

            Save(bookings);

            return booking;
        }

        private void ValidateBooking(IList<Booking> bookings, BookingRequest bookingRequest)
        {
            RoomRepository roomRepository = new RoomRepository(_roomsDataFile);
            Room room = roomRepository.Find(bookingRequest.RoomName);
            if (room == null)
                throw new Exception(string.Format("Oh! I can't book it. There is no room with name <i>{0}</i>. Type <a {{aliceRequest}}>Show rooms</a> to show rooms.", bookingRequest.RoomName));

            Booking presentBooking = GetBooking(bookings, bookingRequest.BookFrom, bookingRequest.BookTo, bookingRequest.RoomName);
            if (presentBooking != null)
            {
                throw new Exception(string.Format("Oh boy! Its not availablle. <a {{aliceRequest}}>Show bookings</a>", presentBooking.RoomName));
            }
        }

        private Booking GetBooking(IList<Booking> bookings, DateTime dateFrom, DateTime dateTo, string roomName)
        {
            IList<Booking> bookingsForSlot = ListEngaged(bookings, dateFrom, dateTo);

            foreach (Booking booking in bookingsForSlot)
            {
                if (booking.RoomName.Equals(roomName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return booking;
                }
            }

            return null;
        }

        public IList<Booking> ListBookings(BookingFilter filter)
        {
            IList<Booking> bookings = List();
            IList<Booking> selectedBookings = ListBookings(bookings, filter);

            return selectedBookings;
        }

        private IList<Booking> ListBookings(IList<Booking> bookings, BookingFilter filter)
        {
            IList<Booking> bookingsForSlot = new List<Booking>();
            foreach (Booking booking in bookings)
            {
                if (filter.Contains(booking))
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
                    (booking.BookedFrom <= dateFrom && booking.BookedTo >= dateFrom) ||
                    (booking.BookedFrom <= dateTo && booking.BookedTo >= dateTo)
                    )
                {
                    if (!bookingsForSlot.Contains(booking))
                        bookingsForSlot.Add(booking);
                }
            }

            return bookingsForSlot;
        }

        private void AddCorrection(ref DateTime dateFrom, ref DateTime dateTo)
        {
            dateFrom = dateFrom.AddMilliseconds(-1);
            dateTo = dateTo.AddMilliseconds(1);
        }
    }
}
