﻿using CommunityToolkit.Mvvm.ComponentModel;
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
        private int maxClinetMsgLength = 50;
        [ObservableProperty]
        private List<string> messages;
        private readonly Queue<string> _msgstack = new();
        [Obsolete]
        partial void OnIsListenChanged(bool value)
        {
            if (value)
            {
                if (Server is null)
                {
                    Server = new TcpServer();
                }
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
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            Clients.Add((client));
                        });
                        var ipPort = client.RemoteEndPoint.ToString();
                        if (!ClientMessages.ContainsKey(ipPort))
                        {
                            ClientMessages.Add(ipPort, new List<string>());
                        }
                        //接收客户端消息传入字典
                        client.OnDataReceived += (s, e) =>
                        {
                            if (SelectEncoding?.GetEncoding()?.GetString(e.Data) is string msg)
                            {
                                var msglist = ClientMessages[ipPort];
                                if (msglist.Count > MaxClinetMsgLength)
                                {
                                    msglist.RemoveRange(0, msglist.Count - MaxClinetMsgLength);
                                }
                                if (_msgstack?.Count > MaxClinetMsgLength)
                                {
                                    Dispatcher.BeginInvoke(() =>
                                    {
                                        _msgstack.Dequeue();
                                    });
                                }
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
                        };
                    }
                };
            }
            else
            {
                if (Server is not null)
                {
                    Server.StopAsync();
                }
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

        public List<string> GetClinetMessage(string clinetip) => ClientMessages[clinetip];
        public TcpSerivceViewModel(Dispatcher dispatcher)
        {
            SelectEncoding = Encodings[^1];
            Dispatcher = dispatcher;
        }
    }
}
