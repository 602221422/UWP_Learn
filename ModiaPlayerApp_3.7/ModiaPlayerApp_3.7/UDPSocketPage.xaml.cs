using System;
using System.IO;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ModiaPlayerApp_3._7
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UDPSocketPage : Page
    {
        public UDPSocketPage()
        {
            this.InitializeComponent();
        }
        // Every protocol typically has a standard port number. For example, HTTP is typically 80, FTP is 20 and 21, etc.
        // For this example, we'll choose different arbitrary port numbers for client and server, since both will be running on the same machine.
        static string ClientPortNumber = "1336";
        static string ServerPortNumber = "1337";

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.StartServer();
            this.StartClient();
        }

        private async void StartServer()
        {
            try
            {
                var serverDatagramSocket = new Windows.Networking.Sockets.DatagramSocket();

                // The ConnectionReceived event is raised when connections are received.
                serverDatagramSocket.MessageReceived += ServerDatagramSocket_MessageReceived;

                this.serverListBox.Items.Add("server is about to bind...");

                // Start listening for incoming TCP connections on the specified port. You can specify any port that's not currently in use.
                await serverDatagramSocket.BindServiceNameAsync(UDPSocketPage.ServerPortNumber);

                this.serverListBox.Items.Add(string.Format("server is bound to port number {0}", UDPSocketPage.ServerPortNumber));
            }
            catch (Exception ex)
            {
                Windows.Networking.Sockets.SocketErrorStatus webErrorStatus = Windows.Networking.Sockets.SocketError.GetStatus(ex.GetBaseException().HResult);
                this.serverListBox.Items.Add(webErrorStatus.ToString() != "Unknown" ? webErrorStatus.ToString() : ex.Message);
            }
        }

        private async void ServerDatagramSocket_MessageReceived(Windows.Networking.Sockets.DatagramSocket sender, Windows.Networking.Sockets.DatagramSocketMessageReceivedEventArgs args)
        {
            string request;
            using (DataReader dataReader = args.GetDataReader())
            {
                request = dataReader.ReadString(dataReader.UnconsumedBufferLength).Trim();
            }

            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => this.serverListBox.Items.Add(string.Format("server received the request: \"{0}\"", request)));

            // Echo the request back as the response.
            using (Stream outputStream = (await sender.GetOutputStreamAsync(args.RemoteAddress, UDPSocketPage.ClientPortNumber)).AsStreamForWrite())
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
                // Create the DatagramSocket and establish a connection to the echo server.
                var clientDatagramSocket = new Windows.Networking.Sockets.DatagramSocket();

                clientDatagramSocket.MessageReceived += ClientDatagramSocket_MessageReceived;

                // The server hostname that we will be establishing a connection to. In this example, the server and client are in the same process.
                var hostName = new Windows.Networking.HostName("localhost");

                this.clientListBox.Items.Add("client is about to bind...");

                await clientDatagramSocket.BindServiceNameAsync(UDPSocketPage.ClientPortNumber);

                this.clientListBox.Items.Add(string.Format("client is bound to port number {0}", UDPSocketPage.ClientPortNumber));

                // Send a request to the echo server.
                string request = "Hello, World!";
                using (var serverDatagramSocket = new Windows.Networking.Sockets.DatagramSocket())
                {
                    using (Stream outputStream = (await serverDatagramSocket.GetOutputStreamAsync(hostName, UDPSocketPage.ServerPortNumber)).AsStreamForWrite())
                    {
                        using (var streamWriter = new StreamWriter(outputStream))
                        {
                            await streamWriter.WriteLineAsync(request);
                            await streamWriter.FlushAsync();
                        }
                    }
                }

                this.clientListBox.Items.Add(string.Format("client sent the request: \"{0}\"", request));
            }
            catch (Exception ex)
            {
                Windows.Networking.Sockets.SocketErrorStatus webErrorStatus = Windows.Networking.Sockets.SocketError.GetStatus(ex.GetBaseException().HResult);
                this.clientListBox.Items.Add(webErrorStatus.ToString() != "Unknown" ? webErrorStatus.ToString() : ex.Message);
            }
        }

        private async void ClientDatagramSocket_MessageReceived(Windows.Networking.Sockets.DatagramSocket sender, Windows.Networking.Sockets.DatagramSocketMessageReceivedEventArgs args)
        {
            string response;
            using (DataReader dataReader = args.GetDataReader())
            {
                response = dataReader.ReadString(dataReader.UnconsumedBufferLength).Trim();
            }

            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => this.clientListBox.Items.Add(string.Format("client received the response: \"{0}\"", response)));

            sender.Dispose();

            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => this.clientListBox.Items.Add("client closed its socket"));
        }
    }
}
