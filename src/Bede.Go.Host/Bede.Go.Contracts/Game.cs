using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Bede.Go.Contracts.Interfaces;

namespace Bede.Go.Contracts
{
    public class Game : IIdentifiable
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public decimal PrizePot { get; set; }
        public decimal Entryfee { get; set; }
        public string CurrencyCode { get; set; }

        public IEnumerable<Location> Locations { get; set; }
        public IEnumerable<Player> Players { get; set; }

        public double Distance { get; set; }
    }
}