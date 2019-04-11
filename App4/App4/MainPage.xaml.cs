using SpreadSheetWorking.Model;
using SpreadSheetWorking.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<MemberInfo> basiclist;
        private ObservableCollection<MemberInfo> mymemlist;
        public MainPage()
        {
            this.InitializeComponent();
            DaysOffListViewModel();
            SongLibraryList.ItemsSource = basiclist;
        }
        public void DaysOffListViewModel()
        {
            //测数据
            ObservableCollection<MemberInfo> members = new ObservableCollection<MemberInfo>();
            for (int i = 0; i < 10; i++)
            {
                var memberinfo = new MemberInfo();
                memberinfo.Userpicture = "http://pic38.nipic.com/20140121/14882888_203648493000_2.jpg";
                memberinfo.UserName = "哈哈哈" + i;
                memberinfo.Alias = "hh" + i;
                memberinfo.WsAlias = "ass" + i;
                memberinfo.Technology = "blay" + i;
                memberinfo.Group = "alisa" + i;
                memberinfo.VacationHour = i;
                members.Add(memberinfo);
            }
            //basiclist = SqlDBHelper.CommonMemList;
            basiclist = members;
            mymemlist = new ObservableCollection<MemberInfo>();
            foreach (var item in basiclist)
            {
                mymemlist.Add(item);
            }
        }
    }
}
