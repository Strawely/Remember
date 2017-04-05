using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Remember
{
    /// <summary>
    /// Логика взаимодействия для High_Score.xaml
    /// </summary>
    public partial class HighScore
    {
        private List<int> _highScoreTime;
        private List<int> _highScoreClicks;

        public HighScore(int time, int clicks)
        {
            InitializeComponent();
            Deserialization();
            if (_highScoreClicks == null)
            {
                _highScoreTime = new List<int>();
                _highScoreClicks = new List<int>();
            }
            AddScoreToList(time, clicks);
            ShowHighScore();
        }

        private void ShowHighScore()
        {
            for (int i = 0; i < _highScoreClicks.Count; i++)
            {
                ListBoxTime.Items.Add(_highScoreTime[i]);
                ListBoxClicks.Items.Add(_highScoreClicks[i]);
            }
        }

        private void AddScoreToList(int time, int clicks)
        {
            int i = 0;
            while (i < _highScoreTime.Count && _highScoreTime != null && _highScoreTime[i] < time)
            {
                i++;
            }
            _highScoreTime.Insert(i, time);
            i = 0;
            while (i < _highScoreClicks.Count && _highScoreClicks != null && _highScoreClicks[i] < clicks)
            {
                i++;
            }
            _highScoreClicks.Insert(i, clicks);
        }

        private void Deserialization()
        {
            if (File.Exists("data.dat"))
            {
                FileStream fileStream = File.OpenRead("data.dat");
                BinaryFormatter bf = new BinaryFormatter();
                GameCondition condition = (GameCondition) bf.Deserialize(fileStream);
                _highScoreTime = condition.HighScoreTime;
                _highScoreClicks = condition.HighScoreClicks;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GameCondition condition = new GameCondition
            {
                HighScoreTime = _highScoreTime,
                HighScoreClicks = _highScoreClicks
            };
            FileStream fileStream = File.Create("data.dat");
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fileStream, condition);
            fileStream.Close();
        }
    }
}