using Bede.Go.Contracts;
using Bede.Go.Core.Helpers;
using Bede.Go.Core.Services;
using Bede.Go.Host.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace Bede.Go.Host.Controllers
{
    [Authorize]
    public class GamesController : ApiController
    {
        private readonly ICrudService<Game> _gamesService;

        public GamesController(
            ICrudService<Game> gamesService)
        {
            this._gamesService = gamesService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Home()
        {
            return Ok($"Hello, {((ClaimsIdentity)User.Identity).Claims.Where(c => c.Type == "name").FirstOrDefault()?.Value}");
        }

        [HttpGet]
        [Route("api/games")]
        public async Task<IHttpActionResult> GetGames([FromUri]GetGamesRequest request)
        {
            return Json(new[]
            {
                new Game
                {
                    Id = 1,
                    Distance = 1.0,
                    Locations = new [] 
                    {
                        new Location { Latitude = 54.8, Longitude = -0.5, Id = 1 }
                    },
                    Name = "Ninja Challenge 3000",
                    StartTime = DateTime.UtcNow.AddMinutes(5)
                },
                new Game
                {
                    Id = 2,
                    Distance = 6,
                    Locations = new [] 
                    {
                        new Location { Latitude = 54.8, Longitude = -0.5, Id = 1 },
                        new Location { Latitude = 54.14, Longitude = -0.3, Id = 435 },
                        new Location { Latitude = 54.9, Longitude = -0.6, Id = 36 }
                    },
                    Name = "CONTENDERS, ARE YOU READY?",
                    StartTime = DateTime.UtcNow.AddMinutes(12).AddSeconds(21) // To show differences 
                },
                new Game
                {
                    Id = 3,
                    Distance = 0.2,
                    Locations = new [] 
                    {
                        new Location { Latitude = 54.9, Longitude = -0.6, Id = 36 },
                        new Location { Latitude = 54.8, Longitude = -0.5, Id = 65 }
                    },
                    Name = "Mecca BingoRun",
                    StartTime = new DateTime(2017,12,08,16,45,0)
                },
                new Game
                {
                    Id = 4,
                    Distance = 3.7,
                    Locations = new [] 
                    {
                        new Location { Latitude = 54.8, Longitude = -0.5, Id = 65 },
                        new Location { Latitude = 54.2, Longitude = -0.49, Id = 625 }
                    },
                    Name = "Bede Gladiators",
                    StartTime = DateTime.UtcNow.AddMinutes(14).AddSeconds(45)
                },
                new Game
                {
                    Id = 5,
                    Distance = 16.9,
                    Locations = new []
                    {
                        new Location { Latitude = 54.14, Longitude = -0.3, Id = 435 },
                        new Location { Latitude = 54.2, Longitude = -0.49, Id = 625 }
                    },
                    Name = "Casino CARNAGE",
                    StartTime = DateTime.UtcNow.AddMinutes(17)
                }
            });

            //if(request == null)
            //{
            //    return BadRequest("Request was not provided");
            //}

            //if (!ModelState.IsValid) { return BadRequest(ModelState); }

            //var currentLocation = new Location { Latitude = request.Latitude, Longitude = request.Longitude };

            //// Gets games within 1 degree of your geo-location, starting later than right now 
            //// (allows clients to show count-downs to 0 but isn't very useful for actually joining)
            //// Client probably shouldn't allow joining a game with less than 1 minute to go?
            //var games = (await this._gamesService.Query().ConfigureAwait(false))
            //                .Where(GamesHelper.ShowGameInSearch(currentLocation))
            //                .OrderBy(g => g.StartTime)
            //                .Take(15);

            //return Json(games);
        }

        [HttpPost]
        [Route("api/games/{id}/join")]
        public async Task<IHttpActionResult> JoinGame(long id, [FromUri]GetGamesRequest request)
        {
            var currentLocation = new Location { Latitude = request.Latitude, Longitude = request.Longitude };
            var game = await _gamesService.Read(id).ConfigureAwait(false);

            if (game.CanPlayerJoinGame(currentLocation))
            {
                game.Players.ToList().Add(new Player { Email = ClaimsPrincipal.Current.Claims.First(c => c.Type.Equals(ClaimTypes.Email)).Value });
                await _gamesService.Update(game).ConfigureAwait(false);

                return Json(game);
            }

            return InternalServerError();
        }
    }
}