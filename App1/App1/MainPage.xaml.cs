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
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace App1
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            // Using the Tag property lets you localize the display name
            // without affecting functionality.
            var cb = (CheckBox)sender;
            if (cb.IsChecked == true)
            {
                AddMenuItem(cb.Tag.ToString(), cb.Content.ToString());
            }
            else
            {
                RemoveMenuItem(cb.Content.ToString());
            }
        }

        private void AddMenuItem(string colorString, string locColorName)
        {
            // Set the color.
            Color newColor = Colors.Blue;
            if (colorString == "green")
                newColor = Colors.Green;
            else if (colorString == "red")
                newColor = Colors.Red;
            else if (colorString == "yellow")
                newColor = Colors.Yellow;

            // Create the menu item.
            var newMenuItem = new MenuFlyoutItem();
            newMenuItem.Text = locColorName;
            newMenuItem.Click += (s, e1) =>
            {
                Rect1.Fill = new SolidColorBrush(newColor);
            };

            // Add the item to the menu.
            RectangleColorMenu.Items.Add(newMenuItem);

            // Sort the menu so it's always consistent.
            var orderedItems = RectangleColorMenu.Items.OrderBy(i => ((MenuFlyoutItem)i).Text).ToList();
            RectangleColorMenu.Items.Clear();
            foreach (var item in orderedItems)
            {
                RectangleColorMenu.Items.Add(item);
            }
        }

        private void RemoveMenuItem(string locColorName)
        {
            // Get any menu items to remove and remove them.
            var items = RectangleColorMenu.Items.Where(i => ((MenuFlyoutItem)i).Text == locColorName);
            foreach (MenuFlyoutItem item in items)
            {
                RectangleColorMenu.Items.Remove(item);
            }
        }
    }
}
