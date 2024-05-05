using CH_PurchaseWpfModule.Enums;

namespace CH_PurchaseWpfModule.Models
{
    public class Metal
    {
        public int Id { get; set; }

        public MetalType MetalType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
