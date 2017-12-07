using System.Collections;
using System.Numerics;
using System;
using Bede.Go.Contracts;
using Bede.Go.Core.Helpers;

namespace Bede.Go.Core.Helpers
{
    public class GameLocationsHelper
    {
        public double diameter = 0.025;
        public int maxLocations =  3;
           
        public ArrayList generateGameLocations(Location centerPosition, int maxPoints)
        {
            ArrayList points = new ArrayList();

            for (int i = 0; i < maxPoints; ++i)
            {
                double radius = diameter / 2.0;
                double theta = RandomHelper.GetRandomBoundedDouble(0, 2 * 3.14159);
                double distance = Math.Sqrt(RandomHelper.GetRandomBoundedDouble(0.0, 1.0)) * radius;

                double px = distance * Math.Cos(theta) + centerPosition.Latitude;
                double py = distance * Math.Sin(theta) + centerPosition.Longitude;

                Location pos = new Location {
                                                Longitude = px,
                                                Latitude = py
                                            };
                                            
                points.Add(pos);
            }

            return points;
        }
    }
}
