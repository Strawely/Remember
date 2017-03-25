using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

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