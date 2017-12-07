using Bede.Go.Contracts;
using Bede.Go.Core.Services.Interfaces;
using Bede.Go.Host.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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

            var games = await this._gamesService.List();

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
            var game = await _gamesService.Read<Game>(id);



            return Json(game);
        }
    }
}