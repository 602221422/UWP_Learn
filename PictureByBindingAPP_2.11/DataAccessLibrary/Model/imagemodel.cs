using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary;
using System.Collections.ObjectModel;
using System.ComponentModel;

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
        public static ObservableCollection<Imagemodel> getimages()
        {
            var images = new ObservableCollection <Imagemodel>
            {
                new Imagemodel { imageId = 1, Title = "AAA1", Author = "aaa", CoverImage = "http://pic1.win4000.com/wallpaper/3/53b7b2af00593.jpg" },
                new Imagemodel { imageId = 2, Title = "BBB1", Author = "bbb", CoverImage = "http://pic34.photophoto.cn/20150117/0005018381613004_b.jpg" },
                new Imagemodel { imageId = 3, Title = "CCC1", Author = "ccc", CoverImage = "http://gss0.baidu.com/-vo3dSag_xI4khGko9WTAnF6hhy/zhidao/pic/item/0ff41bd5ad6eddc4140b13fc39dbb6fd536633cc.jpg" },
                new Imagemodel { imageId = 4, Title = "DDD1", Author = "ddd", CoverImage = "http://b.hiphotos.baidu.com/zhidao/pic/item/1f178a82b9014a90b7aece55af773912b21bee42.jpg" },
                new Imagemodel { imageId = 5, Title = "EEE1", Author = "eee", CoverImage = "http://b-ssl.duitang.com/uploads/item/201505/02/20150502001030_jh4K5.jpeg" },
                new Imagemodel { imageId = 6, Title = "FFF1", Author = "fff", CoverImage = "http://old.bz55.com/uploads/allimg/140325/1-140325150158.jpg" }
            };
            return images;
        }
    }
}
