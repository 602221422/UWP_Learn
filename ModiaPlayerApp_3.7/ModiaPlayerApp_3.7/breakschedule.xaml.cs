using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ModiaPlayerApp_3._7
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class breakschedule : Page
    {
        MediaPlayer _mediaPlayer = new MediaPlayer();
        public breakschedule()
        {
            this.InitializeComponent();
            MediaPlaybackItem moviePlaybackItem = new MediaPlaybackItem(MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/fishes.wmv")));
            MediaBreak midrollMediaBreak = new MediaBreak(MediaBreakInsertionMethod.Interrupt, TimeSpan.FromSeconds(3));
            midrollMediaBreak.PlaybackList.Items.Add(
                new MediaPlaybackItem(MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/ladybug.wmv"))));
            moviePlaybackItem.BreakSchedule.InsertMidrollBreak(midrollMediaBreak);
            /*MediaBreak preRollMediaBreak = new MediaBreak(MediaBreakInsertionMethod.Interrupt);
            MediaPlaybackItem prerollAd = new MediaPlaybackItem(MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/ladybug.wmv")));
            prerollAd.CanSkip = false;
            preRollMediaBreak.PlaybackList.Items.Add(prerollAd);
            moviePlaybackItem.BreakSchedule.PrerollBreak = preRollMediaBreak;*/

            /*midrollMediaBreak = new MediaBreak(MediaBreakInsertionMethod.Replace, TimeSpan.FromSeconds(5));
            MediaPlaybackItem ad = new MediaPlaybackItem(MediaSource.CreateFromUri(new Uri("http://www.fabrikam.com/midroll_ad_3.mp4")),
                TimeSpan.FromSeconds(6),
                TimeSpan.FromSeconds(10));
            ad.CanSkip = false;
            midrollMediaBreak.PlaybackList.Items.Add(ad);*/
            
            _mediaPlayer.Source = moviePlaybackItem;
            mediaPlayerElement.SetMediaPlayer(_mediaPlayer);
            _mediaPlayer.AutoPlay = true;
            _mediaPlayer.BreakManager.BreakStarted += BreakManager_BreakStarted;
            _mediaPlayer.BreakManager.BreakEnded += BreakManager_BreakEnded;
            _mediaPlayer.BreakManager.BreakSkipped += BreakManager_BreakSkipped;
            _mediaPlayer.BreakManager.BreaksSeekedOver += BreakManager_BreaksSeekedOver;
        }
        private async void BreakManager_BreakStarted(MediaBreakManager sender, MediaBreakStartedEventArgs args)
        {
            MediaBreak currentBreak = sender.CurrentBreak;
            var currentIndex = currentBreak.PlaybackList.CurrentItemIndex;
            var itemCount = currentBreak.PlaybackList.Items.Count;

            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                statusTextBlock.Text = $"Playing ad {currentIndex + 1} of {itemCount}");
        }
        private async void BreakManager_BreakEnded(MediaBreakManager sender, MediaBreakEndedEventArgs args)
        {
            // Update UI to show that the MediaBreak is no longer playing
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => statusTextBlock.Text = "");

            args.MediaBreak.CanStart = false;
        }
        private async void BreakManager_BreakSkipped(MediaBreakManager sender, MediaBreakSkippedEventArgs args)
        {
            // Update UI to show that the MediaBreak is no longer playing
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => statusTextBlock.Text = "");

            MediaPlaybackItem currentItem = _mediaPlayer.Source as MediaPlaybackItem;
            if (!(currentItem.BreakSchedule.PrerollBreak is null)
                && currentItem.BreakSchedule.PrerollBreak == args.MediaBreak)
            {
                MediaBreak mediaBreak = new MediaBreak(MediaBreakInsertionMethod.Interrupt, TimeSpan.FromMinutes(10));
                mediaBreak.PlaybackList.Items.Add(new MediaPlaybackItem(MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/ladybug.wmv"))));
                currentItem.BreakSchedule.InsertMidrollBreak(mediaBreak);
            }
        }
        private void BreakManager_BreaksSeekedOver(MediaBreakManager sender, MediaBreakSeekedOverEventArgs args)
        {
            if (args.SeekedOverBreaks.Count > 1
                && args.NewPosition.TotalMinutes > args.OldPosition.TotalMinutes
                && args.NewPosition.TotalMinutes - args.OldPosition.TotalMinutes < 10.0)
                _mediaPlayer.BreakManager.PlayBreak(args.SeekedOverBreaks[0]);
        }
    }
}
