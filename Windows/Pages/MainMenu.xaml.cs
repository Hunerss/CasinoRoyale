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
        public MainMenu(MainWindow win)
        {
            window = win;
            InitializeComponent();
        }

        private void Navigation(object sender, RoutedEventArgs e)
        {
            int buttonNumber = Convert.ToInt32(((Button)sender).Name[4].ToString());
            Console.WriteLine("MainMenu - Navigation - Start log - Button number: " + buttonNumber);
            switch (buttonNumber)
            {
                case 0:
                    window.frame.NavigationService.Navigate(new Poker(window));
                    break;
                case 1:
                    window.frame.NavigationService.Navigate(new Blackjack(window));
                    break;
                case 2:
                    window.frame.NavigationService.Navigate(new Roulette(window));
                    break;
                case 3:
                    window.frame.NavigationService.Navigate(new Slot(window));
                    break;
                case 4:
                    window.frame.NavigationService.Navigate(new Scores(window));
                    break;
                case 5:
                    window.frame.NavigationService.Navigate(new Bank(window));
                    break;
                case 6:
                    window.frame.NavigationService.Navigate(new Licences(window));
                    break;
                case 7:
                    window.frame.NavigationService.Navigate(new AboutUs(window));
                    break;
                case 8:
                    window.frame.NavigationService.Navigate(new Settings(window));
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
