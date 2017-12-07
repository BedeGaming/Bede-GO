using Bede.Go.Contracts;
using Bede.Go.Core.Helpers;
using Bede.Go.Core.Services;
using Bede.Go.Host.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bede.Go.Host.Controllers
{

    public class GamesController : Controller
    {
        private readonly ICrudService<Game> _gamesService;

        public GamesController(ICrudService<Game> gamesService)
        {
            this._gamesService = gamesService;
        }

        [HttpGet]
        [Route("api/games")]
        public async Task<ActionResult> GetGames(GetGamesRequest request)
        {
            if(request == null)
            {
                SetBadRequestStatusCode();
                return Json(new ApiError { Error = "Request was not provided." });
            }
            
            if(!ModelState.IsValid)
            {
                SetBadRequestStatusCode();
                return Json(new ApiError { Error = "Request was invalid.", Data = ModelState });
            }

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

        private void SetBadRequestStatusCode()
        {
            this.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
        }

        [HttpPost]
        [Route("api/games/{id}/join")]
        public async Task<ActionResult> JoinGame(long id)
        {
            throw new NotImplementedException();

            //var game = await _gamesService.Read(id);

            //// Validate game can be joined
            //if(await CanGameBeJoined(game))
            //{

            //}

            //return Json(game);
        }

        private async Task<bool> CanGameBeJoined(Game game)
        {
            // TODO
            return await Task.FromResult(true);
        }
    }
}