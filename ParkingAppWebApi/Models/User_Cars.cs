using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace ParkingAppWebApi.Models
{
    public partial class User_Cars
    {
        public int IdCar { get; set; }
        public int IdUser { get; set; }
        public int IdModel { get; set; }
        public string SerialNumber { get; set; }
        public int IdType { get; set; }

        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime Createdt { get; set; }

        //public  Car_Models IdModelNavigation { get; set; }

        //public  Car_Types IdTypeNavigation { get; set; }

        //public  ParkingUsers IdUserNavigation { get; set; }


    }

    
}
