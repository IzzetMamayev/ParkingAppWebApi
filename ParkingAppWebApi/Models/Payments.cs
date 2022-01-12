using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ParkingAppWebApi.Models
{
    public partial class Payments
    {
        public int IdPayment { get; set; }
        public int IdUser { get; set; }

        [Required]
        public string SerialNumber { get; set; }
        public int IdZone { get; set; }
        public int IdParking { get; set; }
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateConverter))]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime PayDate { get; set; }

        [Required]
        public int IdCar { get; set; }

        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime DateFrom { get; set; }

        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime DateTo { get; set; }
    }
}
