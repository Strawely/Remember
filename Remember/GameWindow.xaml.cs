using System;
using System.Collections.Generic;
using System.IO;
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
        private const int TimerInterval = 1000;
        private CardButton[,] _img;                                                     //инициализируется десериализацией
        private CardButton[] _tmpImages = new CardButton[2];

        private int _leftCardsCount;
        private int _timeCount;
        private int _clicksCount;

        private bool[,] _isShown = null;

        private Timer _timer = new Timer();                                            //стартовать таймер, если ещё не запущен

        private List<String> _pictureList = new List<string>();                         // что-нибудь сделать с дублированием списка массивом
        private String[] _imgFilesStrings;
        private String[,] _imgFiles = null;

        public GameWindow(int width, int height, String pictureSetPath, GameCondition condition)
        {
            
            _leftCardsCount = width * height / 2;
            String path;
            if (pictureSetPath != null)
            {
                path = pictureSetPath;
            }
            else
            {
                path = GetCustomImagesPath();                                          //добавить информационное сообщение для пользователя                                           
            }
            if (condition != null)
            {
                Deserialization(condition);
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

        private void Deserialization(GameCondition condition)
        {
            _imgFiles = condition.ImgFiles;
            _isShown = condition.IsShown;
            _timeCount = condition.CurrentTime;
            _clicksCount = condition.CurrentClicks;
            _leftCardsCount = condition.LeftCards;
        }
        
        private void _timer_Tick(object sender, EventArgs e)
        {
            _timeCount++;
        }

        private void GetImages(String path, int width, int height)
        {
            _imgFilesStrings = Directory.GetFiles(path, "*.jpg");
            if (width * height / 2 > _imgFilesStrings.Length)
            {
                throw new Exception();      //заменить на нормальное исключение
            }
            int k = height * width / 2;
            for (int i = 0; i < k; i++)
            {
                _pictureList.Add(_imgFilesStrings[i]);
                _pictureList.Add(_imgFilesStrings[i]);
            }
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

        private void OnWinning()
        {
            MessageBox.Show("Clicks: " + _clicksCount + "\nTime: " + _timeCount, Properties.Resources.GameWindow_OnWinning_Score);
            this.Closing -= Window_Closing;
        }
        
        private String GenRndImage()
        {
            Random random = new Random();
            int i = random.Next(_pictureList.Count);
            String s = _pictureList[i];
            _pictureList.RemoveAt(i);
            return s;

        }

        private String[,] GenRndPathArray(int width, int height)
        {
            String[,] s = new String[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    s[i, j] = GenRndImage();
                }
            }
            return s;
        }

        private void InitializeComponent(int width, int height, String path)
        {
            InitializeComponent();
            GetImages(path, width, height);
            UniformGrid.Columns = width;
            _img = new CardButton[width, height];
            String[,] fileStrings;
            if (_imgFiles == null && _isShown == null)
            {
                fileStrings = GenRndPathArray(width, height);
                _imgFiles = fileStrings;
                _isShown = new bool[width, height];
            }
            else
            {
                fileStrings = _imgFiles;
            }
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
                    _img[i, j].InternalContent = new Image()
                    {
                        Source = new BitmapImage(new Uri(fileStrings[i,j]))
                    };
                    _img[i, j].Shown = _isShown[i, j];
                    _img[i, j].Click += imgBtn_Click;
                    UniformGrid.Children.Add(_img[i, j]);


                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int w = _img.GetLength(0);
            int h = _img.GetLength(1);
            bool[,] isShown = new bool[w, h];
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    isShown[i, j] = _img[i, j].Shown;
                }
            }
            GameCondition condition = new GameCondition(_timeCount, _clicksCount, _leftCardsCount, _imgFiles, isShown);
            FileStream fileStream = File.Create("data.dat");
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fileStream, condition);
            fileStream.Close();
        }
    }
}
