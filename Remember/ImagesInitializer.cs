using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using HorizontalAlignment = System.Windows.HorizontalAlignment;

namespace Remember
{
    public class ImagesInitializer

    {
        private CardButton[,] _img;

        private List<String> _pictureList = new List<string>();

        public ImagesInitializer(int width, int height, String pictureSetPath)
        {
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
        }

        public CardButton[,] Img
        {
            get { return _img; }
            set { _img = value; }
        }

        private string GetCustomImagesPath()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            return dialog.SelectedPath;
        }

        private void GetImages(String path, int width, int height)
        {
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
                throw new NotEnoughPicturesException();
            }
        }

        private void InitializeComponent(int width, int height, String path)
        {
            GetImages(path, width, height);
            _img = new CardButton[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    _img[i, j] = new CardButton()
                    {
                        VerticalContentAlignment = VerticalAlignment.Stretch,
                        HorizontalContentAlignment = HorizontalAlignment.Stretch,
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
                }
            }
        }

        private String GenRndImage()
        {
            Random random = new Random();
            int i = random.Next(_pictureList.Count);
            String s = _pictureList[i];
            _pictureList.RemoveAt(i);
            return s;

        }
    }
}