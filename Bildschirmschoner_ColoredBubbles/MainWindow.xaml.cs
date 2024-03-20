using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Bildschirmschoner
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            gameRoutine.Interval                = TimeSpan.FromMilliseconds(20);
            gameRoutine.Tick                    += GameRoutine;

            moveDirUp.Add("obj1", false);
            moveDirRight.Add("obj1", true);

            //double_animation_example.From       = 0;
            //double_animation_example.To         = 500;
            //double_animation_example.Duration   = TimeSpan.FromMilliseconds(2333);

            //obj1.BeginAnimation(WidthProperty, double_animation_example); // Animation function call for Target Property of obj "obj1" !
        }

        private DoubleAnimation double_animation_example = new DoubleAnimation();
        private DispatcherTimer gameRoutine = new DispatcherTimer();
        private Random random = new Random();

        private static Dictionary<double, double> obj1_Pos = new Dictionary<double, double>();
        private static Dictionary<double, double> obj1_Middle = new Dictionary<double, double>();
        private static Dictionary<string, bool> moveDirUp = new Dictionary<string, bool>();
        private static Dictionary<string, bool> moveDirRight = new Dictionary<string, bool>();

        private void GameRoutine(object sender, EventArgs e)
        {
            obj1_Pos.Clear();
            obj1_Middle.Clear();
            obj1_Pos.Add(Canvas.GetTop(obj1), Canvas.GetLeft(obj1));
            obj1_Middle.Add(Canvas.GetTop(obj1) + obj1.ActualHeight / 2, Canvas.GetLeft(obj1) + obj1.ActualWidth / 2);

            obj1.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(
                (Byte)random.Next(0, 256), (Byte)random.Next(0, 256), (Byte)random.Next(0, 256)));

            var obj1_moveR = obj1_Pos.ElementAtOrDefault(0).Value  + 6;
            var obj1_moveL = obj1_Pos.ElementAtOrDefault(0).Value  - 6;
            var obj1_moveU = obj1_Pos.ElementAtOrDefault(0).Key    - 6;
            var obj1_moveD = obj1_Pos.ElementAtOrDefault(0).Key    + 6;

            if (play_area.ActualHeight <= (Canvas.GetTop(obj1) + obj1.ActualHeight))
            {
                moveDirUp.Clear();
                moveDirUp.Add("obj1", true);
            }

            if (play_area.ActualWidth <= (Canvas.GetLeft(obj1) + obj1.ActualWidth))
            {
                moveDirRight.Clear();
                moveDirRight.Add("obj1", false);
            }

            if (Canvas.GetLeft(obj1) <= 0)
            {
                moveDirRight.Clear();
                moveDirRight.Add("obj1", true);
            }

            if (Canvas.GetTop(obj1) <= 0)
            {
                moveDirUp.Clear();
                moveDirUp.Add("obj1", false);
            }

            if (moveDirRight["obj1"])
            {
                Canvas.SetLeft(obj1, obj1_moveR);
            }
            else
            {
                Canvas.SetLeft(obj1, obj1_moveL);
            }

            if (moveDirUp["obj1"])
            {
                Canvas.SetTop(obj1, obj1_moveU);
            }
            else
            {
                Canvas.SetTop(obj1, obj1_moveD);
            }

            //Objekte erzeugen an aktueller Position von obj1

            System.Windows.Shapes.Ellipse obj2 = new System.Windows.Shapes.Ellipse();
            obj2.Height = 5;
            obj2.Width = 5;
            obj2.Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(
                (Byte)random.Next(180, 256), (Byte)random.Next(0, 256), (Byte)random.Next(0, 256), (Byte)random.Next(0, 256)));

            play_area.Children.Add(obj2);

            if (moveDirRight.ElementAtOrDefault(0).Key == "obj1" && moveDirRight.ElementAtOrDefault(0).Value == true)
            {
                Canvas.SetLeft(obj2, obj1_Middle.ElementAtOrDefault(0).Value - obj2.ActualWidth / 2);
            }
            else
            {
                Canvas.SetLeft(obj2, obj1_Middle.ElementAtOrDefault(0).Value - obj2.ActualWidth / 2);
            }

            if (moveDirUp.ElementAtOrDefault(0).Key == "obj1" && moveDirUp.ElementAtOrDefault(0).Value == true)
            {
                Canvas.SetTop(obj2, (obj1_Middle.ElementAtOrDefault(0).Key - obj2.ActualHeight * 2));
            }
            else
            {
                Canvas.SetTop(obj2, obj1_Middle.ElementAtOrDefault(0).Key - obj2.ActualHeight / 2);
            }
        }

        private void quit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            if (gameRoutine.IsEnabled)
            {
                gameRoutine.Stop();
            }
            else
            {
                gameRoutine.Start();
            }
        }

        private void gen_rect_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Shapes.Rectangle obj2 = new System.Windows.Shapes.Rectangle();
            obj2.Height = 15;
            obj2.Width = 15;
            obj2.Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(
                (Byte)random.Next(180, 256), (Byte)random.Next(0, 256), (Byte)random.Next(0, 256), (Byte)random.Next(0, 256)));

            play_area.Children.Add(obj2);

            Canvas.SetTop(obj2, obj1_Pos.ElementAtOrDefault(0).Key);
            Canvas.SetLeft(obj2, obj1_Pos.ElementAtOrDefault(0).Value);
        }
    }
}