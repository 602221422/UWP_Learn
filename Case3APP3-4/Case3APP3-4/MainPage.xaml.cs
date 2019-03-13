using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;
// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace Case3APP3_4
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ObservableCollection<ViewModel> viewModels = ViewModelManager.GetViewModels();
        public MainPage()
        {
            this.InitializeComponent();
            dataGrid.DataContext = viewModels;
        }

        private void DataGrid_CellEditEnding(object sender, Microsoft.Toolkit.Uwp.UI.Controls.DataGridCellEditEndingEventArgs e)
        {
            string newValue = (e.EditingElement as TextBox).Text;
            if (startValue != newValue)
            {
                show.Text = startValue + "change" + newValue;
            }
            else
            {
                show.Text = "NO";
            }
        }
        private string startValue = string.Empty;
        private void DataGrid_BeginningEdit(object sender, Microsoft.Toolkit.Uwp.UI.Controls.DataGridBeginningEditEventArgs e)
        {
            startValue = (this.dataGrid.SelectedItem as ViewModel).Name;
        }
    }
}
