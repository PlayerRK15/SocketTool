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
    public partial class StackMenu : UserControl
    {

        public bool IsShowMenu
        {
            get { return (bool)GetValue(IsShowMenuProperty); }
            set
            {
                if (value)
                {
                    ColumnOne.Width = new GridLength(1,GridUnitType.Star);
                    MenuContent.Visibility= Visibility.Visible;
                }
                else
                {
                    ColumnOne.Width = new GridLength(0);
                    MenuContent.Visibility = Visibility.Collapsed;
                }
                SetValue(IsShowMenuProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for IsShowMenu.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowMenuProperty =
            DependencyProperty.Register("IsShowMenu", typeof(bool), typeof(StackMenu), new PropertyMetadata(true));



        public UIElementCollection Items
        {
            get { return (UIElementCollection)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(UIElementCollection), typeof(StackMenu));



        public StackMenu()
        {
            InitializeComponent();
            Items = MenuContent.Children;
        }
        public void SetBinding(object DataContext)
        {
            foreach (var item in Items)
            {
                if(item is StackMenuItem menuItem)
                {
                    menuItem.MenuPanel.DataContext = DataContext;
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)=> IsShowMenu = !IsShowMenu;
    }
}
