﻿using Scores364.Core.Common.Models;
using System;
using System.Threading.Tasks;

namespace Scores364.Core.Common.Interfaces
{
    public interface IEventQueueWriter
    {
        Task Enqueue(Game game);
    }
}
