using CasinoRoyale.classes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using ToDoList.classes;

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
        private static DatabaseOperator dbo = new();
        private RouletteOperations ro;

        private string login;

        private int currentAngle;
        private int totalRotations;
        private int maxRotations;
        private double ballAngle;
        private double ballRadius = 85;

        private readonly int[] wheelNumbers = new int[]
        {
            0, 26, 3, 35, 12, 28, 7, 29, 18, 22, 9, 31, 14, 20, 1, 33, 16, 24, 5, 10, 23, 8, 30, 11, 36, 13, 27, 6, 34, 17, 25, 2, 21, 4, 19, 15, 32
        };

        private List<int> bettedNumbers;
        private List<int> bettedNumbersBets;

        public Roulette(MainWindow win, string login)
        {
            InitializeComponent();
            bettedNumbers = new List<int>();
            bettedNumbersBets = new List<int>();
            window = win;
            this.login = login;

            chips.Text = dbo.GetChips(login).ToString();

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
                ro.SetFile(ballPosition);
                int wonBet = ro.Game();

                if (wonBet > 0)
                    MessageBox.Show("Ball landed on: " + ballPosition + Environment.NewLine + "You won " + wonBet + " form your bets");
                else
                    MessageBox.Show("Ball landed on: " + ballPosition + Environment.NewLine + "You lost all your bets");

                dbo.Add(login, 2, wonBet - Convert.ToInt32(allBet.Text));
                dbo.UpdateChips(login, wonBet - Convert.ToInt32(allBet.Text));
                chips.Text = dbo.GetChips(login).ToString();
                Clear();
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
                window.frame.NavigationService.Navigate(new MainMenu(window, login));
            if (btnName == "1")
            {

                string setBetCheck = setBet.Text;
                string allBetCheck = allBet.Text;
                if (CheckBet(setBetCheck) && CheckBet(allBetCheck) && !CheckUserAccount())
                {
                    setBet.IsEnabled = false;
                    ro = new(bettedNumbers, bettedNumbersBets);
                    ballAngle = rnd.Next(0, 360);
                    maxRotations = rnd.Next(3, 7);
                    rotationTimer.Interval = TimeSpan.FromMilliseconds(30);
                    totalRotations = 0;
                    rotationTimer.Start();
                }
                else
                    if (CheckUserAccount())
                        Clear();
            }
            else
                Console.WriteLine("Blackjack - error log - Navigation button number to bit - " + btnName);
        }

        private void BetOperations(object sender, RoutedEventArgs e)
        {
            string betCheck = setBet.Text;
            if (CheckBet(betCheck))
            {
                int bet = Convert.ToInt32(betCheck);
                string btnName = ((Button)sender).Name[4..];

                Console.WriteLine($"Button Name: {btnName}");

                if (int.TryParse(btnName, out int btnNumber))
                {
                    Console.WriteLine($"Parsed Button Number: {btnNumber}");

                    if (btnNumber >= 0 && btnNumber <= 45)
                    {
                        btn_0.IsEnabled = false;
                        bettedNumbers.Add(btnNumber);
                        bettedNumbersBets.Add(bet);
                    }
                    else
                    {
                        Console.WriteLine("Button number is out of range (0-36)");
                    }
                }
                else
                {
                    Console.WriteLine("Failed to parse button name");
                }

                allBet.Text = (Convert.ToInt32(allBet.Text) + bet).ToString();
                MessageBox.Show("Betted " + bet + " on " + GetBetName(btnName));

                DisableButton("bet_" + btnName);
            }
            else
            {
                MessageBox.Show("Enter correct bet");
            }
        }

        private void DisableButton(string buttonName)
        {
            Button button = GetType().GetField(buttonName, BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(this) as Button;
            if (button != null)
                button.IsEnabled = false;
        }

        private static string GetBetName(string btn_name)
        {
            return btn_name switch
            {
                "37" => "first 12",
                "38" => "second 12",
                "39" => "third 12",
                "40" => "1 to 18",
                "41" => "even",
                "42" => "red",
                "43" => "black",
                "44" => "odd",
                "45" => "18 to 36",
                _ => btn_name,
            };
        }

        private static Boolean CheckBet(string betToCheck)
        {
            Boolean check = true;
            foreach (char character in betToCheck)
            {
                if (!char.IsNumber(character))
                    check = false;
            }
            Console.WriteLine(check);
            return check;
        }

        private Boolean CheckUserAccount()
        {
            return Convert.ToInt32(allBet.Text) >= dbo.GetChips(login);
        }

        private void Clear()
        {
            chips.Text = dbo.GetChips(login).ToString();
            bet_0.IsEnabled = true;
            bet_1.IsEnabled = true;
            bet_2.IsEnabled = true;
            bet_3.IsEnabled = true;
            bet_4.IsEnabled = true;
            bet_5.IsEnabled = true;
            bet_6.IsEnabled = true;
            bet_7.IsEnabled = true;
            bet_8.IsEnabled = true;
            bet_9.IsEnabled = true;
            bet_10.IsEnabled = true;
            bet_11.IsEnabled = true;
            bet_12.IsEnabled = true;
            bet_13.IsEnabled = true;
            bet_14.IsEnabled = true;
            bet_15.IsEnabled = true;
            bet_16.IsEnabled = true;
            bet_17.IsEnabled = true;
            bet_18.IsEnabled = true;
            bet_19.IsEnabled = true;
            bet_20.IsEnabled = true;
            bet_21.IsEnabled = true;
            bet_22.IsEnabled = true;
            bet_23.IsEnabled = true;
            bet_24.IsEnabled = true;
            bet_25.IsEnabled = true;
            bet_26.IsEnabled = true;
            bet_27.IsEnabled = true;
            bet_28.IsEnabled = true;
            bet_29.IsEnabled = true;
            bet_30.IsEnabled = true;
            bet_31.IsEnabled = true;
            bet_32.IsEnabled = true;
            bet_33.IsEnabled = true;
            bet_34.IsEnabled = true;
            bet_35.IsEnabled = true;
            bet_36.IsEnabled = true;
            bet_37.IsEnabled = true;
            bet_38.IsEnabled = true;
            bet_39.IsEnabled = true;
            bet_40.IsEnabled = true;
            bet_41.IsEnabled = true;
            bet_42.IsEnabled = true;
            bet_43.IsEnabled = true;
            bet_44.IsEnabled = true;
            bet_45.IsEnabled = true;
            allBet.Text = "0";
            setBet.IsEnabled = true;
            btn_0.IsEnabled = true;
        }
    }
}
