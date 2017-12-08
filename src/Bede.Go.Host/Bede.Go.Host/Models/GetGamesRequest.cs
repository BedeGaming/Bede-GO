using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Bede.Go.Host.Models
{
    public class GetGamesRequest
    {
        [Range(-180, 180)]
        [JsonProperty("Lng")]
        public double Longitude { get; set; }
        [Range(-90,90)]
        [JsonProperty("Lat")]
        public double Latitude { get; set; }
        public decimal? Accuracy { get; set; } = 0.0M;
    }
}