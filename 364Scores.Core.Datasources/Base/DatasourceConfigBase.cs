using _364Scores.Core.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _364Scores.Core.Datasources.Base
{
    public class DatasourceConfigBase
    {
        protected DatasourceConfig Config { get; private set; }
        protected IEventQueueWriter Queue { get; private set; }
        protected IGameStorage Storage { get; private set; }

        public DatasourceConfigBase(DatasourceConfig config, IEventQueueWriter queue, IGameStorage storage)
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
