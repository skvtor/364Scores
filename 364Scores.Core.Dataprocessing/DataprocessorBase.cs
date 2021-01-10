using Scores364.Core.Common;
using Scores364.Core.Common.Helpers;
using Scores364.Core.Common.Interfaces;
using Scores364.Core.Common.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Scores364.Core.Dataprocessing.Base
{
    public class DataprocessorBase: Configurable<DataprocessorConfig>
    {
        protected IEventQueueReader Queue { get; private set; }
        protected IGameStorageClient Storage { get; private set; }

        private object _synObj = new object();
        private Task _backTask = null;
        private CancellationTokenSource _cancellationTokenSource = null;
        private bool _isStarted = false;

        public DataprocessorBase(ISystemConfig configProvider, IEventQueueReader queue, IGameStorageClient storage)
            :base(configProvider)
        {
            Queue = queue;
            Storage = storage;
        }

        public void Start()
        {
            ReloadConfig();
            lock(_synObj)
            {
                if (_isStarted)
                    return;

                _isStarted = true;
                _cancellationTokenSource = new CancellationTokenSource();

                _backTask = Task.Factory.StartNew(ProcessingLoop, _cancellationTokenSource.Token, TaskCreationOptions.LongRunning);
            }
        }

        public void Stop()
        {
            lock (_synObj)
            {
                if (!_isStarted)
                    return;

                _isStarted = false;
                _cancellationTokenSource.Cancel();
            }
        }

        private async void ProcessingLoop(object objParam)
        {
            var cancellationToken = (CancellationToken)objParam;
            while (!cancellationToken.IsCancellationRequested)
            {
                var gamesPage = new Dictionary<string, Game>();
                int itemsToProcess = Config.ProcessingPageSize;
                while (itemsToProcess > 0)
                {
                    var game = await Queue.Dequeue();
                    if (game == null)
                        break;

                    var key = GamesProcessingHelper.BuildKey(game);
                    if (gamesPage.TryAdd(key, game))
                        itemsToProcess--;
                }

                if (gamesPage.Count > 0)
                    await Storage.AddGames(gamesPage.Values);
                else
                    await Task.Delay(Config.DelayMsOnIdle);
            }
        }
    }
}
