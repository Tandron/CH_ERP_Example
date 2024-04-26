using CH_ERP_WpfApp.Enums;
using Prism.Commands;
using Prism.Mvvm;

namespace CH_ERP_WpfApp.ViewModels
{
    public class NavigationBarViewModel : BindableBase
    {
        private ModuleViews _activeModuleView = ModuleViews.PurchaseMainControl;
        private readonly Action<string> _onRegionChanged;
        public DelegateCommand<object> NavigationCommand { get; }

        public NavigationBarViewModel(Action<string> onRegionChanged)
        {
            _onRegionChanged = onRegionChanged;
            NavigationCommand = new DelegateCommand<object>(NavigationFunc);
        }

        private void NavigationFunc(object moduleViewObject)
        {
            if (moduleViewObject is ModuleViews moduleView)
            {
                ActiveModuleView = moduleView;
            }
        }

        public ModuleViews ActiveModuleView
        {
            get => _activeModuleView;
            set
            {
                _activeModuleView = value;
                _onRegionChanged(value.ToString());
                RaisePropertyChanged();
            }
        }
    }
}
