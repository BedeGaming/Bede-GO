using Bede.Go.Contracts;
using Bede.Go.Host.Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using Bede.Go.Core.Services;

namespace Bede.Go.Host.Controllers
{
    public class GamesController : Controller
    {
        private readonly ICrudService<Game> _gamesService;

        public GamesController(ICrudService<Game> gamesService)
        {
            this._gamesService = gamesService;
        }

        // GET: Games
        public async Task<ActionResult> Index(GetGamesRequest request)
        {
            if(request == null)
            {
                this.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Json(new ApiError { Error = "Request was not provided." });
            }
            
            if(!ModelState.IsValid)
            {
                this.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Json(new ApiError { Error = "Request was invalid.", Data = ModelState });
            }

            var games = await this._gamesService.List();

            return Json(games);
        }
    }
}