using Booking.Core.ServicesContract;
using Microsoft.AspNetCore.Mvc;

namespace Booking.UI.Areas.Order.Controllers
{
    [Area("Order")]
    [Route("Order/[Controller]/[action]")]
    public class OrderAdminController : Controller
    {
        private IOrderForAdminService orderService;
        public OrderAdminController(IOrderForAdminService orderService)
        {
            this.orderService = orderService;
        }
        // GET: OrderAdminController
        public async Task<ActionResult> Index()
        {
            var ordersForAdmin = await orderService.GetAll();
            return View(ordersForAdmin);
        }

        // GET: OrderAdminController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var ordersForAdmin = await orderService.GetOrderId(id);
            return View(ordersForAdmin);
        }
    }
}
