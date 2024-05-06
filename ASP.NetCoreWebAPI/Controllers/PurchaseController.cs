using ASP.NetCoreWebAPI.Models.Purchase;
using CH_PurchaseWpfModule.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NetCoreWebAPI.Controllers
{
    [ApiController]
    [Route("api/purchases")]
    public class PurchaseController(PurchaseDb context) : ControllerBase
    {
        private readonly PurchaseDb _context = context;

        [HttpGet("getCompanyPurchases", Name = "GetCompanyPurchases")]
        public IEnumerable<CompanyPurchase> GetCompanyPurchase(int skipRows, int pageSize)
        {
            return _context.CompanyPurchases.Skip(skipRows).Take(pageSize).ToList();
        }

        [HttpPost("appendCompanyPurchase", Name = "AppendCompanyPurchase")]
        public IActionResult CreateCompanyPurchase(CompanyPurchase companyPurchase)
        {
            _context.CompanyPurchases.Add(companyPurchase);
            _context.SaveChanges();
            return Ok();
        }

        //[HttpDelete]
        //public IActionResult DeleteJobPostingById(int id)
        //{
        //    if (id == 0)
        //        return BadRequest();

        //    var jobPostingFromDb = _context.JobPostings.SingleOrDefault(x => x.Id == id);

        //    if (jobPostingFromDb == null)
        //        return NotFound();

        //    _context.JobPostings.Remove(jobPostingFromDb);
        //    _context.SaveChanges();

        //    return Ok();
        //}
    }
}
