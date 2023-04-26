using NUnit.Framework;
using System;

namespace RepairShop.Tests
{
    public class Tests
    {
        public class RepairsShopTests
        {

        }

        [Test]
        public void Test1()
        {
            Car car = new Car("VW", 3);

            Assert.AreEqual("VW", car.CarModel);
            Assert.AreEqual(3, car.NumberOfIssues);
            Assert.AreEqual(false, car.IsFixed);
        }

        [Test]
        public void Test2()
        {
            Garage garage = new Garage("CarService", 3);

            Assert.AreEqual("CarService", garage.Name);
            Assert.AreEqual(3, garage.MechanicsAvailable);
            Assert.AreEqual(0, garage.CarsInGarage);
        }
        [Test]
        public void Test3()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Garage garage = new Garage("", 3);

            });
        }

        [Test]
        public void Test4()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Garage garage = new Garage("CarService", 0);

            });
        }

        [Test]
        public void Test5()
        {
            Garage garage = new Garage("CarService", 3);
            Car car = new Car("VW", 2);

            garage.AddCar(car);

            Assert.AreEqual(1, garage.CarsInGarage);
        }

        [Test]
        public void Test6()
        {
            Garage garage = new Garage("CarService", 1);
            Car car = new Car("VW", 2);
            Car carOne = new Car("Opel", 2);

            garage.AddCar(car);
            Assert.AreEqual(1, garage.CarsInGarage);

            Assert.Throws<InvalidOperationException>(() =>
            {
                garage.AddCar(carOne);
            });
        }

        [Test]
        public void Test7()
        {
            Garage garage = new Garage("CarService", 4);
            Car car = new Car("VW", 2);
            Car carOne = new Car("Opel", 2);
            Car carTwo = new Car("BMW", 2);

            garage.AddCar(car);
            garage.AddCar(carOne);
            garage.AddCar(carTwo);

            garage.FixCar("VW");
            garage.FixCar("Opel");
            garage.FixCar("BMW");

            Assert.AreEqual(0, car.NumberOfIssues);
            Assert.AreEqual(0, carOne.NumberOfIssues);
            Assert.AreEqual(0, carTwo.NumberOfIssues);
        }

        [Test]
        public void Test8()
        {
            Garage garage = new Garage("CarService", 4);
            Car car = new Car("VW", 2);
            Car carOne = new Car("Opel", 2);
            Car carTwo = new Car("BMW", 2);

            garage.AddCar(car);
            garage.AddCar(carOne);
            garage.AddCar(carTwo);

            Assert.Throws<InvalidOperationException>(() =>
            {
                garage.FixCar("eray");
            });
        }

        [Test]
        public void Test9()
        {
            Garage garage = new Garage("CarService", 4);
            Car car = new Car("VW", 2);
            Car carOne = new Car("Opel", 2);
            Car carTwo = new Car("BMW", 2);

            garage.AddCar(car);
            garage.AddCar(carOne);
            garage.AddCar(carTwo);

            garage.FixCar("VW");
            garage.FixCar("Opel");
            garage.FixCar("BMW");

            Assert.AreEqual(0, car.NumberOfIssues);
            Assert.AreEqual(0, carOne.NumberOfIssues);
            Assert.AreEqual(0, carTwo.NumberOfIssues);

            garage.RemoveFixedCar();

            Assert.AreEqual(0, garage.CarsInGarage);

            Assert.Throws<InvalidOperationException>(() =>
            {
                garage.RemoveFixedCar();
            });
        }

        [Test]
        public void Test10()
        {
            Garage garage = new Garage("CarService", 4);
            Car car = new Car("VW", 2);
            Car carOne = new Car("Opel", 2);
            Car carTwo = new Car("BMW", 2);

            garage.AddCar(car);
            garage.AddCar(carOne);
            garage.AddCar(carTwo);

            string actualResult = garage.Report();
            string expectedResult = "There are 3 which are not fixed: VW, Opel, BMW.";

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}