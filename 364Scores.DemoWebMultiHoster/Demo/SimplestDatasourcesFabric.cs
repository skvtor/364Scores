using Scores364.Core.Datasources;
using Scores364.Core.Datasources.Datasources;
using Scores364.Core.Datasources.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scores364.DemoWebMultiHoster.Demo
{
    public class SimplestDatasourcesFabric : IDatasourcesFabric
    {
        public SimplestDatasourcesFabric() { }

        public IDatasource Build(DatasourceInfo dsInfo)
        {
            return new DummyDatasource();
        }
    }
}
