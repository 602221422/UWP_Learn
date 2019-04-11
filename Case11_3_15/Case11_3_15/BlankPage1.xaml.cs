using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Case11_3_15
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        StorageFile inputFile, outputFile;
        public BlankPage1()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;

            // 产生密钥
            SymmetricKeyAlgorithmProvider syprd = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.DesCbcPkcs7);
            IBuffer keybuffer = keyBtArray.AsBuffer();
            myKey = syprd.CreateSymmetricKey(keybuffer);
            System.Diagnostics.Debug.WriteLine("块大小：" + syprd.BlockLength);
        }
        #region 变量
        // 表示Key的字节数组
        byte[] keyBtArray = { 1, 2, 3, 4, 5, 6, 7, 8 };
        // 表示初始向量（iv）的字节数组
        byte[] ivBtArray = { 1, 2, 3, 4, 5, 6, 7, 8 };
        // 表示加/解密的密钥的对象
        CryptographicKey myKey = null;
        // 表示加密后的缓冲区对象
        IBuffer cryptBuffer = null;
        #endregion

        private async void OnEncryptClick(object sender, RoutedEventArgs e)
        {
            FileOpenPicker opPicker = new FileOpenPicker();
            opPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            opPicker.FileTypeFilter.Add(".txt");
            opPicker.FileTypeFilter.Add(".data");
            this.inputFile = await opPicker.PickSingleFileAsync();
            /*// 将输入的文本转换为字节缓冲区
            IBuffer txtBuffer = CryptographicBuffer.ConvertStringToBinary(txtInput.Text, BinaryStringEncoding.Utf8);*/
            IBuffer buffer = await FileIO.ReadBufferAsync(inputFile);
            Button b = sender as Button;
            b.IsEnabled = false;
            // 进行加密
            this.cryptBuffer = CryptographicEngine.Encrypt(myKey, buffer, ivBtArray.AsBuffer());
            Windows.UI.Popups.MessageDialog msgdlg = new Windows.UI.Popups.MessageDialog("加密完成。");
            await msgdlg.ShowAsync();
            b.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void OnDecryptoClick(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            b.IsEnabled = false;
            // 解密
            IBuffer decryptBuffer = CryptographicEngine.Decrypt(myKey, this.cryptBuffer, ivBtArray.AsBuffer());
            tbDecrypto.Text = CryptographicBuffer.ConvertBinaryToString(BinaryStringEncoding.Utf8, decryptBuffer);
            Windows.UI.Popups.MessageDialog msgbox = new Windows.UI.Popups.MessageDialog("解密完成。");
            await msgbox.ShowAsync();
            b.IsEnabled = true;
        }
    }
}
