using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bede.Go.Contracts
{
    public class Lobby
    {
        public Game Game { get; set; }
        public IEnumerable<Player> Players { get; set; }
    }
}
