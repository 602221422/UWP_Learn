using Windows.UI.Xaml.Controls;
using Windows.Networking;
using Windows.Networking.Sockets;
using System;
using Windows.Storage.Streams;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ModiaPlayerApp_3._7
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConnectSample : Page
    {
        StreamSocket clientSocket = new StreamSocket();
        public ConnectSample()
        {
            this.InitializeComponent();

        }
        async void connect()
        {

            HostName serverHost = new HostName("www.contoso.com");
            string serverServiceName = "http";

            // For simplicity, the sample omits implementation of the
            // NotifyUser method used to display status and error messages 

            // Try to connect to contoso using HTTP (port 80)
            try
            {
                // Call ConnectAsync method with a plain socket
                await clientSocket.ConnectAsync(serverHost, serverServiceName, SocketProtectionLevel.PlainSocket);

                show.Text="Connected";

            }
            catch (Exception exception)
            {
                // If this is an unknown status it means that the error is fatal and retry will likely fail.
                if (SocketError.GetStatus(exception.HResult) == SocketErrorStatus.Unknown)
                {
                    throw;
                }
                show.Text="Connect failed with error: " + exception.Message;
                // Could retry the connection, but for this simple example
                // just close the socket.

                clientSocket.Dispose();
                clientSocket = null;
                return;
            }

            // Now try to send some data
            DataWriter writer = new DataWriter(clientSocket.OutputStream);
            string hello = "Hello, World! ☺ ";
            Int32 len = (int)writer.MeasureString(hello); // Gets the UTF-8 string length.
            writer.WriteInt32(len);
            writer.WriteString(hello);
            show.Text = "Client: sending hello";
            try
            {
                // Call StoreAsync method to store the hello message
                await writer.StoreAsync();
                show.Text = "Client: sent data";

                writer.DetachStream(); // Detach stream, if not, DataWriter destructor will close it.
            }
            catch (Exception exception)
            {
                show.Text = "Store failed with error: " + exception.Message;
                // Could retry the store, but for this simple example
                // just close the socket.

                clientSocket.Dispose();
                clientSocket = null;
                return;
            }

            // Now upgrade the client to use SSL
            try
            {
                // Try to upgrade to SSL
                await clientSocket.UpgradeToSslAsync(SocketProtectionLevel.Ssl, serverHost);
                show.Text = "Client: upgrade to SSL completed";

                // Add code to send and receive data 
                // The close clientSocket when done
            }
            catch (Exception exception)
            {
                // If this is an unknown status it means that the error is fatal and retry will likely fail.
                if (SocketError.GetStatus(exception.HResult) == SocketErrorStatus.Unknown)
                {
                    throw;
                }
                show.Text = "Upgrade to SSL failed with error: " + exception.Message;

                clientSocket.Dispose();
                clientSocket = null;
                return;
            }
        }
    }
}
