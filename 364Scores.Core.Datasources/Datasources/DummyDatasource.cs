using Scores364.Core.Common.Interfaces;
using Scores364.Core.Common.Models;
using Scores364.Core.Datasources.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scores364.Core.Datasources.Datasources
{
    public class DummyDatasource : IDatasource
    {
        private static readonly string[]_teams = new string[] {
            "team01",
            "team02",
            "team03",
            "team04",
            "team05",
            "team06",
            "team07",
            "team08",
            "team09",
            "team10"
        };
        private Random _rnd = new Random(DateTime.UtcNow.Millisecond);

        public async Task CheckUpdate(IEventQueueWriter queue, IGameStorageReader storage, CancellationToken cancellationToken)
        {
            var index1 = _rnd.Next(0, 9);
            var index2 = _rnd.Next(0, 8);
            if (index1 == index2)
                index2 = 9;

            var teams = (await storage.ResolveTeamByName(new[] { _teams[index1], _teams[index2] }, Guid.Empty))
                .Select(x=>x.Value)
                .ToArray();

            var game = new Game
            {
                CompetitionTypeId = "wc",
                Team1Id = teams[0].Id,
                Team2Id = teams[1].Id,
                Time = DateTime.UtcNow.AddSeconds(5)
            };

            await queue.Enqueue(new[] { game });
        }

        public void Configure(Dictionary<string, string> config)
        {
        }
    }
}
