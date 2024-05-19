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
    /// Logika interakcji dla klasy Registration.xaml
    /// </summary>
    public partial class Registration : UserControl
    {
        MainWindow window;
        public Registration(MainWindow win)
        {
            window = win;
            InitializeComponent();
        }

        public Registration(MainWindow win, string login, string password, DateTime date)
        {
            InitializeComponent();
            window = win;
            if(string.IsNullOrEmpty(login))
                txb_0.Text = "";
            else
                txb_0.Text = login;
            if (string.IsNullOrEmpty(password))
                txb_1.Password = "";
            else
                txb_1.Password = password;
            txb_2.SelectedDate = date;
        }

        private void Navigation(object sender, RoutedEventArgs e)
        {
            string btnName = ((Button)sender).Name[4].ToString();
            if (btnName == "0")
                window.frame.NavigationService.Navigate(new MidMenu(window));
            else if (btnName == "1")
            {
                DatabaseOperator dto = new();
                if (string.IsNullOrEmpty(txb_0.Text) || string.IsNullOrEmpty(txb_1.Password) ||
                    string.IsNullOrEmpty(txb_2.Text) || chb_0.IsChecked == false || chb_1.IsChecked == false)
                {
                    MessageBox.Show("Fill all expected data");
                }
                else
                {
                    DateTime? selectedDate = txb_2.SelectedDate;
                    Boolean licence = false, instruction = false;
                    if (chb_0.IsChecked == true)
                        licence = true;
                    if (chb_1.IsChecked == true)
                        instruction = true;
                    if (dto.Register(txb_0.Text, txb_1.Password, selectedDate.Value, licence, instruction))
                    {
                        MessageBox.Show("Your account was created successfully");
                        window.frame.NavigationService.Navigate(new Login(window));
                    }
                    else
                    {
                        MessageBox.Show("There were an error with creating your account, please try again");
                    }
                }

            }
            else if (btnName == "2")
            {
                DateTime? selectedDate = txb_2.SelectedDate;
                if(!selectedDate.HasValue)
                    txb_2.SelectedDate = DateTime.Now;
                window.frame.NavigationService.Navigate(new Licences(window, txb_0.Text, txb_1.Password, selectedDate.Value));
            }
            else if (btnName == "3")
            {
                string blackjackInstructions = "Blackjack:\n\n" +
                "The goal is to get a hand value as close to 21 as possible, but not exceeding it.\n" +
                "The player receives two cards, one face up and one face down.\n" +
                "The player has the option to draw additional cards (\"hit\"), stand with the current cards (\"stand\"), or double the bet (\"double down\").\n" +
                "Doubling down allows the player to receive only one additional card, after which the player's turn ends automatically.\n" +
                "The dealer draws cards to achieve a value of at least 17.\n" +
                "The player wins with a hand better than the dealer's without exceeding 21.";

                string rouletteInstructions = "Roulette:\n\n" +
                    "Players bet on a number or group of numbers on the board.\n" +
                    "The dealer spins the roulette wheel, and the ball stops at one of the numbers.\n" +
                    "Players win if they bet correctly, depending on the type of bet.";

                string slotMachineInstructions = "Slot Machine:\n\n" +
                    "Players insert a coin and start the slot machine.\n" +
                    "The reels spin, and then stop, showing a set of symbols.\n" +
                    "Players win if they get the right combination of symbols on one or more winning lines.";

                MessageBox.Show(blackjackInstructions + "\n\n" + rouletteInstructions + "\n\n" + slotMachineInstructions, "Instrukcje gier kasynowych");
            }
            else
                Console.WriteLine("Register - error log - Navigation button number to bit - " + btnName);
        }
    }
}
