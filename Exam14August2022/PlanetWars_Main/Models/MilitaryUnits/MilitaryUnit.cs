using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Utilities.Messages;
using System;

namespace PlanetWars.Models.MilitaryUnits
{
    public abstract class MilitaryUnit : IMilitaryUnit
    {
        private double cost;
        private int enduranceLevel;

        public MilitaryUnit(double cost)
        {
            Cost = cost;
            this.enduranceLevel = 1;
        }
        public double Cost
        {
            get { return cost; }
            private set { cost = value; }
        }

        public int EnduranceLevel => enduranceLevel;

        public void IncreaseEndurance()
        {
            if (enduranceLevel == 20)
            {
                throw new ArgumentException
                    (ExceptionMessages.EnduranceLevelExceeded);
            }

            enduranceLevel++;
        }
    }
}
