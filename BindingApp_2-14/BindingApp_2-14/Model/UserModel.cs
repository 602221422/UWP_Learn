using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace BindingApp_2_14.Model
{
    public class UserModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Grade { get; set; }
        public StorageFile Image { get; set; }
    }
    public class UserManager
    {
         public static UserModel Getuser()
        {
            UserModel users = new UserModel
            {
                ID = 1,
                Grade = 66,
                Name = "AAA",
            };
            return users;
        }
        async public Task<StorageFile> GetFiles()
        {
            StorageFolder pictureFolder = KnownFolders.PicturesLibrary;
            IReadOnlyList<StorageFile> files = await pictureFolder.GetFilesAsync();
            return files[0];
        }
    }
    public class Getimage 
    {
         public object Convert(object value, Type targetType, object parameter, string language)
        {
            var image = new BitmapImage();
            SetImage(image, (StorageFile)value);
            return image;
        }
        async void SetImage(BitmapImage image, StorageFile file)
        {
            if (file == null) return;
            //var stream = await file.GetThumbnailAsync(ThumbnailMode.ListView);
            var stream = await file.OpenReadAsync();
            await image.SetSourceAsync(stream);
        }
    }
}
