using Scores364.Api.Manager.Models;
using Scores364.Core.Common.Interfaces;
using Scores364.Core.Common.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Scores364.Api.Manager
{
    public class ApiManager
    {
        IGameStorageClient _gamesStorage;
        ICacheManager _cache;

        private const int PageSize = 5;

        public ApiManager(IGameStorageClient gamesStorage, ICacheManager cache)
        {
            _gamesStorage = gamesStorage;
            _cache = cache;
        }

        public Task<GamesPageCachableContainer> GetFirstPage(GamesPageFirstParams pageParam)
        {
            var token = new GamesPageToken
            {
                From = pageParam.From ?? new DateTime(),
                Offset = 0,
                PageSize = PageSize,
                LanguageId = pageParam.LanguageId,
                
            };

            return GetPageByToken(token);
        }

        public Task<GamesPageCachableContainer> GetOffsetPage(GamesPageOffsetParams pageParam)
        {
            var token = GamesPageToken.Deserialize(pageParam.Token);
            token.Offset += pageParam.Offset ?? 0;
            return GetPageByToken(token);
        }

        private async Task<GamesPageCachableContainer> GetPageByToken(GamesPageToken token)
        {
            var pageToken = token.ToString();
            var page = await _cache.GetPage(pageToken);
            if (page == null)
            {
                var games = await _gamesStorage.GetGameInfos(new GameFilteringOptions
                {
                    From = token.From,
                    LanguageId = token.LanguageId,
                    PageIndex = token.Offset,
                    PageSize = token.PageSize
                });

                page = new GamesPage
                {
                    Games = games,
                    PageToken = pageToken
                };

                await _cache.CachePage(pageToken, page);
            }

            CachedDataTypeId dataTypeId = CachedDataTypeId.Realtime;
            if (page.Games.Any())
            {
                var oldestPoint = page.Games.Min(x => x.Time);
                var youngestPoint = page.Games.Max(x => x.Time);

                if (oldestPoint < DateTime.UtcNow.AddMinutes(-5))
                    dataTypeId = CachedDataTypeId.Historical;
                else if (youngestPoint > DateTime.UtcNow.AddMinutes(10))
                    dataTypeId = CachedDataTypeId.NearFuture;
            }

            return new GamesPageCachableContainer
            {
                Page = page,
                DataTypeId = dataTypeId,
            };
        }
    }
}
