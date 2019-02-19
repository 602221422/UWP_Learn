using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadSheetWorking.Model
{
    public class MemberInfo : ObservableObject
    {
        private string username;
        private string alias;
        private string wsalias;
        private string technology;
        private string group;
        private int vacationhour;
        public string UserName
        {
            get { return username; }
            set
            {
                username = value;
                RaisePropertyChanged("UserName");
            }
        }
        public string Alias
        {
            get { return alias; }
            set
            {
                alias = value;
                RaisePropertyChanged("Alias");
            }
        }
        public string WsAlias
        {
            get { return wsalias; }
            set
            {
                wsalias = value;
                RaisePropertyChanged("WsAlias");
            }
        }
        public string Technology
        {
            get { return technology; }
            set
            {
                technology = value;
                RaisePropertyChanged("Technology");
            }
        }
        public string Group
        {
            get { return group; }
            set
            {
                group = value;
                RaisePropertyChanged("Group");
            }
        }

        public int VacationHour
        {
            get { return vacationhour; }
            set
            {
                vacationhour = value;
                RaisePropertyChanged("VacationHour");
            }
        }

        public void emptymem()
        {
            username = null;
            alias = null;
            wsalias = null;
            technology = null;
            group = null;
            vacationhour = 0;
        }
    }
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
