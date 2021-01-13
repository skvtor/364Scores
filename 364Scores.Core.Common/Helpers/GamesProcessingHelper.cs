using Scores364.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scores364.Core.Common.Helpers
{
    public class GamesProcessingHelper
    {
        public static string BuildGameKey(Game game)
        {
            //todo:
            return $"{game.Time.ToString("u")}{game.SportTypeId.ToString("N")}{game.CompetitionTypeId}{game.Team1Id.ToString("N")}{game.Team2Id.ToString("N")}";
        }

        public static string BuildGameDescriptorKey(GameDescriptor game)
        {
            //todo:
            return $"{game.Time.ToString("u")}{game.TeamName1}{game.TeamName2}";
        }
    }
}
