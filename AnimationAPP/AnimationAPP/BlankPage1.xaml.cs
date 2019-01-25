using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace AnimationAPP
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        public BlankPage1()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ConnectedAnimation imageAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("image");
            if (imageAnimation != null)
            {
                imageAnimation.TryStart(DestinationImage);
            }
        }

        private void AnimatedButton_Click(object sender, RoutedEventArgs e)
        {
            animatedButton.Margin = new Thickness(100);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Color rect = new Color();
            rect.R = 200;
            rect.A = 250;
            Rectangle myrect = new Rectangle();
            myrect.Fill = new SolidColorBrush(rect);
            myrect.Height = 100;
            myrect.Width = 100;
            myrect.Margin = new Thickness(10);
            rectangleItems.Items.Add(myrect);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (rectangleItems.Items.Count > 0)
            {
                rectangleItems.Items.RemoveAt(0);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (rectangleItems3.Items.Count > 0)
            {
                rectangleItems3.Items.RemoveAt(0);
            }
        }
    }
   
}
