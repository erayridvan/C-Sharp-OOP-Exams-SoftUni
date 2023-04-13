using NUnit.Framework;
using System;
using System.Numerics;

namespace PlanetWars.Tests
{
    public class Tests
    {
        [TestFixture]
        public class PlanetWarsTests
        {

        }

        [Test]
        public void Test1()
        {
            Weapon weapon = new Weapon("eray", 100, 500);

            Assert.AreEqual("eray", weapon.Name);
            Assert.AreEqual(100, weapon.Price);
            Assert.AreEqual(500, weapon.DestructionLevel);
        }

        [Test]
        public void Test2()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Weapon weapon = new Weapon("eray", -1, 500);
            });
        }

        [Test]
        public void Test3()
        {
            Weapon weapon = new Weapon("eray", 100, 500);

            weapon.IncreaseDestructionLevel();
            weapon.IncreaseDestructionLevel();

            Assert.AreEqual("eray", weapon.Name);
            Assert.AreEqual(100, weapon.Price);
            Assert.AreEqual(502, weapon.DestructionLevel);
        }

        [Test]
        public void Test4()
        {
            Weapon weapon = new Weapon("eray", 100, 500);

            weapon.IncreaseDestructionLevel();
            weapon.IncreaseDestructionLevel();

            Assert.AreEqual("eray", weapon.Name);
            Assert.AreEqual(100, weapon.Price);
            Assert.AreEqual(502, weapon.DestructionLevel);
            Assert.AreEqual(true, weapon.IsNuclear);
        }

        [Test]
        public void Test5()
        {
            Planet planet = new Planet("mars", 300);


            Assert.AreEqual("mars", planet.Name);
            Assert.AreEqual(300, planet.Budget);
            Assert.AreEqual(0, planet.Weapons.Count);
        }

        [Test]
        public void Test6()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Planet planet = new Planet("", 300);
            });
        }

        [Test]
        public void Test7()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Planet planet = new Planet("mars", -300);
            });
        }

        [Test]
        public void Test8()
        {
            Planet planet = new Planet("mars", 300);
            Weapon weaponOne = new Weapon("eray", 100, 500);

            planet.AddWeapon(weaponOne);
            Assert.AreEqual(1, planet.Weapons.Count);

            Weapon weaponTwo = new Weapon("eray", 100, 500);
            Assert.Throws<InvalidOperationException>(() =>
            {
                planet.AddWeapon(weaponTwo);
            });
        }

        [Test]
        public void Test9()
        {
            Planet planet = new Planet("mars", 300);
            Weapon weaponOne = new Weapon("eray", 100, 500);
            Weapon weaponTwo = new Weapon("zayde", 100, 500);

            planet.AddWeapon(weaponOne);
            planet.AddWeapon(weaponTwo);

            Assert.AreEqual(2, planet.Weapons.Count);

            planet.RemoveWeapon("zayde");
            Assert.AreEqual(1, planet.Weapons.Count);
        }

        [Test]
        public void Test10()
        {
            Planet planet = new Planet("mars", 300);
            Weapon weaponOne = new Weapon("eray", 100, 5);
            Weapon weaponTwo = new Weapon("zayde", 100, 5);
            Weapon weaponThree = new Weapon("erdem", 100, 5);

            planet.AddWeapon(weaponOne);
            planet.AddWeapon(weaponTwo);
            planet.AddWeapon(weaponThree);

            Assert.AreEqual(3, planet.Weapons.Count);

            planet.UpgradeWeapon("eray");
            planet.UpgradeWeapon("zayde");

            Assert.AreEqual(6, weaponOne.DestructionLevel);
            Assert.AreEqual(6, weaponTwo.DestructionLevel);

            Assert.Throws<InvalidOperationException>(() =>
            {
                planet.UpgradeWeapon("erdemcho");
            });
        }

        [Test]
        public void Test11()
        {
            Planet planet = new Planet("mars", 300);

            planet.SpendFunds(155);

            Assert.AreEqual(145, planet.Budget);
        }

        [Test]
        public void Test12()
        {
            Planet planet = new Planet("mars", 300);

            planet.Profit(155);

            Assert.AreEqual(455, planet.Budget);
        }

        [Test]
        public void Test13()
        {
            Planet planet = new Planet("mars", 300);
            Weapon weaponOne = new Weapon("eray", 100, 5);
            Weapon weaponTwo = new Weapon("zayde", 100, 5);
            Weapon weaponThree = new Weapon("erdem", 100, 5);

            planet.AddWeapon(weaponOne);
            planet.AddWeapon(weaponTwo);
            planet.AddWeapon(weaponThree);

            double actualResult=planet.MilitaryPowerRatio;

            Assert.AreEqual(15, actualResult);
        }

        [Test]
        public void Test14()
        {
            Planet planetOne = new Planet("mars", 300);
            Planet planetTwo = new Planet("mars1", 300);

            Weapon weaponOne = new Weapon("eray", 100, 5);
            Weapon weaponTwo = new Weapon("zayde", 200, 10);
            Weapon weaponThree = new Weapon("erdem", 100, 6);
            Weapon weaponFour = new Weapon("emir", 122, 5);

            planetOne.AddWeapon(weaponOne);
            planetOne.AddWeapon(weaponTwo);
            planetTwo.AddWeapon(weaponThree);
            planetTwo.AddWeapon(weaponFour);

            string actualResult=planetOne.DestructOpponent(planetTwo);

            string expectedResult = "mars1 is destructed!";

            Assert.AreEqual(expectedResult,actualResult);
        }

        [Test]
        public void Test15()
        {
            Planet planetOne = new Planet("mars", 300);
            Planet planetTwo = new Planet("mars1", 300);

            Weapon weaponOne = new Weapon("eray", 100, 5);
            Weapon weaponTwo = new Weapon("zayde", 200, 5);
            Weapon weaponThree = new Weapon("erdem", 100, 6);
            Weapon weaponFour = new Weapon("emir", 122, 5);

            planetOne.AddWeapon(weaponOne);
            planetOne.AddWeapon(weaponTwo);
            planetTwo.AddWeapon(weaponThree);
            planetTwo.AddWeapon(weaponFour);

            Assert.Throws<InvalidOperationException>(() =>
            {
                planetOne.DestructOpponent(planetTwo);
            });
        }

        [Test]
        public void Test16()
        {
            Planet planet = new Planet("mars", 300);

            
            Assert.Throws<InvalidOperationException>(() =>
            {
                planet.SpendFunds(301);
            });
        }
    }
}
