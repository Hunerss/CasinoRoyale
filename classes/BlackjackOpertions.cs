using CasinoRoyale.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoRoyale.classes
{
    class BlackjackOperations
    {
        protected new int[] deckMath = { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
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

        public Card GenrateCard(Boolean casino)
        {
            if (cards.Count == 0)
                throw new InvalidOperationException("No cards left in the deck");

            int cardIndex = rnd.Next(cards.Count);
            Card card = cards[cardIndex];
            SetCard(casino, card);
            cards.RemoveAt(cardIndex);
            return card;
        }

        private void SetCard(Boolean casino, Card card)
        {
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

        public void DescribeScore(Boolean casino)
        {
            if (casino)
                Console.WriteLine(casinoScore);
            else
                Console.WriteLine(userScore);
        }
        
        public void UpdateDeckMath()
        {
            for (int i = 0; i < deckMath.Length; i++)
                deckMath[i] = 4;

            foreach (Card card in casinoCards)
            {
                int index = card.Value - 2;
                if (index >= 0 && index < deckMath.Length)
                    deckMath[index]--;
            }

            foreach (Card card in userCards)
            {
                int index = card.Value - 2;
                if (index >= 0 && index < deckMath.Length)
                    deckMath[index]--;
            }
        }
    }
}
