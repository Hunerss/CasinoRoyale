using CasinoRoyale.Classes;
using System;
using System.Collections.Generic;

namespace CasinoRoyale.classes
{
    class BlackjackOperations : Operations
    {
        protected List<Card> cards = new();
        protected List<Card> casinoCards = new();
        protected List<Card> userCards = new();
        protected int casinoScore = 0;
        protected int userScore = 0;

        public BlackjackOperations()
        {
            cards = Basic.GenerateDeck();
        }

        public List<Card> GetHand(Boolean casino)
        {
            return casino ? casinoCards : userCards;
        }

        public Boolean CheckUserScore()
        {
            return userScore < 21;
        }

        public Card GenerateCard(Boolean casino)
        {
            if (cards.Count == 0)
                throw new InvalidOperationException("No cards left in the deck");

            int cardIndex = rnd.Next(cards.Count);
            Card card = cards[cardIndex];
            SetCard(casino, card);
            cards.RemoveAt(cardIndex);
            return card;
        }

        public List<Card> GenerateHand(Boolean casino)
        {
            if (casino)
            {
                casinoCards.Clear();
                GenerateCard(true);
                GenerateCard(true);
            }
            else
            {
                userCards.Clear();
                GenerateCard(false);
                GenerateCard(false);
            }
            return GetHand(casino);
        }

        private void SetCard(Boolean casino, Card card)
        {
            Console.WriteLine($"Setting card {card.Id} for {(casino ? "casino" : "user")}");
            if (casino)
                casinoCards.Add(card);
            else
                userCards.Add(card);

            UpdateScore(casino);
        }

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

        private static int CalculateCardValue(Card card, int currentScore)
        {
            return card.Value == 14 ? currentScore + 11 <= 21 ? 11 : 1 : card.Value;
        }

        public void DescribeHand(Boolean casino)
        {
            List<Card> hand = GetHand(casino);
            int score = casino ? casinoScore : userScore;

            foreach (Card card in hand)
            {
                Console.Write(card.Id + " - ");
            }
            UpdateScore(casino);
            Console.WriteLine("Score: " + score);
        }

        public void DescribeScore(Boolean casino)
        {
            Console.WriteLine(casino ? casinoScore : userScore);
        }

        private int CheckWin()
        {
            // codes || 0 - casino win | 1 - user win | 2 - draw | 3 - blackjack ||

            Console.WriteLine("User score: " + userScore);
            Console.WriteLine("Casino score: " + casinoScore);

            if (userScore > 21)
                return 0;

            if (casinoScore > 21)
                return 1;

            if (userScore == casinoScore)
                return 2;

            if (userScore == 21 && userCards.Count == 2)
                return 3;

            if (casinoScore == 21 && casinoCards.Count == 2)
                return 0;

            return userScore > casinoScore ? 1 : 0;
        }

        private Boolean CheckForAs()
        {
            foreach (Card card in casinoCards)
            {
                if (card.Id == "14s" || card.Id == "14h" || card.Id == "14d" || card.Id == "14c")
                    return true;
            }
            return false;
        }


        public int Game()
        {
            DescribeHand(false);
            DescribeHand(true);

            if (userScore > 21)
                return 0;

            if (casinoScore < 17 || (casinoScore == 17 && CheckForAs()))
                while (casinoScore < 17)
                {
                    GenerateCard(true);
                    DescribeHand(true);
                }
            return CheckWin();
        }

        public int InterpreteWin(int win)
        {
            return win switch
            {
                0 => 0,
                1 => bet * 2,
                2 => bet,
                3 => Convert.ToInt32(bet * 2.5),
                _ => throw new InvalidOperationException("Wrong win input"),
            };
        }
    }
}
