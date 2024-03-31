using Microsoft.Extensions.DependencyInjection;
using SocketTool.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SocketTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel viewModel;
#pragma warning disable CS8601 // 引用类型赋值可能为 null。

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = App.Current.Services.GetService<MainViewModel>();
            this.viewModel   = App.Current.Services.GetService<MainViewModel>();
            LoadAsync();
        }
#pragma warning restore CS8601 // 引用类型赋值可能为 null。

        async void LoadAsync()
        {
            await Task.Delay(5000);
            viewModel.ShowLoadAnimation=Visibility.Collapsed;
        }
    }
}