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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Window.Current.CoreWindow.KeyDown += Control1_KeyDown;
        }
        int tag = 1;
        private void Control1_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs e)
        {

            if (e.VirtualKey == Windows.System.VirtualKey.Up)
            {
                tag++;
                Control1.ChangeView(null, null, tag);
            }
            if (e.VirtualKey == Windows.System.VirtualKey.Down)
            {
                tag--;
                Control1.ChangeView(null, null, tag);
            }
   
        }
    }
}
