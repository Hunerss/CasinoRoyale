using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
        private Random rnd = new();

        private int currentAngle;
        private int totalRotations;
        private int maxRotations;
        private double ballAngle;
        private double ballRadius = 85;

        private readonly int[] wheelNumbers = new int[]
        {
            0, 26, 3, 35, 12, 28, 7, 29, 18, 22, 9, 31, 14, 20, 1, 33, 16, 24, 5, 10, 23, 8, 30, 11, 36, 13, 27, 6, 34, 17, 25, 2, 21, 4, 19, 15, 32
        };

        public Roulette(MainWindow win)
        {
            window = win;
            InitializeComponent();

            rotateTransform = new RotateTransform
            {
                CenterX = 150,
                CenterY = 150
            };

            currentAngle = rnd.Next(0, 360);
            rotateTransform.Angle = currentAngle;
            wheel.RenderTransform = rotateTransform;

            rotationTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(160)
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

            /*
            6 - 10
            25 - 8/30
            35 - 21
             
             */

            rotateTransform.Angle = currentAngle;

            if (currentAngle == 0)
                rotationTimer.Interval += TimeSpan.FromMilliseconds(10);

            ballAngle -= 25;
            if (ballAngle < 0)
                ballAngle += 360;

            double ballX = 150 + ballRadius * Math.Cos(ballAngle * Math.PI / 180);
            double ballY = 150 + ballRadius * Math.Sin(ballAngle * Math.PI / 180);
            Canvas.SetLeft(ball, ballX - ball.Width / 2);
            Canvas.SetTop(ball, ballY - ball.Height / 2);

            if (totalRotations >= maxRotations)
            {
                rotationTimer.Stop();
                totalRotations = 0;
                int ballPosition = GetBallPosition();
                MessageBox.Show("Ball landed on: " + ballPosition);
            }
            else
            {
                int ballPosition = GetBallPosition();
                Console.WriteLine("Ball on position: " + ballPosition);
            }
        }

        private int GetBallPosition()
        {
            double sectorAngle = 360.0 / wheelNumbers.Length;
            double normalizedAngle = currentAngle % 360;
            double normalizedBall = ballAngle % 360;
            double normalizedBallAngle = (360 - (normalizedBall + normalizedAngle)) % 360;
            if (normalizedBallAngle < 0)
                normalizedBallAngle += 360;
            int sectorIndex = (int)(normalizedBallAngle / sectorAngle);
            if (sectorIndex >= wheelNumbers.Length)
                sectorIndex -= wheelNumbers.Length;
            if (sectorIndex - 8 < 0)
                sectorIndex = wheelNumbers.Length - 9;
            else
                sectorIndex -= 8;
            return wheelNumbers[sectorIndex];
        }

        private void Navigation(object sender, RoutedEventArgs e)
        {
            string btnName = ((Button)sender).Name[4].ToString();
            if (btnName == "0")
                window.frame.NavigationService.Navigate(new Welcome(window));
            if (btnName == "1")
            {
                ballAngle = rnd.Next(0, 360);
                maxRotations = rnd.Next(3, 7);
                rotationTimer.Interval = TimeSpan.FromMilliseconds(30);
                totalRotations = 0;
                rotationTimer.Start();
            }
            else
                Console.WriteLine("Blackjack - error log - Navigation button number to bit - " + btnName);
        }
    }
}
