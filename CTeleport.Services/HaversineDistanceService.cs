using CTeleport.Services.Interfaces;
using System;

namespace CTeleport.Services
{
    public class HaversineDistanceService : IDistanceService
    {
        public double GetDistance(LatLon pos1, LatLon pos2)
        {
            double R = 3960; // Miles
            var lat = ToRadians(pos2.Latitude - pos1.Latitude);
            var lng = ToRadians(pos2.Longitude - pos1.Longitude);
            var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                          Math.Cos(ToRadians(pos1.Latitude)) * Math.Cos(ToRadians(pos2.Latitude)) *
                          Math.Sin(lng / 2) * Math.Sin(lng / 2);
            var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));

            return R * h2;
        }

        private static double ToRadians(double angleIn10thofaDegree) => (angleIn10thofaDegree * Math.PI) / 1800;
    }
}
