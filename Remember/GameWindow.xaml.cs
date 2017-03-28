using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using HorizontalAlignment = System.Windows.HorizontalAlignment;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Remember
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow
    {
        private const int TimerInterval = 1;
        private CardButton[,] _img;
        private CardButton[] _tmpImages = new CardButton[2];

        private int _leftCardsCount;
        private int _timeCount;
        private int _clicksCount;

        private Timer _timer = new Timer();                                          

        private List<String> _pictureList = new List<string>();
        private List<String> _imgFilesStrings;

        public GameWindow(int width, int height, String pictureSetPath)
        {
            
            _leftCardsCount = width * height / 2;
            String path;
            if (pictureSetPath != null)
            {
                path = pictureSetPath;
            }
            else
            {
                path = GetCustomImagesPath();                                                                                    
            }
            InitializeComponent(width, height, path);
            InitizlizeTimer();
        }

        private void InitizlizeTimer()
        {
            _timer.Interval = TimerInterval;
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private string GetCustomImagesPath()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            return dialog.SelectedPath;
        }
        
        private void _timer_Tick(object sender, EventArgs e)
        {
            _timeCount++;
        }

        private void GetImages(String path, int width, int height)
        {
            _imgFilesStrings = new List<string>();
            var files =
                Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                    .Where(s => s.EndsWith(".gif") || s.EndsWith(".jpg") || s.EndsWith(".png"));
            int k = height * width / 2;
            int i = 0;
            foreach (var abc in files)
            {
                if (i < k)
                {
                    _pictureList.Add(abc);
                    _pictureList.Add(abc);
                    i++;
                }
            }
            if (width * height > _pictureList.Count)
            {
                throw new Exception();      //заменить на нормальное исключение
            }
//            for (int i = 0; i < k; i++)
//            {
//                _pictureList.Add(_imgFilesStrings[i]);
//                _pictureList.Add(_imgFilesStrings[i]);
//            }
        }

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
        
        private String GenRndImage()
        {
            Random random = new Random();
            int i = random.Next(_pictureList.Count);
            String s = _pictureList[i];
            _pictureList.RemoveAt(i);
            return s;

        }

        private void InitializeComponent(int width, int height, String path)
        {
            InitializeComponent();
            GetImages(path, width, height);
            UniformGrid.Columns = width;
            _img = new CardButton[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    _img[i, j] = new CardButton()
                    {
                        HorizontalContentAlignment = HorizontalAlignment.Stretch,
                        VerticalContentAlignment = VerticalAlignment.Stretch,
                        BorderThickness = new Thickness(5, 5, 5, 5),
                        Content = new TextBlock()
                        {
                            Background = CardButton.DefaultBackground
                        }
                    };
                    var rnd = GenRndImage();
                    _img[i, j].InternalContent = new Image()
                    {
                        Source = new BitmapImage(new Uri(rnd))
                    };
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
