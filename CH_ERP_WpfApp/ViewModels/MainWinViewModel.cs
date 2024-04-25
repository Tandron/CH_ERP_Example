using Accessibility;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CH_ERP_WpfApp.ViewModels
{
    public class MainWinViewModel(IRegionManager regionManager) : BindableBase
    {
        private readonly IRegionManager _regionManager = regionManager;
        private string _title = "Prism Unity Application";

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public void SetRegion()
        {
            _regionManager.RequestNavigate("ContentRegion", "PurchaseMainControl");
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000);


                Application.Current.Dispatcher.Invoke(() =>
                {
                    _regionManager.RequestNavigate("ContentRegion", "OrderMainControl");
                    //RegionName = "OrderContentRegion";
                });
            });
        }
    }
}
