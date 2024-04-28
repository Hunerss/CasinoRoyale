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

    }
}
