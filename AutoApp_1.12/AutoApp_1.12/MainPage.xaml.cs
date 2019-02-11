using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
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

namespace AutoApp_1._12
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        
        async internal void DisplayImages(Windows.Storage.StorageFolder rootFolder)
        {
            // Display images from first folder in root\DCIM.
            var dcimFolder = await rootFolder.GetFolderAsync("DCIM");
            var folderList = await dcimFolder.GetFoldersAsync();
            var cameraFolder = folderList[0];
            var fileList = await cameraFolder.GetFilesAsync();
            for (int i = 0; i < fileList.Count; i++)
            {
                var file = (Windows.Storage.StorageFile)fileList[i];
                WriteMessageText(file.Name + "\n");
                DisplayImage(file, i);
            }
        }

        async private void DisplayImage(Windows.Storage.IStorageItem file, int index)
        {
            try
            {
                var sFile = (Windows.Storage.StorageFile)file;
                Windows.Storage.Streams.IRandomAccessStream imageStream =
                    await sFile.OpenAsync(Windows.Storage.FileAccessMode.Read);
                Windows.UI.Xaml.Media.Imaging.BitmapImage imageBitmap =
                    new Windows.UI.Xaml.Media.Imaging.BitmapImage();
                imageBitmap.SetSource(imageStream);
                var element = new Image();
                element.Source = imageBitmap;
                element.Height = 100;
                Thickness margin = new Thickness();
                margin.Top = index * 100;
                element.Margin = margin;
                filescanvas.Children.Add(element);
            }
            catch (Exception e)
            {
                WriteMessageText(e.Message + "\n");
            }
        }

        // Write a message to MessageBlock on the UI thread.
        private Windows.UI.Core.CoreDispatcher messageDispatcher = Window.Current.CoreWindow.Dispatcher;

        private async void WriteMessageText(string message, bool overwrite = false)
        {
            await messageDispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                    if (overwrite)
                        filesblock.Text = message;
                    else
                        filesblock.Text += message;
                });
        }
        async internal void CopyImages(Windows.Storage.StorageFolder rootFolder)
        {
            // Copy images from first folder in root\DCIM.
            var dcimFolder = await rootFolder.GetFolderAsync("DCIM");
            var folderList = await dcimFolder.GetFoldersAsync();
            var cameraFolder = folderList[0];
            var fileList = await cameraFolder.GetFilesAsync();

            try
            {
                var folderName = "Images " + DateTime.Now.ToString("yyyy-MM-dd HHmmss");
                Windows.Storage.StorageFolder imageFolder = await
                    Windows.Storage.KnownFolders.PicturesLibrary.CreateFolderAsync(folderName);

                foreach (Windows.Storage.IStorageItem file in fileList)
                {
                    CopyImage(file, imageFolder);
                }
            }
            catch (Exception e)
            {
                WriteMessageText("Failed to copy images.\n" + e.Message + "\n");
            }
        }

        async internal void CopyImage(Windows.Storage.IStorageItem file,
                                      Windows.Storage.StorageFolder imageFolder)
        {
            try
            {
                Windows.Storage.StorageFile sFile = (Windows.Storage.StorageFile)file;
                await sFile.CopyAsync(imageFolder, sFile.Name);
                WriteMessageText(sFile.Name + " copied.\n");
            }
            catch (Exception e)
            {
                WriteMessageText("Failed to copy file.\n" + e.Message + "\n");
            }
        }
    }
}
