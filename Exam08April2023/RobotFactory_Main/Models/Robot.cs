using RobotService.Models.Contracts;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotService.Models
{
    public abstract class Robot : IRobot
    {
        private string model;
        private int batteryCapacity;
        private int batteryLevel;
        private int convertionCapacityIndex;
        private List<int> interfaceStandards;

        public Robot(string model, int batteryCapacity, int conversionCapacityIndex)
        {
            Model = model;
            BatteryCapacity = batteryCapacity;
            BatteryLevel = batteryCapacity;
            ConvertionCapacityIndex = conversionCapacityIndex;
            interfaceStandards = new List<int>();
        }

        public string Model
        {
            get => model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException
                        (string.Format(ExceptionMessages.ModelNullOrWhitespace));
                }

                model = value;
            }
        }

        public int BatteryCapacity
        {
            get => batteryCapacity;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException
                        (string.Format(ExceptionMessages.BatteryCapacityBelowZero, value));
                }

                batteryCapacity = value;
            }
        }

        public int BatteryLevel
        {
            get { return batteryLevel; }
            private set { batteryLevel = value; }
        }

        public int ConvertionCapacityIndex
        {
            get { return convertionCapacityIndex; }
            private set { convertionCapacityIndex = value; }
        }

        public IReadOnlyCollection<int> InterfaceStandards
            => interfaceStandards.AsReadOnly();

        public void Eating(int minutes)
        {
            int energyProduced = ConvertionCapacityIndex * minutes;
            BatteryLevel += energyProduced;

            if (BatteryLevel > BatteryCapacity)
            {
                BatteryLevel = BatteryCapacity;
            }
        }

        public bool ExecuteService(int consumedEnergy)
        {
            if (BatteryLevel >= consumedEnergy)
            {
                BatteryLevel -= consumedEnergy;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void InstallSupplement(ISupplement supplement)
        {
            interfaceStandards.Add(supplement.InterfaceStandard);
            BatteryLevel -= supplement.BatteryUsage;
            BatteryCapacity -= supplement.BatteryUsage;
        }

        public override string ToString()
        {
            string standards =
                interfaceStandards.Count > 0 ? string.Join(" ", interfaceStandards) : "none";

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{GetType().Name} {Model}:");

            sb.AppendLine($"--Maximum battery capacity: {BatteryCapacity}");
            sb.AppendLine($"--Current battery level: {BatteryLevel}");
            sb.AppendLine($"--Supplements installed: {standards.TrimEnd()}");

            return sb.ToString().TrimEnd();
        }
    }
}
