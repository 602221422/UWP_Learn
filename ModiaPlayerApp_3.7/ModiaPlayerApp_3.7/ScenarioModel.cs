using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ModiaPlayerApp_3._7
{
    public class ScenarioModel
    {
        public string Title { get; set; }
        public Type ClassType { get; set; }
        public static List<ScenarioModel> scenariosShow()
        {
            List<ScenarioModel> scenarios = new List<ScenarioModel>()
            {
                new ScenarioModel()
                {
                    Title="video",
                    ClassType=typeof(MainPage)
                },
                new ScenarioModel()
                {
                    Title="ModiaPlayer2",
                    ClassType=typeof(Modiaplayer2)
                },
                new ScenarioModel()
                {
                    Title="BreakMedia",
                    ClassType=typeof(breakschedule)
                },
                new ScenarioModel()
                {
                    Title="CopyAndPaste",
                    ClassType=typeof(CopyAndPaste)
                },
                new ScenarioModel()
                {
                    Title="StreamSocketAndListenerPage",
                    ClassType=typeof(StreamSocketAndListenerPage)
                },
            };
            return scenarios;
        }
    }
    public class ScenarioBindingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ScenarioModel s = value as ScenarioModel;
            return (Main.Current.Scenarios.IndexOf(s) + 1) + ")" + s.Title;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return true;
        }
    }
}
