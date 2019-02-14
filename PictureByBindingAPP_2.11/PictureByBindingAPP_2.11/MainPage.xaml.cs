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
using Windows.UI.Core;
using System.Collections.ObjectModel;

namespace PictureByBindingAPP_2._11
{
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Imagemodel> images;
        BackgroundDownloader backgroundDownload = new BackgroundDownloader();
        public MainPage()
        {
            this.InitializeComponent();
            images = ImageManager.getimages();  
            foreach (Imagemodel image in images)
            {
                DataAccess.AddData(image.imageId,image.CoverImage, image.Title, image.Author);
                Backgrounddownload(image);
            }
            DispatcherTimerSetup();
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
        async public void Backgrounddownload(Imagemodel imagemodel)
        {   
            StorageFolder folder = await KnownFolders.PicturesLibrary.CreateFolderAsync("ONE", CreationCollisionOption.OpenIfExists);
            StorageFile newFile = await folder.CreateFileAsync(imagemodel.Title+".jpg", CreationCollisionOption.OpenIfExists);
                Uri uri = new Uri(imagemodel.CoverImage);
                DownloadOperation download = backgroundDownload.CreateDownload(uri, newFile);
                await download.StartAsync();
        }
        public void DispatcherTimerSetup()
        {
            Timer timer = new Timer(5000);
            timer.Elapsed += new ElapsedEventHandler(Refersh);
            timer.AutoReset = true;
            timer.Enabled = true;
            GC.KeepAlive(timer);
        }
        async public void Refersh(object sender, ElapsedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                images.Clear();
                for(int i = 0; i < 6; i++)
                {
                images.Add(DataAccess.RandomShowOne());
                }
                //images = DataAccess.ShowAll();
                //picturegrid.ItemsSource = DataAccess.ShowAll();
                //images.Add(new Imagemodel { imageId = 7, Title = "zxc", Author = "qqq", CoverImage = "http://pic34.photophoto.cn/20150117/0005018381613004_b.jpg" });
            }); 
        }
    }
}
