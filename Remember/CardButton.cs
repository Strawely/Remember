using System.Windows.Controls;

namespace Remember
{
    public class CardButton:Button
    {
        private bool _shown = false;
        private object _internalContent;


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

        public void ReversShownLabel()
        {
            Shown = !Shown;
        }

//        public new object Content
//        {
//            get;  set; }

        public object InternalContent
        {
            get { return _internalContent; }
            set { _internalContent = value; }
        }
    }
}