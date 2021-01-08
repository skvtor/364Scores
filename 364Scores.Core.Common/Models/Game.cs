using System;
using System.Collections.Generic;
using System.Text;

namespace _364Scores.Core.Common.Models
{
    public class Game
    {
        public DateTime Time;
        public Guid SportTypeId;
        public string CompetitionId;
        public Guid Team1Id;
        public Guid Team2Id;
    }
}
