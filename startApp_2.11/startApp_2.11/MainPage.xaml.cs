using Windows.ApplicationModel.Activation;
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

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace startApp_2._11
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Windows.System.ProtocolForResultsOperation _operation = null;
        public MainPage()
        {
            this.InitializeComponent();
            ValueSet result = new ValueSet();
            result["ReturnedData"] = "The returned result";
            _operation.ReportCompleted(result);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var protocolForResultsArgs = e.Parameter as ProtocolForResultsActivatedEventArgs;
            // Set the ProtocolForResultsOperation field.
            _operation = protocolForResultsArgs.ProtocolForResultsOperation;

            if (protocolForResultsArgs.Data.ContainsKey("TestData"))
            {
                string dataFromCaller = protocolForResultsArgs.Data["TestData"] as string;
            }
        }
        
        
    }
}
