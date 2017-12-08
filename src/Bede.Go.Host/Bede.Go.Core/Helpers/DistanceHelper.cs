using Bede.Go.Contracts;
using System;
using Bede.Go.Core.Extensions;

namespace Bede.Go.Core.Helpers
{
    public class DistanceHelper
    {
        private static double R = 6371;
        public static double GetDistanceBetween(Location current, Location destination)
        {
            var lat = (destination.Latitude - current.Latitude).ToRadians();
            var lng = (destination.Longitude - current.Longitude).ToRadians();
            var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                          Math.Cos(current.Latitude.ToRadians()) * Math.Cos(destination.Latitude.ToRadians()) *
                          Math.Sin(lng / 2) * Math.Sin(lng / 2);
            var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
            return (R * h2);
        }
    }
}
