using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Case11_3_15
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        StorageFile inputFile, outputFile;
        public MainPage()
        {
            this.InitializeComponent();
        }
        private async void onInputFile(object sender, RoutedEventArgs e)
        {
            FileOpenPicker opPicker = new FileOpenPicker();
            opPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            opPicker.FileTypeFilter.Add(".txt");
            opPicker.FileTypeFilter.Add(".data");
            this.inputFile = await opPicker.PickSingleFileAsync();
            Button btn = sender as Button;
            if (btn != null && inputFile != null)
            {
                btn.Content = inputFile.Path;
            }
        }
        private void Show()
        {

        }
        private async void onOutputFile(object sender, RoutedEventArgs e)
        {
            FileSavePicker fsPicker = new FileSavePicker();
            fsPicker.FileTypeChoices.Add("加密文件", new string[] { ".data" });
            fsPicker.FileTypeChoices.Add("文件文件", new string[] { ".txt" });
            fsPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            this.outputFile = await fsPicker.PickSaveFileAsync();
            Button btn = sender as Button;
            if (btn != null && outputFile != null)
            {
                btn.Content = outputFile.Path;
            }
        }
        private async void onProtect(object sender, RoutedEventArgs e)
        {
            if (inputFile == null || outputFile == null) return; IRandomAccessStream inputstr = await inputFile.OpenAsync(FileAccessMode.Read);
            IRandomAccessStream outputstr = await outputFile.OpenAsync(FileAccessMode.ReadWrite); DataProtectionProvider dp = new DataProtectionProvider("LOCAL=user");
            await dp.ProtectStreamAsync(inputstr, outputstr);
            this.msgLabel.Text = "完成数据加密。"; inputFile = null;
            outputFile = null;
            ClearDisplay();
        }
        private async void onUnProtect(object sender, RoutedEventArgs e)
        {
            if (inputFile == null || outputFile == null)
            {
                return;
            }
            IRandomAccessStream inputstr = await inputFile.OpenAsync(FileAccessMode.Read);
            IRandomAccessStream outputstr = await outputFile.OpenAsync(FileAccessMode.ReadWrite); DataProtectionProvider dp = new DataProtectionProvider();
            await dp.UnprotectStreamAsync(inputstr, outputstr);
            this.msgLabel.Text = "解密数据完成。"; inputFile = null;
            outputFile = null;
            ClearDisplay();
        }
        private void ClearDisplay()
        {
            this.btnPickInputFile.Content = "输入文件...";
            this.btnPickOutputFile.Content = "输出文件...";
            //this.msgLabel.Text = string.Empty;
        }
    }
}
