using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SocketTool.View.TcpView
{
    /// <summary>
    /// TcpServicePage.xaml 的交互逻辑
    /// </summary>
    public partial class TcpServicePage : Page
    {
        ViewModel.TcpSerivceViewModel model;
        public TcpServicePage()
        {
            InitializeComponent();
            this.DataContext= model = new ViewModel.TcpSerivceViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            model.IsListen = true;
        }
    }
}
