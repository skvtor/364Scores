using Scores364.Api.Manager;
using Microsoft.AspNetCore.Mvc;
using Scores364.Api.Manager.Models;
using System.Threading.Tasks;
using System;
using Scores364.Core.Common.Models;

namespace Scores364.DemoWebMultiHoster.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private ApiManager _manager;
        private const int CacheTimeHistoricalData = 2;
        private const int CacheTimeNearFutureData = 1;

        public GamesController(ApiManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<GamesPage> Get(string token = null, DateTime? from = null, int? offset = null, string languageId = null)
        {
            GamesPageCachableContainer pageCachable;
            if(token != null)
                pageCachable = await _manager.GetOffsetPage(new GamesPageOffsetParams { Token = token, Offset = offset });
            else
                pageCachable = await _manager.GetFirstPage(new GamesPageFirstParams { LanguageId = languageId , From = from});

            int cacheForSec = 0;
            switch (pageCachable.DataTypeId)
            {
                case CachedDataTypeId.Historical:
                    cacheForSec = CacheTimeHistoricalData;
                    break;
                case CachedDataTypeId.NearFuture:
                    cacheForSec = CacheTimeNearFutureData;
                    break;
            }

            if (cacheForSec > 0)
                Response.Headers.Add("Cache-Control", $"max-age={cacheForSec}");
            else
                Response.Headers.Add("Cache-Control", "no-cache");
            
            return pageCachable?.Page;
        }
    }
}
