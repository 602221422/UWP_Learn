using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SpreadSheetWorking.Model
{
    public class MemberInfo:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void emptymem()
        {
            userpicture = null;
            username = null;
            alias = null;
            wsalias = null;
            technology = null;
            group = null;
            vacationhour = 0;
        }
        private string userpicture;

        public string Userpicture
        {
            get { return userpicture; }
            set
            {
                userpicture = value;
                OnPropertyChanged();
            }
        }

        private string username;

        public string UserName
        {
            get { return username; }
            set { username = value;
                OnPropertyChanged();
            }
        }

        private string alias;

        public string Alias
        {
            get { return alias; }
            set { alias = value;
                OnPropertyChanged();
            }
        }

        private string wsalias;

        public string WsAlias
        {
            get { return wsalias; }
            set { wsalias = value;
                OnPropertyChanged();
            }
        }

        private string technology;

        public string Technology
        {
            get { return technology; }
            set { technology = value;
                OnPropertyChanged();
            }
        }

        private string group;

        public string Group
        {
            get { return group; }
            set { group = value;
                OnPropertyChanged();
            }
        }

        private int vacationhour;

        public int VacationHour
        {
            get { return vacationhour; }
            set { vacationhour = value;
                OnPropertyChanged();
            }
        }

    }
}
