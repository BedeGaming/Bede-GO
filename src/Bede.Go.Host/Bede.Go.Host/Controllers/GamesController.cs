using System;
using System.Collections.Generic;
using Bede.Go.Contracts;
using Bede.Go.Core.Helpers;
using Bede.Go.Core.Services;
using Bede.Go.Host.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Bede.Go.Host.Controllers
{
    [Authorize]
    public class GamesController : ApiController
    {
        private readonly ICrudService<Game> _gamesService;
        private readonly ICrudService<GameResult> _gameResultsService;
        private readonly ICrudService<Player> _playersService;
        private readonly ICrudService<Winner> _winnersService;
        private readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

        public GamesController(
            ICrudService<Game> gamesService,
            ICrudService<GameResult> gameResultsService,
            ICrudService<Player> playersService,
            ICrudService<Winner> winnersService)
        {
            _gamesService = gamesService;
            _gameResultsService = gameResultsService;
            _playersService = playersService;
            _winnersService = winnersService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Home()
        {
            return
                Ok(
                    $"Hello, {((ClaimsIdentity) User.Identity).Claims.Where(c => c.Type == "name").FirstOrDefault()?.Value}");
        }

        [HttpGet]
        [Route("api/games")]
        public async Task<IHttpActionResult> GetGames([FromUri] GetGamesRequest request)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            //var currentLocation = new Location { Latitude = request.Latitude, Longitude = request.Longitude };

            // Gets games within 1 degree of your geo-location, starting later than right now 
            // (allows clients to show count-downs to 0 but isn't very useful for actually joining)
            // Client probably shouldn't allow joining a game with less than 1 minute to go?
            var games = (await this._gamesService.Query().ConfigureAwait(false))
                            //.Where(GamesHelper.ShowGameInSearch(currentLocation))
                            .OrderBy(g => g.StartTime)
                            .Take(15);


            var test1 = await _gamesService.Query();
            var test2 = test1; //.Where(GamesHelper.ShowGameInSearch(currentLocation));
            return Json(games, JsonSettings);
        }

        [HttpPost]
        [Route("api/games/{id}/join")]
        public async Task<IHttpActionResult> JoinGame(int id, [FromUri] GetGamesRequest request)
        {
            var currentLocation = new Location {Latitude = request.Latitude, Longitude = request.Longitude};
            var game = await _gamesService.Read(id).ConfigureAwait(false);

            if (game.CanPlayerJoinGame(currentLocation))
            {
                var players = game.Players.ToList();
                players.Add(new Player
                    {
                        Email = ClaimsPrincipal.Current.Claims.First(c => c.Type.Equals("email")).Value
                    });
                game.Players = players;
                await _gamesService.Update(game).ConfigureAwait(false);

                return Json(game, JsonSettings);
            }

            return InternalServerError();
        }

        [HttpPost]
        [Route("api/player/location")]
        public async Task<IHttpActionResult> UpdatePlayerLocation([FromBody] UpdatePlayerLocationRequest request)
        {
            var currentLocation = new Location {Latitude = request.Latitude, Longitude = request.Longitude};
            var game = await _gamesService.Read(request.GameId).ConfigureAwait(false);

            if (DateTime.UtcNow < game.StartTime)
            {
                return BadRequest("Game has not finished");
            }

            var winningLocationId = await GetWinningLocationId(game);
            var potentialLocation = game.Locations.SingleOrDefault(l => DistanceHelper.GetDistanceBetween(l, currentLocation) < 0.0001);

            if (potentialLocation != null && potentialLocation.Id == winningLocationId)
            {
                await _winnersService.Create(new Winner
                {
                    GameId = request.GameId,
                    PlayerId = game.Players.Single(p => p.Email.Equals(ClaimsPrincipal.Current.Claims.First(c => c.Type.Equals("email")).Value)).Id
                });
                return Created("", true);
            }

            return Created("", false);
        }

        [HttpGet]
        [Route("api/games/{id}/winners")]
        public async Task<IHttpActionResult> GetWinners(int id)
        {
            var winners = (await _winnersService.Query()).Where(gR => gR.GameId == id);
            var players = new List<Player>();
            foreach (var winner in winners)
            {
                players.Add(await _playersService.Read(winner.PlayerId));
            }
            return Json(new GetWinnersResponse {Winners = players}, JsonSettings);
        }

        private async Task<int> GetWinningLocationId(Game game)
        {
            var gameResult = (await _gameResultsService.Query()).SingleOrDefault(gR => gR.GameId == game.Id);
            if (gameResult != null)
            {
                return gameResult.LocationId;
            }

            var random = new Random(DateTime.UtcNow.Second);
            var element = random.Next(game.Locations.Count());

            gameResult = new GameResult
            {
                GameId = game.Id,
                LocationId = game.Locations.ElementAt(element).Id
            };
            await _gameResultsService.Create(gameResult);
            return gameResult.LocationId;
        }
    }
}