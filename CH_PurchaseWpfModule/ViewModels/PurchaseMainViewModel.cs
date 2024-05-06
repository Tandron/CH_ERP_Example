using CH_PurchaseWpfModule.Models;
using Prism.Events;
using Prism.Mvvm;
using RestSharp;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using UsingEventAggregator.Core;
using UsingEventAggregator.Core.Enums;

namespace CH_PurchaseWpfModule.ViewModels
{
    public class PurchaseMainViewModel : BindableBase
    {
        private readonly RestClient _client = new(@"https://localhost:7057/api/purchases/");
        public ObservableCollection<CompanyPurchaseViewModel> CompanyPurchasesVm { get; }

        public PurchaseMainViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<ErpModeMessageSentEvent>().Subscribe(MessageReceived);
            LoadFromDatabase();
        }

        private void MessageReceived(ErpMode mode)
        {

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

        //private void NewCalcFunc()
        //{
        //    EditCalculationDocViewModel editCalculationDocVm = new();

        //    OpenNewCalcDialog?.Invoke(editCalculationDocVm, () =>
        //    {
        //        editCalculationDocVm.SaveCalculationDoc();
        //        var request = new RestRequest("additem", Method.Post);

        //        request.AddJsonBody(editCalculationDocVm.CalculationDoc);
        //        var response = _client.Execute(request);

        //        if (response.StatusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content) &&
        //            int.TryParse(response.Content, out int idNumber) && idNumber > 0)
        //        {
        //            editCalculationDocVm.Id = idNumber;
        //            CalculationDocItemsVm.Add(editCalculationDocVm);
        //        }
        //    });
        //}
    }
}
