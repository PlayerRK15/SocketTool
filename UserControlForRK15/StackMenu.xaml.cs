using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
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
    public partial class StackMenu : ItemsControl
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
            DependencyProperty.Register("IsShow", typeof(bool), typeof(StackMenu), new PropertyMetadata(true));


        public string MenuTiletxt
        {
            get { return (string)GetValue(MenuTiletxtProperty); }
            set { SetValue(MenuTiletxtProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MenuTiletxt.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuTiletxtProperty =
            DependencyProperty.Register("MenuTiletxt", typeof(string), typeof(StackMenu), new PropertyMetadata("菜单"));



        public double MenuTileFontSize
        {
            get { return (double)GetValue(MenuTileFontSizeProperty); }
            set { SetValue(MenuTileFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MenuTileFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuTileFontSizeProperty =
            DependencyProperty.Register("MenuTileFontSize", typeof(double), typeof(StackMenu), new PropertyMetadata(12d));
        double? oldWidth;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var txt = (TextBlock)sender;
            if(IsShow)
            {
                IsShow = false;
                var animator = new DoubleAnimation();
                var cloumn = Template.FindName("ColumnContent",this) as ColumnDefinition;
                animator.From = cloumn?.ActualWidth;
                oldWidth = cloumn?.ActualWidth;
                animator.To = 0;
                animator.Duration = TimeSpan.FromSeconds(0.2);
                cloumn?.BeginAnimation(ColumnDefinition.MaxWidthProperty, animator);
                (Template.FindName("Show", this) as TextBlock).Text = "▷";
                (Template.FindName("Close", this) as TextBlock).Text = "";
            }
            else
            {
                IsShow = true;
                var animator = new DoubleAnimation();
                (Template.FindName("Show", this) as TextBlock).Text = "";
                (Template.FindName("Close", this) as TextBlock).Text = "◁";
                var cloumn = Template.FindName("ColumnContent", this) as ColumnDefinition;
                animator.From = 0;
                animator.To = oldWidth;
                animator.Duration = TimeSpan.FromSeconds(0.2);
                cloumn?.BeginAnimation(ColumnDefinition.MaxWidthProperty, animator);
            }
        }
    }
}
