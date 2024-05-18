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

namespace CasinoRoyale.Windows.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy Roulette.xaml
    /// </summary>
    public partial class Roulette : UserControl
    {
        private static MainWindow window;
        private DispatcherTimer rotationTimer;
        private RotateTransform rotateTransform;
        private int currentAngle;
        private int totalRotations;
        public Roulette(MainWindow win)
        {
            window = win;
            InitializeComponent();

            rotateTransform = new RotateTransform
            {
                CenterX = 150,
                CenterY = 150 
            };
            wheel.RenderTransform = rotateTransform;

            rotationTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };
            rotationTimer.Tick += RotateWheel;
        }

        private void RotateWheel(object sender, EventArgs e)
        {
            currentAngle += 15;
            if (currentAngle >= 360)
            {
                currentAngle = 0;
                totalRotations++;
            }

            rotateTransform.Angle = currentAngle;

            if (totalRotations >= 3)
            {
                rotationTimer.Stop();
                totalRotations = 0;
            }
        }

        private void Navigation(object sender, RoutedEventArgs e)
        {
            string btnName = ((Button)sender).Name[4].ToString();
            if (btnName == "0")
                window.frame.NavigationService.Navigate(new Welcome(window));
            if (btnName == "1")
            {
                currentAngle = 0;
                totalRotations = 0;
                rotationTimer.Start();
            }
            else
                Console.WriteLine("Blackjack - error log - Navigation button number to bit - " + btnName);
        }
    }
}
