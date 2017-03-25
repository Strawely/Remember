using System;
using System.Collections.Generic;
using System.IO;
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

namespace Remember
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private CardButton[,] _img;
        private CardButton[] _tmpImages = new CardButton[2];

        private String[] _imageFiles;
       
        public GameWindow(int width, int height)
        {
            InitializeComponent(width, height);
            GetImages("C:\\Users\\solom\\Documents\\visual studio 2017\\Projects\\Remember\\src\\pictureSet1");
        }

        private void GetImages(String path)
        {
            _imageFiles = Directory.GetFiles(path);
        }

        private void imgBtn_Click(object sender, RoutedEventArgs e)
        {
            CardButton btn = (CardButton) sender;
            int i = 0;
            while (i < _tmpImages.Length && _tmpImages[i] != null)
            {
                i++;
            }
            switch (i)
            {
                case 2:
                    for (int j = 0; j < 2; j++)
                    {
                        _tmpImages[j].Shown = false;
                        _tmpImages[j] = null;
                    }
                    _tmpImages[0] = btn;
                    btn.Shown = true;
                    break;
                case 0:
                case 1:
                    _tmpImages[i] = btn;
                    btn.Shown = true;
                    break;
            }
            if (_tmpImages[0] != null && _tmpImages[1] != null && _tmpImages[0].Content.ToString().Equals(_tmpImages[1].Content.ToString()))
            {

                for (int j = 0; j < 2; j++)
                {
                    _tmpImages[j].IsEnabled = false;
                    _tmpImages[j].Content = null;
                }
            }

        }

        private void InitializeComponent(int width, int height)
        {
            InitializeComponent();
            UniformGrid.Columns = width;
            _img = new CardButton[width,height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    _img[i, j] = new CardButton()
                    {
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch,
                        
                        
                        InternalContent = new Image()
                        {
                            Source = new BitmapImage(new Uri("C:\\Users\\solom\\Documents\\visual studio 2017\\Projects\\Remember\\src\\pictureSet1\\omNq46VJ8GQ.jpg"))
                        },
                        
                    };
                    _img[i,j].Click += new RoutedEventHandler(this.imgBtn_Click);
                    UniformGrid.Children.Add(_img[i, j]);
                    

                }
            }

            
            
        }


    }
}
