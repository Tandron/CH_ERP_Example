
using CH_OrderWpfModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace CH_OrderWpfModule
{
    public class OrderWpfModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(OrderMainControl));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<OrderMainControl>();
        }
    }

}
