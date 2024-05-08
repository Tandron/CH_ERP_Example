using CH_PurchaseWpfModule.Models;

namespace CH_PurchaseWpfModule.ViewModels
{
    public class EditCompanyPurchaseViewModel : CompanyPurchaseViewModel
    {
        public CompanyPurchase CompanyPurchase => _companyPurchase;

        public EditCompanyPurchaseViewModel()
        {

        }
    }
}