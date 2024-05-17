using CasinoRoyale.Classes;
using System;
using System.Collections.Generic;

namespace CasinoRoyale.classes
{
    class BlackjackOperations
    {
        protected List<Card> cards = new();
        protected List<Card> casinoCards = new();
        protected List<Card> userCards = new();
        protected int casinoScore = 0;
        protected int userScore = 0;
        protected int bet = 0;

        private Random rnd = new();


        public BlackjackOperations()
        {
            cards = Basic.GenerateDeck();
        }

        public void SetBet(int bet)
        {
            this.bet = bet;
        }

        public int GetBet()
        {
            return this.bet;
        }

        public List<Card> GetHand(Boolean casino)
        {
            return casino ? casinoCards : userCards;
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

        public void GenerateCasinoCards()
        {
            casinoCards.Clear();
            GenerateCard(true);
            GenerateCard(true);
            DescribeHand(true);
        }

        private void SetCard(Boolean casino, Card card)
        {
            Console.WriteLine($"Setting card {card.Id} for {(casino ? "casino" : "user")}");
            if (casino)
                casinoCards.Add(card);
            else
                userCards.Add(card);
        }

        private int CalculateCardValue(Card card, int currentScore)
        {
            return card.Value == 1 ? currentScore + 11 <= 21 ? 11 : 1 : card.Value;
        }

        public void DescribeHand(Boolean casino)
        {
            List<Card> hand = casino ? casinoCards : userCards;
            int score = 0;

            foreach (Card card in hand)
            {
                Console.Write(card.Id + " - ");
                if (card.Value > 10)
                    score += 10;
                else
                    score += CalculateCardValue(card, score);
            }
            Console.WriteLine("Score: " + score);
            if (casino)
                casinoScore = score;
            else
                userScore = score;
        }

        public void DescribeHand()
        {
            List<Card> hand = casinoCards;
            int score = 0;

            foreach (Card card in hand)
            {
                if (card.Value > 10)
                    score += 10;
                else
                    score += CalculateCardValue(card, score);
            }
            casinoScore = score;
        }

        public void DescribeScore(Boolean casino)
        {
            if (casino)
                Console.WriteLine(casinoScore);
            else
                Console.WriteLine(userScore);
        }

        private int CheckWin()
        {
            // codes || 0 - casino win | 1 - user win | 2 - draw | 3 - blackjack ||
            if(casinoScore == 21)
                return 0;
            else if(casinoScore > 21)
                return 1;
            else if (casinoScore <= userScore)
            {
                if (userScore == 21 && userCards.Count == 2)
                    return 3;
                else if (casinoScore == userScore)
                    return 2;
                else
                    return 1;
            }
            else
                return 0;
        }

        private Boolean CheckForAs()
        {
            foreach (Card card in casinoCards)
            {
                if(card.Id=="14s" || card.Id == "14h" || card.Id == "14d" || card.Id == "14c")
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
            else
            {
                if (casinoScore < 17 || (casinoScore==17 && CheckForAs()))
                    while (casinoScore < 17)
                    {
                        GenerateCard(true);
                        DescribeHand();
                    }
                DescribeHand(true);
                return CheckWin();
            }
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
