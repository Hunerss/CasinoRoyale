using System.Collections.Generic;

namespace CasinoRoyale.classes
{
    class RouletteOperations
    {
        private List<int> bettedFiles;
        private List<int> bettedFilesBets;
        private readonly int[] wheelNumbers = new int[]
        {
            0, 26, 3, 35, 12, 28, 7, 29, 18, 22, 9, 31, 14, 20, 1, 33, 16, 24, 5, 10, 23, 8, 30, 11, 36, 13, 27, 6, 34, 17, 25, 2, 21, 4, 19, 15, 32
        };
        private int file;
        private int win;

        private static readonly Dictionary<int, (string BetType, int Multiplier)> betTypes = new()
        {
            { 37, ("First 12", 2) },
            { 38, ("Second 12", 2) },
            { 39, ("Third 12", 2) },
            { 40, ("1 to 18", 1) },
            { 41, ("Even", 1) },
            { 42, ("Red", 1) },
            { 43, ("Black", 1) },
            { 44, ("Odd", 1) },
            { 45, ("19 to 36", 1) }
        };

        private static readonly HashSet<int> redNumbers = new()
        {
            1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36
        };

        private static readonly HashSet<int> blackNumbers = new()
        {
            2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35
        };

        public RouletteOperations(List<int> bettedFiles, List<int> bettedFilesBets)
        {
            this.bettedFiles = bettedFiles;
            this.bettedFilesBets = bettedFilesBets;
        }

        public void SetFile(int number)
        {
            file = number;
        }

        public int Game()
        {
            win = 0;  // Reset win at the start of the game

            for (int i = 0; i < bettedFiles.Count; i++)
            {
                int bet = bettedFiles[i];
                int betAmount = bettedFilesBets[i];

                switch (bet)
                {
                    case < 37:
                        if (bet == file)
                            win += betAmount * 35;
                        break;
                    case 37:
                        if (file < 13)
                            win += betAmount * 2;
                        break;
                    case 38:
                        if (file > 12 && file < 25)
                            win += betAmount * 2;
                        break;
                    case 39:
                        if (file > 24)
                            win += betAmount * 2;
                        break;
                    case 40:
                        if (file >= 1 && file <= 18)
                            win += betAmount * 1;
                        break;
                    case 41:
                        if (file % 2 == 0 && file != 0)
                            win += betAmount * 1;
                        break;
                    case 42:
                        if (redNumbers.Contains(file))
                            win += betAmount * 1;
                        break;
                    case 43:
                        if (blackNumbers.Contains(file))
                            win += betAmount * 1;
                        break;
                    case 44:
                        if (file % 2 != 0)
                            win += betAmount * 1;
                        break;
                    case 45:
                        if (file >= 19 && file <= 36)
                            win += betAmount * 1;
                        break;
                    default:
                        break;
                }
            }

            return win;
        }
    }
}
