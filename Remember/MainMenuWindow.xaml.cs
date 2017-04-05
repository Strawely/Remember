using System;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Windows;

namespace Remember
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
//        private int _fieldWidth;
//        private int _fieldHeight;

        public MainWindow()
        {
            InitializeComponent();
            RadioBtnSet1.IsChecked = true;
        }


        /// <summary>
        /// Получает строку пути к файлам картинок хранящимся в папке Resources
        /// </summary>
        /// <returns></returns>
        public static String GetSourcesPath()
        {
            String currentDirectory = Directory.GetCurrentDirectory();
            String s = "Remember\\bin\\Debug";
            int n = s.Length;
            return currentDirectory.Substring(0, currentDirectory.Length - n);
        }

        /// <summary>
        /// Взависимости от положения переключателя выбирает набор картинок
        /// </summary>
        /// <returns>Path to directory with defined picture set</returns>
        private ResourceSet ChoosePictureSet()
        {
            if (RadioBtnSet1.IsChecked == true)
            {
                return PictureSet1.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            }
            if (RadioBtnSet2.IsChecked == true)
            {
                return PictureSet2.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            }
            return null;
        }

        private void OnBtnStartClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var fieldWidth = Int32.Parse(TxtBoxWidth.Text);
                var fieldHeight = Int32.Parse(TxtBoxHeight.Text);
                if (fieldWidth*fieldHeight%2 != 0)
                {
                    throw new OddFieldSizeException();
                }
                Window gameWindow = new GameWindow(fieldWidth, fieldHeight, ChoosePictureSet());
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
            catch (NotEnoughPicturesException)
            {
                MessageBox.Show("Pictures in Set are not enough for this size of field");
            }
            catch (Exception exception)
            {
                Console.Write(exception.StackTrace);
                MessageBox.Show("ERRORchik");
            }
        }
    }
}
