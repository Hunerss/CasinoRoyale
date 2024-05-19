using CasinoRoyale.windows.pages;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CasinoRoyale.Windows.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        private static MainWindow window;
        private string login;
        public MainMenu(MainWindow win, string login)
        {
            window = win;
            this.login = login;
            InitializeComponent();
        }

        private void Navigation(object sender, RoutedEventArgs e)
        {
            string btnName = ((Button)sender).Name[4].ToString();
            if (btnName == "1")
                window.frame.NavigationService.Navigate(new Blackjack(window, login));
            else if (btnName == "2")
                window.frame.NavigationService.Navigate(new Roulette(window, login));
            else if (btnName == "3")
                window.frame.NavigationService.Navigate(new Slot(window, login));
            else if (btnName == "4")
                window.frame.NavigationService.Navigate(new Scores(window, login));
            else if (btnName == "5")
                window.frame.NavigationService.Navigate(new AboutUs(window, login));
            else if (btnName == "6")
                window.frame.NavigationService.Navigate(new Welcome(window));
            else
                Console.WriteLine("Welcome - error log - Navigation button number to bit - " + btnName);
        }
    }
}
