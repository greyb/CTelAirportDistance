using System;
using System.Collections.Generic;
using System.Text;

namespace CTeleport.Services.Interfaces
{
    public class LatLon
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public LatLon(double lat, double lon)
        {
            this.Latitude = lat;
            this.Longitude = lon;
        }
    }
}
