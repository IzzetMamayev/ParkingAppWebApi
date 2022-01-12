using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingAppWebApi.DTOs
{
    public class ChangePasswordDTO
    {

        public int IdInspector { get; set; }
        public int IdUser { get; set; }

        [Required(ErrorMessage = "Kohne parolu daxil edin")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Yeni parolu daxil edin")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Yeni parolu ikinci defe daxil edin")]
        public string ConfirmPassword { get; set; }
    }
}
