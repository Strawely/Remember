using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Button = System.Windows.Controls.Button;

namespace Remember
{
    [Serializable]
    public class CardButton:Button, ISerializable
    {
        private bool _shown;
        private Image _internalContent;
//        private String _contentToString;
        private static Brush _defaultBackground = new ImageBrush(new BitmapImage(new Uri(MainWindow.GetSourcesPath() + "src\\backgr.png")));

        public bool Shown
        {
            get { return _shown; }
            set
            {
                ImageBrush brush = new ImageBrush();
                _shown = value;
                if (_shown)
                {
                    brush.ImageSource = _internalContent.Source;
                    Content = new TextBlock()
                    {
                        Background = brush
                    };
                }
                else
                {
                    
                    Content = new TextBlock()
                    {
                        Background = _defaultBackground
                    };
                }
            }
        }

        public bool IsEqual(CardButton cardButton)
        {
//            return _contentToString.Equals(cardButton._contentToString);
            BitmapImage image1 = (BitmapImage)this.InternalContent.Source;
            BitmapImage image2 = (BitmapImage)cardButton.InternalContent.Source;
            if (image1 == null || image2 == null)
            {
                return false;
            }
            return ToBytes(image1).SequenceEqual(ToBytes(image2));
        }

        private byte[] ToBytes(BitmapImage image)
        {
            byte[] data = new byte[] {};
            if (image != null)
            {
                var encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                using (MemoryStream memory = new MemoryStream())
                {
                    encoder.Save(memory);
                    data = memory.ToArray();
                }
                return data;
            }
            return data;
        }

        public Image InternalContent
        {
            get { return _internalContent; }
            set
            {
                _internalContent = value;
//                _contentToString = ((BitmapImage) _internalContent.Source).UriSource.ToString();
            }
        }

        public static Brush DefaultBackground
        {
            get { return _defaultBackground; }
        }

        public CardButton()
        {
            VerticalContentAlignment = VerticalAlignment.Stretch;
            HorizontalContentAlignment = HorizontalAlignment.Stretch;
            BorderThickness = new Thickness(5, 5, 5, 5);
            Content = new TextBlock
            {
                Background = DefaultBackground
            };
        }

        public CardButton(SerializationInfo info, StreamingContext context)
        {
            Shown = info.GetBoolean("Shown");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Shown", _shown);
        }
    }
}