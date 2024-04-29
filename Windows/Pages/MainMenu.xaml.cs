using CasinoRoyale.windows.pages;
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
        public MainMenu()
        {
            window = (MainWindow)Window.GetWindow(this);
            InitializeComponent();
        }

        private void Navigation(object sender, RoutedEventArgs e)
        {
            int buttonNumber = Convert.ToInt32(((Button)sender).Name[4].ToString());
            Console.WriteLine("MainMenu - Navigation - Start log - Button number: " + buttonNumber);
            switch (buttonNumber)
            {
                case 0:
                    window.frame.NavigationService.Navigate(new Poker());
                    break;
                case 1:
                    window.frame.NavigationService.Navigate(new Blackjack());
                    break;
                case 2:
                    window.frame.NavigationService.Navigate(new Roulette());
                    break;
                case 3:
                    window.frame.NavigationService.Navigate(new Slot());
                    break;
                case 4:
                    window.frame.NavigationService.Navigate(new Scores());
                    break;
                case 5:
                    window.frame.NavigationService.Navigate(new Bank());
                    break;
                case 6:
                    window.frame.NavigationService.Navigate(new Licences());
                    break;
                case 7:
                    window.frame.NavigationService.Navigate(new AboutUs());
                    break;
                case 8:
                    window.frame.NavigationService.Navigate(new Settings());
                    break;
                case 9:
                    Window.GetWindow(this).Close();
                    Application.Current.Shutdown();
                    break;
                default:
                    Console.WriteLine("MainMenu - Navigation - Error log - Button number overflow");
                    break;
            }
        }
    }
}
