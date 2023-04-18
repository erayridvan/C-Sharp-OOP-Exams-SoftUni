using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace EDriveRent.Repositories
{
    public class RouteRepository : IRepository<IRoute>
    {
        private List<IRoute> routes;

        public RouteRepository()
        {
            routes = new List<IRoute>();
        }

        public IReadOnlyCollection<IRoute> GetAll() => routes.AsReadOnly();

        public void AddModel(IRoute model)
        {
            routes.Add(model);
        }

        public IRoute FindById(string identifier)
        {
            return routes.FirstOrDefault(r => r.RouteId == int.Parse(identifier));
        }

        public bool RemoveById(string identifier)
        {
            IRoute currentRoute = routes.FirstOrDefault(r => r.RouteId == int.Parse(identifier));

            if (currentRoute != null)
            {
                routes.Remove(currentRoute);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
