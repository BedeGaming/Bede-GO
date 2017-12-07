using Bede.Go.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bede.Go.Core.Helpers
{
    public class GamesHelper
    {
        public static Expression<Func<Game, bool>> ShowGameInSearch(Location currentLocation)
        {
            return game =>
                    game.Location.Latitude < currentLocation.Latitude + 1
                    && game.Location.Latitude > currentLocation.Latitude - 1
                    && game.Location.Longitude < currentLocation.Longitude + 1
                    && game.Location.Longitude > currentLocation.Longitude - 1
                    && game.StartTime > DateTime.UtcNow;
        }
    }
}
