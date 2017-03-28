using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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

//            System.Windows.Forms.MessageBox.Show(Directory.GetCurrentDirectory());
        }

        private String ChoosePictureSet()
        {
            if (RadioBtnSet1.IsChecked == true)
            {
                return "C:\\Users\\solom\\Documents\\visual studio 2017\\Projects\\Remember\\src\\pictureSet1";
            }
            else
            {
                if (RadioBtnSet2.IsChecked == true)
                {
                    return "C:\\Users\\solom\\Documents\\visual studio 2017\\Projects\\Remember\\src\\pictureSet2";
                }
                else
                {
                    return null;
                }
            }
        }

        private void OnBtnStartClick(object sender, RoutedEventArgs e)
        {
            StartGame();
        }

        private void BtnContinueGame_Click(object sender, RoutedEventArgs eventArgs)
        {
            String file = "data.dat";
            if (File.Exists(file))
            {
                FileStream fileStream = File.OpenRead(file);
                BinaryFormatter bf = new BinaryFormatter();
                try
                {
                    GameCondition condition = (GameCondition)bf.Deserialize(fileStream);
                    fileStream.Close();
                    StartGame(condition);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine(e);
                    System.Windows.Forms.MessageBox.Show("Ex");
                }
            }
        }

        private void StartGame()
        {
            StartGame(null);
        }

        private void StartGame(GameCondition condition)
        {
            try
            {
                _fieldWidth = Int32.Parse(TxtBoxWidth.Text);
                _fieldHeight = Int32.Parse(TxtBoxHeight.Text);
                if (_fieldWidth * _fieldHeight % 2 != 0)
                {
                    throw new OddFieldSizeException();
                }
                Window gameWindow = new GameWindow(_fieldWidth, _fieldHeight, ChoosePictureSet(), condition);
                gameWindow.Show();
                gameWindow.Activate();
                this.Close();

            }
            catch (ArgumentNullException e)
            {
                MessageBox.Show("Do not leave text fields blank");
                Console.WriteLine(e.StackTrace);
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
