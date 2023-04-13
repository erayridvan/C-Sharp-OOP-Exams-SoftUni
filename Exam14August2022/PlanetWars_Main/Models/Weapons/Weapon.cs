using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Utilities.Messages;
using System;

namespace PlanetWars.Models.Weapons
{
    public abstract class Weapon : IWeapon
    {
		private double price;
        private int destructionLevel;

        public Weapon(int destructionLevel, double price)
        {
            DestructionLevel=destructionLevel;
            Price=price;
        }

		public double Price
		{
			get { return price; }
			private set { price = value; }
		}

        public int DestructionLevel
        {
            get => destructionLevel;
            private set
            {
                if (value<1)
                {
                    throw new ArgumentException
                        (string.Format(ExceptionMessages.TooLowDestructionLevel));
                }

                if (value>10)
                {
                    throw new ArgumentException
                        (string.Format(ExceptionMessages.TooHighDestructionLevel));
                }

                destructionLevel = value;
            }
        }
    }
}
