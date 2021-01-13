using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scores364.Api.Manager.Models
{
    public class GamesPageParams
    {
        public string LanguageId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
