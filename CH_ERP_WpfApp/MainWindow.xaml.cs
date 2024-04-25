using CH_ERP_WpfApp.ViewModels;
using Prism.Regions;
using System.Windows;

namespace CH_ERP_WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IRegionManager _regionManager;

        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();
            _regionManager = regionManager;
        }

        private void Window_Loaded(object sender, RoutedEventArgs args)
        {
            if (DataContext is MainWinViewModel mainWinVm)
            {
                mainWinVm.SetRegion();
            }
        }

        private void Window_DataContextChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue is MainWinViewModel mainWinVm)
            {
                mainWinVm.OnRegionChanged += MainWinVm_OnRegionChanged;
            } else if (args.OldValue is MainWinViewModel mainWinUnVm)
            {
                mainWinUnVm.OnRegionChanged -= MainWinVm_OnRegionChanged;
            }
        }

        private void MainWinVm_OnRegionChanged(string mainRegion, string moduleRegion)
        {
            _regionManager.RequestNavigate(mainRegion, moduleRegion);
        }
    }
}