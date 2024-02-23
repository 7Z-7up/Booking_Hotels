using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.UI.Areas.Company.Controllers
{
    [Area("Company")]
    [Route("Company/[Controller]/[action]")]
    public class CompanyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
