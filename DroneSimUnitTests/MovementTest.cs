using DroneSim;
using NUnit.Framework;
using Ploeh.SemanticComparison;
using System;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        //[Test]
        public void SetMapBoundaryNegativeValue_ThrowsException()
        {
            Movement mapZone = new Movement();
            var userInput = "-1 5";

            //mapZone.setMapBoundary(userInput);

            var ex = Assert.Throws<ArgumentException>(() => mapZone.setMapBoundary(userInput));
            Assert.That(ex.Message, Is.EqualTo("X and Y Coordinates must be positive numbers"));
        }

        [Test]
        public void MoveDroneIncorrectInput_ThrowsException()
        {
            Movement mapZone = new Movement();
            var drone = new DroneModel();

            //mapZone.moveDrone(drone, "T");

            var ex = Assert.Throws<ArgumentException>(() => mapZone.moveDrone(drone, "T"));
            Assert.That(ex.Message, Is.EqualTo("Input must contain 'L', 'R' or 'M'. User has input: 'T'"));
        }

        //[Test]
        public void SetDroneStartPositionIncorrectInput_ThrowsException()
        {
            Movement mapZone = new Movement();
            var drone = new DroneModel();
            

            var ex = Assert.Throws<ArgumentException>(() => mapZone.setDroneStartPosition(drone, "1 2 2"));
            Assert.That(ex.Message, Is.EqualTo("Input must be in the correct format. For example '1 2 E'"));
        }
        [Test]
        public void SetDroneStartPosition_ReturnsCorrectPosition()
        {
            Movement mapZone = new Movement();
            var drone = new DroneModel();
            var inputDrone = new DroneModel();
            //var expectedDrone = new DroneModel();
            var expectedDrone = new Likeness<DroneModel, DroneModel>(drone);
            drone.xCord = 1;
            drone.yCord = 2;
            drone.SetDirection(CardinalDirections.East);

            mapZone.setDroneStartPosition(inputDrone, "1 2 E");

            Assert.AreEqual(inputDrone, expectedDrone);
        }

        [Test]
        public void TurnDroneLeft_FromNorthReturnsWest()
        {
            Movement mapZone = new Movement();
            var drone = new DroneModel();
            var inputDrone = new DroneModel();
            drone.SetDirection(CardinalDirections.West);
            inputDrone.SetDirection(CardinalDirections.North);
            var expectedDrone = new Likeness<DroneModel, DroneModel>(drone);

            mapZone.turnLeft(inputDrone);

            Assert.AreEqual(inputDrone, expectedDrone);

        }
    }
}