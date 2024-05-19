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

namespace CasinoRoyale.windows.pages
{
    /// <summary>
    /// Logika interakcji dla klasy ArchivalMainMenu.xaml
    /// </summary>
    public partial class ArchivalMainMenu : UserControl
    {
        MainWindow window;
        public ArchivalMainMenu(MainWindow win)
        {
            this.window = win;
            InitializeComponent();
        }

        private void Navigation(object sender, RoutedEventArgs e)
        {
            string login = "string";
            string btnName = ((Button)sender).Name[4].ToString();
            if (btnName == "1")
                window.frame.NavigationService.Navigate(new Blackjack(window, login));
            else if (btnName == "2")
                window.frame.NavigationService.Navigate(new Roulette(window, login));
            else if (btnName == "3")
                window.frame.NavigationService.Navigate(new Slot(window, login));
            else if (btnName == "4")
                window.frame.NavigationService.Navigate(new Scores(window, login));
            else if (btnName == "5")
                window.frame.NavigationService.Navigate(new AboutUs(window, login));
            else
                Console.WriteLine("Welcome - error log - Navigation button number to bit - " + btnName);
        }
    }
}

/*
 private void UpdateScore(Boolean casino)
        {
            List<Card> hand = casino ? casinoCards : userCards;
            int score = 0;
            int aceCount = 0;

            foreach (Card card in hand)
            {
                if (card.Value == 14)
                {
                    aceCount++;
                    score += CalculateCardValue(card, score);
                }
                else if (card.Value > 10)
                    score += 10;
                else
                    score += card.Value;
            }

            if (casino)
                casinoScore = score;
            else
                userScore = score;
        }
 */
