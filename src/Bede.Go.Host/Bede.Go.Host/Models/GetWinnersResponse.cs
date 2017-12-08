using System.Collections.Generic;
using Bede.Go.Contracts;

namespace Bede.Go.Host.Models
{
    public class GetWinnersResponse
    {
        public IEnumerable<Player> Winners { get; set; }
    }
}