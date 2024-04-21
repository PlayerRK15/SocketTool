
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;


namespace UserControlForRK15
{
    /// <summary>
    /// StackMenuItem.xaml 的交互逻辑
    /// </summary>
    public partial class StackMenuItem : ItemsControl
    {


        public string TileText
        {
            get { return (string)GetValue(TileTextProperty); }
            set { SetValue(TileTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TileText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TileTextProperty =
            DependencyProperty.Register("TileText", typeof(string), typeof(StackMenuItem), new PropertyMetadata(""));
        public Visibility IsShow
        {
            get { return (Visibility)GetValue(IsShowProperty); }
            set { SetValue(IsShowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsShow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowProperty =
            DependencyProperty.Register("IsShow", typeof(Visibility), typeof(StackMenuItem), new PropertyMetadata(Visibility.Visible));


        public StackMenuItem()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsShow == Visibility.Collapsed)
            {
                IsShow=Visibility.Visible;
            }
            else
            {
                IsShow = Visibility.Collapsed;
            }
        }
    }
}
