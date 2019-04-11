using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class StreamwebSocket : Page
    {
        public StreamwebSocket()
        {
            this.InitializeComponent();
        }
        private Windows.Networking.Sockets.StreamWebSocket streamWebSocket;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.streamWebSocket = new Windows.Networking.Sockets.StreamWebSocket();

            this.streamWebSocket.Closed += WebSocket_Closed;

            try
            {
                Task connectTask = this.streamWebSocket.ConnectAsync(new Uri("wss://echo.websocket.org")).AsTask();

                connectTask.ContinueWith(_ =>
                {
                    Task.Run(() => this.ReceiveMessageUsingStreamWebSocket());
                    Task.Run(() => this.SendMessageUsingStreamWebSocket(new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09 }));
                });
            }
            catch (Exception ex)
            {
                Windows.Web.WebErrorStatus webErrorStatus = Windows.Networking.Sockets.WebSocketError.GetStatus(ex.GetBaseException().HResult);
                // Add code here to handle exceptions.
            }
        }

        private async void ReceiveMessageUsingStreamWebSocket()
        {
            try
            {
                using (var dataReader = new DataReader(this.streamWebSocket.InputStream))
                {
                    dataReader.InputStreamOptions = InputStreamOptions.Partial;
                    await dataReader.LoadAsync(256);
                    byte[] message = new byte[dataReader.UnconsumedBufferLength];
                    dataReader.ReadBytes(message);
                    Debug.WriteLine("Data received from StreamWebSocket: " + message.Length + " bytes");
                }
                this.streamWebSocket.Dispose();
            }
            catch (Exception ex)
            {
                Windows.Web.WebErrorStatus webErrorStatus = Windows.Networking.Sockets.WebSocketError.GetStatus(ex.GetBaseException().HResult);
                // Add code here to handle exceptions.
            }
        }

        private async void SendMessageUsingStreamWebSocket(byte[] message)
        {
            try
            {
                using (var dataWriter = new DataWriter(this.streamWebSocket.OutputStream))
                {
                    dataWriter.WriteBytes(message);
                    await dataWriter.StoreAsync();
                    dataWriter.DetachStream();
                }
                Debug.WriteLine("Sending data using StreamWebSocket: " + message.Length.ToString() + " bytes");
            }
            catch (Exception ex)
            {
                Windows.Web.WebErrorStatus webErrorStatus = Windows.Networking.Sockets.WebSocketError.GetStatus(ex.GetBaseException().HResult);
                // Add code here to handle exceptions.
            }
        }

        private void WebSocket_Closed(Windows.Networking.Sockets.IWebSocket sender, Windows.Networking.Sockets.WebSocketClosedEventArgs args)
        {
            Debug.WriteLine("WebSocket_Closed; Code: " + args.Code + ", Reason: \"" + args.Reason + "\"");
            // Add additional code here to handle the WebSocket being closed.
        }
    }
}
