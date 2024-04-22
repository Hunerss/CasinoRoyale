using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoRoyale.Classes
{
    internal class MainOperations
    {
        void CheckIntegrity()
        {
            Boolean check = true;
            if (check)
                Console.WriteLine("CheckIntegrity - Success log - Everything is fine.");
            else
                Console.WriteLine("CheckIntegrity - Error log - Something isn't here.");
        }

        Boolean CreateMainFiles()
        {
            // towrzenie bazowych plików na savy itd...
            // jakoś musi znajdować ten folder i z Rescources kopoiwać wszystkie pliki do docelowego folderu
            return false;
        }
    }
}
