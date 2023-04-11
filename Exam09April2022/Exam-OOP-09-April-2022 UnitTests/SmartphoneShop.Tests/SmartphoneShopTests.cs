using NUnit.Framework;
using System;

namespace SmartphoneShop.Tests
{
    [TestFixture]
    public class SmartphoneShopTests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void Test1()
        {
            Smartphone smartphone = new Smartphone("IPhone", 100);

            Assert.AreEqual("IPhone", smartphone.ModelName);
            Assert.AreEqual(100, smartphone.MaximumBatteryCharge);
            Assert.AreEqual(100, smartphone.CurrentBateryCharge);
        }

        [Test]
        public void Test2()
        {
            Shop shop = new Shop(100);

            Assert.AreEqual(100, shop.Capacity);
            Assert.AreEqual(0, shop.Count);
        }

        [Test]
        public void Test3()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Shop shop = new Shop(-1);
            });
        }

        [Test]
        public void Test4()
        {
            Smartphone smartphone = new Smartphone("IPhone", 100);
            Shop shop = new Shop(100);

            shop.Add(smartphone);

            Assert.AreEqual(1,shop.Count);
        }

        [Test]
        public void Test5()
        {
            Smartphone smartphone = new Smartphone("IPhone", 100);
            Smartphone smartphoneOne = new Smartphone("IPhone", 100);
            Shop shop = new Shop(3);

            shop.Add(smartphone);

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.Add(smartphoneOne);
            });
        }

        [Test]
        public void Test6()
        {
            Smartphone smartphone = new Smartphone("IPhone", 100);
            Smartphone smartphoneOne = new Smartphone("IPhone", 100);
            Shop shop = new Shop(1);

            shop.Add(smartphone);

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.Add(smartphoneOne);
            });
        }

        [Test]
        public void Test7()
        {
            Shop shop = new Shop(1);

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.Remove("IPhone");
            });
        }

        [Test]
        public void Test8()
        {
            Smartphone smartphone = new Smartphone("IPhone", 100);
            Smartphone smartphoneOne = new Smartphone("IPhone1", 100);
            Shop shop = new Shop(2);

            shop.Add(smartphone);
            shop.Add(smartphoneOne);

            shop.Remove("IPhone");
            Assert.AreEqual(1,shop.Count);

            shop.Remove("IPhone1");
            Assert.AreEqual(0, shop.Count);

        }

        [Test]
        public void Test9()
        {
            Smartphone smartphone = new Smartphone("IPhone", 100);
            Smartphone smartphoneOne = new Smartphone("IPhone1", 100);
            Smartphone smartphoneTwo = new Smartphone("IPhone2", 50);

            Shop shop = new Shop(3);

            shop.Add(smartphone);
            shop.Add(smartphoneOne);
            shop.Add(smartphoneTwo);

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.TestPhone("IPhoneasdasf", 50);
            });

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.TestPhone("IPhone2", 100);
            });

            shop.TestPhone("IPhone", 10);

            Assert.AreEqual(90, smartphone.CurrentBateryCharge);
        }

        [Test]
        public void Test10()
        {
            Smartphone smartphone = new Smartphone("IPhone", 100);
            Smartphone smartphoneOne = new Smartphone("IPhone1", 100);
            Smartphone smartphoneTwo = new Smartphone("IPhone2", 50);

            Shop shop = new Shop(3);

            shop.Add(smartphone);
            shop.Add(smartphoneOne);
            shop.Add(smartphoneTwo);

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.ChargePhone("IPhoneasdasf");
            });

            shop.TestPhone("IPhone", 90);
            shop.ChargePhone("IPhone");

            Assert.AreEqual(100, smartphone.CurrentBateryCharge);
        }
    }
}