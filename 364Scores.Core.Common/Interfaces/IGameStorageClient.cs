using Scores364.Core.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scores364.Core.Common.Interfaces
{
    public interface IGameStorageClient: IGameStorageReader
    {
        Task AddGames(IEnumerable<Game> games);
        Task<List<GameInfo>> GetGameInfos(GameFilteringOptions options);
    }
}
