using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ParkingAppWebApi.Models
{
    public partial class Inspectors
    {
        public int IdInspector { get; set; }
        public int IdZone { get; set; }
        public int IdParking { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime Createdt { get; set; }
    }
}
