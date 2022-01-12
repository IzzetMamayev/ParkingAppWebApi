using System;
using System.Collections.Generic;

namespace ParkingAppWebApi.Models
{
    public partial class Parkings
    {
        public int IdParking { get; set; }
        public int IdZone { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
