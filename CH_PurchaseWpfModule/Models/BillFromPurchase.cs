namespace CH_PurchaseWpfModule.Models
{
    public class BillFromPurchase
    {
        public int Id { get; set; }

        public DateTime OrderTime { get; set; }
        public decimal PostalCharges { get; set; }

        public List<Metal> Metals { get; set; } = [];

        public List<Plastic> Plastics { get; set; } = [];
    }
}
