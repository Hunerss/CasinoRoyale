using System;
using System.Collections.Generic;
using System.Linq;

namespace CasinoRoyale.classes
{
    internal class CalculationsBlackjack : Calculations
    {
        protected new int[] deck = { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
        protected int userScore = 0;
        protected int casinoScore = 0;

        // Constructors
        public CalculationsBlackjack() { }
        public CalculationsBlackjack(int firstCard, int secondCard, List<int> playersCards)
        {
            SetCards(firstCard, secondCard);
            SetCards(playersCards);
            SetScore(playersCards, false);
        }

        private void SetScore(List<int> cards, Boolean casino)
        {
            foreach (int card in cards)
                if (casino)
                    casinoScore += card;
                else
                    userScore += card;
        }

        public Boolean Probability()
        {
            SetScore(casinoCards, true);
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
            }
            return false;
        }

        public int CheckWin()
        {
            // codes || 0 - casino win | 1 - user win | 2 - draw | 3 - blackjack ||
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
