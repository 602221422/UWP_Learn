using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace PictureAPP_1._30
{
    public class PictureView
    {
        public string Path { get; set; }
        public BitmapImage bi { get; set; }
    }
    public class PictureList 
    {
        async public Task<BitmapImage> GetBitmapImage()
        {
            StorageFolder pictureFolder = KnownFolders.PicturesLibrary;
            StringBuilder outputText = new StringBuilder();
            IReadOnlyList<StorageFile> files = await pictureFolder.GetFilesAsync();
            Random random = new Random();
            IRandomAccessStream ir = await files[random.Next(files.Count)].OpenAsync(FileAccessMode.Read);
            BitmapImage bi = new BitmapImage();
            await bi.SetSourceAsync(ir);
            return bi;
        }
    }
}
