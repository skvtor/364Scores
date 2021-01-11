using Scores364.Api.Manager.Models;
using Scores364.Core.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace Scores364.Api.Manager
{
    public class ApiManager
    {
        IGameStorageClient _gamesStorage;

        public ApiManager(IGameStorageClient gamesStorage)
        {

        }

        public Task<GamesPage> GetGamesPage(GamesPageParams pageParam)
        {
            return Task.FromResult(new GamesPage());
        }
    }
}
