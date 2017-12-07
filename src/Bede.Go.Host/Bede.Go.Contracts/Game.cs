using System;
using System.Data.Common;

namespace Bede.Go.Contracts
{
    public class Game
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Distance { get; set; }
        public DateTime StartTime { get; set; }
        public Location Location { get; set; }
    }
}