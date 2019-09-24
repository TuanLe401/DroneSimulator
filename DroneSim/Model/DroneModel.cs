using System;
using System.Collections.Generic;
using System.Text;

namespace DroneSim
{
    public enum CardinalDirections
    {
        North,
        South,
        East,
        West
    }

    public class DroneModel
    {
        // Coordinates holding the drone
        public int xCord { get; set; }
        public int yCord { get; set; }

        // Direction set and get
        private CardinalDirections direction;
        public void SetDirection (CardinalDirections cardinalDirection)
        {
            direction = cardinalDirection;
        }
        public CardinalDirections GetDirection()
        {
            return direction;
        }
        
        public string GetFinalDirection()
        {
            return direction.ToString();
        }


        
    }
}
