using Scores364.Core.Common.Models;
using HtmlAgilityPack;
using Scores364.Core.Common.Interfaces;
using Scores364.Core.Datasources.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Scores364.Core.Datasources.Datasources
{
    public class Superplacar3DaysDatasource : IDatasource
    {
        private HttpClient _client = new HttpClient();
        private string _urlTemplate = "";
        private Guid _sportId;
        private string _competitinoTypeId = "";
        private HashSet<string> _alreadyLoadedCache = new HashSet<string>();
        private Dictionary<string, Team> _teamsLocalCache = new Dictionary<string, Team>();

        public void Configure(Dictionary<string, string> config)
        {
            //todo:
            //_urlTemplate = config["UrlTemplate"];
            //_sportTypeId = config["SportTypeId"];
            _competitinoTypeId = "wc";
            _urlTemplate = @"https://superplacar.com.br/proximas-partidas/{0}-{1}-{2}";
        }

        public async Task CheckUpdate(IEventQueueWriter queue, IGameStorageReader storage, CancellationToken cancellationToken)
        {
            var gameDescriptors = await GetInfoByDate(new DateTime(2021, 01, 14));
            var filteredGameDescriptors = FilterLocal(gameDescriptors);
            var games = await Resolve(filteredGameDescriptors, storage);

            if (games.Any())
                await queue.Enqueue(games);
        }

        protected async Task<List<GameDescriptor>> GetInfoByDate(DateTime date)
        {
            //https://superplacar.com.br/proximas-partidas/2021-01-14
            var dt = DateTime.UtcNow;
            var response = await _client.GetAsync(string.Format(_urlTemplate, dt.Year, dt.Month, dt.Day));
            var content = await response.Content.ReadAsStringAsync();
            var html = new HtmlDocument();
            html.LoadHtml(content);
            var nodes = html.DocumentNode.SelectNodes("//div[starts-with(@id, 'dvPartida_')]");

            var games = new List<GameDescriptor>();
            foreach(var node in nodes)
            {
                var timeParts = node.SelectSingleNode(".//div[contains(@class, 'col-xs-6 col-sm-12 hora')]")
                    .InnerText
                    .Replace(Environment.NewLine,"")
                    .Trim()
                    .Split(':');

                var teams = node.SelectNodes(".//span[contains(@class, 'nomeEquipe')]");
                var team1Name = teams[0].SelectSingleNode(".//span[contains(@class, 'visible-xs')]").InnerText;
                var team2Name = teams[1].SelectSingleNode(".//span[contains(@class, 'visible-xs')]").InnerText;

                games.Add(new GameDescriptor
                {
                    Time = new DateTime(dt.Year, dt.Month, dt.Day, int.Parse(timeParts[0]), int.Parse(timeParts[1]), 0),
                    TeamName1 = team1Name,
                    TeamName2 = team2Name
                });
            }

            return games;
        }
        protected async Task<List<Game>> Resolve(List<GameDescriptor> gameDescriptors, IGameStorageReader storage)
        {
            var teamNames = gameDescriptors.Select(x => x.TeamName1).ToHashSet();
            teamNames.UnionWith(gameDescriptors.Select(x => x.TeamName2));

            var teamNamesToResolve = teamNames.Where(x => !_teamsLocalCache.ContainsKey(x)).ToList();
            var resolvedTeams = await storage.ResolveTeamInfo(teamNamesToResolve, _sportId);
            foreach (var rt in resolvedTeams)
                _teamsLocalCache.TryAdd(rt.Key, rt.Value);

            var retVal = new List<Game>();
            foreach (var gd in gameDescriptors)
            {
                var game = new Game()
                {
                    SportTypeId = _sportId,
                    CompetitionTypeId = _competitinoTypeId,
                    Time = gd.Time
                };

                if(string.Compare(gd.TeamName1, gd.TeamName2) >= 0)
                {
                    game.Team1Id = _teamsLocalCache[gd.TeamName1].Id;
                    game.Team2Id = _teamsLocalCache[gd.TeamName2].Id;
                }
                else
                {
                    game.Team1Id = _teamsLocalCache[gd.TeamName2].Id;
                    game.Team2Id = _teamsLocalCache[gd.TeamName1].Id;
                }

                retVal.Add(game);
            }

            return retVal;
        }
        protected List<GameDescriptor> FilterLocal(List<GameDescriptor> games)
        {
            var retVal = new List<GameDescriptor>();
            foreach (var g in games)
            {
                var key = g.Key;
                if(!_alreadyLoadedCache.Contains(key))
                {
                    retVal.Add(g);
                    _alreadyLoadedCache.Add(key);
                }
            }
            return retVal;
        }
    }
}
