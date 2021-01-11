using Scores364.Core.Common.Interfaces;
using Scores364.Core.Common.Models;
using Scores364.Core.Datasources.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scores364.Core.Datasources.Datasources
{
    public class DummyDatasource : IDatasource
    {
        public Task CheckUpdate(IEventQueueWriter queue, IGameStorageClient storage, CancellationToken cancellationToken)
        {
            queue.Enqueue(new Game { CompetitionTypeId = "wc", SportTypeId = Guid.NewGuid(), Team1Id = Guid.NewGuid(), Team2Id = Guid.NewGuid(), Time = DateTime.UtcNow });
            return Task.FromResult(1);
        }
    }
}
