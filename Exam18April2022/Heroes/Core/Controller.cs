using Heroes.Core.Contracts;
using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using Heroes.Models.Map;
using Heroes.Models.Weapons;
using Heroes.Repositories;
using Heroes.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Core
{
    public class Controller : IController
    {
        private HeroRepository heroes;
        private WeaponRepository weapons;

        public Controller()
        {
            heroes = new HeroRepository();
            weapons = new WeaponRepository();
        }

        public string AddWeaponToHero(string weaponName, string heroName)
        {
            IHero hero = heroes.FindByName(heroName);
            IWeapon weapon = weapons.FindByName(weaponName);

            if (hero == null)
            {
                throw new InvalidOperationException
                    (string.Format(OutputMessages.HeroDoesNotExist, heroName));
            }

            if (weapon == null)
            {
                throw new InvalidOperationException
                    (string.Format(OutputMessages.WeaponDoesNotExist, weaponName));
            }

            if (hero.Weapon != null)
            {
                throw new InvalidOperationException
                    (string.Format(OutputMessages.HeroAlreadyHasWeapon, heroName));
            }
            hero.AddWeapon(weapon);
            weapons.Remove(weapon);

            return string.Format(OutputMessages.WeaponAddedToHero, heroName, weapon.GetType().Name.ToLower());
        }

        public string CreateHero(string type, string name, int health, int armour)
        {
            IHero hero = heroes.FindByName(name);

            if (hero != null)
            {
                throw new InvalidOperationException
                    (string.Format(OutputMessages.HeroAlreadyExist, name));
            }

            if (type == nameof(Barbarian))
            {
                hero = new Barbarian(name, health, armour);
                heroes.Add(hero);
            }
            else if (type == nameof(Knight))
            {
                hero = new Knight(name, health, armour);
                heroes.Add(hero);
            }
            else
            {
                throw new InvalidOperationException(OutputMessages.HeroTypeIsInvalid);
            }

            if (hero.GetType().Name == nameof(Barbarian))
            {
                return string.Format(OutputMessages.SuccessfullyAddedBarbarian, name);
            }
            else
            {
                return string.Format(OutputMessages.SuccessfullyAddedKnight, name);
            }
        }

        public string CreateWeapon(string type, string name, int durability)
        {
            IWeapon weapon = weapons.FindByName(name);

            if (weapon != null)
            {
                throw new InvalidOperationException
                    (string.Format(OutputMessages.WeaponAlreadyExists, name));
            }

            if (type == nameof(Claymore))
            {
                weapon = new Claymore(name, durability);
                weapons.Add(weapon);
            }
            else if (type == nameof(Mace))
            {
                weapon = new Mace(name, durability);
                weapons.Add(weapon);
            }
            else
            {
                throw new InvalidOperationException(OutputMessages.WeaponTypeIsInvalid);
            }

            return string.Format
                (OutputMessages.WeaponAddedSuccessfully, type.ToLower(), name);
        }

        public string HeroReport()
        {
            List<IHero> orderedList = heroes
                .Models
                .OrderBy(x => x.GetType().Name)
                .ThenByDescending(x => x.Health)
                .ThenBy(x => x.Name)
                .ToList();
            StringBuilder sb = new StringBuilder();

            foreach (var hero in orderedList)
            {
                sb.AppendLine($"{hero.GetType().Name}: {hero.Name}");
                sb.AppendLine($"--Health: {hero.Health}");
                sb.AppendLine($"--Armour: {hero.Armour}");
                string weapon = hero.Weapon == null ? "Unarmed" : hero.Weapon.Name;
                sb.AppendLine($"--Weapon: {weapon}");
            }

            return sb.ToString().TrimEnd();
        }

        public string StartBattle()
        {
            ICollection<IHero> players = heroes
                .Models
                .Where(h => h.IsAlive && h.Weapon != null).ToList();
            IMap map = new Map();
            string result = map.Fight(players);

            return result;
        }
    }
}
