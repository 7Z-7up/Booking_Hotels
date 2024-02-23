using Booking.Core.ServicesContract;
using Microsoft.AspNetCore.Mvc;

namespace Booking.UI.Areas.Orders.Controllers
{
    public class OrderController : Controller
    {
        public IOrderForUserService orderForUserService;
        public OrderController(IOrderForUserService orderForUserService)
        {
            this.orderForUserService = orderForUserService;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            var ordersForUser = await orderForUserService.GetHistoryUserOrder(id);
            return View(ordersForUser);
        }

        public async Task<IActionResult> Detail(Guid id) 
        {
            var orderForUser = await orderForUserService.GetOrderId(id);
            return View(orderForUser);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            bool OrderIsFinished = await orderForUserService.Delete(id);
            if (OrderIsFinished) 
            {
                return RedirectToAction(nameof(Index), new { id = id });
            }
            else
            {
                return RedirectToAction(nameof(confirmDelete));
            }
        }

        public ActionResult confirmDelete()
        {
            return View();
        }
    }
}
