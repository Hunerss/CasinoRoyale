using CasinoRoyale.windows.pages;
using Microsoft.Win32;
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
    /// Logika interakcji dla klasy MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        private static MainWindow window;
        public MainMenu(MainWindow win)
        {
            window = win;
            InitializeComponent();
        }

        private void Navigation(object sender, RoutedEventArgs e)
        {
            string btnName = ((Button)sender).Name[4].ToString();
            if (btnName == "1")
                window.frame.NavigationService.Navigate(new Poker(window));
            else if (btnName == "2")
                window.frame.NavigationService.Navigate(new Blackjack(window));
            else if (btnName == "3")
                window.frame.NavigationService.Navigate(new Roulette(window));
            else if (btnName == "4")
                window.frame.NavigationService.Navigate(new Slot(window));
            else if (btnName == "5")
                window.frame.NavigationService.Navigate(new Scores(window));
            else if (btnName == "6")
                window.frame.NavigationService.Navigate(new Bank(window));
            else if (btnName == "7")
                window.frame.NavigationService.Navigate(new Licences(window));
            else if (btnName == "8")
                window.frame.NavigationService.Navigate(new AboutUs(window));
            else if (btnName == "9")
                window.frame.NavigationService.Navigate(new Welcome(window));
            else
                Console.WriteLine("Welcome - error log - Navigation button number to bit - " + btnName);
        }
    }
}
