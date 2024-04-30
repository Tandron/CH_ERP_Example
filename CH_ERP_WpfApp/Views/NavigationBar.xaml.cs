using System.Windows;
using System.Windows.Controls;

namespace CH_ERP_WpfApp.Views
{
    /// <summary>
    /// Interaktionslogik für NavigationBar.xaml
    /// </summary>
    public partial class NavigationBar : UserControl
    {
        public NavigationBar()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            object resource = TryFindResource("actuelWidth");
            // If resource found, do something with it
            if (resource is double actuelWidth)
            {
                actuelWidth = ActualWidth;
            }
        }
    }
}
