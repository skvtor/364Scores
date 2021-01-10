using System;
using System.Collections.Generic;
using System.Text;

namespace Scores364.Core.Datasources
{
    public class DatasourcesConfig
    {
        public int DelayMs { get; set; }
        public List<DatasourceInfo> Datasources { get; set; }
    }

    public class DatasourceInfo
    {
        public string Id { get; set; }
        public string TypeId { get; set; }
    }

}
