using PlanetWars.Core.Contracts;
using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Repositories.Contracts;
using PlanetWars.Utilities.Messages;
using System;
using System.Linq;
using System.Text;

namespace PlanetWars.Core
{
    public class Controller : IController
    {
        private IRepository<IPlanet> planets;

        public Controller()
        {
            planets = new PlanetRepository();
        }

        public string AddUnit(string unitTypeName, string planetName)
        {
            IPlanet planet = planets.FindByName(planetName);

            if (planet == default)
            {
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if (unitTypeName != nameof(SpaceForces)
                && unitTypeName != nameof(StormTroopers)
                && unitTypeName != nameof(AnonymousImpactUnit))
            {
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.ItemNotAvailable, unitTypeName));
            }

            IMilitaryUnit military;
            if (unitTypeName == nameof(AnonymousImpactUnit))
            {
                military = new AnonymousImpactUnit();
            }
            else if (unitTypeName == nameof(SpaceForces))
            {
                military = new SpaceForces();
            }
            else
            {
                military = new StormTroopers();
            }

            if (planet.Army.Any(x => x.GetType().Name == unitTypeName))
            {
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.UnitAlreadyAdded, unitTypeName, planetName));
            }

            planet.AddUnit(military);
            planet.Spend(military.Cost);
            return string.Format(OutputMessages.UnitAdded, unitTypeName, planetName);
        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            IPlanet planet = planets.FindByName(planetName);

            if (planet == null)
            {
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if (weaponTypeName != nameof(BioChemicalWeapon)
                && weaponTypeName != nameof(NuclearWeapon)
                && weaponTypeName != nameof(SpaceMissiles))
            {
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.ItemNotAvailable, weaponTypeName));
            }

            if (planet.Weapons.Any(x => x.GetType().Name == weaponTypeName))
            {
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.WeaponAlreadyAdded, weaponTypeName, planetName));
            }

            IWeapon weapon;
            if (weaponTypeName == nameof(BioChemicalWeapon))
            {
                weapon = new BioChemicalWeapon(destructionLevel);
            }
            else if (weaponTypeName == nameof(NuclearWeapon))
            {
                weapon = new NuclearWeapon(destructionLevel);
            }
            else
            {
                weapon = new SpaceMissiles(destructionLevel);
            }

            planet.AddWeapon(weapon);
            planet.Spend(weapon.Price);

            return string.Format(OutputMessages.WeaponAdded, planetName, weaponTypeName);
        }

        public string CreatePlanet(string name, double budget)
        {
            IPlanet planet = planets.FindByName(name);
            if (planet != null)
            {
                return string.Format(OutputMessages.ExistingPlanet, name);
            }

            planet = new Planet(name, budget);
            planets.AddItem(planet);
            return string.Format(OutputMessages.NewPlanet, name);
        }

        public string ForcesReport()
        {
            IOrderedEnumerable<IPlanet> orderdPlanets = planets.Models
                 .OrderByDescending(p => p.MilitaryPower)
                 .ThenBy(p => p.Name);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("***UNIVERSE PLANET MILITARY REPORT***");

            foreach (var planet in orderdPlanets)
            {
                sb.AppendLine(planet.PlanetInfo());
            }

            return sb.ToString().Trim();
        }

        public string SpaceCombat(string planetOne, string planetTwo)
        {
            IPlanet one = planets.FindByName(planetOne);
            IPlanet two = planets.FindByName(planetTwo);
            IPlanet winner;
            IPlanet looser;

            if (one.MilitaryPower > two.MilitaryPower)
            {
                winner = one;
                looser = two;
            }
            else if (one.MilitaryPower < two.MilitaryPower)
            {
                winner = two;
                looser = one;
            }
            else
            {
                if (one.Weapons.Any(x => x.GetType().Name == nameof(NuclearWeapon)) &&
                !two.Weapons.Any(x => x.GetType().Name == nameof(NuclearWeapon)))
                {
                    winner = one;
                    looser = two;
                }
                else if (!one.Weapons.Any(x => x.GetType().Name == nameof(NuclearWeapon)) &&
                two.Weapons.Any(x => x.GetType().Name == nameof(NuclearWeapon)))
                {
                    winner = two;
                    looser = one;
                }
                else
                {
                    one.Spend(one.Budget * 0.5);
                    two.Spend(two.Budget * 0.5);

                    return OutputMessages.NoWinner;
                }
            }

            double additionalForcesProfit = looser.Army.Sum(x => x.Cost) + looser.Weapons.Sum(x => x.Price);
            winner.Spend(winner.Budget * 0.5);
            looser.Spend(looser.Budget * 0.5);

            winner.Profit(looser.Budget);
            winner.Profit(additionalForcesProfit);
            planets.RemoveItem(looser.Name);

            return string.Format(OutputMessages.WinnigTheWar, winner.Name, looser.Name);
        }

        public string SpecializeForces(string planetName)
        {
            IPlanet planet = planets.FindByName(planetName);

            if (planet == null)
            {
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            int count = planet.Army.Count();

            if (count == 0)
            {
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.NoUnitsFound));
            }

            foreach (var unit in planet.Army)
            {
                unit.IncreaseEndurance();
            }

            planet.Spend(1.25);
            return string.Format(OutputMessages.ForcesUpgraded, planetName);
        }
    }
}
