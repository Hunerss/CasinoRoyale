using System;
using System.Windows;
using System.Windows.Controls;
using ToDoList.classes;

namespace CasinoRoyale.Windows.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy Scores.xaml
    /// </summary>
    public partial class Scores : UserControl
    {
        private static MainWindow window;
        private string login;
        public Scores(MainWindow win, string login)
        {
            window = win;
            this.login = login;
            InitializeComponent();
        }

        private void Navigation(object sender, RoutedEventArgs e)
        {
            string btnName = ((Button)sender).Name[4].ToString();
            if (btnName == "0")
                window.frame.NavigationService.Navigate(new MainMenu(window, login));
            else
                Console.WriteLine("Blackjack - error log - Navigation button number to bit - " + btnName);
        }

        private void Window_loaded(object sender, RoutedEventArgs e)
        {
            DatabaseOperator.ShowScores(EventListBox);
        }
    }
}
