using System;
using System.Collections.Generic;
using System.Text;

namespace Scores364.Core.Common.Models
{
    public class Game
    {
        public DateTime Time;
        public Guid SportTypeId;
        public string CompetitionTypeId;
        public Guid Team1Id;
        public Guid Team2Id;
    }
}
