using System;
using System.Collections.Generic;
using System.Text;

namespace _364Scores.Core.Common.Interfaces
{
    public interface ISystemConfig
    {
        T GetConfigSection<T>();
    }
}
