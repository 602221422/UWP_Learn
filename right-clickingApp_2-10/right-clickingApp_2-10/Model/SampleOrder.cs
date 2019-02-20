using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace right_clickingApp_2_10.Model
{
    public class SampleOrder
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string OrderId { get; set; }

        public static SampleOrder Clone(string name,string url,string orderId)
        {
            SampleOrder sampleOrders = new SampleOrder()
            {
                Name = name,
                Url = url,
                OrderId = orderId,
            };
            return sampleOrders;
        }
    }
    public class SampleManager
    {
        public static ObservableCollection<SampleOrder> GetSampleOrders()
        {
            var Samples = new ObservableCollection<SampleOrder>
            {
                new SampleOrder{Name = "lindexi",Url = "lindexi.gitee.io",OrderId="1"},
                new SampleOrder{Name = "ssss",Url = "ttttt",OrderId="2"}
            };
            return Samples;
        }
    }
}
 