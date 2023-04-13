using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Models.Planets
{
    public class Planet : IPlanet
    {
        private string name;
        private double budget;

        private List<IMilitaryUnit> army;
        private List<IWeapon> weapons;

        public Planet(string name, double budget)
        {
            this.Name=name;
            this.Budget = budget;
            army = new List<IMilitaryUnit>();
            weapons = new List<IWeapon>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException
                        (string.Format(ExceptionMessages.InvalidPlanetName));
                }

                name = value;
            }
        }

        public double Budget
        {
            get => budget;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException
                        (string.Format(ExceptionMessages.InvalidBudgetAmount));
                }

                budget = value;
            }
        }

        public double MilitaryPower => Math.Round(CalculateMilitaryPower(), 3);

        public IReadOnlyCollection<IMilitaryUnit> Army => this.army.AsReadOnly();

        public IReadOnlyCollection<IWeapon> Weapons => this.weapons.AsReadOnly();

        public void AddUnit(IMilitaryUnit unit)
        {
            army.Add(unit);
        }

        public void AddWeapon(IWeapon weapon)
        {
            weapons.Add(weapon);
        }

        public string PlanetInfo()
        {
            StringBuilder sb = new StringBuilder(0);
            sb.AppendLine($"Planet: {Name}");
            sb.AppendLine($"--Budget: {Budget} billion QUID");
            sb.Append("--Forces: ");

            if (this.army.Count > 0)
            {
                var forces = new List<string>();
                foreach (var unit in army)
                {
                    forces.Add(unit.GetType().Name);
                }

                sb.AppendLine(string.Join(", ", forces));
            }
            else
            {
                sb.AppendLine("No units");
            }

            sb.Append("--Combat equipment: ");

            if (this.weapons.Count > 0)
            {
                var weaps = new List<string>();

                foreach (var weap in weapons)
                {
                    weaps.Add(weap.GetType().Name);
                }

                sb.AppendLine(string.Join(", ", weaps));
            }
            else
            {
                sb.AppendLine("No weapons");
            }

            sb.AppendLine($"--Military Power: {this.MilitaryPower}");

            return sb.ToString().Trim();
        }

        public void Profit(double amount)
        {
            this.Budget += amount;
        }

        public void Spend(double amount)
        {
            if (this.Budget<amount)
            {
                throw new InvalidOperationException
                    (ExceptionMessages.UnsufficientBudget);
            }

            this.Budget -= amount;
        }

        public void TrainArmy()
        {
            foreach (var force in this.army)
            {
                force.IncreaseEndurance();
            }
        }

        private double CalculateMilitaryPower()
        {
            double mPower = (army.Sum(x => x.EnduranceLevel)) + (weapons.Sum(x => x.DestructionLevel));

            if (Army.Any(x => x.GetType().Name == "AnonymousImpactUnit"))
            {
                mPower *= 1.3;
            }

            if (Weapons.Any(x => x.GetType().Name == "NuclearWeapon"))
            {
                mPower *= 1.45;
            }

            return mPower;
        }
    }
}
