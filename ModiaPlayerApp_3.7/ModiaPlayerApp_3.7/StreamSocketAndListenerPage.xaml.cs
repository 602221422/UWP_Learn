using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Sockets;
using Windows.UI.Core;
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
    public sealed partial class StreamSocketAndListenerPage : Page
    {
        static string PortNumber = "1337";
        public StreamSocketAndListenerPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.StartServer();
            this.StartClient();
        }
        private async void StartServer()
        {
            try
            {
                var streamSocketListener = new StreamSocketListener();
                //接收到连接时将引发ConnectionReceived事件。
                streamSocketListener.ConnectionReceived += this.StreamSocketListener_ConnectionReceived;
                ////开始监听指定端口上传入的TCP连接。您可以指定当前未使用的任何端口。
                await streamSocketListener.BindServiceNameAsync(StreamSocketAndListenerPage.PortNumber);
                this.serverListBox.Items.Add("server is listening...");

            }
            catch (Exception ex)
            {
                SocketErrorStatus webErrorStatus = SocketError.GetStatus(ex.GetBaseException().HResult);
                this.serverListBox.Items.Add(webErrorStatus.ToString() != "Unknown" ? webErrorStatus.ToString() : ex.Message);
            }
        }

        private async void StreamSocketListener_ConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            string request;
            using (var streamReader = new StreamReader(args.Socket.InputStream.AsStreamForRead()))
            {
                request = await streamReader.ReadLineAsync();
            }

            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => this.serverListBox.Items.Add(string.Format("server received the request: \"{0}\"", request)));

            // 将请求回显为响应。
            using (Stream outputStream = args.Socket.OutputStream.AsStreamForWrite())
            {
                using (var streamWriter = new StreamWriter(outputStream))
                {
                    await streamWriter.WriteLineAsync(request);
                    await streamWriter.FlushAsync();
                }
            }

            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => this.serverListBox.Items.Add(string.Format("server sent back the response: \"{0}\"", request)));

            sender.Dispose();

            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => this.serverListBox.Items.Add("server closed its socket"));
        }
        private async void StartClient()
        {
            try
            {
                // Create the StreamSocket and establish a connection to the echo server.
                using (var streamSocket = new Windows.Networking.Sockets.StreamSocket())
                {
                    // The server hostname that we will be establishing a connection to. In this example, the server and client are in the same process.
                    var hostName = new Windows.Networking.HostName("localhost");

                    this.clientListBox.Items.Add("client is trying to connect...");

                    await streamSocket.ConnectAsync(hostName, StreamSocketAndListenerPage.PortNumber);

                    this.clientListBox.Items.Add("client connected");

                    // Send a request to the echo server.
                    string request = "Hello, World!";
                    using (Stream outputStream = streamSocket.OutputStream.AsStreamForWrite())
                    {
                        using (var streamWriter = new StreamWriter(outputStream))
                        {
                            await streamWriter.WriteLineAsync(request);
                            await streamWriter.FlushAsync();
                        }
                    }

                    this.clientListBox.Items.Add(string.Format("client sent the request: \"{0}\"", request));

                    // Read data from the echo server.
                    string response;
                    using (Stream inputStream = streamSocket.InputStream.AsStreamForRead())
                    {
                        using (StreamReader streamReader = new StreamReader(inputStream))
                        {
                            response = await streamReader.ReadLineAsync();
                        }
                    }

                    this.clientListBox.Items.Add(string.Format("client received the response: \"{0}\" ", response));
                }

                this.clientListBox.Items.Add("client closed its socket");
            }
            catch (Exception ex)
            {
                Windows.Networking.Sockets.SocketErrorStatus webErrorStatus = Windows.Networking.Sockets.SocketError.GetStatus(ex.GetBaseException().HResult);
                this.clientListBox.Items.Add(webErrorStatus.ToString() != "Unknown" ? webErrorStatus.ToString() : ex.Message);
            }
        }
    }
}
