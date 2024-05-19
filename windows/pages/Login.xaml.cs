using CasinoRoyale.Windows.Pages;
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
using ToDoList.classes;

namespace CasinoRoyale.windows.pages
{
    /// <summary>
    /// Logika interakcji dla klasy Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        MainWindow window;
        public Login(MainWindow win)
        {
            window = win;
            InitializeComponent();
        }

        private void Navigation(object sender, RoutedEventArgs e)
        {
            string btnName = ((Button)sender).Name[4].ToString();
            if (btnName == "0")
                window.frame.NavigationService.Navigate(new Welcome(window));
            else if (btnName == "1")
            {
                DatabaseOperator dto = new();
                if (string.IsNullOrEmpty(txb_0.Text) || string.IsNullOrEmpty(txb_1.Password))
                {
                    MessageBox.Show("Fill all expected data");
                }
                else
                {
                    if (dto.Login(txb_0.Text, txb_1.Password))
                    {
                        MessageBox.Show("You singed in successfully");
                        window.frame.NavigationService.Navigate(new MainMenu(window, txb_0.Text));
                    }
                    else
                    {
                        MessageBox.Show("There were an error with singing in, please try again");
                    }
                }

            }
            else
                Console.WriteLine("Welcome - error log - Navigation button number to bit - " + btnName);
        }
    }
}
