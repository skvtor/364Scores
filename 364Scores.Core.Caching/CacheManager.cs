using Scores364.Core.Common.Interfaces;
using Scores364.Core.Common.Models;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Scores364.Core.Caching
{
    public class CacheManager: ICacheManager
    {
        public async Task<GamesPage> GetPage(string pageToken)
        {
            return null;
        }
        public async Task CachePage(string pageToken, GamesPage page)
        {
        }
    }
}
