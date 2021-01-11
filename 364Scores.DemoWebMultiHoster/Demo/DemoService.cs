using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scores364.Core.Common.Interfaces;
using Scores364.Core.Dataprocessing.Base;
using Scores364.Core.Datasources;
using Scores364.Core.Datasources.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Scores364.DemoWebMultiHoster.Demo
{
    public class DemoService : BackgroundService
    {
        private IServiceScope _serviceScope;

        private IDatasourcesFabric _datasourcesFabric;
        private ISystemConfig _configProvider;
        private IEventQueueClient _eventQueue;
        private IGameStorageClient _gameStorage;

        private DataprocessorBase _processor;
        private DatasourcesContainer _dssContainer;

        public DemoService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScope = serviceScopeFactory.CreateScope();
        }

        public override void Dispose()
        {
            _serviceScope.Dispose();
            base.Dispose();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _datasourcesFabric = _serviceScope.ServiceProvider.GetService<IDatasourcesFabric>();
            _configProvider = _serviceScope.ServiceProvider.GetService<ISystemConfig>();
            _eventQueue = _serviceScope.ServiceProvider.GetService<IEventQueueClient>();
            _gameStorage = _serviceScope.ServiceProvider.GetService<IGameStorageClient>();

            _processor = new DataprocessorBase(_configProvider, _eventQueue, _gameStorage);
            _dssContainer = new DatasourcesContainer(_datasourcesFabric, _configProvider, _eventQueue, _gameStorage);

             Task.Factory.StartNew((obj)=>_dssContainer.Start(), stoppingToken, TaskCreationOptions.LongRunning);
            _processor.Start();

            return Task.FromResult(1);
        }
    }
}
