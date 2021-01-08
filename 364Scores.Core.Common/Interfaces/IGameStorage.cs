using _364Scores.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _364Scores.Core.Common.Interfaces
{
    public interface IGameStorage
    {
        IDictionary<string, Team> ResolveTeamInfo(IEnumerable<string> teamLocalNames);
        IEnumerable<Game> FilterAlreadyPresent(IEnumerable<Game> games);
        Task AddGames(IEnumerable<Game> games);
    }
}
