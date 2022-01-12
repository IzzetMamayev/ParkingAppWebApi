using ParkingAppWebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ParkingAppWebApi.DTOs
{
    public class SearchPaymentDTO
    {
        [Required(ErrorMessage = "Seriya nomresini daxil edin")]
        public string SerialNum { get; set; }



        [Required(ErrorMessage = "Tarixi daxil edin")]
        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime SearchDate { get; set; }
    }
}
