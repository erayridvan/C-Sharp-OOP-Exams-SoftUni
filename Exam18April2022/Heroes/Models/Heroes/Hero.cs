using Heroes.Models.Contracts;
using Heroes.Utilities.Messages;
using System;

namespace Heroes.Models.Heroes
{
    public abstract class Hero : IHero
    {
        private string name;
        private int health;
        private int armour;
        private IWeapon weapon;

        public Hero(string name, int health, int armour)
        {
            Name = name;
            Health = health;
            Armour = armour;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.HeroNameNull);
                }

                name = value;
            }
        }

        public int Health
        {
            get => health;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.HeroHealthBelowZero);
                }

                health = value;
            }
        }

        public int Armour
        {
            get => armour;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.HeroArmourBelowZero);
                }

                armour = value;
            }
        }

        public bool IsAlive => this.Health > 0;

        public IWeapon Weapon
        {
            get => weapon;
            private set
            {
                if (value is null)
                {
                    throw new ArgumentException(ExceptionMessages.WeaponNull);
                }

                weapon = value;
            }
        }

        public void AddWeapon(IWeapon weapon)
        {
            this.Weapon = weapon;
        }

        public void TakeDamage(int points)
        {
            int value = points;

            if (this.Armour > value)
            {
                Armour -= value;
                value = 0;
            }
            else
            {
                value -= Armour;
                Armour = 0;
            }

            if (value > 0)
            {
                if (Health > value)
                {
                    Health -= value;
                }
                else
                {
                    Health = 0;
                }
            }
        }
    }
}
