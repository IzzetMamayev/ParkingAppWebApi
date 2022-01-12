using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ParkingAppWebApi.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Adı daxil edin")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyadı daxil edin")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email ünvanı daxil edin")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobil telefon nömrəsini daxil edin")]
        public string Phone { get; set; }

        public DateTime Createdt { get; set; }

        [Required(ErrorMessage = "Parolu daxil edin")]
        public string Password { get; set; }
    }
}



//[Required(ErrorMessage = "Adı daxil edin")]
//public string UserName { get; set; }


//[Required(ErrorMessage = "Email ünvanı daxil edin")]
//public string Email { get; set; }


//[Required(ErrorMessage = "Parolu daxil edin")]
//public string Password { get; set; }
