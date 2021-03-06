﻿using System;
using System.Collections.Generic;

namespace Remember
{

    [Serializable]
    public sealed class GameCondition
    {
        private List<int> _highScoreTime;
        private List<int> _highScoreClicks;
        

        public List<int> HighScoreTime
        {
            get { return _highScoreTime; }
            set { _highScoreTime = value; }
        }

        public List<int> HighScoreClicks
        {
            get { return _highScoreClicks; }
            set { _highScoreClicks = value; }
        }
    }
}