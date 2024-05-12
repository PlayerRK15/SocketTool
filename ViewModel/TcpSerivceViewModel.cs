using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using STTech.BytesIO.Tcp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;

namespace SocketTool.ViewModel
{
    internal partial class TcpSerivceViewModel : ObservableObject, INotifyPropertyChanged
    {
        [ObservableProperty]
        private TcpServer? server;
        [ObservableProperty]
        private string? host = "127.0.0.1";
        [ObservableProperty]
        private int port = 6688;

        [ObservableProperty]
        private List<EncodingInfo> encodings = [.. Encoding.GetEncodings()];
        [ObservableProperty]
        private EncodingInfo? selectEncoding;
        [ObservableProperty]
        private int maxClientMsgLength = 50;
        [ObservableProperty]
        private List<string> messages;
        private readonly Queue<string> _msgstack = new();
        [ObservableProperty]
        private string sendText;
        [Obsolete]
        partial void OnIsListenChanged(bool value)
        {
            if (value)
            {
                Server = new TcpServer();
                Server.StopAsync();
                Server.Host = Host;
                Server.Port = Port;
                Server.MaxConnections = ListenLength;
                Server.StartAsync();
                var state = Server.State;
                //客户端连接
                Server.ClientConnected += (s, e) =>
                {
                    if (e.Client is TcpClient client)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            Clients.Add((client));
                        });
                        var ipPort = client.RemoteEndPoint.ToString();
                        if (!ClientMessages.ContainsKey(ipPort))
                        {
                            ClientMessages.Add(ipPort, new List<string>());
                        }
                        //接收客户端消息传入字典
                        client.OnDataReceived += OnClientReceived;
                    }
                };
                Server.ClientDisconnected += (s, e) =>
                {
                    if (e.Client is TcpClient client)
                        Dispatcher.Invoke(() =>
                        {
                            Clients.Remove((client));
                        });
                };
            }
            else
            {
                if (Server is not null)
                {
                    Server.CloseAsync();
                    Server.StopAsync();
                }
            }
        }
        private void OnClientReceived(object? s, STTech.BytesIO.Core.DataReceivedEventArgs e)
        {
            if (s is null)
            {
                return;
            }
            var ipPort = ((TcpClient)s).RemoteEndPoint.ToString();
            if (SelectEncoding?.GetEncoding()?.GetString(e.Data) is string msg)
            {
                var msglist = ClientMessages[ipPort];
                if (msglist.Count > MaxClientMsgLength)
                {
                    msglist.RemoveRange(0, msglist.Count - MaxClientMsgLength);
                }
                Dispatcher.BeginInvoke(() =>
                {
                    while (_msgstack?.Count > MaxClientMsgLength)
                    {
                        _msgstack.Dequeue();
                    }
                });
                msg = $"{DateTime.Now:yyyy-MM-dd hh:mm:dd:fff} 接收:{ipPort}\r\n{msg}\r\n";
                msglist.Add(msg);
                Dispatcher.BeginInvoke(() =>
                {
                    _msgstack?.Enqueue(msg);
#pragma warning disable CS8601 // 引用类型赋值可能为 null。
                    Messages = _msgstack?.ToList();
#pragma warning restore CS8601 // 引用类型赋值可能为 null。
                });
            }
        }
        [ObservableProperty]
        private bool isListen;
        [ObservableProperty]
        private uint listenLength = 10;
        [RelayCommand]
        public void StartListen() => IsListen = true;
        [RelayCommand]
        public void StopListen() => IsListen = false;
        [ObservableProperty]
        private ObservableCollection<TcpClient> clients = [];
        [ObservableProperty]
        private TcpClient? cliented;

        private readonly Dictionary<string, List<string>> ClientMessages = [];

        public Dispatcher Dispatcher { get; }
        [RelayCommand]
        public void SendMessage()
        {
            if (SendText is null)
                return;
#pragma warning disable CS8602 // 解引用可能出现空引用。
            foreach (var client in Server?.Clients)
            {
                client?.Send(SelectEncoding?.GetEncoding().GetBytes(SendText));
            }
#pragma warning restore CS8602 // 解引用可能出现空引用。
            _msgstack?.Enqueue($"发:{SendText}\r\n");
#pragma warning disable CS8601 // 引用类型赋值可能为 null。
            Messages = _msgstack?.ToList();
#pragma warning restore CS8601 // 引用类型赋值可能为 null。
        }
        public List<string> GetClientMessage(string Clientip) => ClientMessages[Clientip];
        public TcpSerivceViewModel(Dispatcher dispatcher)
        {
            SelectEncoding = Encodings[^1];
            Dispatcher = dispatcher;
        }
    }
}
