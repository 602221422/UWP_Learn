using System.Collections.ObjectModel;
using SpreadSheetWorking.Model;
using SpreadSheetWorking;
using System.IO;
using System;
using System.Diagnostics;
using Windows.UI.Xaml;

namespace SpreadSheetWorking.ViewModel
{
    class MainPageViewModel:ObservableObject
    {
        private ObservableCollection<MemberInfo> members;
        public ObservableCollection<MemberInfo> Members
        {
            get { return members; }
            set
            {
                members = value;
                RaisePropertyChanged("Members");
            }
        }
        public MainPageViewModel()
        {
            Readfile();
        }

        /*private ObservableCollection<MemberInfo> GetMember()
        {
            return new ObservableCollection<MemberInfo>()
            {
                new MemberInfo()
                {
                    UserName="saaa",
                    Alias="aaa",
                    WsAlias="ddd",
                    Technology="ooo",
                    Group="ssss",
                    VacationHour=6,
                },
            };
        }*/

        async public void Readfile()
        {
            Stream finalstream = await SpreadsheetHelper.filepathhelper();
            try
            {
                members=SpreadsheetHelper.ReadDataFromExcel(finalstream, "Sheet1", "A2", "F29");
                this.Members = members;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message.ToString());
            }
        }
    }
}
