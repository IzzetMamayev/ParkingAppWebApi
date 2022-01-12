using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingAppWebApi.DTOs
{
    public class ChangeUserInfoDTO
    {
        public int IdUser { get; set; }

        [Required(ErrorMessage = "Adinizi daxil edin")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyadi daxil edin")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email daxil edin")]
        public string Email { get; set; }
    }
}
