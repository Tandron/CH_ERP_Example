using CH_ERP_WpfApp.ViewModels;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using System.Windows;

namespace CH_ERP_WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            var mainWin = Container.Resolve<MainWindow>();

            mainWin.DataContext = new MainWinViewModel(Container.Resolve<IRegionManager>());
            return mainWin;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterForNavigation<CH_PurchaseWpfModule.PurchaseWpfModule>();
            //containerRegistry.RegisterForNavigation<CH_OrderWpfModule.OrderWpfModule>();

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<CH_PurchaseWpfModule.PurchaseWpfModule>();
            moduleCatalog.AddModule<CH_OrderWpfModule.OrderWpfModule>();
        }
    }
}
