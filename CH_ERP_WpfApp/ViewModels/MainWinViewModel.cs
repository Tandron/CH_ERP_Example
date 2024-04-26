using CH_ERP_WpfApp.Enums;
using Prism.Mvvm;
using System.Windows;

namespace CH_ERP_WpfApp.ViewModels
{
    public class MainWinViewModel : BindableBase
    {
        private string _title = "Prism Unity Application";
        public string MainRegion { get; } = "ContentRegion";
        public event Action<string, string> OnRegionChanged = delegate { };
        public NavigationBarViewModel NavigationBarVm { get; }

        public MainWinViewModel()
        {
            NavigationBarVm = new NavigationBarViewModel((moduleRegion) => OnRegionChanged(MainRegion, moduleRegion));
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        public void SetRegion()
        {
            OnRegionChanged(MainRegion, ModuleViews.PurchaseMainControl.ToString());
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    OnRegionChanged(MainRegion, ModuleViews.OrderMainControl.ToString());
                });
            });
        }
    }
}
