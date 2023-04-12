using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace RobotService.Repositories
{
    public class SupplementRepository : IRepository<ISupplement>
    {
        private List<ISupplement> models;

        public SupplementRepository()
        {
            models = new List<ISupplement>();
        }

        public void AddNew(ISupplement model)
        {
            models.Add(model);
        }

        public ISupplement FindByStandard(int interfaceStandard)
        {
            return models.FirstOrDefault(x => x.InterfaceStandard == interfaceStandard);
        }

        public IReadOnlyCollection<ISupplement> Models() => models.AsReadOnly();

        public bool RemoveByName(string typeName)
        {
            if (models.Any(m => m.GetType().Name==typeName))
            {
                models.Remove(models.First(m => m.GetType().Name == typeName));
                return true;
            }

            return false;
        }
    }
}
