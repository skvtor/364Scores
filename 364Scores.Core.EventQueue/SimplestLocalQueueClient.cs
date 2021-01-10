using Scores364.Core.Common.Interfaces;
using Scores364.Core.Common.Models;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Scores364.Core.EventQueue
{
    public class SimplestLocalQueueClient : IEventQueueClient
    {
        ConcurrentQueue<Game> _queue = new ConcurrentQueue<Game>();

        public Task<Game> Dequeue()
        {
            Game retVal;
            if (_queue.TryDequeue(out retVal))
                return Task.FromResult(retVal);

            return Task.FromResult((Game)null);
        }

        public Task Enqueue(Game game)
        {
            _queue.Enqueue(game);
            return Task.FromResult(1);
        }
    }
}
