using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ModiaPlayerApp_3._7
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CopyAndPaste : Page
    {
        DataPackage dataPackage = new DataPackage();

        public CopyAndPaste()
        {
            this.InitializeComponent();
            Copy();
            OutputClipboardText();
            //OutputChange();
        }
        public void Copy()
        {
            dataPackage.RequestedOperation = DataPackageOperation.Copy;
            dataPackage.RequestedOperation = DataPackageOperation.Move;
            dataPackage.SetText("AAAA");
            Clipboard.SetContent(dataPackage);
        }
        async void OutputClipboardText()
        {
            DataPackageView dataPackageView = Clipboard.GetContent();
            if (dataPackageView.Contains(StandardDataFormats.Text))
            {
                string text = await dataPackageView.GetTextAsync();
                outputtext.Text = "Clipboard now contains:" + text;
            }
        }
         void OutputChange()
        {
            Clipboard.ContentChanged += async (s, e) =>
            {
                DataPackageView dataPackageView = Clipboard.GetContent();
                if (dataPackageView.Contains(StandardDataFormats.Text))
                {
                    string text = await dataPackageView.GetTextAsync();
                    outputtext.Text = "Clipboard now contains:" + text;
                }
            };
        }
    }
}
