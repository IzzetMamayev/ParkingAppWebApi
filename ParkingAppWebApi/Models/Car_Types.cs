using System;
using System.Collections.Generic;

namespace ParkingAppWebApi.Models
{
    public partial class Car_Types
    {
        //public Car_Types()
        //{
        //    Automobiles = new HashSet<User_Cars>();
        //}

        public int IdType { get; set; }
        public string Name { get; set; }

        //public virtual ICollection<User_Cars> Automobiles { get; set; }
    }
}
