using CH_PurchaseWpfModule.Models;
using Prism.Events;
using Prism.Mvvm;
using RestSharp;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows;
using UsingEventAggregator.Core;
using UsingEventAggregator.Core.Enums;

namespace CH_PurchaseWpfModule.ViewModels
{
    public class PurchaseMainViewModel : BindableBase
    {
        private readonly RestClient _client = new(@"https://localhost:7057/api/purchases/");
        public ObservableCollection<CompanyPurchaseViewModel> CompanyPurchasesVm { get; } = [];
        private CompanyPurchaseViewModel _selectedCompanyPurchaseItemVm;

        public PurchaseMainViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<ErpModeMessageSentEvent>().Subscribe(MessageReceived);
            LoadFromDatabase();
            //ImportCompanyPurchases();
        }

        private void MessageReceived(ErpMode mode)
        {

        }

        public CompanyPurchaseViewModel SelectedCompanyPurchaseItemVm
        {
            get => _selectedCompanyPurchaseItemVm;
            set
            {
                _selectedCompanyPurchaseItemVm = value;
                RaisePropertyChanged();
            }
        }

        private void LoadFromDatabase()
        {
            Task.Run(() =>
            {
                //Thread.Sleep(13000);
                RestRequest request = new("getCompanyPurchases", Method.Get);
                var response = _client.Execute<List<CompanyPurchase>>(request);

                Application.Current.Dispatcher.BeginInvoke(() =>
                {
                    if (response.StatusCode == HttpStatusCode.OK && response.Data is List<CompanyPurchase> liCompanyPurchases)
                    {
                        foreach (var liDoc in liCompanyPurchases)
                        {

                        }
                        //CompanyPurchasesVm.Add(new(liDoc));
                    }
                });
            });
        }

        private void NewPurchaseCompanyFunc()
        {
            EditCompanyPurchaseViewModel editCompanyPurchaseVm = new();

            //OpenNewCalcDialog?.Invoke(editCalculationDocVm, () =>
            //{
            var request = new RestRequest("appendCompanyPurchase", Method.Post);

            request.AddJsonBody(editCompanyPurchaseVm.CompanyPurchase);
            var response = _client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content) &&
                int.TryParse(response.Content, out int idNumber) && idNumber > 0)
            {
                editCompanyPurchaseVm.Id = idNumber;
                CompanyPurchasesVm.Add(editCompanyPurchaseVm);
            }
            //});
        }

        private void DeleteCompanyPurchaseFunc()
        {
        }

        private string[] GetCompanyPurchaseProperties()
        {
            Type test = new CompanyPurchase().GetType();
            PropertyInfo[] props = test.GetProperties();
            List<string> list = new List<string>();

            foreach (var progInfo in props)
            {
                list.Add(progInfo.Name);
            }

            return list.ToArray();
        }

        private void ImportCompanyPurchases()
        {
            string homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string csvUS500 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Firmenanschriften\us-500\us-500.csv";
            Dictionary<string, string> dictCompanyPurchase = new();
            string[] companyPurchaseProps = GetCompanyPurchaseProperties();

            using (StreamReader sr = new(csvUS500))
            {
                string[] strLiHeader = sr.ReadLine().Split(",");


                //                [0]	"\"first_name\""    string
                //[1] "\"last_name\"" string
                //[2] "\"company_name\""  string
                //[3] "\"address\""   string
                //[4] "\"city\""  string
                //[5] "\"county\""    string
                //[6] "\"state\"" string
                //[7] "\"zip\""   string
                //[8] "\"phone1\""    string
                //[9] "\"phone2\""    string
                //[10]    "\"email\"" string
                //[11]    "\"web\""   string

                while (sr.Peek() >= 0)
                {
                    string[] strLines = sr.ReadLine().Split(",");

                    CompanyPurchase companyPurchase = new()
                    {
                        CompanyName = strLines[2].Trim('\"'),
                        Street = strLines[3].Trim('\"'),
                        City = strLines[4].Trim('\"'),
                        Country = strLines[5].Trim('\"'),
                        PostalCode = strLines[7].Trim('\"'),
                        PhoneNumber = strLines[8].Trim('\"')
                    };
                    CompanyPurchasesVm.Add(new CompanyPurchaseViewModel(companyPurchase));
                }
            }
        }
    }
}
