using BindingTwowayApp_2_14.Model;
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

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace BindingTwowayApp_2_14
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private User user = new User();
        public MainPage()
        {
            this.InitializeComponent();
            if (Resources["user"] is User user)
            {
                user.ID = 10000;
                user.Name = "CQ";
                user.Gender = "不明";
            }
        }
        public User ViewModel { get; set; }
        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if (Resources["user"] is User user)
            {
                user.ID = 100;
                user.Name = "aa";
                user.Gender = "不明";
            }
        }
    }
}
