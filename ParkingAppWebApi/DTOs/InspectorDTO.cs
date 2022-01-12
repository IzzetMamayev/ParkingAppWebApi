using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingAppWebApi.DTOs
{
    public class InspectorDTO
    {
        public string Phone { get; set; }

        [System.ComponentModel.DefaultValue("boshdur")]
        public string Token { get; set; }
        public int IdInspector { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int IdZone { get; set; }
        public int IdParking { get; set; }
    }
}
