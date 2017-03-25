using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

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
                if (_fieldWidth * _fieldHeight % 2 != 0)
                {
                    throw new OddFieldSizeException();
                }
                Window gameWindow = new GameWindow(_fieldWidth, _fieldHeight);
                gameWindow.Show();
                gameWindow.Activate();
                this.Close();
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Do not leave text fields blank");
            }
            catch (FormatException)
            {
                MessageBox.Show("Wrong format of size values");
            }
            catch (OddFieldSizeException)
            {
                MessageBox.Show("Number of cells must be exactly dividable by 2");
            }
            catch (Exception exception)
            {
                Console.Write(exception.StackTrace);
            }
        }
    }
}
