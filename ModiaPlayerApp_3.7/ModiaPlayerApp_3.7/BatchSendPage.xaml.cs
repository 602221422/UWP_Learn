using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
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
    public sealed partial class BatchSendPage : Page
    {
        public BatchSendPage()
        {
            this.InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var streamSocketListener = new Windows.Networking.Sockets.StreamSocketListener();
            streamSocketListener.ConnectionReceived += this.StreamSocketListener_ConnectionReceived;
            await streamSocketListener.BindServiceNameAsync("1337");

            var streamSocket = new Windows.Networking.Sockets.StreamSocket();
            await streamSocket.ConnectAsync(new Windows.Networking.HostName("localhost"), "1337");
            this.SendMultipleBuffersInefficiently(streamSocket, "Hello, World!");
            //this.BatchedSendsCSharpOnly(streamSocket, "Hello, World!");
            //this.BatchedSendsAnyUWPLanguage(streamSocket, "Hello, World!");
        }

        private async void StreamSocketListener_ConnectionReceived(Windows.Networking.Sockets.StreamSocketListener sender, Windows.Networking.Sockets.StreamSocketListenerConnectionReceivedEventArgs args)
        {
            using (var dataReader = new DataReader(args.Socket.InputStream))
            {
                dataReader.InputStreamOptions = InputStreamOptions.Partial;
                while (true)
                {
                    await dataReader.LoadAsync(256);
                    if (dataReader.UnconsumedBufferLength == 0) break;
                    IBuffer requestBuffer = dataReader.ReadBuffer(dataReader.UnconsumedBufferLength);
                    string request = Windows.Security.Cryptography.CryptographicBuffer.ConvertBinaryToString(Windows.Security.Cryptography.BinaryStringEncoding.Utf8, requestBuffer);
                    Debug.WriteLine(string.Format("server received the request: \"{0}\"", request));
                }
            }
        }

        // This implementation incurs kernel transition overhead for each packet written.
        private async void SendMultipleBuffersInefficiently(Windows.Networking.Sockets.StreamSocket streamSocket, string message)
        {
            var packetsToSend = new List<IBuffer>();
            for (int count = 0; count < 5; ++count) { packetsToSend.Add(Windows.Security.Cryptography.CryptographicBuffer.ConvertStringToBinary(message, Windows.Security.Cryptography.BinaryStringEncoding.Utf8)); }

            foreach (IBuffer packet in packetsToSend)
            {
                await streamSocket.OutputStream.WriteAsync(packet);
            }
        }
        // A C#-only technique for batched sends.
        private async void BatchedSendsCSharpOnly(Windows.Networking.Sockets.StreamSocket streamSocket, string message)
        {
            var packetsToSend = new List<IBuffer>();
            for (int count = 0; count < 5; ++count) { packetsToSend.Add(Windows.Security.Cryptography.CryptographicBuffer.ConvertStringToBinary(message, Windows.Security.Cryptography.BinaryStringEncoding.Utf8)); }

            var pendingTasks = new System.Threading.Tasks.Task[packetsToSend.Count];

            for (int index = 0; index < packetsToSend.Count; ++index)
            {
                // track all pending writes as tasks, but don't wait on one before beginning the next.
                pendingTasks[index] = streamSocket.OutputStream.WriteAsync(packetsToSend[index]).AsTask();
                // Don't modify any buffer's contents until the pending writes are complete.
            }

            // Wait for all of the pending writes to complete.
            System.Threading.Tasks.Task.WaitAll(pendingTasks);
        }
    }
}
