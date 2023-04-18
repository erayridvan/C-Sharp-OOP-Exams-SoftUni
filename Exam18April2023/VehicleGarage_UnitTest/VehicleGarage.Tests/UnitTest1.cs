using NUnit.Framework;

namespace VehicleGarage.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_1()
        {
            Vehicle vehicle = new Vehicle("Opel", "Meriva", "CC0101AC", 100250);

            Assert.AreEqual("Opel", vehicle.Brand);
            Assert.AreEqual("Meriva", vehicle.Model);
            Assert.AreEqual("CC0101AC", vehicle.LicensePlateNumber);
            Assert.AreEqual(100, vehicle.BatteryLevel);
            Assert.AreEqual(false, vehicle.IsDamaged);
        }

        [Test]
        public void Test_2()
        {
            Garage garage = new Garage(2);

            Assert.AreEqual(2, garage.Capacity);
            Assert.AreEqual(0, garage.Vehicles.Count);
        }

        [Test]
        public void Test_3()
        {
            Garage garage = new Garage(2);
            Vehicle vehicle = new Vehicle("Opel", "Meriva", "CC0101AC", 100250);


            Assert.AreEqual(true, garage.AddVehicle(vehicle));
            Assert.AreEqual(1, garage.Vehicles.Count);
        }

        [Test]
        public void Test_4()
        {
            Garage garage = new Garage(1);
            Vehicle vehicle = new Vehicle("Opel", "Meriva", "CC0101AC", 100250);
            Vehicle vehicleOne = new Vehicle("Opel", "Astra", "CC0101CC", 100250);


            Assert.AreEqual(true, garage.AddVehicle(vehicle));
            Assert.AreEqual(false, garage.AddVehicle(vehicleOne));
            Assert.AreEqual(1, garage.Vehicles.Count);
        }

        [Test]
        public void Test_5()
        {
            Garage garage = new Garage(1);
            Vehicle vehicle = new Vehicle("Opel", "Meriva", "CC0101AC", 100250);


            Assert.AreEqual(true, garage.AddVehicle(vehicle));
            Assert.AreEqual(false, garage.AddVehicle(vehicle));
            Assert.AreEqual(1, garage.Vehicles.Count);
        }

        [Test]
        public void Test_6()
        {
            Garage garage = new Garage(5);

            Vehicle vehFive = new Vehicle("Opel", "Ka", "AC0101CC", 100850);

            garage.AddVehicle(vehFive);

            garage.DriveVehicle("AC0101CC", 50, false);

            Assert.AreEqual(50, vehFive.BatteryLevel);
            Assert.AreEqual(false, vehFive.IsDamaged);
        }

        [Test]
        public void Test_7()
        {
            Garage garage = new Garage(5);

            Vehicle vehOne = new Vehicle("Opel", "Meriva", "CC0101AC", 100250);

            garage.AddVehicle(vehOne);

            garage.DriveVehicle("CC0101AC", 50, true);

            Assert.AreEqual(50, vehOne.BatteryLevel);
            Assert.AreEqual(true, vehOne.IsDamaged);
        }

        [Test]
        public void Test_8()
        {
            Garage garage = new Garage(5);

            Vehicle meriva = new Vehicle("Opel", "Meriva", "CA0101AC", 100250);
            Vehicle corsa = new Vehicle("Opel", "Corsa", "AC0101AC", 100250);
            Vehicle tigra = new Vehicle("Opel", "Tigra", "CC0101CC", 100250);

            garage.AddVehicle(meriva);
            garage.AddVehicle(corsa);
            garage.AddVehicle(tigra);

            garage.DriveVehicle("CA0101AC", 50, true);
            garage.DriveVehicle("AC0101AC", 70, true);
            garage.DriveVehicle("CC0101CC", 90, false);

            string actualResult = garage.RepairVehicles();
            string expectedResult = "Vehicles repaired: 2";
            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(false,meriva.IsDamaged);
            Assert.AreEqual(false,corsa.IsDamaged);
            Assert.AreEqual(false,tigra.IsDamaged);
        }

        [Test]
        public void Test_9()
        {
            Garage garage = new Garage(5);

            Vehicle meriva = new Vehicle("Opel", "Meriva", "CA0101AC", 100250);
            Vehicle corsa = new Vehicle("Opel", "Corsa", "AC0101AC", 100250);
            Vehicle tigra = new Vehicle("Opel", "Tigra", "CC0101CC", 100250);

            garage.AddVehicle(meriva);
            garage.AddVehicle(corsa);
            garage.AddVehicle(tigra);

            garage.DriveVehicle("CA0101AC", 50, true);
            garage.DriveVehicle("AC0101AC", 70, true);
            garage.DriveVehicle("CC0101CC", 90, false);

            int actualResult = garage.ChargeVehicles(50);
            int expectedResult = 3;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Test_10()
        {
            Garage garage = new Garage(5);

            Vehicle vehFive = new Vehicle("Opel", "Ka", "AC0101CC", 100850);

            garage.AddVehicle(vehFive);

            garage.DriveVehicle("AC0101CC", 50, false);

            Assert.AreEqual(50, vehFive.BatteryLevel);
            Assert.AreEqual(false, vehFive.IsDamaged);

            garage.DriveVehicle("AC0101CC", 40, true);

            Assert.AreEqual(10, vehFive.BatteryLevel);
            Assert.AreEqual(true, vehFive.IsDamaged);

            garage.DriveVehicle("AC0101CC", 40, true);

            Assert.AreEqual(10, vehFive.BatteryLevel);
            Assert.AreEqual(true, vehFive.IsDamaged);
        }

        [Test]
        public void Test_11()
        {
            Garage garage = new Garage(5);

            Vehicle vehFive = new Vehicle("Opel", "Ka", "AC0101CC", 100850);

            garage.AddVehicle(vehFive);

            garage.DriveVehicle("AC0101CC", 150, false);

            Assert.AreEqual(100, vehFive.BatteryLevel);
            Assert.AreEqual(false, vehFive.IsDamaged);

        }

        [Test]
        public void Test_12()
        {
            Garage garage = new Garage(5);

            Vehicle vehFive = new Vehicle("Opel", "Ka", "AC0101CC", 100850);

            garage.AddVehicle(vehFive);

            garage.DriveVehicle("AC0101CC", 70, false);

            Assert.AreEqual(30, vehFive.BatteryLevel);
            Assert.AreEqual(false, vehFive.IsDamaged);

            garage.DriveVehicle("AC0101CC", 70, false);

            Assert.AreEqual(30, vehFive.BatteryLevel);
            Assert.AreEqual(false, vehFive.IsDamaged);
        }

        [Test]
        public void Test_13()
        {
            Garage garage = new Garage(5);

            Vehicle vehFive = new Vehicle("Opel", "Ka", "AC0101CC", 100850);

            garage.AddVehicle(vehFive);

            garage.DriveVehicle("AC0101CC", 40, true);

            Assert.AreEqual(60, vehFive.BatteryLevel);
            Assert.AreEqual(true, vehFive.IsDamaged);

            garage.DriveVehicle("AC0101CC", 30, false);

            Assert.AreEqual(60, vehFive.BatteryLevel);
            Assert.AreEqual(true, vehFive.IsDamaged);
        }

        [Test]
        public void Test_14()
        {
            Garage garage = new Garage(2);
            Vehicle vehicle = new Vehicle("Opel", "Meriva", "CC0101AC", 100250);

            Assert.AreEqual(true, garage.AddVehicle(vehicle));
            Assert.AreEqual(1, garage.Vehicles.Count);

            Assert.AreEqual(false, garage.AddVehicle(vehicle));

        }

        [Test]
        public void Test_15()
        {
            Garage garage = new Garage(1);
            Vehicle vehicle = new Vehicle("Opel", "Meriva", "CC0101AC", 100250);
            Vehicle vehicleOne = new Vehicle("Opel", "Corsa", "CC0101AC", 100250);

            Assert.AreEqual(true, garage.AddVehicle(vehicle));
            Assert.AreEqual(1, garage.Vehicles.Count);

            Assert.AreEqual(false, garage.AddVehicle(vehicleOne));

        }

        [Test]
        public void Test_16()
        {
            Garage garage = new Garage(1);
            Vehicle vehicle = new Vehicle("Opel", "Meriva", "CC0101AC", 100250);

            Assert.AreEqual(true, garage.AddVehicle(vehicle));
            Assert.AreEqual(1, garage.Vehicles.Count);

            Assert.AreEqual(false, garage.AddVehicle(vehicle));

        }

        [Test]
        public void Test_17()
        {
            Garage garage = new Garage(5);

            Vehicle one = new Vehicle("Opel", "Ka", "AC0101AC", 100850);
            Vehicle two = new Vehicle("Opel", "Kuga", "AC0101CC", 100850);

            garage.AddVehicle(one);
            garage.AddVehicle(two);

            garage.DriveVehicle("AC0101AC", 40, false);
            garage.DriveVehicle("AC0101CC", 40, false);

            int actualResult = garage.ChargeVehicles(70);
            int expectedResult = 2;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void Test_18()
        {
            Garage garage = new Garage(5);

            Vehicle meriva = new Vehicle("Opel", "Meriva", "CA0101AC", 100250);
            Vehicle corsa = new Vehicle("Opel", "Corsa", "AC0101AC", 100250);
            Vehicle tigra = new Vehicle("Opel", "Tigra", "CC0101CC", 100250);

            garage.AddVehicle(meriva);
            garage.AddVehicle(corsa);
            garage.AddVehicle(tigra);

            garage.DriveVehicle("CA0101AC", 50, true);
            garage.DriveVehicle("AC0101AC", 50, true);
            garage.DriveVehicle("CC0101CC", 50, false);

            int actualResult = garage.ChargeVehicles(40);
            int expectedResult = 0;
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}