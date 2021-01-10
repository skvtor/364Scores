using Scores364.Core.Common.Interfaces;
using System.Threading;

namespace Scores364.Core.Common
{
    public abstract class Configurable<T>
    {
        protected T Config { get { return _configurationBuffer[_configurationIndex]; } }

        private volatile int _configurationIndex;
        private T[] _configurationBuffer;
        private ISystemConfig _configProvider;

        public Configurable(ISystemConfig configProvider)
        {
            _configurationBuffer = new T[2];
            _configurationIndex = 1;
            _configProvider = configProvider;
        }

        public void ReloadConfig()
        {
            int newIndex = (_configurationIndex + 1) % 2;
            _configurationBuffer[newIndex] = _configProvider.GetConfigSection<T>();
            Interlocked.Exchange(ref _configurationIndex, newIndex);
        }
    }
}
