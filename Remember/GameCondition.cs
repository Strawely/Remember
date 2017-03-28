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
        private int _currentTime;
        private int _currentClicks;
        private int _leftCards;

        private List<String> _imgList;
//        private CardButton[,] _buttons;
//        private List<int> _topScoreTime;
//        private List<int> _topScoreClicks;

        public GameCondition(int currentTime, int currentClicks, int leftCards, List<string> imgList /*, List<int> topScoreTime,
            List<int> topScoreClicks*/)
        {
            _currentClicks = currentClicks;
            _leftCards = leftCards;
            _imgList = imgList;
//            _topScoreTime = topScoreTime;
//            _topScoreClicks = topScoreClicks;
            _currentTime = currentTime;
        }

        public int CurrentTime
        {
            get { return _currentTime; }
            set { _currentTime = value; }
        }

        public int CurrentClicks
        {
            get { return _currentClicks; }
            set { _currentClicks = value; }
        }

        public int LeftCards
        {
            get { return _leftCards; }
            set { _leftCards = value; }
        }

        public List<string> ImgList
        {
            get { return _imgList; }
            set { _imgList = value; }
        }

//        public List<int> TopScoreTime
//        {
//            get { return _topScoreTime; }
//            set { _topScoreTime = value; }
//        }
//
//        public List<int> TopScoreClicks
//        {
//            get { return _topScoreClicks; }
//            set { _topScoreClicks = value; }
//        }
    }
}