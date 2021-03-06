﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;

namespace Remember
{
    public class ImagesInitializer

    {
        private CardButton[,] _img;

        private int _width;
        private int _height;

        private readonly List<BitmapImage> _pictureList = new List<BitmapImage>();

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
        public void Initialize(ResourceSet pictureSetPath)
        {
            if (pictureSetPath != null)
            {
                GetImages(pictureSetPath);
            }
            else
            {
                GetCustomImages();
            }
            InitializeImgSources();
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

        private void GetCustomImages()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            var files =
               Directory.GetFiles(dialog.SelectedPath, "*.*", SearchOption.AllDirectories)
                   .Where(s => s.EndsWith(".gif") || s.EndsWith(".jpg") || s.EndsWith(".png"));
            int k = _height * _width / 2;
            int i = 0;
            foreach (var entry in files)
            {
                if (i < k)
                {
                    BitmapImage bmpTmp = new BitmapImage(new Uri(entry));
                    _pictureList.Add(bmpTmp);
                    _pictureList.Add(bmpTmp);
                    i++;
                }
            }
            if (_width * _height > _pictureList.Count)
            {
                throw new NotEnoughPicturesException();
            }
        }

        private BitmapImage BmpToBitmapImage(Bitmap bmp)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bmp.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }

        private void GetImages(ResourceSet path)
        {
           
            int k = _height * _width / 2;
            int i = 0;
            ResourceSet resourceSet = path; /*PictureSet1.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true,
                true);*/
            foreach (DictionaryEntry entry in resourceSet)
            {
                if (i < k)
                {
                    BitmapImage bmpTmp = BmpToBitmapImage((Bitmap) entry.Value);
                    _pictureList.Add(bmpTmp);
                    _pictureList.Add(bmpTmp);
                    i++;
                }
            }
            if (_width * _height > _pictureList.Count)
            {
                throw new NotEnoughPicturesException();
            }
        }

        private void InitializeImgSources()
        {
            _img = new CardButton[_width, _height];
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    _img[i, j] = new CardButton();
                    var rnd = GenRndImage();
                    _img[i, j].InternalContent = rnd;
                }
            }
        }

        private Image GenRndImage()
        {
            Random random = new Random();
            int i = random.Next(_pictureList.Count);
            Image img = new Image
            {
                Source = _pictureList[i]
            };
            _pictureList.RemoveAt(i);
            return img;

        }
    }
}