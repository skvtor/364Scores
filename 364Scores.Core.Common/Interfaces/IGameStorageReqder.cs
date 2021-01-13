using Scores364.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scores364.Core.Common.Interfaces
{
    public interface IGameStorageReader
    {
        Task<IDictionary<string, Team>> ResolveTeamByName(IEnumerable<string> name, Guid sportId, string languageId = null);
        Task<IEnumerable<Game>> FilterAlreadyPresent(IEnumerable<Game> games);
    }
}
