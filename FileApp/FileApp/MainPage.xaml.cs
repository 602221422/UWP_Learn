using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Search;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Security.Cryptography;
namespace FileApp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            //readpicture();
            //readpicture2();
            //selectfilesbymonth();
            //savefile();
            //createfile();
            //writefile();
            //writefilebybuffer();
            writefilebystream();
        }
        //显示本地图片库的图片和子文件夹名
        async public void readpicture()
        {
            StorageFolder pictureFolder = KnownFolders.PicturesLibrary;
            StringBuilder outputText = new StringBuilder();
            IReadOnlyList<StorageFile> fileList = await pictureFolder.GetFilesAsync();
            outputText.AppendLine("Files:");
            foreach(StorageFile file in fileList)
            {
                outputText.Append(file.Name + "\n");
            }
            IReadOnlyList<StorageFolder> folderList = await pictureFolder.GetFoldersAsync();
            outputText.AppendLine("Folders:");
            foreach(StorageFolder folder in folderList)
            {
                outputText.Append(folder.Name + "\n");
            }
            outputtextblock.Text = outputText.ToString();
        }
        //显示本地图片库的图片和子文件夹名，若为子文件夹，加folder
        async public void readpicture2()
        {
            StorageFolder pictureFolder2 = KnownFolders.PicturesLibrary;
            StringBuilder outputText2 = new StringBuilder();
            IReadOnlyList<IStorageItem> itemList = await pictureFolder2.GetItemsAsync();
            foreach(var item in itemList)
            {
                if(item is StorageFolder)
                {
                    outputText2.Append(item.Name + "folder\n");
                }
                else
                {
                    outputText2.Append(item.Name + "\n");
                }
            }
            outputtextblock2.Text = outputText2.ToString();
        }
        //按月分组，每个虚拟文件夹都表示一组具有相同月份的文件。
        async public void selectfilesbymonth()
        {
            StorageFolder sf = KnownFolders.PicturesLibrary;
            StorageFolderQueryResult queryResult = sf.CreateFolderQuery(CommonFolderQuery.GroupByMonth);

            IReadOnlyList<StorageFolder> folderList = await queryResult.GetFoldersAsync();
            StringBuilder stringBuilder = new StringBuilder();
            foreach(StorageFolder folder in folderList)
            {
                IReadOnlyList<StorageFile> storageFiles = await folder.GetFilesAsync();
                stringBuilder.AppendLine(folder.Name + "(" + storageFiles.Count + ")");
                foreach(StorageFile file in storageFiles)
                {
                    stringBuilder.AppendLine("  " + file.Name);
                }
            }
            outputtextblock3.Text = stringBuilder.ToString();
        }
        //使用选取器保存文件并显示FileSavePicker，将其保存到已选取的文件
        //该示例检查了文件是否有效并将其自己的文件名写入其中
        async public void savefile()
        {
            var savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("PlainText", new List<string>() { ".txt" });
            savePicker.SuggestedFileName = "NewDocument";
            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                //在完成更改并调用CompleteUpdatesAsync之前，防止对文件的远程版本进行更新。
                CachedFileManager.DeferUpdates(file);
                //写入文件
                await FileIO.WriteTextAsync(file, file.Name);
                //让Windows知道我们已经完成了文件的更改，
                //这样其他应用程序就可以更新文件的远程版本。
                //完成更新可能需要Windows请求用户输入
                Windows.Storage.Provider.FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);
                if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
                {
                    outputtextblock4.Text = "file" + file.Name + " was saved.";
                }
                else
                {
                    outputtextblock4.Text = "file" + file.Name + " couldn't be saved.";
                }
            }
            else
            {
                outputtextblock4.Text = "Operation cancelled.";
            }
        }
        async public void createfile()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            //C:\Users\minfyl\AppData\Local\Packages\00c5388f-d771-482f-b8d5-1f0b6bedb627_0a6a7zspvpf88\LocalState
            StorageFile storageFile = await storageFolder.CreateFileAsync("test.txt", CreationCollisionOption.ReplaceExisting);
            outputtextblock3.Text = "创建文件";
        }
        //调用FileIO.WriteTextAsync方法向文件写入文本（1步）
        async public void writefile()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile storageFile = await storageFolder.GetFileAsync("test.txt");
            await FileIO.WriteTextAsync(storageFile, "xxxxxxxxxxx");
            outputtextblock4.Text += "调用FileIO.WriteTextAsync方法向文件写入文本" + "\n";
        }
        /*使用缓冲区将字节写入文件（2步）
        1.调用CryptographicBuffer.ConvertStringToBinary获取缓冲区字节
        2.调用FileIO.WriteBufferAsync将字节写入缓冲区*/
        async public void writefilebybuffer()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile storageFile = await storageFolder.GetFileAsync("test.txt");
            var buffer = CryptographicBuffer.ConvertStringToBinary("asasaadwqewqewqe", BinaryStringEncoding.Utf8);
            await FileIO.WriteBufferAsync(storageFile, buffer);
            outputtextblock4.Text += "使用缓冲区将字节写入文件" + "\n";
        }
        /*使用流将文本写入文件（4步）
        1.调用StorageFile.OpenAsync方法打开文件，返回文件的内容流
        2.调用IRandomAccessStream.GetOutputStreamAt获取输出流
        3.创建新的DateWriter对象并调用DataWriter.WriteString方法写入输出流
        4.将文本保存到文件与DataWriter.StoreAsync并关闭IOutputSream.FlushAsync与流*/
        async public void writefilebystream()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile storageFile = await storageFolder.GetFileAsync("test.txt");
            var stream = await storageFile.OpenAsync(FileAccessMode.ReadWrite);
            using (var outputstream = stream.GetOutputStreamAt(0))
            {
                using (var datawriter = new Windows.Storage.Streams.DataWriter(outputstream))
                {
                    datawriter.WriteString("xhhffhfqhf");
                    await datawriter.StoreAsync();
                    await outputstream.FlushAsync();
                }
            }
            stream.Dispose();
            outputtextblock4.Text += "使用流将文本写入文件" + "\n";
        }
    }
}
