using NUnit.Framework;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace RobotFactory.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Factory factory = new Factory("Factory1", 10);

            Assert.AreEqual("Factory1",factory.Name);
            Assert.AreEqual(10,factory.Capacity);
            Assert.AreEqual(0,factory.Robots.Count);
            Assert.AreEqual(0,factory.Supplements.Count);
        }
        [Test]
        public void Test2()
        {
            // Arrange
            Factory factory = new Factory("Factory1", 10);
            string name = "Supplement1";
            int interfaceStandard = 1;

            // Act
            string result = factory.ProduceSupplement(name, interfaceStandard);
            string expectedResult = $"Supplement: {name} IS: {interfaceStandard}";
            // Assert
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(1, factory.Supplements.Count);
        }

        [Test]
        public void Test3()
        {
            Factory factory = new Factory("Factory1", 10);
            Robot robot = new Robot("Model1", 100.0, 1);
            Supplement supplement = new Supplement("Supplement1", 1);

            // Act
            bool result = factory.UpgradeRobot(robot, supplement);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, robot.Supplements.Count);
        }

        [Test]
        public void Test4()
        {
            Factory factory = new Factory("Factory1", 10);
            Robot robot1 = new Robot("Model1", 100.0, 1);
            Robot robot2 = new Robot("Model2", 200.0, 2);
            factory.Robots.Add(robot1);
            factory.Robots.Add(robot2);
            double price = 150.0;

            // Act
            Robot result = factory.SellRobot(price);

            // Assert
            Assert.AreEqual(robot1, result);
            Assert.AreEqual(2, factory.Robots.Count);
        }

        [Test]
        public void Test5()
        {
            Robot robot = new Robot("Model1", 150, 5);
            Factory factory = new Factory("Factory1",10);
            string result = factory.ProduceRobot("Model1", 150, 5);

            string expectedResul = $"Produced --> {robot}";

            Assert.AreEqual(expectedResul, result);
        }

        [Test]
        public void Test6()
        {
            Robot robot = new Robot("Model1", 150, 5);

            Assert.AreEqual("Model1", robot.Model);
            Assert.AreEqual(150, robot.Price);
            Assert.AreEqual(5, robot.InterfaceStandard);
            Assert.AreEqual(0, robot.Supplements.Count);
        }

        [Test]
        public void Test7()
        {
            Robot robot = new Robot("Model1", 150, 5);
            string result = robot.ToString();

            string expected=$"Robot model: {"Model1"} IS: {5}, Price: {150:f2}";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Test8()
        {
            Supplement supplement = new Supplement("Suppliment1", 5);
            string result = supplement.ToString();

            string expected = $"Supplement: Suppliment1 IS: 5";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Test9()
        {
            Supplement supplement = new Supplement("Suppliment1", 5);

            Assert.AreEqual("Suppliment1", supplement.Name);
            Assert.AreEqual(5, supplement.InterfaceStandard);
        }

        [Test]
        public void Test10()
        {
            Factory factory = new Factory("Factory1", 1);
            factory.ProduceRobot("Robot1", 150, 1);

            string result = factory.ProduceRobot("Robot1", 150, 1);
            string ecpectedResult = "The factory is unable to produce more robots for this production day!";

            Assert.AreEqual(ecpectedResult, result);
        }
    }
}