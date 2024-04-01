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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserControlForRK15
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class StackMenu : UserControl
    {


        public bool IsShowMenu
        {
            get { return (bool)GetValue(IsShowMenuProperty); }
            set { SetValue(IsShowMenuProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsShowMenu.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowMenuProperty =
            DependencyProperty.Register("IsShowMenu", typeof(bool), typeof(StackMenu), new PropertyMetadata(true));


        public StackMenu()
        {
            InitializeComponent();

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var animatoin = new DoubleAnimation();


            if (IsShowMenu)
            {
                IsShowMenu = false;
                animatoin.Duration = new Duration(TimeSpan.FromSeconds(1));
                animatoin.To = this.Width - 20;
                MenuContent.Visibility = Visibility.Collapsed;
            }
            else
            {
                IsShowMenu = true;
                MenuContent.Visibility = Visibility.Visible;
            }
            if (animatoin != null)
            {
                this.BeginAnimation(WidthProperty, animatoin);
                await Task.Delay(1000);
            }
        }
    }
}
