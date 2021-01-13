using System;
using System.Collections.Generic;
using System.Text;

namespace Scores364.Core.Common.Models
{
    public class GameFilteringOptions
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string LanguageId { get; set; }
    }
}
