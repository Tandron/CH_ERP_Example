using CH_ERP_WpfApp.CustomControl;
using System.Windows;
using System.Windows.Controls;

namespace CH_ERP_WpfApp.Views
{
    public class NavButtonStackPanel : StackPanel
    {
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(double), typeof(NavButtonStackPanel),
                new FrameworkPropertyMetadata(15.0,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnCornerRadiusChanged));

        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        private static void OnCornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is NavButtonStackPanel navButtonStackPanel && args.NewValue is double newCornerRadius)
            {
                foreach (var item in navButtonStackPanel.Children)
                {
                    if (item is NavButton navButton)
                    {
                        navButton.CornerRadius = newCornerRadius;
                    }
                }
            }
        }

        public void SetAllNavButtonOnExpand(bool isExpand)
        {
            foreach (var item in Children)
            {
                if (item is NavButton navButton)
                {
                    navButton.IsExpand = isExpand;
                }
            }
        }
    }

    /// <summary>
    /// Interaktionslogik für NavigationBar.xaml
    /// </summary>
    public partial class NavigationBar : UserControl
    {
        public NavigationBar()
        {
            InitializeComponent();
        }

        private void DoubleAnimation_Completed(object sender, EventArgs args)
        {
            if (toggleBtnIsExpand.IsChecked is bool isChecked)
            {
                navBtnStackpan.SetAllNavButtonOnExpand(isChecked);
            }
        }
    }
}
