using CH_ERP_WpfApp.Enums;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace CH_ERP_WpfApp.Views
{
    public class NavButton : ToggleButton
    {
        public static readonly DependencyProperty ModuleMainViewProperty =
            DependencyProperty.Register("ModuleMainView", typeof(ModuleViews), typeof(NavButton),
                new FrameworkPropertyMetadata(ModuleViews.PurchaseMainControl,
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty ActiveModuleMainViewProperty =
            DependencyProperty.Register("ActiveModuleMainView", typeof(ModuleViews), typeof(NavButton),
                new FrameworkPropertyMetadata(ModuleViews.PurchaseMainControl,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnActiveModuleMainViewChanged));

        public static readonly DependencyProperty ModuleCommandProperty =
            DependencyProperty.Register("ModuleCommand", typeof(ICommand), typeof(NavButton));

        public NavButton()
        {
            Loaded += (sender, args) => IsChecked = ActiveModuleMainView == ModuleMainView;
        }

        private static void OnActiveModuleMainViewChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is NavButton navButton && args.NewValue is ModuleViews moduleViews)
                navButton.IsChecked = navButton.ModuleMainView == moduleViews;
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
