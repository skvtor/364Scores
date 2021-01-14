using Scores364.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scores364.Core.Common.Models
{
    public class GamesPageCachableContainer
    {
        public GamesPage Page { get; set; }
        public CachedDataTypeId DataTypeId { get; set; }
    }
}
