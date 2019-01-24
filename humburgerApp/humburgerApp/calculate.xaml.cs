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
        double first,second;
        string number=null;
        string sign=null;
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

        private void Doc_Click(object sender, RoutedEventArgs e)
        {
            number += ".";
            show.Text += ".";
        }

        private void Add_Click(object sender, RoutedEventArgs e)   //“+”
        {
            first = Convert.ToDouble(number);
            sign = "+";
            show.Text += "+";
            number = null;
        }

        private void Subtract_Click(object sender, RoutedEventArgs e)    //“-”
        {
            first = Convert.ToDouble(number);
            sign = "-";
            show.Text += "-";
            number = null;
        }

        private void Multiply_Click(object sender, RoutedEventArgs e)   //“*”
        {
            first = Convert.ToDouble(number);
            sign = "*";
            show.Text += "*";
            number = null;
        }

        private void Divide_Click(object sender, RoutedEventArgs e)   //“/”
        {
            first = Convert.ToDouble(number);
            sign = "/";
            show.Text += "/";
            number = null;
        }

        private void Fuhaogen_Click(object sender, RoutedEventArgs e) //开根号
        {
            double i;
            first = Convert.ToDouble(number);
            i = ca.Calculate_sqrt(first);
            show.Text = i.ToString();
            number = i.ToString();
        }

        private void Mi_Click(object sender, RoutedEventArgs e)   //求幂
        {
            first = Convert.ToDouble(number);
            sign = "^";
            show.Text += "^";
            number = null;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Restart_Click(object sender, RoutedEventArgs e)   //重新开始
        {
            first = 0;
            second = 0;
            sign = null;
            number = null;
            show.Text = "";
        }

        private void Equal_Click(object sender, RoutedEventArgs e)   //求结果
        {
            if(sign=="^")
            {
                second = Convert.ToDouble(number);
                int i;
                i = Convert.ToInt32(second);
                show.Text = ca.Calculate_power(first, i).ToString();
                number = ca.Calculate_power(first, i).ToString();
            }
            else {
                second = Convert.ToDouble(number);
                if(sign == "/"&&second==0)
                {
                    show.Text = "除数不能为0，请重新开始";
                }
                else
                {
                    show.Text = ca.Calculate_subtracting(first, second, sign).ToString();
                    number = ca.Calculate_subtracting(first, second, sign).ToString();
                }
                
            }
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
