using System;
using System.Collections.Generic;

namespace ParkingAppWebApi.Models
{
    public partial class Car_Models
    {
        //public Car_Models()
        //{
        //    Automobiles = new HashSet<User_Cars>();
        //}
        public int IdModel { get; set; }
        public string Name { get; set; }

        //public virtual ICollection<User_Cars> Automobiles { get; set; }
    }
}
