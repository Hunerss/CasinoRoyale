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
    /// Logika interakcji dla klasy Poker.xaml
    /// </summary>
    public partial class Poker : UserControl
    {
        private static MainWindow window;
        public Poker()
        {
            window = (MainWindow)Window.GetWindow(this);
            InitializeComponent();
        }
    }
}
