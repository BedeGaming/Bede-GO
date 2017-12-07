using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bede.Go.Host.Models
{
    public class GetGamesRequest
    {
        [Required]
        [Range(-180, 180)]
        [JsonProperty("Lng")]
        public decimal Longitude { get; set; }
        [Required]
        [Range(-90,90)]
        [JsonProperty("Lat")]
        public decimal Latitude { get; set; }
        [Required]
        public decimal? Accuracy { get; set; } = 0.0M;
    }
}