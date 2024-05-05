using CH_PurchaseWpfModule.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NetCoreWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly ILogger<PurchaseController> _logger;

        public PurchaseController(ILogger<PurchaseController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<CompanyPurchase> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new CompanyPurchase
            {
                //Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                //TemperatureC = Random.Shared.Next(-20, 55),
                //Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
