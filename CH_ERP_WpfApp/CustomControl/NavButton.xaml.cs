using CH_ERP_WpfApp.Enums;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace CH_ERP_WpfApp.CustomControl
{
    /// <summary>
    /// Interaktionslogik für NavButton.xaml
    /// </summary>
    public partial class NavButton : ToggleButton
    {
        public static readonly DependencyProperty ModuleMainViewProperty =
            DependencyProperty.Register("ModuleMainView", typeof(ModuleViews), typeof(NavButton),
        new FrameworkPropertyMetadata(ModuleViews.PurchaseMainControl));

        public static readonly DependencyProperty ActiveModuleMainViewProperty =
            DependencyProperty.Register("ActiveModuleMainView", typeof(ModuleViews), typeof(NavButton),
                new FrameworkPropertyMetadata(ModuleViews.PurchaseMainControl,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnActiveModuleMainViewChanged));

        public static readonly DependencyProperty ModuleCommandProperty =
            DependencyProperty.Register("ModuleCommand", typeof(ICommand), typeof(NavButton));

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(double), typeof(NavButton),
                new FrameworkPropertyMetadata(15.0));

        public static readonly DependencyProperty IsExpandProperty =
            DependencyProperty.Register("IsExpand", typeof(bool), typeof(NavButton),
                new FrameworkPropertyMetadata(true,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsExpandChanged));

        //new FrameworkPropertyMetadata(new Thickness(15.0),
        //            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnCornerRadiusChanged));

        //public new static readonly DependencyProperty ContentProperty =
        //    DependencyProperty.Register("Content", typeof(object), typeof(NavButton));

        public static readonly DependencyProperty ImageContentProperty =
            DependencyProperty.Register("ImageContent", typeof(Image), typeof(NavButton));

        public static readonly DependencyProperty TextContentProperty =
            DependencyProperty.Register("TextContent", typeof(TextBlock), typeof(NavButton));

        public NavButton()
        {
            InitializeComponent();
            Loaded += (sender, args) => IsChecked = ActiveModuleMainView == ModuleMainView;
        }

        private static void OnActiveModuleMainViewChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is NavButton navButton && args.NewValue is ModuleViews moduleViews)
                navButton.IsChecked = navButton.ModuleMainView == moduleViews;
        }

        private static void OnIsExpandChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is NavButton navButton && args.NewValue is ModuleViews moduleViews)
                navButton.IsChecked = navButton.ModuleMainView == moduleViews;
        }

        //public new object Content
        //{
        //    get => (object)GetValue(ContentProperty);
        //    set => SetValue(ContentProperty, value);
        //}

        public Image ImageContent
        {
            get => (Image)GetValue(ImageContentProperty);
            set => SetValue(ImageContentProperty, value);
        }

        public TextBlock TextContent
        {
            get => (TextBlock)GetValue(TextContentProperty);
            set => SetValue(TextContentProperty, value);
        }

        public ModuleViews ModuleMainView
        {
            get => (ModuleViews)GetValue(ModuleMainViewProperty);
            set => SetValue(ModuleMainViewProperty, value);
        }

        public ModuleViews ActiveModuleMainView
        {
            get => (ModuleViews)GetValue(ActiveModuleMainViewProperty);
            set => SetValue(ActiveModuleMainViewProperty, value);
        }

        public ICommand ModuleCommand
        {
            get => (ICommand)GetValue(ModuleCommandProperty);
            set => SetValue(ModuleCommandProperty, value);
        }

        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public bool IsExpand
        {
            get => (bool)GetValue(IsExpandProperty);
            set => SetValue(IsExpandProperty, value);
        }

        protected override void OnClick()
        {
            if (ModuleCommand != null && ModuleCommand.CanExecute(ModuleMainView))
            {
                ModuleCommand.Execute(ModuleMainView);
                IsChecked = ActiveModuleMainView == ModuleMainView;
            }
        }
    }
}
