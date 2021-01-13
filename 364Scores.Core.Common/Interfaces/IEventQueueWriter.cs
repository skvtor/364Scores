using Scores364.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scores364.Core.Common.Interfaces
{
    public interface IEventQueueWriter
    {
        Task Enqueue(IEnumerable<Game> games);
    }
}
