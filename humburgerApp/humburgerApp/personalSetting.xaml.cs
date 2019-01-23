using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace humburgerApp
{
    public class calculate
    {
        public double Calculate_subtracting(double x, double y, string i)
        {
            switch (i)
            {
                case "+":
                    return (x + y);
                case "-":
                    return (x - y);
                case "*":
                    return (x * y);
                case "/":
                    return (x / y);
                default:
                    return 0;

            }
        }
        public double Calculate_sqrt(double x)
        {
            return Math.Sqrt(x);
        }
        public double Calculate_power(double x, int number)
        {
            double sum = 1;
            for (int i = 0; i < number; i++)
            {
                sum = sum * x;
            }
            return sum;
        }
    }
    public sealed partial class psesonalSetting : Page
    {
        double first,second,result;
        string number;
        string sign;
        calculate ca = new calculate();
        public psesonalSetting()
        {
            this.InitializeComponent();
            
        }
        private void Shu1_Click(object sender, RoutedEventArgs e)
        {
            number += 1;
            show.Text += 1;
        }

        private void Shu2_Click(object sender, RoutedEventArgs e)
        {
            number += 2;
            show.Text += 2;
        }

        private void Shu3_Click(object sender, RoutedEventArgs e)
        {
            number += 3;
            show.Text += 3;
        }

        private void Shu4_Click(object sender, RoutedEventArgs e)
        {
            number += 4;
            show.Text += 4;
        }

        private void Shu5_Click(object sender, RoutedEventArgs e)
        {
            number += 5;
            show.Text += 5;
        }

        private void Shu6_Click(object sender, RoutedEventArgs e)
        {
            number += 6;
            show.Text += 6;
        }

        private void Shu7_Click(object sender, RoutedEventArgs e)
        {
            number += 7;
            show.Text += 7;
        }

        private void Shu8_Click(object sender, RoutedEventArgs e)
        {
            number += 8;
            show.Text += 8;
        }

        private void Shu0_Click(object sender, RoutedEventArgs e)
        {
            number += 0;
            show.Text += 0;
        }


        private void Shu9_Click(object sender, RoutedEventArgs e)
        {
            number += 9;
            show.Text += 9;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            first = Convert.ToDouble(number);
            sign = "+";
            show.Text += "+";
            number = null;
        }

        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            second = Convert.ToDouble(number);
            show.Text = ca.Calculate_subtracting(first, second, sign).ToString();
        }
    }
}
/*       private void BackButton_Click(object sender, RoutedEventArgs e)
       {
           On_BackRequested();
       }
       private bool On_BackRequested()
       {
           if (this.Frame.CanGoBack)
           {
               this.Frame.GoBack();
               return true;
           }
           return false;
       }*/
