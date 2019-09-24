using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DroneSim
{
    public class Movement
    {
        public struct MapCoordinates
        {
            public int x, y;

            public MapCoordinates(int c1, int c2)
            {
                x = c1;
                y = c2;
            }
        }

        public void setMapBoundary(string input)
        {
            try
            {
                //Verify positive digits input
                if (!Char.IsDigit(input, 0) || !Char.IsDigit(input, 2))
                {
                    throw new ArgumentException("X and Y Coordinates must be positive numbers");
                }

                if (input.Split(' ').Length != 2)
                {
                    throw new ArgumentException("User must put 2 numbers to represent X and Y coordinate");
                }

                int[] mapCoords = input.Split(' ').Select(int.Parse).ToArray();
                var mapBoundary = new MapCoordinates(mapCoords[0], mapCoords[1]);

                if (mapBoundary.x == 0 && mapBoundary.y == 0)
                {
                    throw new ArgumentException("Top right boundary cannot be 0 0");
                }

                GridModel.xBoundary = mapBoundary.x;
                GridModel.yBoundary = mapBoundary.y;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public DroneModel setDroneStartPosition(DroneModel drone, string input)
        {
            try
            {
                var dronePlacement = input.Split(' ');
                if (dronePlacement.Length > 3 || dronePlacement[2].Any(x => !char.IsLetter(x)))
                {
                    throw new ArgumentException("Input must be in the correct format. For example '1 2 E'");
                }
                else
                {
                    drone.xCord = Int32.Parse(dronePlacement[0]);
                    drone.yCord = Int32.Parse(dronePlacement[1]);

                    switch (dronePlacement[2].ToUpper())
                    {
                        case "N":
                            drone.SetDirection(CardinalDirections.North);
                            break;
                        case "S":
                            drone.SetDirection(CardinalDirections.South);
                            break;
                        case "E":
                            drone.SetDirection(CardinalDirections.East);
                            break;
                        case "W":
                            drone.SetDirection(CardinalDirections.West);
                            break;
                        default:
                            throw new ArgumentException("One of four cardinal directions (NESW) must be entered");
                    }
                    return drone;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return drone;
            }
        }

        public DroneModel moveDrone(DroneModel drone, string movementInfo)
        {
            var moveCode = movementInfo.ToUpper().ToCharArray();
            foreach (var move in moveCode)
            {
                switch (move)
                {
                    case 'L':
                        turnLeft(drone);
                        break;
                    case 'R':
                        turnRight(drone);
                        break;
                    case 'M':
                        moveForward(drone);
                        break;
                    default:
                        throw new ArgumentException($"Input must contain 'L', 'R' or 'M'. User has input: '{move}'");
                }
            }
            return drone;
        }
        

        public DroneModel turnLeft(DroneModel drone)
        {
            var currentDirection = drone.GetDirection();

            // Turn left and set updated direction
            switch (currentDirection)
            {
                case CardinalDirections.North:
                    drone.SetDirection(CardinalDirections.West);
                    break;
                case CardinalDirections.East:
                    drone.SetDirection(CardinalDirections.North);
                    break;
                case CardinalDirections.South:
                    drone.SetDirection(CardinalDirections.East);
                    break;
                case CardinalDirections.West:
                    drone.SetDirection(CardinalDirections.South);
                    break;
            }
            return drone;
        }

        public DroneModel turnRight(DroneModel drone)
        {
            var currentDirection = drone.GetDirection();

            // Turn right and set updated direction
            switch (currentDirection)
            {
                case CardinalDirections.North:
                    drone.SetDirection(CardinalDirections.East);
                    break;
                case CardinalDirections.East:
                    drone.SetDirection(CardinalDirections.South);
                    break;
                case CardinalDirections.South:
                    drone.SetDirection(CardinalDirections.West);
                    break;
                case CardinalDirections.West:
                    drone.SetDirection(CardinalDirections.North);
                    break;
            }

            return drone;
        }

        public DroneModel moveForward(DroneModel drone)
        {
            var currentDirection = drone.GetDirection();

            switch (currentDirection)
            {
                case CardinalDirections.North:
                    if (GridModel.yBoundary - drone.yCord > 0)
                    {
                        drone.yCord++;
                    }
                    break;
                case CardinalDirections.East:
                    if (GridModel.xBoundary - drone.xCord > 0)
                    {
                        drone.xCord++;
                    }
                    break;
                case CardinalDirections.South:
                    if (drone.yCord > 0)
                    {
                        drone.yCord--;
                    }
                    break;
                case CardinalDirections.West:
                    if (drone.xCord > 0)
                    {
                        drone.xCord--;
                    }
                    break;
            }

            return drone;
        }
    }
}
