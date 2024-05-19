using CasinoRoyale.classes;
using CasinoRoyale.Windows.Pages;
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
using ToDoList.classes;

namespace CasinoRoyale.windows.pages
{
    /// <summary>
    /// Logika interakcji dla klasy Slot.xaml
    /// </summary>
    public partial class Slot : UserControl
    {
        private static MainWindow window;
        private readonly Random random = new();
        private readonly DispatcherTimer timer = new();
        private static DatabaseOperator dbo = new();
        private int animationFrame;
        private int spinDuration;
        private int betAmount;
        private string login;

        private readonly string[] symbols = new[]
        {
            "pack://application:,,,/images/bell.png",
            "pack://application:,,,/images/cherries.png",
            "pack://application:,,,/images/grape.png",
            "pack://application:,,,/images/jackpot-machine.png",
            "pack://application:,,,/images/lemon.png",
            "pack://application:,,,/images/orange.png",
            "pack://application:,,,/images/plum.png",
            "pack://application:,,,/images/seven.png",
            "pack://application:,,,/images/star.png"
        };

        private List<string> availableSymbols = new();

        public Slot(MainWindow win, string login)
        {
            InitializeComponent();
            window = win;
            this.login = login;
            timer.Interval = TimeSpan.FromMilliseconds(160);
            timer.Tick += Timer_Tick;
            DisplayBeginingReels();
            chips.Text = dbo.GetChips(login).ToString();
            availableSymbols.AddRange(symbols);
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            animationFrame++;
            if (animationFrame > spinDuration)
            {
                timer.Stop();
                DisplayFinalReelValues();
                CheckWin();
            }
            else
                UpdateReels();
        }

        private void CheckWin()
        {
            List<string> symbols = new()
            {
                reel1.Source.ToString(),
                reel2.Source.ToString(),
                reel3.Source.ToString(),
                reel4.Source.ToString(),
                reel5.Source.ToString()
            };

            Dictionary<string, int> sameSymbols = symbols.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());

            int most = sameSymbols.Values.Max();
            var mostFrequentSymbol = sameSymbols.FirstOrDefault(x => x.Value == most);

            string message = "Try again! " + Environment.NewLine + " You lost: ";

            if (most >= 3)
            {
                message = most switch
                {
                    3 => "Small Win!",
                    4 => "Medium Win!",
                    5 => "JACKPOT!",
                    _ => message
                };
                message += Environment.NewLine + "You won: ";
                int win = 0;
                if (mostFrequentSymbol.Key == symbols[1] || mostFrequentSymbol.Key == symbols[2] ||
                    mostFrequentSymbol.Key == symbols[3] || mostFrequentSymbol.Key == symbols[4] ||
                    mostFrequentSymbol.Key == symbols[5])
                    win = betAmount * 3;
                else if (mostFrequentSymbol.Key == symbols[0] || mostFrequentSymbol.Key == symbols[7] ||
                    mostFrequentSymbol.Key == symbols[8])
                    win = betAmount * 5;
                else
                    win = betAmount * 10;

                dbo.Add(login, 3, win - betAmount);
                dbo.UpdateChips(login, win - betAmount);
                chips.Text = dbo.GetChips(login).ToString();
            }
            MessageBox.Show(message += betAmount);
            bet.IsEnabled = true;
            btn_0.IsEnabled = true;
            btn_1.IsEnabled = true;
            btn_2.IsEnabled = false;
        }

        private void DisplayBeginingReels()
        {
            reel1.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            reel2.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            reel3.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            reel4.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            reel5.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));

            topReel1.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            topReel2.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            topReel3.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            topReel4.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            topReel5.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));

            bottomReel1.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            bottomReel2.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            bottomReel3.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            bottomReel4.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            bottomReel5.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
        }


        private void UpdateReels()
        {
            reel1.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            reel2.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            reel3.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            reel4.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            reel5.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));

            topReel1.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            topReel2.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            topReel3.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            topReel4.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            topReel5.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));

            bottomReel1.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            bottomReel2.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            bottomReel3.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            bottomReel4.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            bottomReel5.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
        }

        private void DisplayFinalReelValues()
        {
            reel1.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            reel2.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            reel3.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            reel4.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            reel5.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));

            topReel1.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            topReel2.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            topReel3.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            topReel4.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            topReel5.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));

            bottomReel1.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            bottomReel2.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            bottomReel3.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            bottomReel4.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            bottomReel5.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));

        }

        private void Navigation(object sender, RoutedEventArgs e)
        {
            string btnName = ((Button)sender).Name[4].ToString();
            if (btnName == "0")
                window.frame.NavigationService.Navigate(new MainMenu(window, login));
            else if(btnName == "1")
            {
                if (CheckBet() && !CheckUserAccount())
                {
                    betAmount = Convert.ToInt32(bet.Text);
                    chips.Text = (Convert.ToInt32(chips.Text) - betAmount).ToString();
                    bet.IsEnabled = false;
                    btn_0.IsEnabled = false;
                    btn_1.IsEnabled = false;
                    btn_2.IsEnabled = true;
                }
            }
            else if (btnName == "2")
            {
                if(betAmount != Convert.ToInt32(bet.Text))
                    betAmount = Convert.ToInt32(bet.Text);
                spinDuration = random.Next(30, 50);
                animationFrame = 0;
                timer.Start();
            }
            else
                Console.WriteLine("Blackjack - error log - Navigation button number to bit - " + btnName);
        }

        private Boolean CheckBet()
        {
            string betToCheck = bet.Text;
            Boolean check = true;
            foreach (char character in betToCheck)
            {
                if (!char.IsNumber(character))
                    check = false;
            }
            return check;
        }

        private Boolean CheckUserAccount()
        {
            return Convert.ToInt32(bet.Text) >= dbo.GetChips(login);
        }
    }
}