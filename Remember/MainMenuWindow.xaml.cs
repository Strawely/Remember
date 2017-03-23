using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Remember
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _fieldWidth;
        private int _fieldHeight;

        public MainWindow()
        {
            InitializeComponent();
        }

        public int FieldWidth
        {
            get { return _fieldWidth; }
        }

        public int FieldHeight
        {
            get { return _fieldHeight; }
        }

        private void OnBtnStartClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _fieldWidth = Int32.Parse(TxtBoxWidth.Text);
                _fieldHeight = Int32.Parse(TxtBoxHeight.Text);
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Do not leave text fields blank");
            }
            catch (FormatException formatException)
            {
                MessageBox.Show("Wrong format of size values");
            }
            catch (Exception exception)
            {
                Console.Write(exception.StackTrace);
            }
        }
    }
}
