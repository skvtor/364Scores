using Scores364.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scores364.Core.Common.Interfaces
{
    public interface IGameStorageClient
    {
        Task<IDictionary<string, Team>> ResolveTeamInfo(IEnumerable<string> teamLocalNames);
        Task<IEnumerable<Game>> FilterAlreadyPresent(IEnumerable<Game> games);
        Task AddGames(IEnumerable<Game> games);
    }
}
