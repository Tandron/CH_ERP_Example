using CH_PurchaseWpfModule.ViewModels;
using Prism.Events;
using System.Windows;
using System.Windows.Controls;

namespace CH_PurchaseWpfModule.Views
{
    /// <summary>
    /// Interaktionslogik für PurchaseMainControl.xaml
    /// </summary>
    public partial class PurchaseMainControl : UserControl
    {
        public PurchaseMainControl(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            DataContext = new PurchaseMainViewModel(eventAggregator);
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue is PurchaseMainViewModel purchaseMainVm)
            {
                dataGrid1.FilteredItemsSource = purchaseMainVm.CompanyPurchasesVm;
            }
        }
    }
}
