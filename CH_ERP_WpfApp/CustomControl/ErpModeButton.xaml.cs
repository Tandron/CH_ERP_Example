using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
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
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnErpModeChanged));

        public static readonly DependencyProperty ImageUriProperty =
            DependencyProperty.Register("ImageUri", typeof(Uri), typeof(ErpModeButton),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnImageUriChanged));

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(double), typeof(ErpModeButton),
                new FrameworkPropertyMetadata(15.0));

        public static readonly DependencyProperty IsExpandProperty =
            DependencyProperty.Register("IsExpand", typeof(bool), typeof(ErpModeButton),
                new FrameworkPropertyMetadata(true,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsExpandChanged));

        public new static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(ErpModeButton),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender, OnContentChanged));

        public ErpModeButton()
        {
            InitializeComponent();
        }

        public ErpMode ErpModeView
        {
            get => (ErpMode)GetValue(ErpModeViewProperty);
            set => SetValue(ErpModeViewProperty, value);
        }

        public new object Content
        {
            get => (object)GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        public Uri ImageUri
        {
            get => (Uri)GetValue(ImageUriProperty);
            set => SetValue(ImageUriProperty, value);
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

        private static void OnIsExpandChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is ErpModeButton erpModeButton && args.NewValue is bool isExpand)
            {
                erpModeButton.txtContent.Visibility = erpModeButton.btnContextLine.Visibility =
                    isExpand ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is ErpModeButton erpModeButton && args.NewValue is string strContent)
                erpModeButton.txtContent.Text = strContent;
        }

        private static void OnErpModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is ErpModeButton erpModeButton && args.NewValue is string strContent)
                erpModeButton.txtContent.Text = strContent;
        }

        private static void OnImageUriChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is ErpModeButton erpModeButton && args.NewValue is Uri resourceUri)
                erpModeButton.btnImage.Source = new BitmapImage(resourceUri);
        }

        protected override void OnClick()
        {
            ErpModeView = Enum.GetValues(typeof(ErpMode)).Length == (int)ErpModeView + 1 ? 0 : ErpModeView + 1;
        }
    }
}
