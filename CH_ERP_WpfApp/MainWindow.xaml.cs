using CH_ERP_WpfApp.ViewModels;
using System.Windows;

namespace CH_ERP_WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs args)
        {
            if (DataContext is MainWinViewModel mainWinVm)
            {
                mainWinVm.SetRegion();
            }
        }
    }
}