using CasinoRoyale.classes;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace CasinoRoyale.windows.pages
{
    /// <summary>
    /// Logika interakcji dla klasy CardModel.xaml
    /// </summary>
    public partial class CardModel : UserControl
    {
        public Point TargetPositon;
        public Point StartPosition;
        public double TimeToTarget;

        private static string BackgroundCardPath;
        private static string BackgroundReversPath = "pack://application:,,,/images/cards/reverse.jpg";

        public CardModel()
        {
            InitializeComponent();
        }

        public void SwichBackground()
        {
            if (background.Source == new BitmapImage(new Uri(BackgroundCardPath, UriKind.Absolute)))
                background.Source = new BitmapImage(new Uri(BackgroundReversPath, UriKind.Absolute));
            else
                background.Source = new BitmapImage(new Uri(BackgroundCardPath, UriKind.Absolute));
        }

        public void SetBackground(Card card)
        {
            string link = "pack://application:,,,/images/cards/" + card.Image;
            BackgroundCardPath = link;
            background.Source = new BitmapImage(new Uri(link, UriKind.Absolute));
        }

        public void SetPosition(double x, double y)
        {
            SetValue(Window.LeftProperty, x - MainGrid.ActualWidth / 2);
            SetValue(Window.TopProperty, y - MainGrid.ActualHeight / 2);
        }

        public Point GetPosition()
        {
            Point p = new()
            {
                X = (double)GetValue(Window.LeftProperty) + MainGrid.ActualWidth / 2,
                Y = (double)GetValue(Window.TopProperty) + MainGrid.ActualHeight / 2
            };
            return p;
        }


        public void Update()
        {
            TimeToTarget += 0.02;
            if (TimeToTarget > 1)
                TimeToTarget = 1;

            SetPosition(Lerp(GetPosition().X, TargetPositon.X, TimeToTarget), Lerp(GetPosition().Y, TargetPositon.Y, TimeToTarget));
        }

        public static double Lerp(double a, double b, double t)
        {
            return a * (1 - t) + b * t;
        }

        public void SetTargetPosition(Point target)
        {
            StartPosition = GetPosition();
            TimeToTarget = 0;
            TargetPositon = target;
        }
    }
}
