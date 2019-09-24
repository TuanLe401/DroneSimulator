using System;
using System.Collections.Generic;
using System.Linq;

namespace DroneSim
{

    public class Program
    {
        static void Main(string[] args)
        {

            Movement mapZone = new Movement();
            var droneSquad = new List<DroneModel>();
            var userInput = "";
            
            while (GridModel.xBoundary == 0 && GridModel.yBoundary == 0)
            {
                Console.WriteLine("Input Coordinates for top right of battlefield");
                userInput = Console.ReadLine();
                mapZone.setMapBoundary(userInput);
            }

            while (userInput != "stop")
            {
                var drone = new DroneModel();
                Console.WriteLine("Input starting position of drone or 'stop' to cease sending out drones");
                userInput = Console.ReadLine();
                if (userInput.ToLower() == "stop") continue;

                mapZone.setDroneStartPosition(drone, userInput);

                Console.WriteLine("Input movement information");
                userInput = Console.ReadLine();
                mapZone.moveDrone(drone, userInput);
                droneSquad.Add(drone);
            }

            Console.WriteLine("Drone final position information: ");
            foreach(var drone in droneSquad)
            {
                Console.WriteLine($"{drone.xCord} {drone.yCord} {drone.GetFinalDirection()}");
            }

            Console.WriteLine("Exiting...");
            userInput = Console.ReadLine();
        }

        
    }
}
