using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Review_allAPP.Model
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
                    Title="Share Text",
                    ClassType=typeof(ShareText)
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
            return (MainPage.Current.Scenarios.IndexOf(s) + 1) + ")" + s.Title;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return true;
        }
    }
}
