using Scores364.Core.Common.Interfaces;
using Scores364.Core.Dataprocessing;
using Scores364.Core.Datasources;
using System.Collections.Generic;

namespace Scores364.DemoWebMultiHoster.Demo
{
    public class LocalConfig : ISystemConfig
    {
        public T GetConfigSection<T>()
            where T : class, new()
        {
            //todo:  :(
            if (typeof(T).Name == typeof(DatasourcesConfig).Name)
            {
                return (new DatasourcesConfig()
                {
                    Datasources = new List<DatasourceInfo>
                    {
                        //new DatasourceInfo { Id = "ds1", TypeId = "DummyDatasource" },
                        new DatasourceInfo { Id = "ds2", TypeId = "Superplacar3DaysDatasource" }
                    },
                    DelayMs = 1000
                }) as T;
            }

            if (typeof(T).Name == typeof(DataprocessorConfig).Name)
            {
                return (new DataprocessorConfig()
                {
                    DelayMsOnIdle = 6000,
                    ProcessingPageSize = 3
                }) as T;
            }

            return new T();
        }
    }
}
