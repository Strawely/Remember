using System.Windows;
using System.Windows.Controls;

namespace Remember
{

    public class ClickableImage:Image
    {
        private bool _shown = false;

        public bool Shown
        {
            get { return _shown; }
            set { _shown = value; }
        }

        
    }
}