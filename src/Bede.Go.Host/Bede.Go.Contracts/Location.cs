namespace Bede.Go.Contracts
{
    public class Location
    {
        public long Id { get; set; }
        public decimal Lng { get; set; }
        public decimal Lat { get; set; }
        public decimal Accuracy  { get; set; }
    }
}