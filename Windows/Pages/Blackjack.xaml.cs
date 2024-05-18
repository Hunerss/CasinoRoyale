using CasinoRoyale.classes;
using CasinoRoyale.windows.pages;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CasinoRoyale.Windows.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy Blackjack.xaml
    /// </summary>
    public partial class Blackjack : UserControl
    {
        private static MainWindow window;
        private static BlackjackOperations bj;

        private static List<CardModel> cards;
        private static List<CardModel> userCards;
        private static List<CardModel> casinoCards;

        DispatcherTimer timer;
        DispatcherTimer startCardsTimer;

        private static Boolean methodBlocker;
        private static int startCards;
        private static int check;

        public Blackjack(MainWindow win)
        {
            methodBlocker = false;
            InitializeComponent();
            methodBlocker = true;
            cards = new();
            timer = new();
            startCardsTimer = new();
            startCards = 4;
            check = 1;
            window = win;
            bj = new();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            for (int i = 0; i < cards.Count; i++)
                cards[i].Update();
        }

        private void StartCardsTimer_Tick(object? sender, EventArgs e)
        {
            if (startCards > 0)
            {
                CardModel card = new();
                Point startPos = StackCard.GetPosition();
                card.SetPosition(startPos.X, startPos.Y);
                Console.WriteLine(check + " " + Convert.ToInt32(3/2));
                if (check % 2 == 0)
                    card.SetTargetPosition(new Point(-45 + (2 - Convert.ToInt32(startCards / 2)) * 95, 60));
                else                                 
                    card.SetTargetPosition(new Point(50 + (2 - Convert.ToInt32(startCards / 2)) * 95, 210));
                check++;
                Cnv.Children.Add(card);
                cards.Add(card);
                startCards--;
            }
            else
            {
                startCardsTimer.Stop();
            }
        }

        private void Game(object sender, RoutedEventArgs e)
        {
            string btnName = ((Button)sender).Name[4].ToString();
            //ShowCards(true, bj.GetHand(true));
            if (btnName == "4")
            {

            }
            else
            {
                int score = bj.Game();
                //ShowCards(true, bj.GetHand(true));
                Console.WriteLine("Score: " + score);
                Console.WriteLine("Start bet: " + bj.GetBet());
                Console.WriteLine("Recived bet: " + bj.InterpreteWin(score));
            }
        }

        private void ShowCards()
        {
            timer.Interval = TimeSpan.FromMilliseconds(30);
            timer.Tick += Timer_Tick;
            timer.Start();

            startCardsTimer.Interval = TimeSpan.FromMilliseconds(200);
            startCardsTimer.Tick += StartCardsTimer_Tick;
            startCardsTimer.Start();
        }

        private void Navigation(object sender, RoutedEventArgs e)
        {
            string btnName = ((Button)sender).Name[4].ToString();
            if (btnName == "0")
            {
                window.frame.NavigationService.Navigate(new Welcome(window));

            }
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
                    //ShowCards(true, bj.GetHand(true));
                    ShowCards();
                    bj.GenerateCard(false);
                    bj.GenerateCard(false);
                }
            }
            else if (btnName == "2")
            {
                Console.WriteLine(bj.GenerateCard(false).Id);
                //ShowCards(false, bj.GetHand(false));
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
                string outcome;
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
            string betToCheck = bet.Text;
            Boolean check = true;
            foreach (char character in betToCheck)
            {
                if (!char.IsNumber(character))
                    check = false;
            }
            return check;
        }
    }
}
