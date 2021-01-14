using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scores364.Api.Manager.Models
{
    public class GamesPageOffsetParams
    {
        public string Token { get; set; }
        public int? Offset { get; set; }
    }
}
