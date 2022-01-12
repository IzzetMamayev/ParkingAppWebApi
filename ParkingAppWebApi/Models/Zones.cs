using System;
using System.Collections.Generic;

namespace ParkingAppWebApi.Models
{
    public partial class Zones
    {
        public int IdZone { get; set; }
        public string IdName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
