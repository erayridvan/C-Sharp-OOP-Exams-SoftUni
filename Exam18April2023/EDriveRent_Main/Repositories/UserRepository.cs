using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EDriveRent.Repositories
{
    public class UserRepository : IRepository<IUser>
    {
        private List<IUser> users;

        public UserRepository()
        {
            users = new List<IUser>();
        }

        public IReadOnlyCollection<IUser> GetAll() => users.AsReadOnly();

        public void AddModel(IUser model)
        {
            users.Add(model);
        }

        public IUser FindById(string identifier)
        {
            return users.FirstOrDefault(u => u.DrivingLicenseNumber == identifier);
        }

        public bool RemoveById(string identifier)
        {
            IUser currentUser = users.FirstOrDefault(u => u.DrivingLicenseNumber == identifier);

            if (currentUser != null)
            {
                users.Remove(currentUser);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
