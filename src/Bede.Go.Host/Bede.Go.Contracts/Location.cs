namespace Bede.Go.Contracts
{
    public class Location
    {
        public long Id { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public decimal Accuracy  { get; set; }
    }
}