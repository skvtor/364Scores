using Scores364.Core.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scores364.Core.Common.Models
{
    public class GameDescriptor
    {
        private string _key;
        public string Key
        {
            get {
                if (_key == null)
                    _key = GamesProcessingHelper.BuildGameDescriptorKey(this);
                return _key;
            }
        }

        public DateTime Time;
        public string TeamName1;
        public string TeamName2;
    }
}
