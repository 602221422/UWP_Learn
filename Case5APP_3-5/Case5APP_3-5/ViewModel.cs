using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case5APP_3_5
{
    public class ViewModel
    {
        public string Description { get; set; }
    }
    public class ViewModelManager
    {
        public static ObservableCollection<ViewModel> GetViewModels()
        {
            var Manager = new ObservableCollection<ViewModel>{
                new ViewModel{ Description="aassssssssddsdsdksajdgsjgdwuqygdqwmsabchasjqgdjqdjkasbda"},
                new ViewModel{ Description="ddd"},
            };
            return Manager;
        }
    }
}
