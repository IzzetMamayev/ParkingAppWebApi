using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingAppWebApi.DTOs
{
    public class UserDTO
    {
        public string Phone { get; set; }

        [System.ComponentModel.DefaultValue("boshdur")]
        public string Token { get; set; }
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}
