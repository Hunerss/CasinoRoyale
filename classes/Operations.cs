using System;

namespace CasinoRoyale.classes
{
    abstract class Operations
    {
        protected int bet = 0;
        protected readonly Random rnd = new();

        public void SetBet(int bet)
        {
            this.bet = bet;
        }

        public int GetBet()
        {
            return this.bet;
        }
    }
}
