#if ASPNetCoreAPI
using Microsoft.EntityFrameworkCore;
#endif

namespace CH_PurchaseWpfModule.Models
{
    public class Metal
    {
        public int Id { get; set; }

        public int MetalType { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }

#if ASPNetCoreAPI
        [Precision(18, 2)]
#endif
        public decimal Price { get; set; }
    }
}
