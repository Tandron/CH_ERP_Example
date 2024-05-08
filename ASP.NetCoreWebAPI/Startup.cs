using ASP.NetCoreWebAPI.Models.Purchase;
using Microsoft.EntityFrameworkCore;

namespace ASP.NetCoreWebAPI
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration ConfigRoot { get; } = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<PurchaseDb>(options => 
            //options.UseSqlServer("server=.;database=PurchaseDb;trusted_connection=true;"));

            services.AddDbContext<PurchaseDb>(options => options.UseSqlServer(ConfigRoot.GetConnectionString("PurchaseDbConnection")));
            //services.AddDbContext<PurchaseDb>(options => options.UseSqlServer(PurchaseDb.GetSqlConnectionString()));
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
