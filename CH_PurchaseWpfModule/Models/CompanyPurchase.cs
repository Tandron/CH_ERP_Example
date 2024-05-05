namespace CH_PurchaseWpfModule.Models
{
    public class CompanyPurchase
    {
        public int Id { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public string Street { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string FaxNumber { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public string PurchaseType { get; set; } = string.Empty;
        public DateTime PurchaseDate { get; set; }

        public List<BillFromPurchase> BillFromPurchases { get; set; } = [];

    }
}
