﻿using CasinoRoyale.classes;
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
    /// Logika interakcji dla klasy Welcome.xaml
    /// </summary>
    public partial class Welcome : UserControl
    {
        private static MainWindow window;
        public Welcome()
        {
            window = (MainWindow)Window.GetWindow(this);
            InitializeComponent();
        }

        private void Navigation(object sender, RoutedEventArgs e)
        {
            window.frame.NavigationService.Navigate(new MainMenu());
        }

        private void Navigation(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Welcome - success log - Moving to Main Menu");
            window.frame.NavigationService.Navigate(new MainMenu(window));
        }
    }
}
