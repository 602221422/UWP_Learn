using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Case9App_3_11
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Savefile();
        }

        /*async public void Saveimg()
        {
            WriteableBitmap writeableBitmap = imgCover.Source as WriteableBitmap;
            if (writeableBitmap == null) return;
            string filename =   "ass.jpg";
            StorageFolder fd = await KnownFolders.PicturesLibrary.CreateFolderAsync("AAA", CreationCollisionOption.OpenIfExists);
            StorageFile file = await fd.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                var encoder = await
                BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream);
                encoder.SetPixelData(BitmapPixelFormat.Bgra8,
                                        BitmapAlphaMode.Ignore,
                                        (uint)writeableBitmap.PixelWidth,
                                        (uint)writeableBitmap.PixelHeight,
                                        200,
                                        200,
                                        writeableBitmap.PixelBuffer.ToArray());

                await encoder.FlushAsync();
            }
        }*/
        private async void Savefile()
        {
            var picker = new FolderPicker();
            var pfolder = await picker.PickSingleFolderAsync();
            StorageApplicationPermissions.FutureAccessList.Add(pfolder);
            //StorageFolder folder = await KnownFolders.DocumentsLibrary.CreateFolderAsync("AAA", CreationCollisionOption.OpenIfExists);
            StorageFile sampleFile = await pfolder.CreateFileAsync("sample.txt",CreationCollisionOption.ReplaceExisting);
        }
    }
}
