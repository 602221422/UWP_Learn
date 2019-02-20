using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using right_clickingApp_2_10.Model;
// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace right_clickingApp_2_10
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<SampleOrder> sampleOrders;
        public MainPage()
        {
            this.InitializeComponent();
            sampleOrders = SampleManager.GetSampleOrders();
            DataGridshow.ItemsSource = sampleOrders;
        }
        private void MenuFlyoutItem_Copy(object sender, RoutedEventArgs e)
        {
            
            MenuFlyoutItem mfi = sender as MenuFlyoutItem;
            SampleOrder seleted = mfi.DataContext as SampleOrder;

            var copiedItem = SampleOrder.Clone(seleted.Name,seleted.Url,seleted.OrderId);
            sampleOrders.Add(copiedItem);
            DataGridshow.ItemsSource= sampleOrders;
        }

        private void MenuFlyoutItem_Delete(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem mfi = sender as MenuFlyoutItem;
            SampleOrder seleted = mfi.DataContext as SampleOrder;
            sampleOrders.Remove(seleted);
        }
    }
}
