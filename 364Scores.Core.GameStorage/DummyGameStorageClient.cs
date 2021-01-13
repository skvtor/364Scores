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
    public class DummyGameStorageClient : IGameStorageClient
    {
        private static Dictionary<string, Game> _tableOfGames = new Dictionary<string, Game>();
        private static Dictionary<string, Team> _tableOfTeams = new Dictionary<string, Team>();

        public Task AddGames(IEnumerable<Game> games)
        {
            lock(_tableOfGames)
            {
                foreach (var game in games)
                {
                    var key = GamesProcessingHelper.BuildGameKey(game);
                    _tableOfGames.TryAdd(key, game);
                }
            }
            
            return Task.FromResult(1);
        }

        public Task<IEnumerable<Game>> FilterAlreadyPresent(IEnumerable<Game> games)
        {
            var retVal = new List<Game>();
            foreach (var game in games)
            {
                var key = GamesProcessingHelper.BuildGameKey(game);
                if (!_tableOfGames.ContainsKey(key))
                    retVal.Add(game);
            }

            return Task.FromResult((IEnumerable<Game>)retVal);
        }

        public Task<IDictionary<string, Team>> ResolveTeamInfo(IEnumerable<string> teamLocalNames, Guid sportId)
        {
            var retVal = new Dictionary<string, Team>();
            lock (_tableOfTeams)
            {
                foreach(var name in teamLocalNames)
                {
                    if(!_tableOfTeams.TryGetValue(name, out var team))
                    {
                        team = new Team
                        {
                            Id = Guid.NewGuid(),
                            Name = name,
                            SportTypeId = sportId
                        };
                        _tableOfTeams.Add(name, team);
                    }
                    retVal.TryAdd(team.Name, team);
                }
            }
            return Task.FromResult((IDictionary<string, Team>)retVal);
        }

        public Task<List<Game>> GetGames(GameFilteringOptions options)
        {
            lock (_tableOfGames)
            {
                var retVal = _tableOfGames.Values.Where(x => x.Time >= options.From && x.Time <= options.To).ToList();
                return Task.FromResult(retVal);
            }
        }
    }
}
