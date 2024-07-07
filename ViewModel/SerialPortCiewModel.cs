using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using STTech.BytesIO.Serial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTool.ViewModel
{
    partial class SerialPortCiewModel : ObservableObject, INotifyPropertyChanged
    {
        [ObservableProperty]
        private string _com;
        [ObservableProperty]
        private int _baudRate;
        [ObservableProperty]
        private bool isContent;
        [ObservableProperty]
        Queue<byte[]> _data;
        [ObservableProperty]
        string sendMsg;
        [ObservableProperty]
        List<string> _coms;
        [ObservableProperty]
        List<int> baudRates;
        private SerialClient _client;

        public SerialPortCiewModel()
        {
            _client = new SerialClient();
            Coms = [.. _client.GetPortNames()];
            BaudRates = [9600, 38400];
        }
        [RelayCommand]
        public void ContentCom()
        {
            if (_client?.IsConnected == false)
            {
                _client.PortName = Com;
                _client.BaudRate = BaudRate;
                _client.Connect();
                _client.OnDataReceived += Client_OnDataReceived;
                IsContent = true;
            }
        }
        [RelayCommand]
        public void Disconnect()
        {
            if (_client?.IsConnected == true)
            {
                _client.DiscardInBuffer();
                _client.Disconnect();
                IsContent = false;
            }
        }
        [RelayCommand]
        public void SendMessage()
        {
            if (_client?.IsConnected == true)
            {
                _client.Send(Convert.FromHexString(SendMsg));
            }
        }
        private void Client_OnDataReceived(object? sender, STTech.BytesIO.Core.DataReceivedEventArgs e)
        {
            lock (Data)
            {
                Data.Enqueue(e.Data);
            }
        }
    }
}
