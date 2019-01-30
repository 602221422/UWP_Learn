using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace PictureAPP_1._30
{
    public sealed partial class First : Page
    {
        public First()
        {
            this.InitializeComponent();
            selectpicture();
        }

        private void Gobutton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
        async public void selectpicture()
        {
           StorageFolder pictureFolder = KnownFolders.PicturesLibrary;
            StringBuilder outputText = new StringBuilder();
            IReadOnlyList<StorageFile> files = await pictureFolder.GetFilesAsync();
            /*文件选取器
           FileOpenPicker openPicker = new FileOpenPicker();
           openPicker.ViewMode = PickerViewMode.Thumbnail;
           openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
           openPicker.FileTypeFilter.Add(".jpg");
           openPicker.FileTypeFilter.Add(".jpeg");
           openPicker.FileTypeFilter.Add(".png");
           //StorageFile file = await openPicker.PickSingleFileAsync();*/
            IRandomAccessStream ir = await files[1].OpenAsync(FileAccessMode.Read);
            BitmapImage bi = new BitmapImage();
            await bi.SetSourceAsync(ir);
            image1.Source = bi;
            IRandomAccessStream ir2 = await files[2].OpenAsync(FileAccessMode.Read);
            BitmapImage bi2 = new BitmapImage();
            await bi2.SetSourceAsync(ir2);
            image2.Source = bi2;
            IRandomAccessStream ir3 = await files[6].OpenAsync(FileAccessMode.Read);
            BitmapImage bi3 = new BitmapImage();
            await bi3.SetSourceAsync(ir3);
            image3.Source = bi3;
        }

        private void SplitButton_Click(object sender, RoutedEventArgs e)
        {
            splitview1.IsPaneOpen = !splitview1.IsPaneOpen;
        }
    }
}
