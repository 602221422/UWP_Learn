using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Devices;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace ModiaPlayerApp_3._7
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MediaPlayer mediaPlayer = new MediaPlayer();
        public MainPage()
        {
            this.InitializeComponent();
            //播放媒体文件，看不见视频。
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/fishes.wmv"));
            
            Showdevices();
        }
        async public void Showdevices()
        {
            string audioSelector = MediaDevice.GetAudioRenderSelector();
            //获取所选类型的所有可用设备列表
            var outputDevices = await DeviceInformation.FindAllAsync(audioSelector);
            //将返回的设备添加到 ComboBox 以允许用户选择设备。
            foreach (var device in outputDevices)
            {
                var deviceItem = new ComboBoxItem();
                deviceItem.Content = device.Name;
                deviceItem.Tag = device;
                _audioDeviceComboBox.Items.Add(deviceItem);
            }
        }
        //在用于设备组合框的 SelectionChanged 事件中，MediaPlayer 的 AudioDevice 属性设置为所选设备，
        //该属性存储在 ComboBoxItem 的 Tag 属性中。
        private void _audioDeviceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeviceInformation selectedDevice = (DeviceInformation)((ComboBoxItem)_audioDeviceComboBox.SelectedItem).Tag;
            if (selectedDevice != null)
            {
                mediaPlayer.AudioDevice = selectedDevice;                
                //使用 MediaPlayerElement 在 XAML 中呈现视频
                _mediaplayerelement.SetMediaPlayer(mediaPlayer);
                mediaPlayer.Play();
            }
        }
        //将 Position 属性设置为当前播放位置向后3秒的值。
        private void SkipForWardButton_Click(object sender, RoutedEventArgs e)
        {
            var session = mediaPlayer.PlaybackSession;
            session.Position = session.Position + TimeSpan.FromSeconds(3);
        }
        //使用切换按钮在正常播放速度和 2 倍播放速度之间切换。
        private void SpeedToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            mediaPlayer.PlaybackSession.PlaybackRate = 2.0;
        }

        private void SpeedToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            mediaPlayer.PlaybackSession.PlaybackRate = 1.0;
        }
        private void MediaPlaybackSession_BufferingStarted(MediaPlaybackSession sender,object args)
        {
            MediaPlaybackSessionBufferingStartedEventArgs bufferingStartedEventArgs = args as MediaPlaybackSessionBufferingStartedEventArgs;
            if(bufferingStartedEventArgs!=null && bufferingStartedEventArgs.IsPlaybackInterruption)
            {
                //提示播放中断
            }
        }
        private void MediaPlaybackSession_BufferingEnded(MediaPlaybackSession sender, object args)
        {
            // 更新UI以指示回放不再缓冲
        }
    }
}
