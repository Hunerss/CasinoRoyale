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

namespace CasinoRoyale.windows.pages
{
    /// <summary>
    /// Logika interakcji dla klasy MidMenu.xaml
    /// </summary>
    public partial class MidMenu : UserControl
    {
        MainWindow window;
        public MidMenu(MainWindow win)
        {
            this.window = win;
            InitializeComponent();
        }

        private void Navigation(object sender, RoutedEventArgs e)
        {
            string btnName = ((Button)sender).Name[4].ToString();
            if (btnName == "1")
                window.frame.NavigationService.Navigate(new Login(window));
            else if (btnName == "2")
                window.frame.NavigationService.Navigate(new Registration(window));
            else
                Console.WriteLine("Welcome - error log - Navigation button number to bit - " + btnName);
        }
    }
}
