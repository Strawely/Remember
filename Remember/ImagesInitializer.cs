using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace Remember
{
    public class ImagesInitializer

    {
        private CardButton[,] _img;

        private int _width;
        private int _height;

        private readonly List<String> _pictureList = new List<string>();

        private static ImagesInitializer _instance;

        private ImagesInitializer()
        {
       
        }

        public static ImagesInitializer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ImagesInitializer();
                }
                return _instance;
            }
        }
        public void Initialize(String pictureSetPath)
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
            InitializeImgSources(path);
        }

        public CardButton[,] Img
        {
            get { return _img; }
            set { _img = value; }
        }

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        private string GetCustomImagesPath()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            return dialog.SelectedPath;
        }

        private void GetImages(String path)
        {
            var files =
                Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                    .Where(s => s.EndsWith(".gif") || s.EndsWith(".jpg") || s.EndsWith(".png"));
            int k = _height * _width / 2;
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
            if (_width * _height > _pictureList.Count)
            {
                throw new NotEnoughPicturesException();
            }
        }

        private void InitializeImgSources(String path)
        {
            GetImages(path);
            _img = new CardButton[_width, _height];
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    _img[i, j] = new CardButton();
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