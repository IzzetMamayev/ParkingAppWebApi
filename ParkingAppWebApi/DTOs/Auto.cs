using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingAppWebApi.DTOs
{
    public class Auto
    {
        public int idCar { get; set; }
        public int IdUser { get; set; }
        public int IdModel { get; set; }
        public string SerialNumber { get; set; }
        public int IdType { get; set; }
        public DateTime? Createdt { get; set; }
    }
}
