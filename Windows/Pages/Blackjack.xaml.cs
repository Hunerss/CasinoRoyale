using CasinoRoyale.classes;
using CasinoRoyale.windows.pages;
using System;
using System.Linq;
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
        private static List<Card> userCards;
        private static List<Card> casinoCards;

        private static DispatcherTimer timer;
        private static DispatcherTimer startCardsTimer;
        private static DispatcherTimer casinoTimer;

        private static int startCards;
        private static int casinoCardsNumber;
        private static int check;

        private static int user;
        private static int casino;
        private static int score;

        public Blackjack(MainWindow win)
        {
            InitializeComponent();
            SetStage();
            window = win;
        }

        private void CasinoTimer_Tick(object? sender, EventArgs e)
        {
            if (casinoCardsNumber > 0)
            {
                CardModel card = new();
                Point startPos = StackCard.GetPosition();
                card.SetPosition(startPos.X, startPos.Y);
                card.SetBackground(casinoCards[casino]);
                card.SetTargetPosition(new Point(-45 + (casino+1) * 95, 60));
                casino++;
                Cnv.Children.Add(card);
                cards.Add(card);
                casinoCardsNumber--;
            }
            else
            {
                casinoTimer.Stop();
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
                btn_0.IsEnabled = true;
                btn_1.IsEnabled = true;
                btn_2.IsEnabled = false;
                btn_3.IsEnabled = false;
                btn_4.IsEnabled = false;
                bet.IsEnabled = true;
            }
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
                Console.WriteLine(check + " " + Convert.ToInt32(3 / 2));
                if (check % 2 == 0)
                {
                    card.SetBackground(casinoCards[casino]);
                    if (casino == 0)
                    {
                        card.SwichBackground();
                    }
                    casino++;
                    card.SetTargetPosition(new Point(-45 + (2 - Convert.ToInt32(startCards / 2)) * 95, 60));
                }
                else
                {
                    card.SetBackground(userCards[user]);
                    user++;
                    card.SetTargetPosition(new Point(50 + (2 - Convert.ToInt32(startCards / 2)) * 95, 210));
                }
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

        private void AddCard()
        {
            CardModel card = new();
            Point startPos = StackCard.GetPosition();
            card.SetPosition(startPos.X, startPos.Y);
            card.SetBackground(userCards[user]);
            card.SetTargetPosition(new Point(50 + user * 95, 210));
            user++;
            Cnv.Children.Add(card);
            cards.Add(card);
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

        private void SetStage()
        {
            cards = new();
            timer = new();
            startCardsTimer = new();
            casinoTimer = new();
            startCards = 4;
            check = 1;
            user = 0;
            casino = 0;
            bj = new();

            casinoTimer.Interval = TimeSpan.FromMilliseconds(200);
            casinoTimer.Tick += CasinoTimer_Tick;
        }

        private void ClearCanvas()
        {
            Cnv.Children.Clear();
            CardModel card = new()
            {
                Name = "StackCard"
            };
            card.SetPosition(670, 110);
            Cnv.Children.Add(card);
            SetStage();
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
                    ClearCanvas();
                    btn_0.IsEnabled = false;
                    btn_1.IsEnabled = false;
                    btn_2.IsEnabled = true;
                    btn_3.IsEnabled = true;
                    btn_4.IsEnabled = true;
                    bet.IsEnabled = false;
                    int betAmount = Convert.ToInt32(bet.Text);
                    bj.SetBet(betAmount);
                    userCards = bj.GenerateHand(false);
                    casinoCards = bj.GenerateHand(true);
                    ShowCards();
                }
            }
            else if (btnName == "2")
            {
                Console.WriteLine(bj.GenerateCard(false).Id);
                AddCard();
                if (!bj.CheckUserScore())
                    btn_2.IsEnabled = false;
                btn_3.IsEnabled = false;
            }
            else if (btnName == "3")
            {
                bj.GenerateCard(false);
                AddCard();
                bj.SetBet(bj.GetBet() * 2);
                bet.Text = bj.GetBet().ToString();
                btn_2.IsEnabled = false;
                btn_3.IsEnabled = false;
            }
            else if (btnName == "4")
            {
                ((CardModel)Cnv.Children[2]).SwichBackground();
                score = bj.Game();
                Console.WriteLine("Game scoer " + score);
                casinoCards = bj.GetHand(true);
                casinoCardsNumber = casinoCards.Count - 2;
                casinoTimer.Start();
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
