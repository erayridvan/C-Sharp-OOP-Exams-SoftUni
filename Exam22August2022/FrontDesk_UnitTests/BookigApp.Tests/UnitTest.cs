using FrontDeskApp;
using NUnit.Framework;
using System;

namespace BookigApp.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            Room room = new Room(6, 200);
            Assert.AreEqual(6, room.BedCapacity);
            Assert.AreEqual(200, room.PricePerNight);
        }

        [Test]
        public void Test2()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Room room = new Room(0, 200);
            });
        }

        [Test]
        public void Test3()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Room room = new Room(1, -2);
            });
        }

        [Test]
        public void Test4()
        {
            Room room = new Room(3, 200);
            Booking booking = new Booking(123, room, 5);

            Assert.AreEqual(123, booking.BookingNumber);
            Assert.AreEqual(3, booking.Room.BedCapacity);
            Assert.AreEqual(200, booking.Room.PricePerNight);
            Assert.AreEqual(5, booking.ResidenceDuration);
        }

        [Test]
        public void Test5()
        {
            Hotel hotel = new Hotel("eray",5);

            Assert.AreEqual("eray", hotel.FullName);
            Assert.AreEqual(5, hotel.Category);
            Assert.AreEqual(0, hotel.Rooms.Count);
            Assert.AreEqual(0, hotel.Bookings.Count);

            Assert.Throws<ArgumentNullException>(() =>
            {
                Hotel hotel = new Hotel(" ", 5);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                Hotel hotel = new Hotel("", 5);
            });

            Assert.Throws<ArgumentException>(() =>
            {
                Hotel hotel = new Hotel("eray", 6);
            });
        }

        [Test]
        public void Test6()
        {
            Hotel hotel = new Hotel("eray", 4);
            Room room = new Room(3, 200);

            hotel.AddRoom(room);

            Assert.AreEqual(1, hotel.Rooms.Count);
        }

        [Test]
        public void Test7()
        {
            Hotel hotel = new Hotel("eray", 4);
            Room room = new Room(3, 30);

            hotel.AddRoom(room);

            Assert.Throws<ArgumentException>(() =>
            {
                hotel.BookRoom(0, 1, 5, 300);
            });

            Assert.Throws<ArgumentException>(() =>
            {
                hotel.BookRoom(1, -1, 5, 300);
            });

            Assert.Throws<ArgumentException>(() =>
            {
                hotel.BookRoom(1, 1, 0, 300);
            });

            hotel.BookRoom(2, 1, 5, 300);

            Assert.AreEqual(150,hotel.Turnover);
            Assert.AreEqual(1, hotel.Bookings.Count);
        }
    }
}