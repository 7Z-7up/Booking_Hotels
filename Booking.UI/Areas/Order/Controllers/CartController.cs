using Booking.Core.Domain.Entities;
using Booking.Core.Domain.IdentityEntities;
using Booking.Core.DTO;
using Booking.Core.Helpers.Classes;
using Booking.Core.Services;
using Booking.Core.ServicesContract;
using Booking.UI.Controllers;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;

namespace Booking.UI.Areas.Order.Controllers
{
    [Area("Order")]
    [Route("Order/[Controller]/[action]")]
    public class CartController : Controller
    {
        public IOrderForCart OrderForCart;
        private List<Room> roomsList;
        private List<Guid> orderedRoomIds;
        private CreateOrderDTO createOrderDTO;
        private readonly UserManager<AppUser> _userManager;
        bool check=false;
        public CartController(IOrderForCart orderForCart, UserManager<AppUser> _userManager)
        {
            OrderForCart = orderForCart;
            createOrderDTO = new CreateOrderDTO();
        }
        public async Task<IActionResult> Create() 
        {
            if (!check)
            {
                List<Guid> orderedRoomIds = HttpContext.Session.Get<List<Guid>>("RoomList") ?? new List<Guid>();
                return View(await OrderForCart.PutRoomsInDTO(orderedRoomIds));
            }
            else
            {
                return View(createOrderDTO);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderDTO createOrderDTO)
        {
            //if (ModelState.IsValid)
            //{
                //var currentUser = await _userManager.GetUserAsync(User);
                Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                //List<Guid> orderedRoomIds = HttpContext.Session.Get<List<Guid>>("RoomList") ?? new List<Guid>();
                await OrderForCart.Create(userId, createOrderDTO);
            return RedirectToAction("Index", "PayPal", new { orderId = createOrderDTO.OrderID});
            //return View("",createOrderDTO);///change
            //}
            //return View(createOrderDTO);
        }
        /*public IActionResult Edit()
        {
            //CreateOrderDTO orderDTO = ;
            return View();
        }

        public IActionResult Edit(Guid id,CreateOrderDTO createOrderDTO)
        {
            OrderForCart.Update(id, createOrderDTO);
            return RedirectToAction("Index");
        }*/

        public async Task<IActionResult> remove(Guid id)
        {
            List<Guid> orderedRoomIds = HttpContext.Session.Get<List<Guid>>("RoomList") ?? new List<Guid>();
            int indexToRemove = orderedRoomIds.IndexOf(id);
            if (indexToRemove != -1)
            {
                orderedRoomIds.RemoveAt(indexToRemove);
                HttpContext.Session.Set("RoomList", orderedRoomIds);
            }
            return RedirectToAction("Create");
        }
    }
}
