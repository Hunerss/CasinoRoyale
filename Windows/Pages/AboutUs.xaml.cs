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
    /// Logika interakcji dla klasy AboutUs.xaml
    /// </summary>
    public partial class AboutUs : UserControl
    {
        private static MainWindow window;
        public AboutUs()
        {
            window = (MainWindow)Window.GetWindow(this);
            InitializeComponent();
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

/*
 The Storied Legacy of Casino Royale

Established amidst the opulence of Monaco in the year 1876, Casino Royale emerged as a beacon of elegance and extravagance during the illustrious Belle Époque era. From its inception, the casino captivated the imaginations of nobility, aristocracy, and social elite, drawing in members of royal families from across Europe.

Among its prestigious patrons were members of the Italian and French royal families, who frequented the halls of Casino Royale, indulging in games of chance and reveling in the splendor of the grand establishment. Their presence lent an air of sophistication and prestige to the casino, solidifying its reputation as a sanctuary for the elite.

As the years passed, Casino Royale flourished, becoming synonymous with luxury and glamour. Yet, amidst the glittering façade, the casino harbored a secret role during times of turmoil. During the tumultuous years of war, the management of Casino Royale discreetly supported the Allied forces, providing vital resources and intelligence from their privileged vantage point.

Additionally, the casino extended its benevolence to those affected by the ravages of war, offering aid and support to the impoverished inhabitants of war-torn territories. Through charitable initiatives and humanitarian efforts, the management of Casino Royale demonstrated a commitment to alleviating suffering and fostering hope in communities devastated by conflict.

However, as the dawn of the digital age approached and technological advancements reshaped the landscape of entertainment, Casino Royale faced a pivotal moment of transition. In [insert current year], after centuries of traditional operation, the venerable institution made the bold decision to embrace the digital frontier.

With the launch of its digital platform, Casino Royale embarked on a new chapter in its storied history, bringing the allure and excitement of the casino experience to a global audience. Combining cutting-edge technology with timeless elegance, the digital incarnation of Casino Royale continues to uphold its legacy of excellence while embracing the opportunities of the modern era.

As it forges ahead into the future, Casino Royale remains a testament to resilience, adaptability, and the enduring allure of the casino experience. From its illustrious origins in Monaco to its digital evolution, Casino Royale stands as a symbol of sophistication, luxury, and timeless entertainment.
 */