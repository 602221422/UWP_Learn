using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace case_4_3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        private void ZoomSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (Control1 != null)
            {
                Control1.ChangeView(null, null, (float)e.NewValue);
            }
        }
        private void Control1_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            ZoomSlider.Value = Control1.ZoomFactor;
        }

        private async void ScrollViewer_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;
            var doubleTapPoint = e.GetPosition(scrollViewer);

            if (scrollViewer.ZoomFactor != 1)
            {
                scrollViewer.ZoomToFactor(1);
            }
            else if (scrollViewer.ZoomFactor == 1)
            {
                scrollViewer.ZoomToFactor(2);

                var dispatcher = Window.Current.CoreWindow.Dispatcher;
                await dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                {
                    scrollViewer.ScrollToHorizontalOffset(doubleTapPoint.X);
                    scrollViewer.ScrollToVerticalOffset(doubleTapPoint.Y);
                });
            }
        }
        int tag = 1;
        private void Control1_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            
            if(e.Key==Windows.System.VirtualKey.Up)
            {
                tag++;
                Control1.ChangeView(null, null, tag);
            }
            if (e.Key == Windows.System.VirtualKey.Down)
            {
                tag--;
                Control1.ChangeView(null, null, tag);
            }

        }
    }
}
