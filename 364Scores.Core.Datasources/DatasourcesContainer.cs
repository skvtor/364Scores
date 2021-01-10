using Scores364.Core.Common;
using Scores364.Core.Common.Interfaces;
using Scores364.Core.Datasources.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Scores364.Core.Datasources
{
    public class DatasourcesContainer : Configurable<DatasourcesConfig>
    {
        private IEventQueueWriter _eventQueue;
        private IGameStorageClient _gameStorage;
        private IDatasourcesFabric _datasourcesFabric;

        private List<IDatasource> _datasources = new List<IDatasource>();
        private object _syncObj = new object();
        private CancellationTokenSource _cancellationTokenSource = null;
        private bool _isStarted = false;


        public DatasourcesContainer(IDatasourcesFabric datasourcesFabric, ISystemConfig configProvider, IEventQueueWriter eventQueue, IGameStorageClient gameStorage) 
            : base(configProvider)
        {
            _datasourcesFabric = datasourcesFabric;
            _eventQueue = eventQueue;
            _gameStorage = gameStorage;
        }
        public Task Start()
        {
            ReloadConfig();

            foreach(var dsInfo in Config.Datasources)
            {
                var ds = _datasourcesFabric.Build(dsInfo);
                _datasources.Add(ds);
            }

            return ProcessingLoop();
        }
        public void Stop()
        {
            lock (_syncObj)
            {
                if (_cancellationTokenSource != null)
                    _cancellationTokenSource.Cancel();
            }
        }

        private async Task ProcessingLoop()
        {
            var delay = Config.DelayMs;

            try
            {
                lock (_syncObj)
                {
                    if (_isStarted)
                        return;

                    _isStarted = true;
                    _cancellationTokenSource = new CancellationTokenSource();
                }

                var cancellationToken = _cancellationTokenSource.Token;

                List<Task> list = new List<Task>();
                while (!cancellationToken.IsCancellationRequested)
                {
                    list.Clear();
                    foreach (var ds in _datasources)
                        list.Add(ds.CheckUpdate(_eventQueue, _gameStorage, cancellationToken));

                    await Task.WhenAll(list).ConfigureAwait(false);
                }
            }
            finally
            {
                lock (_syncObj)
                {
                    _isStarted = false;
                    _cancellationTokenSource.Dispose();
                    _cancellationTokenSource = null;
                }
            }
        }
    }
}
