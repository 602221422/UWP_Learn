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
    public sealed partial class MessageWebSocket : Page
    {
        public MessageWebSocket()
        {
            this.InitializeComponent();
        }
        private Windows.Networking.Sockets.MessageWebSocket messageWebSocket;
        //使用了 WebSocket.org 回显服务器 — 将服务回显到向其发送任一消息的发送方。
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.messageWebSocket = new Windows.Networking.Sockets.MessageWebSocket();

            // In this example, we send/receive a string, so we need to set the MessageType to Utf8.
            this.messageWebSocket.Control.MessageType = Windows.Networking.Sockets.SocketMessageType.Utf8;

            this.messageWebSocket.MessageReceived += WebSocket_MessageReceived;
            this.messageWebSocket.Closed += WebSocket_Closed;

            try
            {
                Task connectTask = this.messageWebSocket.ConnectAsync(new Uri("wss://echo.websocket.org")).AsTask();
                connectTask.ContinueWith(_ => this.SendMessageUsingMessageWebSocketAsync("Hello, World!"));
            }
            catch (Exception ex)
            {
                Windows.Web.WebErrorStatus webErrorStatus = Windows.Networking.Sockets.WebSocketError.GetStatus(ex.GetBaseException().HResult);
                // Add additional code here to handle exceptions.
            }
        }

        private async Task SendMessageUsingMessageWebSocketAsync(string message)
        {
            using (var dataWriter = new DataWriter(this.messageWebSocket.OutputStream))
            {
                dataWriter.WriteString(message);
                await dataWriter.StoreAsync();
                dataWriter.DetachStream();
            }
            Debug.WriteLine("Sending message using MessageWebSocket: " + message);
        }

        private void WebSocket_MessageReceived(Windows.Networking.Sockets.MessageWebSocket sender, Windows.Networking.Sockets.MessageWebSocketMessageReceivedEventArgs args)
        {
            try
            {
                using (DataReader dataReader = args.GetDataReader())
                {
                    dataReader.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
                    string message = dataReader.ReadString(dataReader.UnconsumedBufferLength);
                    Debug.WriteLine("Message received from MessageWebSocket: " + message);
                    this.messageWebSocket.Dispose();
                }
            }
            catch (Exception ex)
            {
                Windows.Web.WebErrorStatus webErrorStatus = Windows.Networking.Sockets.WebSocketError.GetStatus(ex.GetBaseException().HResult);
                // Add additional code here to handle exceptions.
            }
        }

        private void WebSocket_Closed(Windows.Networking.Sockets.IWebSocket sender, Windows.Networking.Sockets.WebSocketClosedEventArgs args)
        {
            Debug.WriteLine("WebSocket_Closed; Code: " + args.Code + ", Reason: \"" + args.Reason + "\"");
            // Add additional code here to handle the WebSocket being closed.
        }
    }
}
