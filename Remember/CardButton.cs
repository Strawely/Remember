using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Remember
{
    public class CardButton:Button
    {
        private bool _shown = false;
        private object _internalContent;
        private String _contentToString;


        public bool Shown
        {
            get { return _shown; }
            set
            {
                _shown = value;
                if (_shown)
                {
                    Content = _internalContent;
                }
                else
                {
                    Content = null;
                }
            }
        }



        public bool CompareContent(CardButton cardButton)
        {
            return this._contentToString.Equals(cardButton._contentToString);
        }

        public object InternalContent
        {
            get
            {
                if (_internalContent.GetType() == typeof(Image))
                {
                    return _internalContent.ToString();
                }
                else
                {
                    throw new Exception();
                }
            }
            set
            {
                _internalContent = value;
                _contentToString = ((BitmapImage) ((Image) _internalContent).Source).UriSource.ToString();
            }
        }
    }
}