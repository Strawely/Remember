using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Remember
{
    public class CardButton:Button
    {
        private bool _shown = false;
        private Image _internalContent;
        private String _contentToString;
        private static Brush _defaultBackground = new ImageBrush(new BitmapImage(new Uri("C:\\Users\\solom\\Documents\\visual studio 2017\\Projects\\Remember\\src\\backgr.png")));

        public bool Shown
        {
            get { return _shown; }
            set
            {
                BrushConverter brushConverter = new BrushConverter();
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



        public bool CompareContent(CardButton cardButton)
        {
            return this._contentToString.Equals(cardButton._contentToString);
        }

        public Image InternalContent
        {
            get { return _internalContent; }
            set
            {
                _internalContent = value;
                _contentToString = ((BitmapImage) _internalContent.Source).UriSource.ToString();
            }
        }

        public static Brush DefaultBackground
        {
            get { return _defaultBackground; }
        }
    }
}