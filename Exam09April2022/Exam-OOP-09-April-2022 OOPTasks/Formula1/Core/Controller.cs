﻿using Formula1.Core.Contracts;
using Formula1.Models;
using Formula1.Models.Contracts;
using Formula1.Repositories;
using Formula1.Utilities;
using System;
using System.Linq;
using System.Text;

namespace Formula1.Core
{
    public class Controller : IController
    {
        private PilotRepository pilotRepository;
        private RaceRepository raceRepository;
        private FormulaOneCarRepository carRepository;

        public Controller()
        {
            pilotRepository = new PilotRepository();
            raceRepository = new RaceRepository();
            carRepository = new FormulaOneCarRepository();
        }

        public string AddCarToPilot(string pilotName, string carModel)
        {
            IPilot pilot = pilotRepository.FindByName(pilotName);
            IFormulaOneCar car = carRepository.FindByName(carModel);

            if (pilot == null || pilot.Car != null)
            {
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.PilotDoesNotExistOrHasCarErrorMessage, pilotName));
            }

            if (car == null)
            {
                throw new NullReferenceException
                    (string.Format(ExceptionMessages.CarDoesNotExistErrorMessage, carModel));
            }

            pilot.AddCar(car);
            carRepository.Remove(car);

            return string.Format(OutputMessages.SuccessfullyPilotToCar, pilotName, car.GetType().Name, carModel);
        }

        public string AddPilotToRace(string raceName, string pilotFullName)
        {
            IPilot pilot = pilotRepository.FindByName(pilotFullName);
            IRace race = raceRepository.FindByName(raceName);

            if (race == null)
            {
                throw new NullReferenceException
                    (string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }

            if (pilot == null || !pilot.CanRace || race.Pilots.Any(p => p.FullName == pilotFullName))
            {
                throw new InvalidOperationException
                   (string.Format(ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));
            }

            race.AddPilot(pilot);

            return string.Format(OutputMessages.SuccessfullyAddPilotToRace,pilotFullName,raceName);
        }

        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
        {
            IFormulaOneCar car;
            if (type != "Ferrari" && type != "Williams")
            {
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.InvalidTypeCar, type));
            }

            if (carRepository.Models.Any(c => c.Model == model))
            {
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.CarExistErrorMessage, model));
            }

            if (type == nameof(Ferrari))
            {
                car =
                    new Ferrari(model, horsepower, engineDisplacement);
            }
            else
            {
                car=
                    new Williams(model, horsepower, engineDisplacement);
            }

            carRepository.Add(car);
            return string.Format(OutputMessages.SuccessfullyCreateCar, type, model);

        }

        public string CreatePilot(string fullName)
        {
            IPilot pilot = new Pilot(fullName);

            if (pilotRepository.Models.Any(p => p.FullName == fullName))
            {
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.PilotExistErrorMessage, fullName));
            }

            pilotRepository.Add(pilot);

            return string.Format(OutputMessages.SuccessfullyCreatePilot, fullName);
        }

        public string CreateRace(string raceName, int numberOfLaps)
        {
            IRace race = new Race(raceName, numberOfLaps);

            if (raceRepository.Models.Any(r => r.RaceName == raceName))
            {
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.RaceExistErrorMessage, raceName));
            }

            raceRepository.Add(race);
            return string.Format
                (OutputMessages.SuccessfullyCreateRace, raceName);
        }

        public string PilotReport()
        {
            StringBuilder sb = new StringBuilder();

            var pilots = pilotRepository
                .Models
                .OrderByDescending(x => x.NumberOfWins);

            foreach (var pilot in pilots)
            {
                sb.AppendLine(pilot.ToString());
            }

            return sb.ToString().Trim();
        }

        public string RaceReport()
        {
            var races = raceRepository.Models.Where(r => r.TookPlace == true);

            StringBuilder sb = new StringBuilder();

            foreach (var race in races)
            {
                sb.AppendLine(race.RaceInfo());
            }

            return sb.ToString().TrimEnd();
        }

        public string StartRace(string raceName)
        {

            var race = raceRepository.FindByName(raceName);

            if (race == null)
            {
                throw new NullReferenceException
                    (string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }

            if (race.Pilots.Count() < 3)
            {
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.InvalidRaceParticipants, raceName));
            }

            if (race.TookPlace)
            {
                throw new InvalidOperationException
                    (string.Format(ExceptionMessages.RaceTookPlaceErrorMessage, raceName));
            }

            var orderdPilots = race.Pilots.OrderByDescending
                (x => x.Car.RaceScoreCalculator(race.NumberOfLaps)).Take(3);

            var winner = orderdPilots.First();
            winner.WinRace();
            race.TookPlace = true;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Pilot {orderdPilots.First().FullName} wins the {raceName} race.");
            sb.AppendLine($"Pilot {orderdPilots.Skip(1).First().FullName} is second in the {raceName} race.");
            sb.AppendLine($"Pilot {orderdPilots.Skip(2).First().FullName} is third in the {raceName} race.");

            return sb.ToString().TrimEnd();
        }
    }
}
