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
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public ErpModeButton()
        {
            InitializeComponent();
        }

        public ErpMode ErpModeView
        {
            get => (ErpMode)GetValue(ErpModeViewProperty);
            set => SetValue(ErpModeViewProperty, value);
        }

        protected override void OnClick()
        {
            ErpModeView = Enum.GetValues(typeof(ErpMode)).Length == (int)ErpModeView + 1 ? 0 : ErpModeView + 1;
        }
    }
}
