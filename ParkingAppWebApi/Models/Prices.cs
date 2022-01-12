using System;
using System.Collections.Generic;

namespace ParkingAppWebApi.Models
{
    public partial class Prices
    {
        public int Id { get; set; }
        public int IdZone { get; set; }
        public int IdParking { get; set; }
        public int DateType { get; set; }
        public decimal Price { get; set; }
    }
}
