using System.Collections.ObjectModel;
using SpreadSheetWorking.Model;
using SpreadSheetWorking;
using System.IO;
using System;
using System.Diagnostics;

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
        async public void Readfile()
        {
            Stream finalstream = await SpreadsheetHelper.filepathhelper();
            try
            {
                SpreadsheetHelper.ReadDataFromExcel(finalstream, "Sheet1", "A2", "F29", members);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message.ToString());
            }
        }
    }
}
