using System;
using System.Collections.Generic;
using System.Text;

namespace Scores364.Core.Common.Interfaces
{
    public interface ISystemConfig
    {
        T GetConfigSection<T>() where T : class, new();
    }
}
