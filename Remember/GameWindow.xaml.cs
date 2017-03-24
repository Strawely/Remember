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

namespace Remember
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private Image[,] _img;
        private Button btn;
        public GameWindow(int width, int height)
        {
            InitializeComponent(width, height);
        }

        private void InitializeComponent(int width, int height)
        {
            InitializeComponent();
            UniformGrid.Columns = width;
            _img = new Image[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    _img[i, j] = new Image()
                    {
                        Width = 100,
                        Height = 100,
                        Source = new BitmapImage(new Uri("C:\\Users\\solom\\YandexDisk\\Фотокамера\\Бал\\большойвесенний\\YZZhk5ZAzGE.jpg"))
                    };
                    
                    UniformGrid.Children.Add(_img[i, j]);
                    

                }
            }
            btn = new Button()
            {
                Height = 100, Width = 500, Content = "Btn"
            };
            
        }


    }
}
