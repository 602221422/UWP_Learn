using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
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
using DataAccessLibrary;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace PictureAPP_1._30
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Selectpicture();
        }
        async public void AddData(object sender, RoutedEventArgs e)
        {
            StorageFolder pictureFolder = KnownFolders.PicturesLibrary;
            StringBuilder outputText = new StringBuilder();
            IReadOnlyList<StorageFile> fileList = await pictureFolder.GetFilesAsync();
            outputText.AppendLine("Files:");
            foreach (StorageFile file in fileList)
            {
                DataAccess.AddData(file.Path);
                outtext.Text = DataAccess.GetData();
            }
        }

        async public void Selectpicture()
        {
            ListViewItem item = new ListViewItem();
            StorageFolder pictureFolder = KnownFolders.PicturesLibrary;
            StringBuilder outputText = new StringBuilder();
            IReadOnlyList<StorageFile> files = await pictureFolder.GetFilesAsync();
            Random random = new Random();
            IRandomAccessStream ir = await files[random.Next(files.Count)].OpenAsync(FileAccessMode.Read);
            BitmapImage bi = new BitmapImage();
            await bi.SetSourceAsync(ir);
            img1.Source = bi;
            IRandomAccessStream ir2 = await files[random.Next(files.Count)].OpenAsync(FileAccessMode.Read);
            BitmapImage bi2 = new BitmapImage();
            await bi2.SetSourceAsync(ir2);
            img2.Source = bi2;
            IRandomAccessStream ir3 = await files[random.Next(files.Count)].OpenAsync(FileAccessMode.Read);
            BitmapImage bi3 = new BitmapImage();
            await bi3.SetSourceAsync(ir3);
            img3.Source = bi3;
            IRandomAccessStream ir4 = await files[random.Next(files.Count)].OpenAsync(FileAccessMode.Read);
            BitmapImage bi4 = new BitmapImage();
            await bi4.SetSourceAsync(ir4);
            img4.Source = bi4;
            IRandomAccessStream ir5 = await files[random.Next(files.Count)].OpenAsync(FileAccessMode.Read);
            BitmapImage bi5 = new BitmapImage();
            await bi5.SetSourceAsync(ir5);
            img5.Source = bi5;
            IRandomAccessStream ir6 = await files[random.Next(files.Count)].OpenAsync(FileAccessMode.Read);
            BitmapImage bi6 = new BitmapImage();
            await bi6.SetSourceAsync(ir6);
            img6.Source = bi6;
            IRandomAccessStream ir7 = await files[random.Next(files.Count)].OpenAsync(FileAccessMode.Read);
            BitmapImage bi7 = new BitmapImage();
            await bi7.SetSourceAsync(ir7);
            img7.Source = bi7;
            IRandomAccessStream ir8 = await files[random.Next(files.Count)].OpenAsync(FileAccessMode.Read);
            BitmapImage bi8 = new BitmapImage();
            await bi8.SetSourceAsync(ir8);
            img8.Source = bi8;
            IRandomAccessStream ir9 = await files[random.Next(files.Count)].OpenAsync(FileAccessMode.Read);
            BitmapImage bi9 = new BitmapImage();
            await bi9.SetSourceAsync(ir9);
            img9.Source = bi9;
        }
    }

}
