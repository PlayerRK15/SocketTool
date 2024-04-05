
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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

        public UIElementCollection Items
        {
            get { return (UIElementCollection)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(UIElementCollection), typeof(StackMenuItem));


        public object MenuItemContent
        {
            get { return (object)GetValue(MenuItemContentProperty); }
            set { SetValue(MenuItemContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MenuItemContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuItemContentProperty =
            DependencyProperty.Register("MenuItemContent", typeof(object), typeof(StackMenuItem), new PropertyMetadata(null));
        private bool isSeleceted=false;

        public event RoutedEventHandler OnSelcet;
        public bool IsSelected { get => isSeleceted; 
            private set
            {
                if(value)
                {
                    OnSelcet?.Invoke(this,new RoutedEventArgs());
                    MenuPanel.Visibility = Visibility.Visible;
                    IcoRotate.Angle = -90;
                }
                else
                {
                    MenuPanel.Visibility    = Visibility.Collapsed;
                    IcoRotate.Angle = 180;
                }
                isSeleceted = value;
            }
        }

        public StackMenuItem()
        {
            InitializeComponent();
            IsSelected=true;
            Items= MenuPanel.Children;
        }

        private void SelectChange(object sender, RoutedEventArgs e)
        {
            if (IsSelected)
            {
                IsSelected = false;
            }
            else
            {
                IsSelected = true;
            }
        }
    }
}
