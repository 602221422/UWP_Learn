using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using PictureByBindingAPP_2._11_Model;
using DataAccessLibrary;
using Windows.Networking.BackgroundTransfer;
using System.Timers;
using System.Threading;

namespace PictureByBindingAPP_2._11
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<Imagemodel> images;
        public MainPage()
        {
            this.InitializeComponent();
            images = ImageManager.getimages();  
            foreach (Imagemodel image in images)
            {
                DataAccess.AddData(image.imageId,image.CoverImage, image.Title, image.Author);
            }
            Backgrounddownload();
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += new ElapsedEventHandler(PeriodicTaskHandler);
            timer.Start();

        }
        private void Picturegrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            var image = (Imagemodel)e.ClickedItem;
            show.Text = DataAccess.selectData(image.imageId);
        }

        private void Imagebutton_Click(object sender, RoutedEventArgs e)
        {
            var folder = ApplicationData.Current.LocalFolder;
            show.Text = folder.Path;
        }
        async public void Backgrounddownload()
        {
            BackgroundDownloader backgroundDownload = new BackgroundDownloader();
            StorageFolder folder = await KnownFolders.PicturesLibrary.CreateFolderAsync("ONE", CreationCollisionOption.OpenIfExists);
            foreach (Imagemodel image in images)
            {
                StorageFile newFile = await folder.CreateFileAsync(image.Title+".jpg", CreationCollisionOption.OpenIfExists);
                Uri uri = new Uri(image.CoverImage);
                DownloadOperation download = backgroundDownload.CreateDownload(uri, newFile);
                await download.StartAsync();
            }
        }
        static void PeriodicTaskHandler(object sender, ElapsedEventArgs e)
        {
            
        }
    }
}
