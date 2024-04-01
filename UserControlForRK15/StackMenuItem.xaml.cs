
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace UserControlForRK15
{
    /// <summary>
    /// StackMenuItem.xaml 的交互逻辑
    /// </summary>
    public partial class StackMenuItem : UserControl
    {


        public Brush HeadTileBackGroup
        {
            get { return (Brush)GetValue(HeadTileBackGroupProperty); }
            set { SetValue(HeadTileBackGroupProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeadTileBackGroup.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeadTileBackGroupProperty =
            DependencyProperty.Register("HeadTileBackGroup", typeof(Brush), typeof(StackMenuItem), 
                new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF02E2FF"))));


        public Brush ChildBackGroup
        {
            get { return (Brush)GetValue(ChildBackGroupProperty); }
            set { SetValue(ChildBackGroupProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChildBackGroup.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChildBackGroupProperty =
            DependencyProperty.Register("ChildBackGroup", typeof(Brush), typeof(StackMenuItem),
                new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF02E2FF"))));



        public string HeadText
        {
            get { return (string)GetValue(HeadTextProperty); }
            set { SetValue(HeadTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeadText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeadTextProperty =
            DependencyProperty.Register("HeadText", typeof(string), typeof(StackMenuItem), new PropertyMetadata("未命名"));



        public StackMenuItem()
        {
            InitializeComponent();
        }
    }
}
