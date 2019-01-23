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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace NavAPP
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Page2 : Page
    {
        public Page2()
        {
            this.InitializeComponent();
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is string && !string.IsNullOrWhiteSpace((string)e.Parameter))
            {
                greeting.Text = $"Hi,{e.Parameter.ToString()}";
                /*
                   新的字符串格式化。
                   以前是 abc {0} cccc {1}，格式化占位符是用序号的，从0开始，依此类推。
                   现在:$"abc {x} ccc {y}"，这样就把序号忽略了，直接把对应的变量作为占位符，
                   为了和过去的序号占位符区别，前面加了个$符号。 
                */
            }
            else
            {
                greeting.Text = "Hi!";
            }
            base.OnNavigatedTo(e);
        }
    }
}
