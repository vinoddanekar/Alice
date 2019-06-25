using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace RoomBookingExtension
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

        public List<Booking> List()
        {
            List<Booking> bookings;
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

        public Booking Cancel(BookingRequest request)
        {
            List<Booking> bookings = List();
            Booking booking = bookings.Find(
                               o => o.RoomName.Equals(request.RoomName, StringComparison.InvariantCultureIgnoreCase)
                               && o.BookedFromUtc == request.BookFromUtc
                               && o.BookedBy.Equals(request.BookedBy, StringComparison.InvariantCultureIgnoreCase)
                               );
            if (booking == null)
                throw new Exception("Ooops! There is no such booking.");

            bookings.Remove(booking);
            Save(bookings);

            return booking;
        }

        public void DeleteAll()
        {
            File.WriteAllText(_dataFile, string.Empty);
        }

        public Booking Book(BookingRequest bookingRequest)
        {
            IList<Booking> bookings = List();
            BookingRequest requestToAdd = PrepareBookingRequest(bookings, bookingRequest);

            Booking booking = requestToAdd.ToBooking();
            bookings.Add(booking);

            Save(bookings);

            return booking;
        }

        private BookingRequest PrepareBookingRequest(IList<Booking> bookings, BookingRequest bookingRequest)
        {
            BookingRequest resultingRequest = bookingRequest.Clone();
            resultingRequest.BookToUtc = resultingRequest.BookToUtc.AddMilliseconds(-1);

            RoomRepository roomRepository = new RoomRepository(_roomsDataFile);
            BookingRequestValidator validator = new BookingRequestValidator(roomRepository);
            if (!validator.Validate(resultingRequest))
            {
                throw new Exception(validator.ValidationError);
            }

            if (resultingRequest.ExplicitBooking)
            {
                Booking presentBooking = GetBooking(bookings, bookingRequest.BookFromUtc, bookingRequest.BookToUtc, bookingRequest.RoomName);
                if (presentBooking != null)
                {
                    if (presentBooking.BookedBy.Equals(resultingRequest.BookedBy, StringComparison.InvariantCultureIgnoreCase))
                        throw new Exception("It was booked by you already.");
                    else
                        throw new Exception(string.Format("Oh boy! Its not availablle. <a {{aliceRequestAct}}>Show bookings</a>", presentBooking.RoomName));
                }
            }
            else
            {                
                Room room = GetAnyAvailableRoom(bookings, resultingRequest.BookFromUtc, resultingRequest.BookToUtc);
                if (room == null)
                    throw new Exception("Oh boy! Hatter left no room for you.");

                resultingRequest.RoomName = room.Name;
            }

            return resultingRequest;
        }

        private Room GetAnyAvailableRoom(IList<Booking> bookings, DateTime dateFrom, DateTime dateTo)
        {
            RoomRepository roomRepository = new RoomRepository(_roomsDataFile);
            IList<Room> rooms = roomRepository.List();

            foreach (Room room in rooms)
            {
                if (!room.ExplicitBooking)
                {
                    Booking booking = GetBooking(bookings, dateFrom, dateTo, room.Name);
                    if (booking == null)
                        return room;
                }
            }

            return null;
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
                if (filter.Filter(booking))
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
                    (booking.BookedFromUtc <= dateFrom && booking.BookedToUtc >= dateFrom) ||
                    (booking.BookedFromUtc <= dateTo && booking.BookedToUtc >= dateTo)
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
