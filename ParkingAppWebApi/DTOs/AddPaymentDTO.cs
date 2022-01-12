using ParkingAppWebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ParkingAppWebApi.DTOs
{

    public class CustomDateAttribute : RangeAttribute
    {
        public CustomDateAttribute()
          : base(typeof(DateTime),
                  DateTime.Now.AddYears(-6).ToShortDateString(),
                  DateTime.Now.ToShortDateString())
        { }
    }

    public class AddPaymentDTO
    {
        [Required]
        public int IdPayment { get; set; }
        [Required]
        public int IdUser { get; set; }

        [Required(ErrorMessage = "Seriya nomresini daxil edin")] public string SerialNumber { get; set; }
        public int IdZone { get; set; }
        [Required]
        public int IdParking { get; set; }

        [Required]
        public decimal Price { get; set; }

        //[DataType(DataType.Date)]
        //[JsonConverter(typeof(JsonDateConverter))]
        //public DateTime PayDate { get; set; }
        [Required]
        public int IdCar { get; set; }

        [Required(ErrorMessage = "Bashlangic tarixi qeyd edin")]
        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime DateFrom { get; set; }

        [Required(ErrorMessage = "Bitme tarixini qeyd edin")]
        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime DateTo { get; set; }

    }
}
