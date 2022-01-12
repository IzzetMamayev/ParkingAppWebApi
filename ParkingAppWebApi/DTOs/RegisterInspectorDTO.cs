using ParkingAppWebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ParkingAppWebApi.DTOs
{
    public class RegisterInspectorDTO
    {
        public int IdInspector { get; set; }

        [Required(ErrorMessage = "Adı daxil edin")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyadı daxil edin")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Mobil telefon nömrəsini daxil edin")]
        public string Phone { get; set; }

        public DateTime Createdt { get; set; }

        [Required(ErrorMessage = "Parolu daxil edin")]
        public string Password { get; set; }
        public int IdParking { get; set; }
        public int IdZone { get; set; }
    }
}
