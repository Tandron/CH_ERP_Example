using CH_ERP_WpfApp.Enums;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using UsingEventAggregator.Core;
using UsingEventAggregator.Core.Enums;

namespace CH_ERP_WpfApp.ViewModels
{

    public class NavigationBarViewModel : BindableBase
    {
        private ModuleViews _activeModuleView = ModuleViews.PurchaseMainControl;
        private readonly Action<string> _onRegionChanged;
        private ErpMode _editMode = ErpMode.GameMode;
        private readonly IEventAggregator _eventAggregator;
        public DelegateCommand<object> NavigationCommand { get; }
        private readonly Dictionary<ErpMode, string> _dictErpModes = [];

        public NavigationBarViewModel(Action<string> onRegionChanged, IEventAggregator eventAggregator)
        {
            _onRegionChanged = onRegionChanged;
            _eventAggregator = eventAggregator;
            NavigationCommand = new DelegateCommand<object>(NavigationFunc);

            _dictErpModes.Add(ErpMode.EditMode, "Editor Modus");
            _dictErpModes.Add(ErpMode.GameMode, "Spiel Modus");
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

        public string StrErpMode => _dictErpModes.TryGetValue(EditMode, out string? value) ? value : "";

        public ErpMode EditMode
        {
            get => _editMode;
            set
            {
                _editMode = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(StrErpMode));
                _eventAggregator.GetEvent<ErpModeMessageSentEvent>().Publish(_editMode);
            }
        }
    }
}
