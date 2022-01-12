using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ParkingAppWebApi.Models
{
    public partial class ParkingUsers
    {
        //public ParkingUsers()
        //{
        //    Automobiles = new HashSet<User_Cars>();
        //}

        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime Createdt { get; set; }
        public string Password { get; set; }

        //public virtual ICollection<User_Cars> Automobiles { get; set; }
    }
}
