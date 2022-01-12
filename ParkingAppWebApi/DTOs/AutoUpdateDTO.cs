using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingAppWebApi.DTOs
{
    public class AutoUpdateDTO
    {
        public int IdUser { get; set; }
        public int idCar { get; set; }
        public int IdModel { get; set; }
        public int IdType { get; set; }
        public string SerialNumber { get; set; }
    }
}
