using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;
namespace PictureByBindingAPP_2._11_Model
{
    public class Imagemodel
    {
        public int imageId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string CoverImage { get; set; }
    }
    public class ImageManager
    {
        public static List<Imagemodel> getimages()
        {
            var images = new List<Imagemodel>
            {
                new Imagemodel { imageId = 1, Title = "AAA", Author = "aaa", CoverImage = "Assets/P9.jpg" },
                new Imagemodel { imageId = 2, Title = "BBB", Author = "bbb", CoverImage = "Assets/P5.jpg" },
                new Imagemodel { imageId = 3, Title = "CCC", Author = "ccc", CoverImage = "Assets/P8.jpg" },
                new Imagemodel { imageId = 4, Title = "DDD", Author = "ddd", CoverImage = "Assets/P7.jpg" },
                new Imagemodel { imageId = 5, Title = "EEE", Author = "eee", CoverImage = "Assets/P6.jpg" },
                new Imagemodel { imageId = 6, Title = "FFF", Author = "fff", CoverImage = "Assets/P5.jpg" }
            };
            return images;
        }
    }
}
