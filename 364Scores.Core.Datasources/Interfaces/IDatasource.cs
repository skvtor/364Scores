using Scores364.Core.Common.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Scores364.Core.Datasources.Interfaces
{
    public interface IDatasource
    {
        void Configure(Dictionary<string, string> config);
        Task CheckUpdate(IEventQueueWriter queue, IGameStorageReader storage, CancellationToken cancellationToken);
    }
}
