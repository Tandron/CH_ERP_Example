using CH_PurchaseWpfModule.Models;
using Prism.Mvvm;

namespace CH_PurchaseWpfModule.ViewModels
{
    public class CompanyPurchaseViewModel : BindableBase
    {
        private readonly CompanyPurchase _companyPurchase;

        public CompanyPurchaseViewModel()
        {
            _companyPurchase = new();
        }

        public CompanyPurchaseViewModel(CompanyPurchase companyPurchase)
        {
            _companyPurchase = companyPurchase ?? new CompanyPurchase();
        }

        public int Id
        {
            get => _companyPurchase.Id; 
            set
            {
                _companyPurchase.Id = value;
                RaisePropertyChanged();
            }
        }

        public string CompanyName
        {
            get => _companyPurchase.CompanyName;
            set
            {
                _companyPurchase.CompanyName = value;
                RaisePropertyChanged();
            }
        }

        public string Street
        {
            get => _companyPurchase.Street;
            set
            {
                _companyPurchase.Street = value;
                RaisePropertyChanged();
            }
        }

        public string City
        {
            get => _companyPurchase.City;
            set
            {
                _companyPurchase.City = value;
                RaisePropertyChanged();
            }
        }

        public string Country
        {
            get => _companyPurchase.Country;
            set
            {
                _companyPurchase.Country = value;
                RaisePropertyChanged();
            }
        }

        public string Phone
        {
            get => _companyPurchase.Phone;
            set
            {
                _companyPurchase.Phone = value;
                RaisePropertyChanged();
            }
        }   

        public string PostalCode
        {
            get => _companyPurchase.PostalCode;
            set
            {
                _companyPurchase.PostalCode = value;
                RaisePropertyChanged();
            }
        }

        public string PhoneNumber
        {
            get => _companyPurchase.PhoneNumber;
            set
            {
                _companyPurchase.PhoneNumber = value;
                RaisePropertyChanged();
            }
        }

        public string FaxNumber
        {
            get => _companyPurchase.FaxNumber;
            set
            {
                _companyPurchase.FaxNumber = value;
                RaisePropertyChanged();
            }
        }

        public string Description
        {
            get => _companyPurchase.Description;
            set
            {
                _companyPurchase.Description = value;
                RaisePropertyChanged();
            }
        }

        public string PurchaseType
        {
            get => _companyPurchase.PurchaseType;
            set
            {
                _companyPurchase.PurchaseType = value;
                RaisePropertyChanged();
            }
        }

        public DateTime PurchaseDate
        {
            get => _companyPurchase.PurchaseDate;
            set
            {
                _companyPurchase.PurchaseDate = value;
                RaisePropertyChanged();
            }
        }
    }
}
