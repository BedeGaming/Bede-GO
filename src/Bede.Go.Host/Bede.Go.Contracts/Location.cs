using System.ComponentModel.DataAnnotations;

namespace Bede.Go.Contracts
{
    public class Location
    {
        public long Id { get; set; }
        [Required]
        [Range(-180,180)]
        public double Longitude { get; set; }
        [Required]
        [Range(-90,90)]
        public double Latitude { get; set; }
        public decimal? Accuracy { get; set; } = 0.0M;
    }
}