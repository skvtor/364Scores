using Scores364.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scores364.Core.Common.Interfaces
{
    public interface IGameStorageReader
    {
        Task<IDictionary<string, Team>> ResolveTeamInfo(IEnumerable<string> teamLocalNames, Guid sportId);
        Task<IEnumerable<Game>> FilterAlreadyPresent(IEnumerable<Game> games);
    }
}
