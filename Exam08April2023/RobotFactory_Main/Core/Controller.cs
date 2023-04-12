using RobotService.Core.Contracts;
using RobotService.Models;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using RobotService.Utilities.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotService.Core
{
    public class Controller : IController
    {
        private RobotRepository robots;
        private SupplementRepository supplements;

        public Controller()
        {
            robots = new RobotRepository();
            supplements = new SupplementRepository();
        }

        public string CreateRobot(string model, string typeName)
        {
            if (typeName != nameof(DomesticAssistant) && typeName != nameof(IndustrialAssistant))
            {
                return string.Format(OutputMessages.RobotCannotBeCreated, typeName);
            }

            IRobot robot;

            if (typeName == nameof(DomesticAssistant))
            {
                robot = new DomesticAssistant(model);
            }
            else
            {
                robot = new IndustrialAssistant(model);
            }

            robots.AddNew(robot);

            return string.Format(OutputMessages.RobotCreatedSuccessfully, typeName, model);
        }

        public string CreateSupplement(string typeName)
        {
            if (typeName != nameof(SpecializedArm) && typeName != nameof(LaserRadar))
            {
                return string.Format(OutputMessages.SupplementCannotBeCreated, typeName);
            }

            ISupplement supplement;

            if (typeName == nameof(SpecializedArm))
            {
                supplement = new SpecializedArm();
            }
            else
            {
                supplement = new LaserRadar();
            }

            supplements.AddNew(supplement);

            return string.Format(OutputMessages.SupplementCreatedSuccessfully, typeName);
        }

        public string PerformService(string serviceName, int intefaceStandard, int totalPowerNeeded)
        {
            int availablePOwer = 0;
            int usedRobotsCount = 0;

            List<IRobot> robotsToPerformService =
                robots.Models()
                .Where(x => x.InterfaceStandards.Contains(intefaceStandard))
                .ToList();

            if (robotsToPerformService.Count == 0)
            {
                return string.Format(OutputMessages.UnableToPerform, intefaceStandard);
            }

            var orderedList = robotsToPerformService.OrderByDescending(x => x.BatteryLevel);

            availablePOwer = robotsToPerformService.Sum(s => s.BatteryLevel);

            if (availablePOwer < totalPowerNeeded)
            {
                return string.Format
                    (OutputMessages.MorePowerNeeded, serviceName,
                    totalPowerNeeded - availablePOwer);
            }

            foreach (IRobot robot in orderedList)
            {
                if (robot.BatteryLevel >= totalPowerNeeded)
                {
                    robot.ExecuteService(totalPowerNeeded);
                    usedRobotsCount++;
                    break;
                }
                else
                {
                    totalPowerNeeded -= robot.BatteryLevel;
                    robot.ExecuteService(robot.BatteryLevel);
                    usedRobotsCount++;
                }
            }

            return string.Format
                (OutputMessages.PerformedSuccessfully, serviceName, usedRobotsCount);
        }

        public string Report()
        {
            List<IRobot> robotsForRepor = robots.Models()
                .OrderByDescending(r => r.BatteryLevel)
                .ThenBy(r => r.BatteryCapacity)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var robot in robotsForRepor)
            {
                sb.AppendLine(robot.ToString().TrimEnd());
            }

            return sb.ToString().TrimEnd();
        }

        public string RobotRecovery(string model, int minutes)
        {
            List<IRobot> robotsWithModel = robots.Models()
                .Where(r => r.Model == model).ToList();
            int fedCount = 0;

            foreach (Robot robot in robotsWithModel)
            {
                if (robot.BatteryLevel < robot.BatteryCapacity / 2) // Check if BatteryLevel is under 50% of BatteryCapacity
                {
                    robot.Eating(minutes); // Feed the robot using the Eating() method
                    fedCount++;
                }
            }

            return string.Format(OutputMessages.RobotsFed, fedCount);
        }

        public string UpgradeRobot(string model, string supplementTypeName)
        {
            ISupplement supplement = supplements.Models()
                .FirstOrDefault(x => x.GetType().Name == supplementTypeName);

            int interfaceValue = supplement.InterfaceStandard;

            List<IRobot> robotsA = new List<IRobot>();

            foreach (var robotA in robots.Models().Where(r => r.Model == model))
            {
                if (!robotA.InterfaceStandards.Contains(supplement.InterfaceStandard))
                {
                    robotsA.Add(robotA);
                }
            }

            if (robotsA.Count == 0)
            {
                return string.Format(OutputMessages.AllModelsUpgraded, model);
            }

            IRobot robot = robotsA[0];

            robot.InstallSupplement(supplement);
            supplements.RemoveByName(supplementTypeName);

            return string.Format(OutputMessages.UpgradeSuccessful, model, supplementTypeName);
        }
    }
}
