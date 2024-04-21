using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace SocketTool.ViewModel
{
    internal partial class MainViewModel:ObservableObject, INotifyPropertyChanged
    {
        [ObservableProperty]
        private Visibility showLoadAnimation=Visibility.Visible;
        [ObservableProperty]
        private TabControl? navitePage;
        [ObservableProperty]
        private List<TabItem> pageList=[];
        public void Navite(UserControl page,bool isAdd=false,string header="新建标签页")
        {
            var hander = new StackPanel() { Orientation = Orientation.Horizontal };
            hander.Children.Add(new TextBlock() { Text = header });
            var closeBtn = new TextBlock() { Text = "×"};
            hander.Children.Add(closeBtn);
            var tabItem = new TabItem
            {
                Header = hander,
                Content = page
            };
            PageList.Add(tabItem);
            closeBtn.MouseDown += (s, e) => {
                PageList.Remove(tabItem);
                NavitePage.Items.Refresh();
            };
            NavitePage.SelectedItem = tabItem;
            NavitePage.Items.Refresh();
        }
        [RelayCommand]
        public void OpenTcpService()
        {
            Navite(new View.TcpView.TcpServiceView(), header:"TCP服务端");
        }
        [RelayCommand]
        public void OpenTcpClient() => Navite(new View.TcpView.TcpClientView(), header: "Tcp客户端");
        partial void OnNavitePageChanged(TabControl? value)
        {
            value.ItemsSource= PageList;
        }
    }
}
