using _364Scores.Core.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _364Scores.Core.Dataprocessing.Base
{
    public class DataprocessorBase
    {
        protected DataprocessorConfig Config { get; private set; }
        protected IEventQueueReader Queue { get; private set; }
        protected IGameStorage Storage { get; private set; }

        public DataprocessorBase(DataprocessorConfig config, IEventQueueReader queue, IGameStorage storage)
        {
            Config = config;
            Queue = queue;
            Storage = storage;
        }

        public virtual Task Start() { return Task.FromResult(1); }
        public virtual Task Stop() { return Task.FromResult(1); }

        public virtual Task CheckUpdate() { return Task.FromResult(1); }
    }
}
