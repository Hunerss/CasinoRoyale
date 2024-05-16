using CasinoRoyale.classes;
using CasinoRoyale.windows.pages;
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
    /// Logika interakcji dla klasy Blackjack.xaml
    /// </summary>
    public partial class Blackjack : UserControl
    {
        private static MainWindow window;
        private static BlackjackOperations bj = new();
        public Blackjack(MainWindow win)
        {
            window = win;
            InitializeComponent();
        }

        private void Game(object sender, RoutedEventArgs e)
        {
            string btnName = ((Button)sender).Name[4].ToString();
            if(btnName == "4")
            {

                Console.WriteLine(bj.GenrateCard(false).Id);
            }
            else
            {
                bj.DescribeHand(false);
            }
        }

        private void Navigation(object sender, RoutedEventArgs e)
        {
            string btnName = ((Button)sender).Name[4].ToString();
            if (btnName == "0")
                window.frame.NavigationService.Navigate(new Welcome(window));
            else
                Console.WriteLine("Blackjack - error log - Navigation button number to bit - " + btnName);
        }
    }
}
