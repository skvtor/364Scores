using Scores364.Api.Manager;
using Microsoft.AspNetCore.Mvc;
using Scores364.Api.Manager.Models;
using System.Threading.Tasks;

namespace Scores364.DemoWebMultiHoster.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private ApiManager _manager;

        public GamesController(ApiManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public Task<GamesPage> Get(GamesPageParams request)
        {
            return _manager.GetGamesPage(request);
        }
    }
}
