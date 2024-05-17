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
        private int animationFrame;
        private int spinDuration;

        private readonly string[] symbols = new[]
        {
            "pack://application:,,,/images/sym_1.png",
            "pack://application:,,,/images/sym_2.png",
            "pack://application:,,,/images/sym_3.png",
            "pack://application:,,,/images/sym_4.png"
        };


        public Slot(MainWindow win)
        {
            InitializeComponent();
            window = win;
            timer.Interval = TimeSpan.FromMilliseconds(50);
            timer.Tick += Timer_Tick;
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
            {
                UpdateReels();
            }
        }

        private void CheckWin()
        {
            List<string> symbols = new List<string>()
            {
                reel1.Source.ToString(),
                reel2.Source.ToString(),
                reel3.Source.ToString(),
                reel4.Source.ToString(),
                reel5.Source.ToString()
            };

            Dictionary<string, int> sameSymbols = symbols.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());

            int most = sameSymbols.Values.Max();

            string message = "Try again!";

            if (most >= 3)
            {
                if (most == 3)
                    message = "Small Win!";
                else if (most == 4)
                    message = "Medium Win!";
                else if (most == 5)
                    message = "JACKPOT!";
            }

            MessageBox.Show(message);
        }

        private void Spin_Click(object sender, RoutedEventArgs e)
        {
            spinDuration = random.Next(30, 50);
            animationFrame = 0;
            timer.Start();
        }

        private void UpdateReels()
        {
            reel1.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            reel2.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            reel3.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            reel4.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            reel5.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
        }

        private void DisplayFinalReelValues()
        {
            reel1.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            reel2.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            reel3.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            reel4.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
            reel5.Source = new BitmapImage(new Uri(symbols[random.Next(symbols.Length)], UriKind.Absolute));
        }

        private void Navigation(object sender, RoutedEventArgs e)
        {
            string btnName = ((Button)sender).Name[4].ToString();
            if (btnName == "0")
                window.frame.NavigationService.Navigate(new Welcome(window));
            else
                Console.WriteLine("Blackjack - error log - Navigation button number to bit - " + btnName);
        }
    }
}
