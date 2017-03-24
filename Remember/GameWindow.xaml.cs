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
        private Button[,] _img;
        private Button[] _tmpImages = new Button[2];

        public GameWindow(int width, int height)
        {
            InitializeComponent(width, height);
        }

        private void InitializeComponent(int width, int height)
        {
            InitializeComponent();
            UniformGrid.Columns = width;
            _img = new Button[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    _img[i, j] = new Button()
                    {
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch,
                        
                        Content = new Image()
                        {
                            Source = new BitmapImage(new Uri("C:\\Users\\solom\\Documents\\visual studio 2017\\Projects\\Remember\\src\\pictureSet1\\omNq46VJ8GQ.jpg"))
                        },
                        
                    };
                    
                    UniformGrid.Children.Add(_img[i, j]);
                    

                }
            }

            
            
        }


    }
}
