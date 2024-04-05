using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SocketTool.ViewModel
{
    internal partial class MainViewModel:ObservableObject, INotifyPropertyChanged
    {
        [ObservableProperty]
        private Visibility showLoadAnimation=Visibility.Visible;
        [ObservableProperty]
        private Frame? navitePage;
        public void Navite(Page page,bool isAdd=false)
        {
            NavitePage?.Navigate(page);
        }
    }
}
