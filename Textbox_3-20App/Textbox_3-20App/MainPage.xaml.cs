using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Textbox_3_20App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.textshow.TextChanged+= new TextChangedEventHandler(this.Year_TextChanging);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < textshow.Text.Length; i++)
            {
                if (Char.IsNumber(textshow.Text[i]))
                {
                    return;
                }

                else if (!Char.IsNumber(textshow.Text[i]))
                {
                    textshow.Text= textshow.Text.Remove(i,1);
                }
            }
            
        }
        private void Year_TextChanging(object sender, TextChangedEventArgs e)
        {
            string data = textshow.Text.ToString();
            StringBuilder sb = new StringBuilder(data);
            int i = 0;
            while (i < sb.Length)
            {
                if (!Char.IsNumber(sb[i]))
                {
                    sb.Remove(i, 1);
                    i = 0;
                }
                else
                {
                    i++;
                }
            }
            this.textshow.Text = sb.ToString();
        }

    }
}
