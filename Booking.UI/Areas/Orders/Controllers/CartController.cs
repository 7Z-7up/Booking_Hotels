using Booking.Core.Domain.Entities;
using Booking.Core.DTO;
using Booking.Core.Helpers.Classes;
using Booking.Core.ServicesContract;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Booking.UI.Areas.Orders.Controllers
{
    public class CartController : Controller
    {
        public IOrderForCart OrderForCart;
        private List<Room> roomsList;
        public CartController(IOrderForCart orderForCart)
        {
            OrderForCart = orderForCart;
        }
        public IActionResult Create(List<Room>rooms) 
        {
            roomsList = rooms;
            ViewBag.rooms = rooms;
            return View();
        }
        public IActionResult Create(CreateOrderDTO createOrderDTO)
        {
            OrderForCart.Create(roomsList, createOrderDTO);
            return View();
        }
        public IActionResult Edit()
        {
            //CreateOrderDTO orderDTO = ;
            return View();
        }

        public IActionResult Edit(Guid id,CreateOrderDTO createOrderDTO)
        {
            OrderForCart.Update(id, createOrderDTO);
            return RedirectToAction("Index");
        }

        public  async Task<IActionResult> remove(Guid id, CreateOrderDTO createOrderDTO)
        {
            Room room = await OrderForCart.GetById(id);
            OrderForCart.Delete(id, createOrderDTO);
            roomsList.Remove(room);
            return RedirectToAction("Index");
        }
    }
}
