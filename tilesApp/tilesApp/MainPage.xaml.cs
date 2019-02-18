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
using tilesApp.Helpers;
using Windows.Storage;
using Windows.Foundation.Metadata;
using Windows.UI.Popups;
using Windows.ApplicationModel;
using Windows.UI.StartScreen;
// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace tilesApp
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

        private void ButtonSendToast_Click(object sender, RoutedEventArgs e)
        {
            ToastHelper.ShowOrderLunchToast();
        }
        private async void ShowLiveTileTipIfNeeded()
        {
            try
            {
                //检查以前是否已经展示过这个技巧，
                //如果是这样，不要再向用户显示提示
                if (ApplicationData.Current.LocalSettings.Values.Any(i => i.Key.Equals("ShownLiveTileTile")))
                {
                    //始终展示
                }
                ApplicationData.Current.LocalSettings.Values["ShownLiveTileTip"] = true;
                //如果启动屏幕管理器API不存在
                if (!ApiInformation.IsTypePresent("Windows.UI.StartScreen.StartScreenManager"))
                {
                    ShowMessage("StartScreenManager API isn't present on this build.");
                    return;
                }
                //获取应用程序列表条目
                var appListEntry = (await Package.Current.GetAppListEntriesAsync())[0];
                //获取开始屏幕管理器
                var startScreenManager = StartScreenManager.GetDefault();
                //检查Start是否支持固定应用程序
                bool supportsPin = startScreenManager.SupportsAppListEntry(appListEntry);
                if (!supportsPin)
                {
                    ShowMessage("Start doesn't support pinning.");
                    return;
                }
                //固定
                bool isPinned = await StartScreenManager.GetDefault().ContainsAppListEntryAsync(appListEntry);

                //如果已经固定上
                if (isPinned)
                {
                    ShowMessage("The tile is already pinned to Start!");
                    return;
                }

                // Otherwise, show the tip
                FlyoutPinTileTip.ShowAt(ButtonShowTip);
            }
            catch (Exception ex)
            {
                ShowMessage(ex.ToString());
            }
        }

        private async void ShowMessage(string v)
        {
            await new MessageDialog(v).ShowAsync();
        }

        private async void ButtonPinTile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //获取自己的应用程序列表条目
                var appListEntry = (await Package.Current.GetAppListEntriesAsync())[0];

                // And pin your app
                bool didPin = await StartScreenManager.GetDefault().RequestAddAppListEntryAsync(appListEntry);

                if (didPin)
                {
                    ShowMessage("Success! Tile was pinned!");
                }
                else
                {
                    ShowMessage("Tile was NOT pinned, did you click no on the dialog?");
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.ToString());
            }
        }

        private void ButtonShowTip_Click(object sender, RoutedEventArgs e)
        {
            ShowLiveTileTipIfNeeded();
        }
    }
}
