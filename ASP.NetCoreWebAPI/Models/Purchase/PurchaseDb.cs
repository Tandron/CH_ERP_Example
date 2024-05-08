using CH_PurchaseWpfModule.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ASP.NetCoreWebAPI.Models.Purchase
{
    public class PurchaseDb(DbContextOptions<PurchaseDb> options) : DbContext(options)
    {
        // https://stackoverflow.com/questions/29110241/how-do-you-configure-the-dbcontext-when-creating-migrations-in-entity-framework
        // https://learn.microsoft.com/de-de/dotnet/api/microsoft.entityframeworkcore.dbcontext.onconfiguring?view=efcore-8.0
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    SqlConnectionStringBuilder strCon = new()
        //    {
        //        DataSource = Directory.GetCurrentDirectory() + @"\Databases\",
        //        //                DataSource = @"C:\Users\NinjaShadow\source\repos\CH_ERP_Example\ASP.NetCoreWebAPI\Databases\",
        //        InitialCatalog = "PurchaseDb",
        //        TrustServerCertificate = false
        //    };

        //    optionsBuilder.UseSqlServer(strCon.ConnectionString);
        //}

        public DbSet<BillFromPurchase> BillFromPurchases { get; set; }
        public DbSet<CompanyPurchase> CompanyPurchases { get; set; }
        public DbSet<Metal> Metals { get; set; }
        public DbSet<Plastic> Plastics { get; set; }


        //public static string GetSqlConnectionString()
        //{
        //    SqlConnectionStringBuilder strCon = new()
        //    {
        //        DataSource = Directory.GetCurrentDirectory() + @"\Databases\",
        //        InitialCatalog = "PurchaseDb",
        //        TrustServerCertificate = false,
                 
        //    };
 
        //    return strCon.ConnectionString;
        //}
    }
}
