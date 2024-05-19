using CasinoRoyale.Classes;
using System;

namespace CasinoRoyale.classes
{
    public class Card
    {
        public string Id { get; private set; } // e.g. 2s
        public int Value { get; private set; } // 2
        public string Suit { get; private set; } // spades
        public string Image { get; private set; } // images/2s.png

        public Card(int cardValue, string suit)
        {
            Id = $"{cardValue}{suit[0]}";
            Value = cardValue;
            Suit = suit;
            Image = $"{cardValue}{suit[0]}.jpg";
        }
    }
}
