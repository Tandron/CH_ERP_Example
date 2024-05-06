#if ASPNetCoreAPI
using Microsoft.EntityFrameworkCore;
#endif

namespace CH_PurchaseWpfModule.Models
{
    public class Plastic
    {
        public int Id { get; set; }

        public int PlaticType { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";

#if ASPNetCoreAPI
        [Precision(18, 2)]
#endif
        public decimal Price { get; set; }

    }
}
