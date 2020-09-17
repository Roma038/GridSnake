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
using System.Windows.Threading;

namespace CanvasCells
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool up;
        private bool down;
        private bool left;
        private bool right;
        private int x = 0;
        private int y = 0;
        Rectangle head;
        Rectangle firstFood;
        static int i = 0;
        List<Rectangle> FoodList = new List<Rectangle>(100);
        public MainWindow()
        {
            InitializeComponent();
            head = new Rectangle()
            {
                Width = 25,
                Height = 25,
                Fill = Brushes.Green,  
            };
            grid.Children.Add(head);

            for (int i = 0; i < 100; i++)
            {
                FoodList.Add(new Rectangle() { Width = 25, Height = 25, Fill = Brushes.Red });   
            }

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Start();

            firstFood = new Rectangle() { Width = 25, Height = 25, Fill = Brushes.Yellow };//First random spawned food
            Random random = new Random();
            int rowNum = random.Next(0, 26);
            int columnNum = random.Next(0, 24);
            grid.Children.Add(firstFood);
            Grid.SetRow(firstFood, rowNum);
            Grid.SetColumn(firstFood, columnNum);

        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            Move();
            BodyMove();

        }

        private void grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                down = true;
                up = false;
                right = false;
                left = false;
            }
            if (e.Key == Key.Up)
            {
                up = true;
                down = false;
                right = false;
                left = false;
            }
            if (e.Key == Key.Right)
            {
                right = true;
                up = false;
                down = false;
                left = false;
            }
            if (e.Key == Key.Left)
            {
                left = true;
                right = false;
                up = false;
                down = false;
            }
        }
        private void BodyMove()
        {
            Random random = new Random();
            int rowNum = random.Next(0, 26);
            int columnNum = random.Next(0, 24);

            if (Grid.GetRow(head) == Grid.GetRow(firstFood) && Grid.GetColumn(head) == Grid.GetColumn(firstFood))
            {
                //i++;
                grid.Children.Add(new Rectangle() { Width = 25, Height = 25, Fill = Brushes.Aqua });
                Grid.SetRow(this, rowNum);
                Grid.SetColumn(this, columnNum);
                grid.Children.Remove(firstFood);

            }
            if (Grid.GetRow(head) == Grid.GetRow(FoodList[i]) && Grid.GetColumn(head) == Grid.GetColumn(FoodList[i]))
            {
                
                grid.Children.Add(FoodList[i++]);
                Grid.SetRow(FoodList[i++], rowNum);
                Grid.SetColumn(FoodList[i++], columnNum);
            }

            if (down && y < 27)
            {

                Grid.SetRow(FoodList[i], Grid.GetRow(head) - 1);
                Grid.SetColumn(FoodList[i], Grid.GetColumn(head));
            }
            else if (up && y > 0)
            {

                Grid.SetRow(FoodList[i], Grid.GetRow(head) + 1);
                Grid.SetColumn(FoodList[i], Grid.GetColumn(head));
            }
            else if (left && x > 0)
            {

                Grid.SetColumn(FoodList[i], Grid.GetColumn(head) + 1);
                Grid.SetRow(FoodList[i], Grid.GetRow(head));
            }
            else if (right && x < 25)
            {

                Grid.SetRow(FoodList[i], Grid.GetRow(head));
                Grid.SetColumn(FoodList[i], Grid.GetColumn(head) - 1);
            }
        }

        //Method for Snake moving
        private void Move()
        {
            
           
            if (down && y < 27)
            {
                
                Grid.SetRow(head, ++y);
                
                //Grid.SetRow(FoodList[i++], Grid.GetRow(head) - 1);
                //Grid.SetColumn(FoodList[i++], Grid.GetColumn(head));
            }
            else if (up && y > 0)
            {
                Grid.SetRow(head, --y);
                
                //Grid.SetRow(FoodList[i++], Grid.GetRow(head) + 1);
                //Grid.SetColumn(FoodList[i++], Grid.GetColumn(head));
            }
            else if (left && x > 0)
            {
                Grid.SetColumn(head, --x);
                
                //Grid.SetColumn(FoodList[i++], Grid.GetColumn(head) + 1);
                //Grid.SetRow(FoodList[i++], Grid.GetRow(head));
            }
            else if (right && x < 25)
            {
                Grid.SetColumn(head, ++x);
                
                //Grid.SetRow(FoodList[i++], Grid.GetRow(head));
                //Grid.SetColumn(FoodList[i++], Grid.GetColumn(head) - 1);
            }
        }
    }
}
