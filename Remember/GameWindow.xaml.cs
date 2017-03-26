using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using HorizontalAlignment = System.Windows.HorizontalAlignment;

namespace Remember
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow
    {
        private CardButton[,] _img;
        private CardButton[] _tmpImages = new CardButton[2];

        private List<String> _pictureList = new List<string>();
        private String[] _imgFilesStrings;

        public GameWindow(int width, int height, String pictureSetPath)
        {
            InitializeComponent(width, height);
            String path;
            if (pictureSetPath != null)
            {
                path = pictureSetPath;
            }
            else
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.ShowDialog();
                path = dialog.SelectedPath;
            }
            GetImages(path, width, height);
            InitButtonPictures();
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

                for (int j = 0; j < 2; j++)
                {
                    _tmpImages[j].Click -= imgBtn_Click;
//                    _tmpImages[j].IsEnabled = false;
                    _tmpImages[j] = null;
                }
            }

        }

        private void InitButtonPictures()
        {
            for (int i = 0; i < _img.GetLength(0); i++)
            {
                for (int j = 0; j < _img.GetLength(1); j++)
                {
                    var rnd = GenRndImage();
                    _img[i,j].InternalContent = new Image()
                    {
                        Source = new BitmapImage(new Uri(rnd))
                    };
                }
            }
        }

        public String GenRndImage()
        {
            Random random = new Random();
            int i = random.Next(_pictureList.Count);
            String s = _pictureList[i];
            _pictureList.RemoveAt(i);
            return s;

        }

        private void InitializeComponent(int width, int height)
        {
            InitializeComponent();
            UniformGrid.Columns = width;
            _img = new CardButton[width,height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    _img[i, j] = new CardButton()
                    {
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch,
                        BorderThickness = new Thickness(5,5,5,5)
                    };
                    _img[i, j].Background = CardButton.DefaultBackground;
                    _img[i,j].Click += imgBtn_Click;
                    UniformGrid.Children.Add(_img[i, j]);
                    

                }
            }
        }
    }
}
