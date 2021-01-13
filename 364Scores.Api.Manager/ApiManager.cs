using Scores364.Api.Manager.Models;
using Scores364.Core.Common.Interfaces;
using Scores364.Core.Common.Models;
using System;
using System.Threading.Tasks;

namespace Scores364.Api.Manager
{
    public class ApiManager
    {
        IGameStorageClient _gamesStorage;

        public ApiManager(IGameStorageClient gamesStorage)
        {
            _gamesStorage = gamesStorage;
        }

        public async Task<GamesPage> GetGamesPage(GamesPageParams pageParam)
        {
            var games = await _gamesStorage.GetGameInfos(new GameFilteringOptions { From = pageParam.From, To = pageParam.To });

            return new GamesPage
            {
                Games = games
            };
        }
    }
}
