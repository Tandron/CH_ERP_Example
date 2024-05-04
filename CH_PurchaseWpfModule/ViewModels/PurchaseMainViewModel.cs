using Prism.Events;
using Prism.Mvvm;
using UsingEventAggregator.Core;
using UsingEventAggregator.Core.Enums;

namespace CH_PurchaseWpfModule.ViewModels
{
    public class PurchaseMainViewModel : BindableBase
    {
        public PurchaseMainViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<ErpModeMessageSentEvent>().Subscribe(MessageReceived);
        }

        private void MessageReceived(ErpMode mode)
        {

        }
    }
}
