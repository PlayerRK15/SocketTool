using CommunityToolkit.Mvvm.ComponentModel;
using STTech.BytesIO.Tcp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SocketTool.ViewModel
{
    internal partial class TcpSerivceViewModel : ObservableObject, INotifyPropertyChanged
    {
        [ObservableProperty]
        private TcpServer? server;
        [ObservableProperty]
        private string? host;
        [ObservableProperty]
        private int port;
        [ObservableProperty]
        private bool isListen;
        [ObservableProperty]
        private List<EncodingInfo> encodings=[.. Encoding.GetEncodings()];
        [ObservableProperty]
        private Encoding? selectEncoding=Encoding.UTF8;
        [ObservableProperty]
        private int maxClinetMsgLength=50;
        [ObservableProperty]
        private ObservableCollection<string> messages = [];
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
                //客户端连接
                Server.ClientConnected += (s, e) =>
                {
                    if (s is TcpClient client)
                    {
                        Clients.Add((client));
                        var ipPort = client.RemoteEndPoint.ToString();
                        if (!ClientMessages.ContainsKey(ipPort))
                        {
                            ClientMessages.Add(ipPort, new List<string>());
                        }
                        //接收客户端消息传入字典
                        client.OnDataReceived += (s, e) =>
                        {
                            if(SelectEncoding?.GetString(e.Data) is string msg)
                            {
                                var msglist = ClientMessages[ipPort];
                                if (msglist.Count > MaxClinetMsgLength)
                                {
                                    msglist.RemoveRange(0, msglist.Count- MaxClinetMsgLength);
                                }
                                if(Messages?.Count>MaxClinetMsgLength)
                                {
                                    for (int i = 0; i < Messages.Count- MaxClinetMsgLength; i++)
                                    {
                                        Messages.RemoveAt(i);
                                    }
                                }
                                msg = $"{DateTime.Now:yyyy-MM-dd hh:mm:dd:fff} 接收:{ipPort}\r\n{msg}\r\n";
                                msglist.Add(msg);
                                Messages?.Add(msg);
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
        private uint listenLength;
        [ObservableProperty]
        private ObservableCollection<TcpClient> clients;

        private readonly Dictionary<string, List<string>> ClientMessages=[];
        public List<string> GetClinetMessage(string clinetip) => ClientMessages[clinetip];
    }
}
