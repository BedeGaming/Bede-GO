using System;
using System.Data.Common;
using Bede.Go.Contracts.Interfaces;

namespace Bede.Go.Contracts
{
    public class Game : IIdentifiable
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Distance { get; set; }
        public DateTime StartTime { get; set; }
        public Location Location { get; set; }
    }
}