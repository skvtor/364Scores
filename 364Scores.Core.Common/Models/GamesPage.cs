using Scores364.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scores364.Core.Common.Models
{
    public class GamesPage
    {
        public string PageToken { get; set; }
        public List<GameInfo> Games { get; set; }
    }
}
