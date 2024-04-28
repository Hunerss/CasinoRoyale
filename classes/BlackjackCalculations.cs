using System;
using System.Collections.Generic;

namespace CasinoRoyale.classes
{
    internal class BlackjackCalculations
    {
        private int[] deck = { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
        private List<int> casinoCards = new();
        private List<int> userCards = new();
        private int userScore = 0;
        private int casinoScore = 0;
        private int bet = 0;
        // Base function
        public void SetCards(int firstCard, int secondCard)
        {
            casinoCards.Add(firstCard);
            casinoCards.Add(secondCard);
            deck[firstCard - 2]--;
            deck[secondCard - 2]--;
        }

        // Adding new Card
        public void SetCards(int newCard)
        {
            casinoCards.Add(newCard);
            deck[newCard - 2]--;

        }

        // Changing Card Array
        public void SetCards(int[] usersCard)
        {
            for (int i = 0; i < deck.Length; i++)
                deck[i] = usersCard[i];
            foreach (int card in usersCard)
                userCards.Add(card);
        }

        public void SetScore(List<int> cards, Boolean casino)
        {
            foreach (int card in cards)
                if (casino)
                    casinoScore += card;
                else
                    userScore += card;
        }

        public void SetBet(int bet)
        {
            this.bet = bet;
        }

        public Boolean Probability()
        {
            if (casinoScore < 21)
            {
                int difference = 21 - casinoScore;
                int possibleCards = 0, allPossibleCards = 0;
                for (int i = 2; i < 15; i++)
                    if (difference - i > -1)
                        possibleCards += deck[i - 2];
                foreach (int card in deck)
                    allPossibleCards += card;

                if (possibleCards / allPossibleCards >= 0.25)
                    return true;
                else
                    return false;
            }
            else
                return false;

        }

        public int CheckWin()
        {
            // codes 0 - casino win | 1 - user win | 2 - draw | 3 - blackjack
            if (casinoScore <= userScore)
                if (userCards.Contains(14) && (userCards.Contains(13) || userCards.Contains(12) || userCards.Contains(11) || userCards.Contains(10)))
                    return 3;
                else
                    if (casinoScore == userScore)
                        return 2;
                    else
                        return 1;
            else
                return 0;
        }
    }
}
