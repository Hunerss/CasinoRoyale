using CasinoRoyale.classes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CasinoRoyale.Windows.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy Blackjack.xaml
    /// </summary>
    public partial class Blackjack : UserControl
    {
        private static MainWindow window;
        private static BlackjackOperations bj;

        private static Boolean methodBlocker;

        public Blackjack(MainWindow win)
        {
            window = win;
            methodBlocker = false;
            InitializeComponent();
            methodBlocker = true;
            bj = new();
        }

        private void Game(object sender, RoutedEventArgs e)
        {
            string btnName = ((Button)sender).Name[4].ToString();
            ShowCards(true, bj.GetHand(true));
            if (btnName == "4")
            {

            }
            else
            {
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
            //if( casino )
            //{
            //    cas_cards.Text = "";
            //    foreach (Card card in cards)
            //    {
            //        cas_cards.Text += card.Id;
            //    }
            //}
            //else
            //{
            //    use_cards.Text = "";
            //    foreach (Card card in cards)
            //    {
            //        use_cards.Text += card.Id;
            //    }
            //}
        }

        private void Navigation(object sender, RoutedEventArgs e)
        {
            string btnName = ((Button)sender).Name[4].ToString();
            if (btnName == "0")
                window.frame.NavigationService.Navigate(new Welcome(window));
            else if (btnName == "1")
            {
                if (CheckBet())
                {
                    btn_1.IsEnabled = false;
                    btn_2.IsEnabled = true;
                    btn_3.IsEnabled = true;
                    btn_4.IsEnabled = true;
                    bet.IsEnabled = false;
                    int betAmount = Convert.ToInt32(bet.Text);
                    bj.SetBet(betAmount);
                    bj.GenerateCasinoCards();
                    ShowCards(true, bj.GetHand(true));
                    bj.GenerateCard(false);
                    bj.GenerateCard(false);
                }
            }
            else if (btnName == "2")
            {
                Console.WriteLine(bj.GenerateCard(false).Id);
                ShowCards(false, bj.GetHand(false));
                if (!bj.CheckUserScore()) 
                {
                    btn_2.IsEnabled = false;
                    btn_3.IsEnabled = false;
                }
            }
            else if (btnName == "3")
            {
                bj.GenerateCard(false);
                bj.SetBet(bj.GetBet() * 2);
                bet.Text = bj.GetBet().ToString();
                btn_2.IsEnabled = false;
                btn_3.IsEnabled = false;
            }
            else if (btnName == "4")
            {
                int score = bj.Game();
                string outcome = "";
                if (score != 0 && score != 2)
                    outcome = "win";
                else if (score == 2)
                    outcome = "drew";
                else
                    outcome = "lose";
                MessageBox.Show("You " + outcome + Environment.NewLine + 
                                "Bet you made: " + bj.GetBet() + Environment.NewLine + 
                                "Bet after game: " + bj.InterpreteWin(score));
                bj = new();
                btn_1.IsEnabled = true;
                btn_2.IsEnabled = false;
                btn_3.IsEnabled = false;
                btn_4.IsEnabled = false;
                bet.IsEnabled = true;
            }
            else
                Console.WriteLine("Blackjack - error log - Navigation button number to bit - " + btnName);
        }

        private Boolean CheckBet()
        {
            if (methodBlocker)
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
            return false;
        }
    }
}
