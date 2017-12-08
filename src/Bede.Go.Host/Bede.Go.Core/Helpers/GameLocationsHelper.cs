using System.Collections;
using System.Numerics;
using System;
using Bede.Go.Contracts;
using Bede.Go.Core.Helpers;
using System.Collections.Generic;
using Bede.Go.Core.Configuration;

namespace Bede.Go.Core.Helpers
{
    public class GameLocationsHelper
    {
        private static Random random = new Random();
        public static List<Location> generateGameLocations(Location centerPosition, int maxPoints)
        {
            List<Location> points = new List<Location>();

            for (int i = 0; i < maxPoints; ++i)
            {
                double radius = CoreConfiguration.PlayAreaDiameter / 2.0;

                double theta = random.NextDouble() * (2 * 3.14159 - 0) + 0; 
                double distance = Math.Sqrt(random.NextDouble() * (1 - 0) + 0) * radius;

                double px = distance * Math.Cos(theta) + centerPosition.Latitude;
                double py = distance * Math.Sin(theta) + centerPosition.Longitude;

                Location pos = new Location {
                                                Latitude = px,
                                                Longitude = py
                                            };
                                            
                points.Add(pos);               
            }

            return points;
        }
    }
}
