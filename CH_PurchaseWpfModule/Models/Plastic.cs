using CH_PurchaseWpfModule.Enums;

namespace CH_PurchaseWpfModule.Models
{
    public class Plastic
    {
        public int Id { get; set; }

        public PlaticType PlaticType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

    }
}
