using Booking.Core.ServicesContract;
using Microsoft.AspNetCore.Mvc;

namespace Booking.UI.Areas.Order.Controllers
{
    [Area("Order")]
    [Route("Order/[Controller]/[action]")]
    public class PayPalController : Controller
    {
        public IOrderForUserService OrderForUserService;
        public PayPalController(IOrderForUserService OrderForUserService)
        {
           this.OrderForUserService = OrderForUserService;
        }

        public async Task<IActionResult> Index(Guid orderId)
        {
            return View(await OrderForUserService.GetOrderId(orderId));
        }

        /*[HttpPost]
        public async Task<IActionResult> Order(CancellationToken cancellationToken)
        {
            try
            {
                // set the transaction price and currency!!!!!!!!!!!!
                var price = "10.00";
                var currency = "USD";

                // "reference" is the transaction key!!!!!!!!!!!!
                var reference = GetRandomInvoiceNumber();// "INV002";

                var response = await _paypalClient.CreateOrder(price, currency, reference);

                return Ok(response);
            }
            catch (Exception e)
            {
                var error = new
                {
                    e.GetBaseException().Message
                };

                return BadRequest(error);
            }
        }
        public async Task<IActionResult> Capture(string orderId, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _paypalClient.CaptureOrder(orderId);

                var reference = response.purchase_units[0].reference_id;

                // Put your logic to save the transaction here !!!!!!!!!
                // You can use the "reference" variable as a transaction key

                return Ok(response);
            }
            catch (Exception e)
            {
                var error = new
                {
                    e.GetBaseException().Message
                };

                return BadRequest(error);
            }
        }
        public static string GetRandomInvoiceNumber()
        {
            return new Random().Next(999999).ToString();
        }
        public IActionResult Success()
        {
            return View();
        }*/
    }
}
