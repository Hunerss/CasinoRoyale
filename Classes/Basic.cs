using CasinoRoyale.classes;
using System.Collections.Generic;
using System.Windows.Documents;

namespace CasinoRoyale.Classes
{
    internal class Basic
    {
        public static string ConvertToCardName(int value)
        {
            return value switch
            {
                2 => "Two",
                3 => "Three",
                4 => "Four",
                5 => "Five",
                6 => "Six",
                7 => "Seven",
                8 => "Eight",
                9 => "Nine",
                10 => "Ten",
                11 => "Jack",
                12 => "Queen",
                13 => "King",
                14 => "Ace",
                _ => "Unknown",
            };
        }

        public static List<Card> GenerateDeck()
        {
            string[] Suits = { "spades", "hearts", "diamonds", "clubs" };
            int[] Values = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };

            List<Card> deck = new();

            foreach (string suit in Suits)
                foreach (int value in Values)
                    deck.Add(new Card(value, suit));

            return deck;
        }

    }
}
