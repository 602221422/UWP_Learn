using DocumentFormat.OpenXml.Packaging;
using SpreadSheetWorking.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using SpreadSheetWorking.ViewModel;
using Windows.UI.StartScreen;
using Windows.Storage;
using Windows.Foundation.Metadata;
using Windows.ApplicationModel;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SpreadSheetWorking
{
    public sealed partial class MainPage : Page
    {
        MemberInfo mymember = new MemberInfo();
        ObservableCollection<MemberInfo> memcoll = new ObservableCollection<MemberInfo>();
        public MainPage()
        {
            this.InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ShowLiveTileTipIfNeeded();
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
                FlyoutPinTileTip.ShowAt(ShowTip);
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

        private void Show_ItemClick(object sender, ItemClickEventArgs e)
        {
            var info = (MemberInfo)e.ClickedItem;
            ToastHelper.ShowInfoToast(info.UserName, info.VacationHour);
        }
    }
}
