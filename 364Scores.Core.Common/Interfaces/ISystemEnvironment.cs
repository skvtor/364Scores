using System;
using System.Collections.Generic;
using System.Text;

namespace Scores364.Core.Common.Interfaces
{
    public interface ISystemEnvironment
    {
        IEventQueueClient SventQueue { get; }
        IGameStorageClient GameStorage { get; }
        ISystemConfig ConfigManager { get; }
    }
}
