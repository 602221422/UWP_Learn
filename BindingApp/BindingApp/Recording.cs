using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BindingApp
{
    public class Recording
    {
        public string ArtistName { get; set; }
        public string CompositionName { get; set; }
        public DateTime ReleaseDataTime { get; set; }
        public Recording()
        {
            this.ArtistName = "aaaaa";
            this.CompositionName = "bbbb";
            this.ReleaseDataTime = new DateTime(1761, 1, 1);
        }
        public string OneLineSummary
        {
            get
            {
                return $"{this.CompositionName} by {this.ArtistName},released:"
                    + this.ReleaseDataTime.ToString("d");
            }
        }
    }
    public class RecordingViewModel
    {
        private Recording defaultRecording = new Recording();
        public Recording DefaultRecording { get { return this.defaultRecording; } }
        private ObservableCollection<Recording> recordings = new ObservableCollection<Recording>();
        public ObservableCollection <Recording> Recordings { get { return this.recordings; } }
        public RecordingViewModel()
        {
            this.recordings.Add(new Recording()
            {
                ArtistName = "zxcccc",
                CompositionName = "aaaa",
                ReleaseDataTime = new DateTime(1748, 12, 1)
            });
            this.recordings.Add(new Recording()
            {
                ArtistName = "wergsfafsd",
                CompositionName = "wwww",
                ReleaseDataTime = new DateTime(1748, 8, 1)
            });
            this.recordings.Add(new Recording()
            {
                ArtistName = "ttttt",
                CompositionName = "ccccc",
                ReleaseDataTime = new DateTime(1777, 1, 1)
            });

        }
    }
    public class StringFormatter : Windows.UI.Xaml.Data.IValueConverter
    {
        public object Convert(object value,Type targetType, object paramter,string language)
        {
            string formatstring = paramter as string;
            if (!string.IsNullOrEmpty(formatstring))
            {
                return string.Format(formatstring, value);
            }
            return value.ToString();
        }
        public object ConvertBack(object value,Type targetType,object parameter,string language)
        {
            throw new NotImplementedException();
        }
    }
}
