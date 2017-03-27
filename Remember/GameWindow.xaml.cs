using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using HorizontalAlignment = System.Windows.HorizontalAlignment;
using Image = System.Windows.Controls.Image;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Remember
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow
    {
        private CardButton[,] _img;
        private CardButton[] _tmpImages = new CardButton[2];

        private List<BitmapImage> _pictureList = new List<BitmapImage>();
        private String[] _imgFilesStrings;

        private int _timerCount;
        private int _clickCounter;
        private int _notOpenedCount;

        private Timer _timer;

        public GameWindow(int width, int height, List<BitmapImage> picBitmaps)
        {
            InitializeComponent(width, height);
            _pictureList = picBitmaps;
            InitButtonPictures();
        }

//        private void GetImages(Bitmap[] path, int width, int height)
//        {
////            _imgFilesStrings = Directory.GetFiles(path, "*.jpg");
//            if (width * height / 2 > path.Length)
//            {
//                throw new Exception();      //заменить на нормальное исключение
//            }
//            int k = height * width / 2;
//            for (int i = 0; i < k; i++)
//            {
//                _pictureList.Add(path[i]);
//                _pictureList.Add(path[i]);
//            }
//        }

        private void imgBtn_Click(object sender, RoutedEventArgs e)
        {
            CardButton btn = (CardButton) sender;
            int i = 0;
            if (_clickCounter == 0)
            {
                _timer.Start();
            }
            _clickCounter++;
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

                for (int j = 0; j < 2; j++)
                {
                    _tmpImages[j].Click -= imgBtn_Click;
//                    _tmpImages[j].IsEnabled = false;
                    _tmpImages[j] = null;
                }
                _notOpenedCount--;
                if (_notOpenedCount == 0)
                {
                    OnWinningDialog();
                    _timer.Stop();
                }
            }

        }

        private void OnWinningDialog()
        {
            
            MessageBox.Show("Time: " + _timerCount + "\n" + "Clicks: " + _clickCounter, "Points");
            this.Close();
        }

//        private ImageBrush CreateBrushFromBitmap(Bitmap bmp)
//        {
//            return
//                new ImageBrush(Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty,
//                    BitmapSizeOptions.FromEmptyOptions()));
//        }

        private void InitButtonPictures()
        {
            for (int i = 0; i < _img.GetLength(0); i++)
            {
                for (int j = 0; j < _img.GetLength(1); j++)
                {
                    BitmapImage rnd = GenRndImage();
                    
                    _img[i, j].InternalContent = new Image
                    {
                        Source = rnd
                    };
                }
            }
        }

        public BitmapImage GenRndImage()
        {
            Random random = new Random();
            int i = random.Next(_pictureList.Count);
            BitmapImage s = _pictureList[i];
            _pictureList.RemoveAt(i);
            return s;

        }

        public void _timer_Tick(object sender, EventArgs eventArgs)
        {
            _timerCount++;
        }

        private void InitializeComponent(int width, int height)
        {
            InitializeComponent();
            _notOpenedCount = width*height/2;
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += _timer_Tick;
            UniformGrid.Columns = width;
            _img = new CardButton[width,height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    _img[i, j] = new CardButton
                    {
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch,
                        BorderThickness = new Thickness(5, 5, 5, 5),
                        Content = new TextBlock()
                        {
                            Background = CardButton.DefaultBackground
                        },
                        HorizontalContentAlignment = HorizontalAlignment.Stretch,
                        VerticalContentAlignment = VerticalAlignment.Stretch
                    };
                    _img[i,j].Click += imgBtn_Click;
                    UniformGrid.Children.Add(_img[i, j]);
                    

                }
            }
        }
        
    }
}
