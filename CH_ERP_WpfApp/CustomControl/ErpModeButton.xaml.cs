using System.Windows;
using System.Windows.Controls;
using UsingEventAggregator.Core.Enums;

namespace CH_ERP_WpfApp.CustomControl
{
    /// <summary>
    /// Interaktionslogik für ErpModeButton.xaml
    /// </summary>
    public partial class ErpModeButton : Button
    {
        public static readonly DependencyProperty ErpModeViewProperty =
           DependencyProperty.Register("ErpModeView", typeof(ErpMode), typeof(ErpModeButton),
                new FrameworkPropertyMetadata(ErpMode.GameMode,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)); //, OnActiveErpModeViewChanged));

        public ErpModeButton()
        {
            InitializeComponent();
        }

        public ErpMode ErpModeView
        {
            get => (ErpMode)GetValue(ErpModeViewProperty);
            set => SetValue(ErpModeViewProperty, value);
        }

        //private static void OnActiveErpModeViewChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        //{
        //    if (d is ErpModeButton erpModeButton && args.NewValue is ErpMode erpMode)
        //        navButton.IsChecked = navButton.ModuleMainView == moduleViews;
        //}

        protected override void OnClick()
        {
            ErpModeView = Enum.GetValues(typeof(ErpMode)).Length == (int)ErpModeView + 1 ? 0 : ErpModeView++;
            //if (ModuleCommand != null && ModuleCommand.CanExecute(ModuleMainView))
            //{
            //    ModuleCommand.Execute(ModuleMainView);
            //    IsChecked = ActiveModuleMainView == ModuleMainView;
            //}
        }
    }
}
