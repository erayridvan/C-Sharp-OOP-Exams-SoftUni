using RobotService.Models;
using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotService.Repositories
{
    public class RobotRepository : IRepository<IRobot>
    {
        private List<IRobot> models;

        public RobotRepository()
        {
            models = new List<IRobot>();
        }
        public void AddNew(IRobot model)
        {
            models.Add(model);
        }

        public IRobot FindByStandard(int interfaceStandard)
            => models.FirstOrDefault(r => r.InterfaceStandards.Contains(interfaceStandard));

        public IReadOnlyCollection<IRobot> Models() => models.AsReadOnly();

        public bool RemoveByName(string typeName)
        {
            IRobot robot = models.FirstOrDefault(r => r.Model == typeName);

            if (robot != null)
            {
                models.Remove(robot);
                return true;
            }

            return false;
        }
    }
}
