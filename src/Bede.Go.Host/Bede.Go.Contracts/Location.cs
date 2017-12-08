using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using Bede.Go.Contracts.Interfaces;
using Bede.Go.Contracts;

namespace Bede.Go.Contracts
{
    public class Location : IIdentifiable
    {
        public int Id { get; set; }
        [Required]
        [Range(-180,180)]
        public double Longitude { get; set; }
        [Required]
        [Range(-90,90)]
        public double Latitude { get; set; }
        public decimal? Accuracy { get; set; } = 0.0M;
    }
}