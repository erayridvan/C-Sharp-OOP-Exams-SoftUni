using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace PlanetWars.Repositories
{
    public class WeaponRepository : IRepository<IWeapon>
    {
        private List<IWeapon> models;

        public WeaponRepository()
        {
            this.models = new List<IWeapon>();
        }

        public IReadOnlyCollection<IWeapon> Models => this.models.AsReadOnly();

        public void AddItem(IWeapon model)
        {
            models.Add(model);
        }

        public IWeapon FindByName(string name)
        {
            return models.FirstOrDefault(m => m.GetType().Name == name);
        }

        public bool RemoveItem(string name)
        {
            IWeapon weapon = models.FirstOrDefault(m => m.GetType().Name == name);

            if (weapon is null)
            {
                return false;
            }

            models.Remove(weapon);
            return true;
        }
    }
}
