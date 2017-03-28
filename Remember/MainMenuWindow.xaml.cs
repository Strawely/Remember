using System;
using System.IO;
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
            RadioBtnSet1.IsChecked = true;
        }

        public static String GetSourcesPath()
        {
            String currentDirectory = Directory.GetCurrentDirectory();
            String s = "Remember\\bin\\Debug";
            int n = s.Length;
            return currentDirectory.Substring(0, currentDirectory.Length - n);
        }

        private String ChoosePictureSet()
        {
            if (RadioBtnSet1.IsChecked == true)
            {
                return GetSourcesPath() + "src\\pictureSet1";
            }
            else
            {
                if (RadioBtnSet2.IsChecked == true)
                {
                    return GetSourcesPath() + "src\\pictureSet2";
                }
                else
                {
                    return null;
                }
            }
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
                Window gameWindow = new GameWindow(_fieldWidth, _fieldHeight, ChoosePictureSet());
                gameWindow.Show();
                gameWindow.Activate();
                Close();
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
                MessageBox.Show("ERRORchik");
            }
        }
    }
}
