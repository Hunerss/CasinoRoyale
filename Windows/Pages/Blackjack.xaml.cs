using CasinoRoyale.classes;
using CasinoRoyale.windows.pages;
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

namespace CasinoRoyale.Windows.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy Blackjack.xaml
    /// </summary>
    public partial class Blackjack : UserControl
    {
        private static MainWindow window;
        private static BlackjackOperations bj;
        public Blackjack(MainWindow win)
        {
            window = win;
            InitializeComponent();
            bj = new();
            bj.GenerateCasinoCards();
        }

        private void Game(object sender, RoutedEventArgs e)
        {
            string btnName = ((Button)sender).Name[4].ToString();
            ShowCards(true, bj.GetHand(true));
            if(btnName == "4")
            {
                Console.WriteLine(bj.GenerateCard(false).Id);
                ShowCards(false, bj.GetHand(false));
            }
            else
            {
                int x = Convert.ToInt32(bet.Text);
                bj.SetBet(x);
                int score = bj.Game();
                ShowCards(true, bj.GetHand(true));
                Console.WriteLine("Score: " + score);
                Console.WriteLine("Start bet: " + bj.GetBet());
                Console.WriteLine("Recived bet: " + bj.InterpreteWin(score));
            }
        }

        private void ShowCards(Boolean casino, List<Card> cards)
        {
            Console.WriteLine("Kot");
            if( casino )
            {
                cas_cards.Text = "";
                foreach (Card card in cards)
                {
                    cas_cards.Text += card.Id;
                }
            }
            else
            {
                use_cards.Text = "";
                foreach (Card card in cards)
                {
                    use_cards.Text += card.Id;
                }
            }
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
