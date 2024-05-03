using CH_PurchaseWpfModule.ViewModels;
using Prism;
using Prism.Events;
using System.ComponentModel;
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
    }
}
