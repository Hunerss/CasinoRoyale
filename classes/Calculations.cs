using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoRoyale.classes
{
    abstract class Calculations
    {
        protected int[] deck = Array.Empty<int>();
        protected List<int> casinoCards = new();
        protected List<int> userCards = new();
        protected int bet = 0;

        // Base function
        protected void SetCards(int firstCard, int secondCard)
        {
            SetCard(firstCard);
            SetCard(secondCard);
        }

        // Adding new Card
        public void SetCard(int newCard)
        {
            casinoCards.Add(newCard);
            deck[newCard - 2]--;
        }

        // Changing Card Array
        public void SetCards(List<int> usersCard)
        {
            for (int i = 0; i < deck.Length; i++)
                deck[i] = usersCard[i];
            foreach (int card in usersCard)
                userCards.Add(card);
        }

        public void SetBet(int bet)
        {
            this.bet = bet;
        }
    }
}
