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
        private static object _teamLocker = new object();
        private static Dictionary<string, Team> _indexOfTeamNames = new Dictionary<string, Team>();
        private static Dictionary<Guid, Team> _indexOfTeamIds = new Dictionary<Guid, Team>();
        private static Dictionary<string, Game> _tableOfGames = new Dictionary<string, Game>();


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

        public Task<IDictionary<string, Team>> ResolveTeamByName(IEnumerable<string> names, Guid sportId, string languageId = null)
        {
            var retVal = new Dictionary<string, Team>();
            lock (_teamLocker)
            {
                foreach(var name in names)
                {
                    if(!_indexOfTeamNames.TryGetValue(name, out var team))
                    {
                        team = new Team
                        {
                            Id = Guid.NewGuid(),
                            Name = name,
                            SportTypeId = sportId
                        };
                        _indexOfTeamNames.Add(name, team);
                        _indexOfTeamIds.Add(team.Id, team);
                    }
                    retVal.TryAdd(team.Name, team);
                }
            }
            return Task.FromResult((IDictionary<string, Team>)retVal);
        }

        public Task<List<GameInfo>> GetGameInfos(GameFilteringOptions options)
        {
            lock (_tableOfGames)
            {
                lock (_teamLocker)
                {
                    var games = _tableOfGames.Values
                        .OrderBy(x => x.Time)
                        .Where(x => x.Time >= options.From)
                        .Skip(options.PageSize * options.PageIndex)
                        .Take(options.PageSize)
                        .ToList();

                    var retVal = games.Select(x => new GameInfo
                    {
                        Time = x.Time,
                        TeamName1 = _indexOfTeamIds[x.Team1Id].Name,
                        TeamName2 = _indexOfTeamIds[x.Team2Id].Name,
                    }).ToList();

                    return Task.FromResult(retVal);
                }
            }
        }
    }
}
