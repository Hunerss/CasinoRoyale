﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        public CardModel()
        {
            InitializeComponent();
        }

        public void SetPosition(double x, double y)
        {
            SetValue(Window.LeftProperty, x - MainGrid.ActualWidth / 2);
            SetValue(Window.TopProperty, y - MainGrid.ActualHeight / 2);
        }

        public Point GetPosition()
        {
            Point p = new Point();
            p.X = (double)GetValue(Window.LeftProperty) + MainGrid.ActualWidth / 2;
            p.Y = (double)GetValue(Window.TopProperty) + MainGrid.ActualHeight / 2;
            return p;
        }


        public void Update()
        {
            TimeToTarget += 0.02;
            if (TimeToTarget > 1)
                TimeToTarget = 1;

            SetPosition(Lerp(StartPosition.X, TargetPositon.X, TimeToTarget), Lerp(StartPosition.Y, TargetPositon.Y, TimeToTarget));
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