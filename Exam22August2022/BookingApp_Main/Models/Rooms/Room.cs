using BookingApp.Models.Rooms.Contracts;
using BookingApp.Utilities.Messages;
using System;

namespace BookingApp.Models.Rooms
{
    public abstract class Room : IRoom
    {
        private double pricePerNight = 0;

        protected Room(int bedCapacity)
        {
            BedCapacity = bedCapacity;
        }

        public int BedCapacity { get; }

        public double PricePerNight
        {
            get { return pricePerNight; }
            private set
            {
                if (value<0)
                {
                    throw new ArgumentException(ExceptionMessages.PricePerNightNegative);
                }

                pricePerNight = value;
            }
        }

        public void SetPrice(double price)
        {
            this.PricePerNight = price;
        }
    }
}
