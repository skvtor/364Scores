using _364Scores.Core.Common.Models;
using System;
using System.Threading.Tasks;

namespace _364Scores.Core.Common.Interfaces
{
    public interface IEventQueueWriter
    {
        Task Enqueue(Game game);
    }
}
