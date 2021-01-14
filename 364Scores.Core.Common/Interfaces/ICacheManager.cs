using Scores364.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scores364.Core.Common.Interfaces
{
    public interface ICacheManager
    {
        Task<GamesPage> GetPage(string pageToken);
        Task CachePage(string pageToken, GamesPage page);
    }
}
