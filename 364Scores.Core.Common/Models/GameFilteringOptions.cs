using System;
using System.Collections.Generic;
using System.Text;

namespace Scores364.Core.Common.Models
{
    public class GameFilteringOptions
    {
        public DateTime From { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string LanguageId { get; set; }
    }
}
