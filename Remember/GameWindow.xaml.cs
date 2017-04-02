using System;
using System.Windows;
using MessageBox = System.Windows.Forms.MessageBox;
using Timer = System.Windows.Forms.Timer;

namespace Remember
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow
    {
        private CardButton[,] _img;
        private readonly CardButton[] _tmpImages = new CardButton[2];

        private const int TimerInterval = 1;
        private readonly int _width;
        private readonly int _height;
        private int _leftCardsCount;
        private int _timeCount;
        private int _clicksCount;

        private readonly Timer _timer = new Timer();                                          
        
        public GameWindow(int width, int height, String pictureSetPath)
        {
            _width = width;
            _height = height;
            _leftCardsCount = width * height / 2;
            String path = pictureSetPath;
            InitializeComponent(path);
            InitizlizeTimer();
        }

        private void InitizlizeTimer()
        {
            _timer.Interval = TimerInterval;
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }
        
        private void _timer_Tick(object sender, EventArgs e)
        {
            _timeCount++;
        }

        /************************
         * Здесь надо упростить *
         ************************/
        private void imgBtn_Click(object sender, RoutedEventArgs e)
        {
            _clicksCount++;
            CardButton btn = (CardButton) sender;
            int i = 0;
            while (i < _tmpImages.Length && _tmpImages[i] != null)
            {
                i++;
            }
            switch (i)
            {
                case 2:
                    for (int j = 0; j < 2; j++)
                    {
                        _tmpImages[j].Shown = false;
                        _tmpImages[j] = null;
                    }
                    _tmpImages[0] = btn;
                    btn.Shown = true;
                    break;
                case 0:
                case 1:
                    _tmpImages[i] = btn;
                    btn.Shown = true;
                    break;
            }
            if (_tmpImages[0] != null && _tmpImages[1] != null && _tmpImages[0].CompareContent(_tmpImages[1]))
            {
                _leftCardsCount--;
                for (int j = 0; j < 2; j++)
                {
                    _tmpImages[j].Click -= imgBtn_Click;
                    _tmpImages[j] = null;
                }
            }
            if (_leftCardsCount == 0)
            {
                _timer.Stop();
                OnWinning();
            }
        }

        private void ShowHighScore()
        {
            Window highScoreWindow = new HighScore(_timeCount, _clicksCount);
            highScoreWindow.Show();
            highScoreWindow.Activate();
        }

        private void OnWinning()
        {
            MessageBox.Show("Clicks: " + _clicksCount + "\nTime: " + _timeCount + " ms", Properties.Resources.GameWindow_OnWinning_Score);
            ShowHighScore();
        }

        private void InitImgMatrix(String path)
        {
            var imageInitializer = ImagesInitializer.Instance;
            imageInitializer.Width = _width;
            imageInitializer.Height = _height;
            imageInitializer.Initializer(path);
            _img = imageInitializer.Img;
        }

        private void InitializeComponent(String path)
        {
            InitializeComponent();
            UniformGrid.Columns = _width;
            InitImgMatrix(path);
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    _img[i, j].Click += imgBtn_Click;
                    UniformGrid.Children.Add(_img[i, j]);
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
