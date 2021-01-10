using Scores364.Core.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Scores364.Core.Datasources.Interfaces
{
    public interface IDatasource
    {
        Task CheckUpdate(IEventQueueWriter queue, IGameStorageClient storage, CancellationToken cancellationToken);
        bool IsRun { get; }
    }
}
