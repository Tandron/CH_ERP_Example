using CH_ERP_WpfApp.Enums;
using Prism.Events;
using Prism.Mvvm;
using System.Windows;

namespace CH_ERP_WpfApp.ViewModels
{
    public class MainWinViewModel : BindableBase
    {
        public string MainRegion { get; } = "ContentRegion";
        public event Action<string, string> OnRegionChanged = delegate { };
        public NavigationBarViewModel NavigationBarVm { get; }

        public MainWinViewModel(IEventAggregator eventAggregator)
        {
            NavigationBarVm = new NavigationBarViewModel((moduleRegion) => OnRegionChanged(MainRegion, moduleRegion), eventAggregator);
        }
    }
}
