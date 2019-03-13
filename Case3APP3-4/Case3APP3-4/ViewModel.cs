using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Case3APP3_4
{
    public class ViewModel
    {
        public int Id{ get;set;}
        public string Name{get;set;}
        public string Sex{get;set;}
        public static ViewModel GetViewModel(int id, string name, string sex)
        {
            ViewModel viewModel = new ViewModel() {
                Id = id,
                Name = name,
                Sex = sex,
            };
            return viewModel;
        }
    }
    public class ViewModelManager
    {
        public static ObservableCollection<ViewModel> GetViewModels()
        {
            var Manager = new ObservableCollection<ViewModel>{
                new ViewModel{ Id=1,Name="aaa",Sex="girl" },
                new ViewModel{ Id=2,Name="ddd",Sex="girl" },
            };
            return Manager;
        }
    }
}
