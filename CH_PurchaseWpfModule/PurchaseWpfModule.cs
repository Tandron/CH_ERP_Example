using CH_PurchaseWpfModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace CH_PurchaseWpfModule
{
    public class PurchaseWpfModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();

            regionManager.RegisterViewWithRegion("ContentRegion", typeof(PurchaseMainControl));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<PurchaseMainControl>();
        }
    }
}
