using Bede.Go.Contracts;
using Bede.Go.Core.Helpers;
using Bede.Go.Core.Services;
using Bede.Go.Host.Models;
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
            return Ok(((ClaimsIdentity)User.Identity).Claims);
        }

        [HttpGet]
        [Route("api/games")]
        public async Task<IHttpActionResult> GetGames([FromUri]GetGamesRequest request)
        {
            if(request == null)
            {
                return BadRequest("Request was not provided");
            }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var currentLocation = new Location { Latitude = request.Latitude, Longitude = request.Longitude };

            // Gets games within 1 degree of your geo-location, starting later than right now 
            // (allows clients to show count-downs to 0 but isn't very useful for actually joining)
            // Client probably shouldn't allow joining a game with less than 1 minute to go?
            var games = (await this._gamesService.Query().ConfigureAwait(false))
                            .Where(GamesHelper.ShowGameInSearch(currentLocation))
                            .OrderBy(g => g.StartTime)
                            .Take(15);

            return Json(games);
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