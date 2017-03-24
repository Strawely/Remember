using System;
using System.Windows;

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
            catch (Exception exception)
            {
                Console.Write(exception.StackTrace);
            }
        }
    }
}
