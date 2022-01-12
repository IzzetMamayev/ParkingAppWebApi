using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingAppWebApi.DTOs
{
    public class GetPriceDTO
    {
        public int idUser { get; set; }
        public int idZone{ get; set; }
        public int idParking { get; set; }
        public string fromDate { get; set; } 
        public string fromTime { get; set; }
        public string toDate { get; set; }
        public string toTime { get; set; }
        public int idCar { get; set; }


    }
}
