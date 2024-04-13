using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
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
    public partial class StackMenu : StackPanel
    {
        public StackMenu()
        {
            InitializeComponent();
        }
        public bool IsShow
        {
            get { return (bool)GetValue(IsShowProperty); }
            set { SetValue(IsShowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsShow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowProperty =
            DependencyProperty.Register("IsShow", typeof(bool), typeof(StackMenu), new PropertyMetadata(false));


        public string MenuTiletxt
        {
            get { return (string)GetValue(MenuTiletxtProperty); }
            set { SetValue(MenuTiletxtProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MenuTiletxt.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuTiletxtProperty =
            DependencyProperty.Register("MenuTiletxt", typeof(string), typeof(StackMenu), new PropertyMetadata("菜单"));


    }
}
