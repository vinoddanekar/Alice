using NUnit.Framework;
using RoomBookingLib;
using System;
using System.Collections.Generic;

namespace Tests
{
    public class Tests
    {
        private BookingRepository _repository;
        private DateTime _dateToTest;

        public Tests()
        {
            string basePath = @"D:\Vinod\repos\Alice\Alice\RoomBookingLib.Tests\TestData";
            string bookingDataFile = System.IO.Path.Combine(basePath, "bookings.json");
            string roomsDataFile = System.IO.Path.Combine(basePath, "rooms.json");
            _repository = new BookingRepository(bookingDataFile, roomsDataFile);

            _dateToTest = DateTime.Now.Date.AddDays(1);
            _dateToTest = DateTime.SpecifyKind(_dateToTest, DateTimeKind.Unspecified);

            _repository.DeleteAll();
        }

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void ShouldBookOn1PmTest()
        {
            // Arrange
            DateTime bookDate = _dateToTest.AddHours(13);
            BookingRequest request = new BookingRequest
            {
                RoomName = "Room 1",
                BookedBy = "Vinod",
                BookFromUtc = GetTimeInUtc(bookDate),
                BookToUtc = GetTimeInUtc(bookDate.AddHours(1))
            };

            // Act
            Booking booking = _repository.Book(request);

            // Assert
            Assert.AreEqual(request.BookFromUtc, booking.BookedFromUtc);
        }

        [Test]
        public void ShouldBookOn2PmTest()
        {
            // Arrange
            DateTime bookDate = _dateToTest.AddHours(14);
            BookingRequest request = new BookingRequest
            {
                RoomName = "Room 1",
                BookedBy = "Vinod",
                BookFromUtc = GetTimeInUtc(bookDate),
                BookToUtc = GetTimeInUtc(bookDate.AddHours(1))
            };

            // Act
            Booking booking = _repository.Book(request);

            // Assert
            Assert.AreEqual(request.BookFromUtc, booking.BookedFromUtc);
        }

        [Test]
        public void ShouldNotBookOn1AndHalfPmTest()
        {   
            // Arrange
            DateTime bookDate = _dateToTest.AddHours(13).AddMinutes(30);
            BookingRequest request = new BookingRequest
            {
                RoomName = "Room 1",
                BookedBy = "Vinod",
                BookFromUtc = GetTimeInUtc(bookDate),
                BookToUtc = GetTimeInUtc(bookDate.AddHours(1))
            };

            // Act
            Exception exception = Assert.Throws<Exception>(() => _repository.Book(request));

            // Assert
            Assert.That(exception.Message, Is.EqualTo("Oh boy! Its not availablle. <a {aliceRequestAct}>Show bookings</a>"));
        }

        [Test]
        public void ShouldNotBookLessThanFiveMinuteDurationTest()
        {
            // Arrange
            DateTime bookDate = _dateToTest.AddHours(16).AddMinutes(30);
            BookingRequest request = new BookingRequest
            {
                RoomName = "Room 1",
                BookedBy = "Vinod",
                BookFromUtc = GetTimeInUtc(bookDate),
                BookToUtc = GetTimeInUtc(bookDate.AddMinutes(3))
            };

            // Act
            Exception exception = Assert.Throws<Exception>(() => _repository.Book(request));

            // Assert
            Assert.That(exception.Message, Is.EqualTo("Hey, you should book room for 5 minutes at least"));
        }

        [Test]
        public void ShouldListTest()
        {
            // Arrange
            DateTime bookDate = GetTimeInUtc(_dateToTest);
            BookingFilter filter = new BookingFilter();
            filter.BookedFromUtc = bookDate;
            filter.BookedToUtc = bookDate.AddDays(1);

            // Act
            IList<Booking> bookings = _repository.ListBookings(filter);

            // Assert
            Assert.AreEqual(2, bookings.Count);
        }

        private DateTime GetTimeInUtc(DateTime localDateTime)
        {
            TimeZoneInfo indiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime dateTimeUtc = TimeZoneInfo.ConvertTimeToUtc(localDateTime, indiaTimeZone);

            return dateTimeUtc;
        }
    }
}