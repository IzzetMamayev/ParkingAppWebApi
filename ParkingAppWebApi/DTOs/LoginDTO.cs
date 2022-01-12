using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingAppWebApi.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Mobil telefon nömrəsini daxil edin")]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Parolu daxil edin")]
        public string Password { get; set; }
    }
}
