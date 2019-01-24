using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace humburgerApp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ObservableCollection<FontFamily> fonts = new ObservableCollection<FontFamily>();
        public MainPage()
        {
            this.InitializeComponent();
/*            fonts.Add( new FontFamily("whilt"));
            fonts.Add( new FontFamily("black"));
            fonts.Add( new FontFamily("blue"));
            fonts.Add( new FontFamily("green"));*/
        }
        private void Btn_close_splitview_Click(object sender, RoutedEventArgs e)
        {
            splitview.IsPaneOpen = !splitview.IsPaneOpen;
        }
        private void Navlinkslist_ItemClick(object sender, ItemClickEventArgs e)
        {
            switch(e.ClickedItem.ToString())
            {
                case "计算器":
                    childframe1.Navigate(typeof(psesonalSetting));
                    break;
                case "地图":
                    childframe1.Navigate(typeof(Map));
                    break;
                case "使用帮助":
                    childframe1.Navigate(typeof(UserGuide));
                    break;
                case "Settings":
                    childframe1.Navigate(typeof(Settings));
                    break;
                default:
                    break;
            }
        }

        private void Select_background_DropDownClosed(object sender, object e)
        {
            string colour = select_background.SelectionBoxItem.ToString();
            switch (colour)
            {
                case "whilt":
                    gridall.Background = new SolidColorBrush(Windows.UI.Colors.White);
                    grid1.Background = new SolidColorBrush(Windows.UI.Colors.WhiteSmoke);
                    break;
                case "gray":
                    gridall.Background = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    grid1.Background = new SolidColorBrush(Windows.UI.Colors.GreenYellow);
                    break;
                case "blue":
                    gridall.Background = new SolidColorBrush(Windows.UI.Colors.AliceBlue);
                    grid1.Background = new SolidColorBrush(Windows.UI.Colors.DeepSkyBlue);
                    break;
                case "green":
                    gridall.Background = new SolidColorBrush(Windows.UI.Colors.LightSeaGreen);
                    grid1.Background = new SolidColorBrush(Windows.UI.Colors.LightGreen);
                    break;
            }
        }
    }
}
