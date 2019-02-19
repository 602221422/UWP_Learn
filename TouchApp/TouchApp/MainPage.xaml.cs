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
using Windows.UI.Xaml.Shapes;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace TouchApp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private TranslateTransform dragTranslation;
        public MainPage()
        {
            this.InitializeComponent();
            show.PointerPressed += TouchRectangle_PointerPressed;
            show.PointerReleased += TouchRectangle_PointerReleased;
            show.PointerExited += Touch_PinterExited;
            show.ManipulationDelta += touchRectangle_ManipulationDelta;
            dragTranslation = new TranslateTransform();
            show.RenderTransform = this.dragTranslation;

        }
        private void Touch_PinterExited(object sender, PointerRoutedEventArgs e)
        {
            Rectangle rect = sender as Rectangle;
            if (null != rect)
            {
                rect.Width = 200;
                rect.Height = 100;
            }
        }
        private void TouchRectangle_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            Rectangle rect = sender as Rectangle;
            if (null != rect)
            {
                rect.Width = 200;
                rect.Height = 100;
            }
        }
        private void TouchRectangle_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Rectangle rect = sender as Rectangle;
            if (null != rect)
            {
                rect.Width = 250;
                rect.Height = 150;
            }
        }
        void touchRectangle_ManipulationDelta(object sender,ManipulationDeltaRoutedEventArgs e)
        {
            // Move the rectangle.
            dragTranslation.X += e.Delta.Translation.X;
            dragTranslation.Y += e.Delta.Translation.Y;
        }

    }
}
