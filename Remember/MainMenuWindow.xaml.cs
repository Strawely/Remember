using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Windows;
using System.Windows.Documents;
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
            RadioBtnSet1.IsChecked = true;
        }

//        private String ChoosePictureSet()
//        {
//
//        }

        private List<BitmapImage> GetImages()
        {
            List<BitmapImage> imgList = new List<BitmapImage>();
            ResourceSet resourceSet = PictureSet1.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true,
                true);
            foreach (DictionaryEntry entry in resourceSet)
            {
                String resourceKey = entry.Key.ToString();
                object resource = entry.Value;
                imgList.Add((BitmapImage)resource);
            }
            return imgList;
//            if (RadioBtnSet1.IsChecked == true)
//            {
//                return "C:\\Users\\solom\\Source\\Repos\\Remember\\src\\pictureSet1";
//
//            }
//            else
//            {
//                if (RadioBtnSet2.IsChecked == true)
//                {
//                    return "C:\\Users\\solom\\Source\\Repos\\Remember\\src\\pictureSet2";
//                }
//                else
//                {
//                    return null;
//                }
//            }
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
                Window gameWindow = new GameWindow(_fieldWidth, _fieldHeight, GetImages());
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

        private void BtnStart_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }
    }
}
