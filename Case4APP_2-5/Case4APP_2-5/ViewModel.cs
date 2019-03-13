﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case4APP_2_5
{
    public class ViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ViewModel()
        {
            this.Id = 1;
            this.Name = "sss";
        }
      
    }
    public class ViewModelManager
    {
        public static ObservableCollection<ViewModel> GetViewModels()
        {
            var Manager = new ObservableCollection<ViewModel>{
                new ViewModel{ Id=1,Name="aaa"},
                new ViewModel{ Id=2,Name="ddd"},
            };
            return Manager;
        }
    }
}
