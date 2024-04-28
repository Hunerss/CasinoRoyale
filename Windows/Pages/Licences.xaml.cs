using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CasinoRoyale.Windows.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy Licences.xaml
    /// </summary>
    public partial class Licences : UserControl
    {
        private static MainWindow window;
        public Licences(MainWindow win)
        {
            window = win;
            InitializeComponent();
        }
    }
}

/*
 Licencja użytkowania dla aplikacji gry kasynowej

    Definicje

    W niniejszej umowie, "Aplikacja" oznacza grę kasynową, w tym wszelkie związane z nią oprogramowanie, materiały, pliki, dane i treści. "Licencjobiorca" oznacza osobę fizyczną lub podmiot prawnie uprawniony do korzystania z Aplikacji zgodnie z niniejszą umową. "Wydawca" oznacza podmiot lub firmę, która udostępnia Aplikację.

    Licencja

    Wydawca udziela Licencjobiorcy ograniczonej, nieprzenośnej, niepodzielnej i nieodpłatnej licencji na korzystanie z Aplikacji zgodnie z warunkami niniejszej umowy. Licencjobiorca nie ma prawa do sprzedaży, sublicencji, dystrybucji ani innej formy przeniesienia praw związanych z Aplikacją.

    Ograniczenia użytkowania

    Licencjobiorca nie ma prawa do:
        Modyfikowania, dostosowywania, dekompilowania ani odwracania inżynierii Aplikacji.
        Usuwania, zmieniania ani zakłócania oznaczeń praw autorskich, znaków wodnych lub innych informacji o prawach własności intelektualnej zawartych w Aplikacji.
        Korzystania z Aplikacji w sposób sprzeczny z prawem lub szkodliwy dla innych użytkowników lub osób trzecich.

    Prawa własności intelektualnej

    Wydawca zachowuje pełne prawo własności intelektualnej do Aplikacji, w tym praw autorskich, znaków towarowych i innych praw własności intelektualnej. Licencjobiorca nie nabiera żadnych praw własności intelektualnej do Aplikacji ani do jakiejkolwiek jej części.

    Gwarancje

    Aplikacja jest udostępniana "w stanie, w jakim się znajduje", bez żadnych wyraźnych ani domniemanych gwarancji, w tym gwarancji jakości, przydatności do określonego celu ani niezawodności. Licencjobiorca korzysta z Aplikacji na własne ryzyko.

    Ograniczenie odpowiedzialności

    W żadnym przypadku Wydawca nie będzie odpowiedzialny wobec Licencjobiorcy za żadne bezpośrednie, pośrednie, przypadkowe, następcze ani karne szkody wynikające z korzystania z Aplikacji lub związane z nią.

    Postanowienia ogólne

    Niniejsza umowa stanowi całość porozumienia między stronami w odniesieniu do korzystania z Aplikacji i zastępuje wszelkie wcześniejsze lub współczesne ustalenia, ustne lub pisemne, dotyczące tej samej kwestii. Umowa podlega prawu obowiązującemu w kraju, w którym została zawarta.
 
 */