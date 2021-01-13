using Scores364.Core.Common.Interfaces;
using Scores364.Core.Common.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scores364.Core.EventQueue
{
    public class SimplestLocalQueueClient : IEventQueueClient
    {
        private static ConcurrentQueue<Game> _queue = new ConcurrentQueue<Game>();

        public Task<Game> Dequeue()
        {
            Game retVal;
            if (_queue.TryDequeue(out retVal))
                return Task.FromResult(retVal);

            return Task.FromResult((Game)null);
        }

        public Task Enqueue(IEnumerable<Game> games)
        {
            foreach(var game in games)
                _queue.Enqueue(game);

            return Task.FromResult(1);
        }
    }
}
