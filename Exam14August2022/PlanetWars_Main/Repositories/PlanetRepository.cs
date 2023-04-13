using PlanetWars.Models.Planets;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace PlanetWars.Repositories
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        private List<IPlanet> models;

        public PlanetRepository()
        {
            models = new List<IPlanet>();
        }

        public IReadOnlyCollection<IPlanet> Models => this.models.AsReadOnly();

        public void AddItem(IPlanet model)
        {
            models.Add(model);
        }

        public IPlanet FindByName(string name)
        {
            IPlanet planet = models.FirstOrDefault(m => m.Name == name);

            return planet;
        }

        public bool RemoveItem(string name)
        {
            IPlanet planet = models.FirstOrDefault(m => m.Name == name);

            if (planet is null)
            {
                return false;
            }

            models.Remove(planet);
            return true;
        }
    }
}
