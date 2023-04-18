using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace EDriveRent.Repositories
{
    public class VehicleRepository : IRepository<IVehicle>
    {
        private List<IVehicle> vehicles;

        public VehicleRepository()
        {
            vehicles = new List<IVehicle>();
        }

        public IReadOnlyCollection<IVehicle> GetAll() => vehicles.AsReadOnly();

        public void AddModel(IVehicle model)
        {
            vehicles.Add(model);
        }

        public IVehicle FindById(string identifier)
        {
            return vehicles.FirstOrDefault(v => v.LicensePlateNumber == identifier);
        }

        public bool RemoveById(string identifier)
        {
            IVehicle currentVehicle = vehicles.FirstOrDefault(v => v.LicensePlateNumber == identifier);

            if (currentVehicle!=null)
            {
                vehicles.Remove(currentVehicle);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
