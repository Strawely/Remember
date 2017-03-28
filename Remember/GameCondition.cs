using System;
using System.Collections.Generic;

namespace Remember
{
    /// <summary>
    /// Тут будет синглтон
    /// </summary>

    [Serializable]
    public class GameCondition
    {
        private List<int> _highScoreTime;
        private List<int> _highScoreClicks;

        public GameCondition(List<int> highScoreTime, List<int> highScoreClicks)
        {
            _highScoreTime = highScoreTime;
            _highScoreClicks = highScoreClicks;
        }

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