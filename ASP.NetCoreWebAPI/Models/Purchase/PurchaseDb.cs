using CH_PurchaseWpfModule.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NetCoreWebAPI.Models.Purchase
{
    public class PurchaseDb(DbContextOptions<PurchaseDb> options) : DbContext(options)
    {
        public DbSet<BillFromPurchase> BillFromPurchases { get; set; }
        public DbSet<CompanyPurchase> CompanyPurchases { get; set; }
        public DbSet<Metal> Metals { get; set; }
        public DbSet<Plastic> Plastics { get; set; }
    }
}
