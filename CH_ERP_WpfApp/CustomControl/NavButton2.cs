//using CH_ERP_WpfApp.Enums;
//using System.Diagnostics;
//using System.Drawing;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Controls.Primitives;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;

//namespace CH_ERP_WpfApp.Views
//{
//    public class NavButton : ToggleButton
//    {
//        private new readonly object Content;

//        public static readonly DependencyProperty ModuleMainViewProperty =
//            DependencyProperty.Register("ModuleMainView", typeof(ModuleViews), typeof(NavButton),
//                new FrameworkPropertyMetadata(ModuleViews.PurchaseMainControl));

//        public static readonly DependencyProperty ActiveModuleMainViewProperty =
//            DependencyProperty.Register("ActiveModuleMainView", typeof(ModuleViews), typeof(NavButton),
//                new FrameworkPropertyMetadata(ModuleViews.PurchaseMainControl,
//                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnActiveModuleMainViewChanged));

//        public static readonly DependencyProperty ModuleCommandProperty =
//            DependencyProperty.Register("ModuleCommand", typeof(ICommand), typeof(NavButton));

//        public static readonly DependencyProperty ImageContentProperty =
//            DependencyProperty.Register("ImageContent", typeof(Image), typeof(NavButton));

//        public static readonly DependencyProperty TextContentProperty =
//            DependencyProperty.Register("TextContent", typeof(TextBlock), typeof(NavButton));

//        public NavButton()
//        {
//            Loaded += (sender, args) => IsChecked = ActiveModuleMainView == ModuleMainView;
//            MouseEnter += NavButton_MouseEnter;
//            //Checked += NavButton_Checked;
//            //MouseMove += NavButton_MouseMove;
//            //StackPanel stackPanel = new();
//            //stackPanel.Children.Add(new TextBlock("fdfd"));
//            Image img = new()
//            {
//                //Source = new BitmapImage(new Uri("foo.png"))
//            };

//            TextBlock textBlock = new()
//            {
//                Text = "ewew"
//            };

//            StackPanel stackPanel = new()
//            {
//                Orientation = Orientation.Horizontal,
//                Margin = new Thickness(10)
//            };
//            //stackPanel.Children.Add(img);
//            stackPanel.Children.Add(textBlock);
//            //stackPanel.Children[0] = stackPanel;
//            Content = stackPanel;
//        }

//        private void NavButton_MouseMove(object sender, MouseEventArgs e)
//        {
//            if (sender is NavButton navButton)
//            {
//                //navButton.BorderBrush = navButton.Background = Brushes.Coral;
//                Debug.WriteLine(navButton.Background);
//            }
//        }

//        private void NavButton_Checked(object sender, RoutedEventArgs e)
//        {
//        }

//        private void NavButton_MouseEnter(object sender, MouseEventArgs e)
//        {
//            //if (sender is NavButton navButton)
//            //{
//            //    navButton.BorderBrush = navButton.Background = Brushes.Coral;
//            //}
//        }

//        private static void OnActiveModuleMainViewChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
//        {
//            if (d is NavButton navButton && args.NewValue is ModuleViews moduleViews)
//                navButton.IsChecked = navButton.ModuleMainView == moduleViews;
//        }

//        public Image ImageContent
//        {
//            get => (Image)GetValue(ImageContentProperty);
//            set => SetValue(ImageContentProperty, value);
//        }

//        public TextBlock TextContent
//        {
//            get => (TextBlock)GetValue(TextContentProperty);
//            set => SetValue(TextContentProperty, value);
//        }

//        public ModuleViews ModuleMainView
//        {
//            get => (ModuleViews)GetValue(ModuleMainViewProperty);
//            set => SetValue(ModuleMainViewProperty, value);
//        }

//        public ModuleViews ActiveModuleMainView
//        {
//            get => (ModuleViews)GetValue(ActiveModuleMainViewProperty);
//            set => SetValue(ActiveModuleMainViewProperty, value);
//        }

//        public ICommand ModuleCommand
//        {
//            get => (ICommand)GetValue(ModuleCommandProperty);
//            set => SetValue(ModuleCommandProperty, value);
//        }

//        protected override void OnClick()
//        {
//            if (ModuleCommand != null && ModuleCommand.CanExecute(ModuleMainView))
//            {
//                ModuleCommand.Execute(ModuleMainView);
//                IsChecked = ActiveModuleMainView == ModuleMainView;
//            }
//        }
//    }
//}
