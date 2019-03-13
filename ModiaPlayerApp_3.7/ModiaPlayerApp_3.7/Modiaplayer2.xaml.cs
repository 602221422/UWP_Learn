using Windows.UI.Xaml.Controls;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using System;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ModiaPlayerApp_3._7
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Modiaplayer2 : Page
    {
        MediaSource _mediaSource;
        MediaPlayer _mediaPlayer;
        MediaPlaybackItem _mediaPlaybackItem;
        public Modiaplayer2()
        {
            this.InitializeComponent();
        }
        async public void Showvediobyfile()
        {
            var filepicker = new Windows.Storage.Pickers.FileOpenPicker();
            string[] filetypes = new string[] { ".wmv", ".mp4", ".mkv" };
            foreach(string fileType in filetypes)
            {
                filepicker.FileTypeFilter.Add(fileType);
            }
            filepicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.VideosLibrary;
            StorageFile file = await filepicker.PickSingleFileAsync();
            if(!(file is null))
            {
                _mediaSource = MediaSource.CreateFromStorageFile(file);
                _mediaPlayer = new MediaPlayer();
                _mediaPlayer.Source = _mediaSource;
                mediaPlayerElement.SetMediaPlayer(_mediaPlayer);
                _mediaPlayer.AutoPlay = true;
            }
        }

        private void Show_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //Showvediobyfile();
            Showmediaplaybackitem();
        }
        async public void Showmediaplaybackitem()
        {
            var filepicker = new Windows.Storage.Pickers.FileOpenPicker();
            string[] filetypes = new string[] { ".wmv", ".mp4", ".mkv" };
            foreach (string fileType in filetypes)
            {
                filepicker.FileTypeFilter.Add(fileType);
            }
            filepicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.VideosLibrary;
            StorageFile file = await filepicker.PickSingleFileAsync();
            if (!(file is null))
            {
                _mediaSource = MediaSource.CreateFromStorageFile(file);
                _mediaPlaybackItem = new MediaPlaybackItem(_mediaSource);

                _mediaPlaybackItem.AudioTracksChanged += PlaybackItem_AudioTracksChanged;
                _mediaPlaybackItem.VideoTracksChanged += MediaPlaybackItem_VideoTracksChanged;
                _mediaPlaybackItem.TimedMetadataTracksChanged += MediaPlaybackItem_TimedMetadataTracksChanged;

                _mediaPlayer = new MediaPlayer();
                _mediaPlayer.Source = _mediaPlaybackItem;
                mediaPlayerElement.SetMediaPlayer(_mediaPlayer);
                _mediaPlayer.AutoPlay = true;
            }
        }
        private async void MediaPlaybackItem_VideoTracksChanged(MediaPlaybackItem sender, IVectorChangedEventArgs args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                videoTracksComboBox.Items.Clear();
                for (int index = 0; index < sender.VideoTracks.Count; index++)
                {
                    var videoTrack = sender.VideoTracks[index];
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = String.IsNullOrEmpty(videoTrack.Label) ? $"Track {index}" : videoTrack.Label;
                    item.Tag = index;
                    videoTracksComboBox.Items.Add(item);
                }
            });
        }
        //使用 ComboBox 来显示可用的视频轨。
        /*在组合框的 SelectionChanged 处理程序中，
         * 从所选项的 Tag 属性检索轨索引。 
         * 设置媒体播放项的 VideoTracks 列表的 SelectedIndex 属性
         * 会导致 MediaElement 或 MediaPlayer 将活动视频轨切换为指定索引。*/
        private void videoTracksComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int trackIndex = (int)((ComboBoxItem)((ComboBox)sender).SelectedItem).Tag;
            _mediaPlaybackItem.VideoTracks.SelectedIndex = trackIndex;
        }
        private async void PlaybackItem_AudioTracksChanged(MediaPlaybackItem sender, IVectorChangedEventArgs args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                audioTracksComboBox.Items.Clear();
                for (int index = 0; index < sender.AudioTracks.Count; index++)
                {
                    var audioTrack = sender.AudioTracks[index];
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = String.IsNullOrEmpty(audioTrack.Label) ? $"Track {index}" : audioTrack.Label;
                    item.Tag = index;
                    videoTracksComboBox.Items.Add(item);
                }
            });
        }
        private void audioTracksComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int trackIndex = (int)((ComboBoxItem)((ComboBox)sender).SelectedItem).Tag;
            _mediaPlaybackItem.AudioTracks.SelectedIndex = trackIndex;
        }
        /*MediaPlaybackItem 对象还可能包含零个或多个 TimedMetadataTrack 对象。 
         * 计时元数据轨可以包含副标题或描述文字，或者可以包含专属于你的应用的自定义数据。 
         * 计时元数据曲目包含由继承自 IMediaCue 的对象表示的提示列表，例如 DataCue 或 TimedTextCue。
         * 每个提示都有开始时间和持续时间，用于确定何时激活提示以及持续多久。*/
        private async void MediaPlaybackItem_TimedMetadataTracksChanged(MediaPlaybackItem sender, IVectorChangedEventArgs args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                for (int index = 0; index < sender.TimedMetadataTracks.Count; index++)
                {
                    var timedMetadataTrack = sender.TimedMetadataTracks[index];

                    ToggleButton toggle = new ToggleButton()
                    {
                        Content = String.IsNullOrEmpty(timedMetadataTrack.Label) ? $"Track {index}" : timedMetadataTrack.Label,
                        Tag = (uint)index
                    };
                    toggle.Checked += Toggle_Checked;
                    toggle.Unchecked += Toggle_Unchecked;

                    MetadataButtonPanel.Children.Add(toggle);
                }
            });
        }
        private void Toggle_Checked(object sender, RoutedEventArgs e) =>
    _mediaPlaybackItem.TimedMetadataTracks.SetPresentationMode((uint)((ToggleButton)sender).Tag,
        TimedMetadataTrackPresentationMode.PlatformPresented);
        private void Toggle_Unchecked(object sender, RoutedEventArgs e) =>
    _mediaPlaybackItem.TimedMetadataTracks.SetPresentationMode((uint)((ToggleButton)sender).Tag,
        TimedMetadataTrackPresentationMode.Disabled);
    }
}
