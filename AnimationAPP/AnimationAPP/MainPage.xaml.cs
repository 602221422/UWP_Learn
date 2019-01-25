using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
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

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace AnimationAPP
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            var frame = new Frame();
            frame.ContentTransitions = new TransitionCollection();
            frame.ContentTransitions.Add(new NavigationThemeTransition());
            //按钮显示动画
            var compositor = Window.Current.Compositor;
            var animation = compositor.CreateVector3KeyFrameAnimation();
            animation.InsertKeyFrame(1.0f, new Vector3(2.0f, 2.0f, 1.0f));
            animation.Duration = TimeSpan.FromSeconds(1);
            animation.Target = "Scale";

            button.StartAnimation(animation);
        }

        private void ContentHost_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Rectangle newItem = new Rectangle();
            Random rand = new Random();

            newItem.Height = 200;
            newItem.Width = 200;
            newItem.Fill=new SolidColorBrush(
                Color.FromArgb(255,(byte)rand.Next(0,255),(byte)rand.Next(0,255), (byte)rand.Next(0, 255)));
            ContentHost.Content = newItem;
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("image", SourceImage);
            Frame.Navigate(typeof(BlankPage1));
        }

        private void Myrectangle_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            EnterStoryboard.Begin();
        }

        private void Myrectangle_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ExitStoryboard.Begin();
        }

        private void MyRectangle_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            myRectangle.Margin = new Thickness(400, 0, 0, 0);
            PointerReleasedStoryBoard.Begin();
        }

        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            Rectangle newItem = new Rectangle();
            Random rand = new Random();

            newItem.Height = 100;
            newItem.Width = 100;
            newItem.Fill = new SolidColorBrush(Color.FromArgb(255,
                    (byte)rand.Next(0, 255), (byte)rand.Next(0, 255), (byte)rand.Next(0, 255)));
            // Insert a new Rectangle of a random color into the ItemsControl at index 2.
            ItemsList.Items.Insert(2, newItem);
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("image", SourceImage);
            Frame.Navigate(typeof(BlankPage2));
        }
    }
}
