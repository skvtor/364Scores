using Scores364.Core.Common.Helpers;
using Scores364.Core.Common.Interfaces;
using Scores364.Core.Common.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scores364.Core.GameStorage
{
    public class SimplestGameStorageClient : IGameStorageClient
    {
        private static ConcurrentDictionary<string, Game> _tableOfGames = new ConcurrentDictionary<string, Game>();
        private static ConcurrentDictionary<Guid, Team> _tableOfTeams = new ConcurrentDictionary<Guid, Team>();

        public Task AddGames(IEnumerable<Game> games)
        {
            foreach (var game in games)
            {
                var key = GamesProcessingHelper.BuildKey(game);
                _tableOfGames.TryAdd(key, game);
            }

            return Task.FromResult(1);
        }

        public Task<IEnumerable<Game>> FilterAlreadyPresent(IEnumerable<Game> games)
        {
            var retVal = new List<Game>();
            foreach (var game in games)
            {
                var key = GamesProcessingHelper.BuildKey(game);
                if (!_tableOfGames.ContainsKey(key))
                    retVal.Add(game);
            }

            return Task.FromResult((IEnumerable<Game>)retVal);
        }

        public Task<IDictionary<string, Team>> ResolveTeamInfo(IEnumerable<string> teamLocalNames)
        {
            var retVal = _tableOfTeams.Values.Where(x => teamLocalNames.Contains(x.Name)).ToDictionary(k => k.Name, v => v);
            return Task.FromResult((IDictionary<string, Team>)retVal);
        }
    }
}
