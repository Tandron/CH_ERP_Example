#if ASPNetCoreAPI
using Microsoft.EntityFrameworkCore;
#endif

namespace CH_PurchaseWpfModule.Models
{
    public class BillFromPurchase
    {
        public int Id { get; set; }

        public DateTime OrderTime { get; set; }

#if ASPNetCoreAPI
        [Precision(18, 2)]
#endif
        public decimal PostalCharges { get; set; }

        public List<Metal> Metals { get; set; } = [];

        public List<Plastic> Plastics { get; set; } = [];
    }
}
