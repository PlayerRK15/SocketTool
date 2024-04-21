using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SocketTool.View.TcpView;
using STTech.BytesIO.Tcp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SocketTool.ViewModel
{
    internal partial class TcpClientViewModel : ObservableObject, INotifyPropertyChanged
    {
        [ObservableProperty]
        private TcpClient? tcpClient;
        [ObservableProperty]
        private string? host = "127.0.0.1";
        [ObservableProperty]
        private int port = 6688;
        [ObservableProperty]
        private List<EncodingInfo> encodings = [.. Encoding.GetEncodings()];
        [ObservableProperty]
        private EncodingInfo? selectEncoding;
        public Dispatcher Dispatcher { get; }
        [ObservableProperty]
        private bool isConnect;
        [ObservableProperty]
        private string readMessage;
        [ObservableProperty]
        private string sendText;
        partial void OnIsConnectChanged(bool value)
        {
            if (value == true)
            {
                if (TcpClient != null)
                {
                    TcpClient.Disconnect();
                    TcpClient.Dispose();
                }
                TcpClient = new TcpClient();
                TcpClient.Host = $"{Host}";
                TcpClient.Port = Port;
                var result = TcpClient.Connect();
                TcpClient.OnDataReceived += TcpClient_OnDataReceived;
            }
            else
            {
                if (TcpClient != null && TcpClient.IsConnected)
                {
                    TcpClient.Disconnect();
                    TcpClient.Dispose();
                }
            }
        }
        private int _readCount;
        partial void OnReadMessageChanged(string value)
        {
            _readCount++;
            if (_readCount >= 100)
            {
                ReadMessage = "";
            }
        }
        private void TcpClient_OnDataReceived(object? sender, STTech.BytesIO.Core.DataReceivedEventArgs e)
        {
            var msg = SelectEncoding?.GetEncoding().GetString(e.Data);
            Dispatcher.Invoke(() =>
            {
                ReadMessage += $"收:{msg}\r\n";
            });
        }
        [RelayCommand]
        public void SendMessage()
        {
            TcpClient?.Send(SelectEncoding?.GetEncoding().GetBytes(SendText));
            ReadMessage += $"发:{SendText}\r\n";
        }
        [RelayCommand]
        public void StartConnect() => IsConnect = true;
        [RelayCommand]
        public void StopConnect() => IsConnect = false;
        public TcpClientViewModel(Dispatcher dispatcher)
        {
            SelectEncoding = Encodings[^1];
            Dispatcher = dispatcher;
        }
    }
}
