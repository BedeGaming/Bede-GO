using Bede.Go.Contracts;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Bede.Go.Core.Helpers
{
    public static class GamesHelper
    {
        private static readonly double DistanceConst = 100;

        public static Expression<Func<Game, bool>> ShowGameInSearch(Location currentLocation)
        {
            return game => game.PlayerIsInRange(currentLocation)
                        && game.StartTime > DateTime.UtcNow.AddSeconds(30);
        }

        public static bool CanPlayerJoinGame(this Game game, Location currentLocation)
        {
            return true; //PlayerIsInRange(game, currentLocation);
        }

        private static bool PlayerIsInRange(this Game game, Location playerLocation)
        {
            return true;
            // return game.Locations.Any(gameLocation => DistanceHelper.GetDistanceBetween(gameLocation, playerLocation) < DistanceConst);
        }
    }
}
